using Microsoft.JSInterop;

namespace W3Css.Blazor.Internal;

/// <summary>
/// Drives the reference-counted body scroll-lock JS module for one owning overlay
/// component. Each owner contributes at most one lock, releases it on disposal,
/// and tolerates teardown after the circuit is gone.
/// </summary>
internal sealed class W3ScrollLockInterop(IJSRuntime js) : IAsyncDisposable
{
    private const string ModulePath = "./_content/W3Css.Blazor/w3ScrollLock.js";

    private IJSObjectReference? module;
    private bool locked;

    public async Task SyncAsync(bool shouldLock)
    {
        if (shouldLock == locked)
        {
            return;
        }

        try
        {
            module ??= await js.InvokeAsync<IJSObjectReference>("import", ModulePath);
            await module.InvokeVoidAsync(shouldLock ? "lock" : "unlock");
            locked = shouldLock;
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
            if (locked && module is not null)
            {
                await module.InvokeVoidAsync("unlock");
                locked = false;
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
