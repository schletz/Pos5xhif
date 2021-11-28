# Render Fragments, ref und Event Handling: Die ModalDialog Komponente

Oftmals werden Dialoge verwendet, um z. B. eine Aktion bestätigen zu lassen. Mit Bootstrap
können modale Dialoge mit den CSS Klassen leicht erstellt werden.

Um eine allgemeine Dialogkomponente zu erstellen, erstellen wir im Ordner *Shared* des WASM
Projektes eine neue Razor Component und nennen sie *ModalDialog.razor*.

Der Inhalt soll von der aufrufenden Komponente gesetzt werden können. Unsere Komponente soll
so genutzt werden können: 

```html
<ModalDialog @ref="DeleteConfirmDialog" Title="Angebot löschen">
    <p>Das ist der Inhalt des Dialoges.</p>
</ModalDialog>
```

Wir sehen 2 Neuigkeiten:
- Mit *ref* wird die Komponente an ein Property im Viewmodel der aufrufenden Komponente gebunden
  und kann daher im Programmablauf gesteuert werden.
- Es werden nicht nur Parameter wie *Title* verwendet, im Inneren wird HTML angegeben.

Die Datei [ModalDialog.razor](ScsOnlineShop/ScsOnlineShop.Wasm/Shared/ModalDialog.razor)
kann wie folgt implementiert werden:

```html
@if (Show)
{
    <div class="modal" style="display: block" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                @if (!string.IsNullOrEmpty(Title))
                {
                    <div class="modal-header">
                        <h5 class="modal-title">@Title</h5>
                    </div>
                }
                <div class="modal-body">
                    @ChildContent
                    <div>
                        <button type="button" class="btn btn-primary"
                            @onclick="@(()=>{
                            Show = false;
                            _taskCompletionSource?.TrySetResult(true);
                        })">
                            OK
                        </button>
                        @if (ShowCancel)
                        {
                            <button type="button" class="ms-2 btn btn-secondary"
                            data-bs-dismiss="modal"
                            @onclick="@(()=>{
                            Show = false;
                            _taskCompletionSource?.TrySetResult(false);
                        })">
                                Cancel
                            </button>
                        }
                    </div>
                </div>
            </div>
        </div>
    </div>
}

@code {
    private TaskCompletionSource<bool>? _taskCompletionSource;
    private bool Show { get; set; }
    private bool ShowCancel { get; set; }

    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    public Task<bool> ShowConfirmation()
    {
        ShowCancel = true;
        return ShowDialog();
    }
    public Task<bool> ShowDialog()
    {
        _taskCompletionSource = new();
        Show = true;
        return _taskCompletionSource.Task;
    }
}
```

## Verwenden der Klasse *RenderFragment*

Im Code wird ein Parameter vom Typ *RenderFragment* verwendet. Es wird mit dem Inhalt der Komponente,
die der Aufrufer festlegt, befüllt. In unserem Fall ist dies der HTML Code, der als Inhalt des
Dialoges ausgegeben wird. Wir können in der Komponente einfach mit *@ChildContent* darauf
zugreifen und diesen ausgeben. Der Name *ChildContent* ist eine Konvention und muss eingehalten werden.

## Eventhandling

Für den OK Button wird ein onclick Eventhandler definiert. In Javascript ist *onclick*, ... dafür zuständig.
Wollen wir in Blazor eine C# Methode aufrufen, definieren wir mit dem @ Zeichen die entsprechenden
Handler. Nun wird beim Klick auf den Button die angegebene C# Methode aufgerufen.

## *TaskCompletionSource* für die Verwendung von await

Damit wir in der aufrufenden Komponente mit *var result = await MyDialog.ShowConfirmation()*
die Benutzereingabe abwarten können, wird die Klasse *TaskCompletionSource* verwendet. Diese
Klasse bietet einen Task als Property an, der beim Instanzieren den Status *Running* hat.
Durch die Methode *TrySetResult()* in einem Eventhandler wird das Ergebnis gesetzt. Nun hat
der Task den Status *RanToCompletion* und *await* speichert das Ergebnis in eine Variable. Der Code
kann danach weiter ausgeführt werden.

## Verwenden der Komponente

In der Komponente [StoreOffers](ScsOnlineShop/ScsOnlineShop.Wasm/Components/StoreOffers.razor)
wird der Dialog in der Komponente eingebunden.

**StoreOffers.razor**
```html
<ModalDialog @ref="DeleteConfirmDialog" Title="Angebot löschen">
    <p>Soll das Angebot <em>@OfferToDelete?.ProductEan (@OfferToDelete?.Product.Name)</em> gelöscht werden?</p>
</ModalDialog>
```

Die Anweisung *@ref* ermöglicht es, im ViewModel ein Property zu definieren, welches eine Referenz
auf die Instanz speichert. Dadurch können Methoden wie z. B. *ShowConfirmation()* im
Programmablauf aufgerufen werden.

**StoreOffers.razor.cs**
```c#
public partial class StoreOffers
{
    /* ... */
    public ModalDialog DeleteConfirmDialog { get; set; } = default!;

    public async Task DeleteOffer(OfferDto offer)
    {
        var result = await DeleteConfirmDialog.ShowConfirmation();
        if (!result) { return; }
        /* ... */
    }
}
```

> **Hinweis:** *@ref* sollte sparsam verwendet werden. Wenn immer möglich sollte mit Binding
> gearbeitet werden und der Renderer kümmert sich selbst um die Darstellung. Außerdem sollten
> keine Parameter (die mit *[Parameter]* definiert wurden) der referenzierten Komponente im 
> Programmablauf gesetzt werden.

