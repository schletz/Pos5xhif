# Komponenten kommunizieren: Parameter

## Parent zu child: Parameterwerte

Wir möchten eine allgemeine Komponente Spinner entwickeln. Dieser Spinner zeigt einen animierten
Kreis und deutet an, dass gerade Daten vom Server geladen werden. Wir möchten sie z. B. in *AddStore*
so verwenden:

```html
<Spinner Visible="Busy" />
```

Busy ist ein Property der *AddStore* Komponente. Wenn es auf true gesetzt wird, soll der Spinner
angezeigt werden. Ist der Wert false, soll er wieder verschwinden.

Legen wir nun im Ordner *Components* eine neue Razor component mit dem Namen *Spinner.razor* an.
Der Code hat folgendes Aussehen:

**Spinner.razor**
```html
@if (Visible)
{
    <div v-if="loading" class="spinner"></div>
}

@code {
    [Parameter]
    public bool Visible { get; set; }
}
```

Normalerweise definieren wir die Properties in einer eigenen (partial) Klasse (also *Spinner.razor.cs*). Da
es aber sehr wenig Code ist, schreiben wir ihn gleich in die *.razor* Datei. Wichtig ist die
Annotation *Parameter*. *Visible* wird somit automatisch zum Parameter, der beim Einbauen der Komponente
gesetzt werden kann.

Damit der Spinner animiert wird, verwenden wir isolated css. Blazor lädt automatisch - wenn
vorhanden - die Datei *(Component).razor.css*. Wenn wir eine Datei *Spinner.razor.css* anlegen und
folgendes Stylesheet definieren, wird es automatisch mit der Komponente geladen.

**Spinner.razor.css**
```css
.spinner {
    border: 16px solid silver;
    border-top: 16px solid #337AB7;
    border-radius: 50%;
    width: 80px;
    height: 80px;
    animation: spin 700ms linear infinite;
    top: 50%;
    left: 50%;
    margin-top: -40px;
    margin-left: -40px;
    position: absolute;
}

@keyframes spin {
    0% {
        transform: rotate(0deg)
    }

    100% {
        transform: rotate(360deg)
    }
}
```

## Child zu parent: Events

Es gibt auch Situationen, wo Child Komponenten (also Komponente B, die in Komponente A verwendet
wird), an die Parent Komponente Informationen weitergeben möchten. In anderen Frameworks wie
Vue.js wird dies mit einer *emit()* Methode bewerkstelligt.

In Blazor wird dies mittels Callback Funktion gelöst. Wir können in der Parent Komponente eine
Methode als Parameter definieren:

**Stores.razor**
```c#
<AddStore OnStoreAddedCallback="OnStoreAdded" />
```

*OnStoreAdded* ist eine ganz "normale" Methode und wird in der Komponente definiert. Die
Besonderheit ist lediglich, dass sie von der Child Komponente (also *AddStore*) aufgerufen wird.

Die Parameter von *OnStoreAdded* müssen allerdings mit dem verwendeten Typ in der Child Komponente
zusammenpassen. Die Child Komponente kann nämlich auch einen Parameter mitgeben. In unserem Fall
ist dies der neue Store, der eingefügt wurde. Dadurch können wir ihn in unsere Liste der Stores
aufnehmen, ohne dass wir erneut einen Request absetzen und alle Stores neu laden müssen.

**Stores.razor.cs**
```c#
public partial class Stores
{
    [Inject]
    public HttpClient HttpClient { get; set; } = default!;

    public List<StoreDto> StoreList { get; private set; } = new();

    protected override async Task OnInitializedAsync()
    {
        // using System.Net.Http.Json;
        StoreList = await HttpClient
            .GetFromJsonAsync<List<StoreDto>>("/api/stores") ?? new();
    }

    public void OnStoreAdded(StoreDto storeDto)
    {
        StoreList.Add(storeDto);
    }
}
```

In *AddStore* wird der Parameter *OnStoreAddedCallback* wie üblich als Parameter definiert.
Der Datentyp *EventCallback* bzw. *EventCallback&lt;T&gt;* ist von Blazor vorgegeben und sollte
verwendet werden. Es kann grundsätzlich jede *Action* oder Func *Typ* verwendet werden,
Blazor empfiehlt aber die Verwendung dieses Typs, da hier gewährleistet ist, dass die
Parent Komponente ggf. neu gerendert wird.

Mit *InvokeAsync()* kann die Callback Methode aufgerufen werden. Da *EventCallback* als
struct definiert ist, gibt es übrigens auch keine Warnmeldung bei verwendeten nullable
reference types.

**AddStore.razor.cs**
```c#
public partial class AddStore
{
    [Inject]
    public HttpClient HttpClient { get; set; } = default!;

    [Parameter]
    public EventCallback<StoreDto> OnStoreAddedCallback { get; set; }

    public bool Busy { get; private set; }
    public string? ErrorMessage { get; private set; }
    public StoreDto NewStore { get; private set; } = new(guid: default, name: string.Empty);

    public async Task HandleValidSubmit()
    {
        // Spinner aktivieren, damit der User nicht mehrmals speichert.
        Busy = true;
        try
        {
            var result = await HttpClient.PostAsJsonAsync("api/stores", NewStore);
            if (!result.IsSuccessStatusCode)
            {
                ErrorMessage = result.StatusCode == System.Net.HttpStatusCode.BadRequest
                    ? await result.Content.ReadAsStringAsync()
                    : "Fehler beim Senden der Daten.";
                return;
            }
            var newStore = await result.Content.ReadFromJsonAsync<StoreDto>();
            await OnStoreAddedCallback.InvokeAsync(newStore);
            // Das Eingabefeld nach erfolgter Eingabe wieder leeren.
            NewStore = new(guid: default, name: string.Empty);
        }
        finally
        {
            // Der Spinner sollte immer - auch im Fehlerfall - wieder deaktiviert werden.
            // Sonst bleibt er "hängen". Hier bietet sich finally an.
            Busy = false;
        }
    }
}
```
