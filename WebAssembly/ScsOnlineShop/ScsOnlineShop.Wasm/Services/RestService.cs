using ScsOnlineShop.Shared.Dto;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm
{
    public class RestService
    {
        private readonly HttpClientHandler _handler;           // Genauere Steuerung des HttpClient
        private readonly HttpClient _client;                   // Einzige Instanz des HttpClient
        private UserDto? _currentUser;                          // Aktuell angemeldeter Benutzer.
        // Properties werden von System.Text.Json in camelCase umgewandelt. Daher muss bei der
        // Deserialisierung Case Sensitive deaktiviert werden.
        private readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        /// <summary>
        /// Liefert den angemelteten Benutzer oder null, wenn noch kein
        /// Login stattgefunden hat.
        /// </summary>
        public UserDto? CurrentUser => _currentUser;

        /// <summary>
        /// Konstruktor. Legt die Http Einstellungen fest.
        /// </summary>
        public RestService(string baseAddress)
        {
            _handler = new HttpClientHandler() { ClientCertificateOptions = ClientCertificateOption.Manual };
            _client = new HttpClient(_handler) { BaseAddress = new Uri(baseAddress) };
        }
        /// <summary>
        /// Meldet den User an der Adresse (baseUrl)/user/login an und setzt den Token als
        /// Default Request Header für zukünftige Anfragen.
        /// </summary>
        /// <param name="user">Benutzer, der angemeldet werden soll.</param>
        /// <returns>Userobjekt mit Token wenn erfolgreich, null bei ungültigen Daten.</returns>
        public async Task<bool> TryLoginAsync(LoginDto login)
        {
            UserDto? user = await SendAsync<UserDto>(HttpMethod.Post, "User/Login", login);
            _currentUser = user;
            if (user is null) { return false; }
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);
            return true;
        }

        /// <summary>
        /// Löscht den Token aus den HTTP Headern. Ein Logout Request in der API ist nicht nötig.
        /// </summary>
        /// <returns></returns>
        public void Logout()
        {
            _currentUser = null;
            _client.DefaultRequestHeaders.Authorization = null;
        }


        public Task<T?> SendAsync<T>(HttpMethod method, string actionUrl) => SendAsync<T>(method, actionUrl, "", null);
        public Task<T?> SendAsync<T>(HttpMethod method, string actionUrl, object requestData) => SendAsync<T>(method, actionUrl, "", requestData);
        public Task<T?> SendAsync<T>(HttpMethod method, string actionUrl, string idParam) => SendAsync<T>(method, actionUrl, idParam, null);
        /// <summary>
        /// Sendet einen Request an die REST API und gibt das Ergebnis zurück.
        /// </summary>
        /// <typeparam name="T">Typ, in den die JSON Antwort des Servers umgewandelt wird.</typeparam>
        /// <param name="method">HTTP Methode, die zum Senden des Requests verwendet wird.</param>
        /// <param name="actionUrl">Adresse, die in {baseUrl}/{actionUrl}/{idParam} ersetzt wird.</param>
        /// <param name="idParam">Adresse, die in {baseUrl}/{actionUrl}/{idParam} ersetzt wird.</param>
        /// <param name="requestData">Daten, die als JSON Request Body bzw. als Parameter bei GET Requests gesendet werden.</param>
        /// <returns></returns>
        public async Task<T?> SendAsync<T>(HttpMethod method, string actionUrl, string idParam, object? requestData)
        {
            string url = $"{actionUrl}/{idParam}";

            try
            {
                // Die Daten für den Request Body als JSON serialisieren und mitsenden.
                string jsonContent = JsonSerializer.Serialize(requestData);
                StringContent request = new StringContent(
                    jsonContent, System.Text.Encoding.UTF8, "application/json"
                );

                HttpResponseMessage response;
                if (method == HttpMethod.Get)
                {
                    string? parameters = requestData as string;
                    if (!string.IsNullOrEmpty(parameters))
                        url = $"{url}?{parameters}";
                    response = await _client.GetAsync(url);
                }
                else if (method == HttpMethod.Post)
                { response = await _client.PostAsync(url, request); }
                else if (method == HttpMethod.Put)
                { response = await _client.PutAsync(url, request); }
                else if (method == HttpMethod.Delete)
                { response = await _client.DeleteAsync(url); }
                else
                {
                    throw new ApplicationException($"Unsupported Request Method for {url}");
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Request for {url} not successful, Status {(int)response.StatusCode}.");
                }
                string result = await response.Content.ReadAsStringAsync();
                try
                {
                    return JsonSerializer.Deserialize<T>(result, _jsonOptions);
                }
                catch (Exception e)
                {
                    throw new ApplicationException($"Cannot parse result at {url}", e);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Request not successful at {url}.", e);
            }
        }
    }
}
