namespace W3Css.Blazor;

/// <summary>
/// Stores toast notifications for a <c>W3ToastProvider</c>.
/// </summary>
public sealed class W3ToastService
{
    private readonly object gate = new();
    private readonly List<W3ToastMessage> messages = [];

    /// <summary>
    /// Raised when the visible toast collection changes.
    /// </summary>
    public event Action? ToastsChanged;

    /// <summary>
    /// Current toast notifications. Returns a snapshot so callers can enumerate it safely.
    /// </summary>
    public IReadOnlyList<W3ToastMessage> Toasts
    {
        get
        {
            lock (gate)
            {
                return messages.ToArray();
            }
        }
    }

    /// <summary>
    /// Shows a toast notification.
    /// </summary>
    public W3ToastMessage Show(string message, W3ToastOptions? options = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        options ??= new W3ToastOptions();

        var toast = new W3ToastMessage(
            Guid.NewGuid(),
            message,
            options.Title,
            options.Kind,
            options.Duration,
            options.ActionText,
            options.OnAction);

        lock (gate)
        {
            messages.Add(toast);
        }

        ToastsChanged?.Invoke();

        return toast;
    }

    /// <summary>
    /// Shows an information toast notification.
    /// </summary>
    public W3ToastMessage ShowInfo(string message, string? title = null)
    {
        return Show(message, new W3ToastOptions { Kind = W3AlertKind.Info, Title = title });
    }

    /// <summary>
    /// Shows a success toast notification.
    /// </summary>
    public W3ToastMessage ShowSuccess(string message, string? title = null)
    {
        return Show(message, new W3ToastOptions { Kind = W3AlertKind.Success, Title = title });
    }

    /// <summary>
    /// Shows a warning toast notification.
    /// </summary>
    public W3ToastMessage ShowWarning(string message, string? title = null)
    {
        return Show(message, new W3ToastOptions { Kind = W3AlertKind.Warning, Title = title });
    }

    /// <summary>
    /// Shows a danger toast notification.
    /// </summary>
    public W3ToastMessage ShowDanger(string message, string? title = null)
    {
        return Show(message, new W3ToastOptions { Kind = W3AlertKind.Danger, Title = title });
    }

    /// <summary>
    /// Dismisses a toast notification by id.
    /// </summary>
    public void Dismiss(Guid id)
    {
        bool removed;

        lock (gate)
        {
            removed = messages.RemoveAll(message => message.Id == id) > 0;
        }

        if (removed)
        {
            ToastsChanged?.Invoke();
        }
    }

    /// <summary>
    /// Clears all toast notifications.
    /// </summary>
    public void Clear()
    {
        bool changed;

        lock (gate)
        {
            changed = messages.Count > 0;
            messages.Clear();
        }

        if (changed)
        {
            ToastsChanged?.Invoke();
        }
    }
}

