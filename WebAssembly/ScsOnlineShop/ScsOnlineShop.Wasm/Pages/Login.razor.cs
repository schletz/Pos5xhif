using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Shared.Dto;
using ScsOnlineShop.Wasm.Services;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Pages
{
    public partial class Login
    {
        [Inject]
        public RestService RestService { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        public LoginDto LoginDto { get; private set; } = new();
        public string? Message { get; private set; }

        private async Task HandleValidSubmit()
        {
            Message = null;
            try
            {
                var (success, message) = await RestService.TryLoginAsync(LoginDto);
                if (!success)
                {
                    Message = message;
                    return;
                }
                    var queryParams = System.Web.HttpUtility.ParseQueryString(new System.Uri(NavigationManager.Uri).Query);
                    NavigationManager.NavigateTo(queryParams["returnUrl"] ?? "");
                
            }
            finally
            {

            }
        }
    }
}
