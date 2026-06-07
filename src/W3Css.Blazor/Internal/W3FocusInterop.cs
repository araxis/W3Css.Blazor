using Microsoft.JSInterop;

namespace W3Css.Blazor.Internal;

/// <summary>
/// Moves keyboard focus to an element by id for roving-tabindex widgets, so focus
/// follows the active item after keyboard navigation. Reuses one module import and
/// tolerates teardown after the circuit is gone.
/// </summary>
internal sealed class W3FocusInterop(IJSRuntime js) : IAsyncDisposable
{
    private const string ModulePath = "./_content/W3Css.Blazor/w3Focus.js";

    private IJSObjectReference? module;

    public async Task FocusIdAsync(string? id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return;
        }

        try
        {
            module ??= await js.InvokeAsync<IJSObjectReference>("import", ModulePath);
            await module.InvokeVoidAsync("focusId", id);
        }
        catch (JSDisconnectedException)
        {
        }
        catch (ObjectDisposedException)
        {
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (module is not null)
            {
                await module.DisposeAsync();
            }
        }
        catch (JSDisconnectedException)
        {
        }
        catch (ObjectDisposedException)
        {
        }
    }
}
