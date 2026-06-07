using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace W3Css.Blazor.Internal;

/// <summary>
/// Drives the shared focus-trap JS module for a single owning overlay component.
/// Reuses one connection, skips redundant updates, never connects a trap that has
/// not yet been activated, and tolerates teardown after the circuit is gone.
/// </summary>
internal sealed class W3FocusTrapInterop(IJSRuntime js) : IAsyncDisposable
{
    private const string ModulePath = "./_content/W3Css.Blazor/w3FocusTrap.js";

    private IJSObjectReference? module;
    private IJSObjectReference? handle;
    private (bool Active, bool AutoFocus, bool RestoreFocus, string? Selector) state;

    public async Task SyncAsync(
        ElementReference root,
        bool active,
        bool autoFocus = true,
        bool restoreFocus = true,
        string? initialFocusSelector = null)
    {
        var next = (active, autoFocus, restoreFocus, initialFocusSelector);

        // Do not connect a trap that has never been active, and skip redundant
        // updates so ordinary re-renders do not repeatedly call into JS.
        if (handle is null && !active)
        {
            return;
        }

        if (handle is not null && state == next)
        {
            return;
        }

        try
        {
            module ??= await js.InvokeAsync<IJSObjectReference>("import", ModulePath);

            var options = new
            {
                active,
                autoFocus,
                restoreFocus,
                initialFocusSelector,
            };

            if (handle is null)
            {
                handle = await module.InvokeAsync<IJSObjectReference>("connect", root, options);
            }
            else
            {
                await handle.InvokeVoidAsync("update", options);
            }

            state = next;
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
            if (handle is not null)
            {
                await handle.InvokeVoidAsync("dispose");
                await handle.DisposeAsync();
            }

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
