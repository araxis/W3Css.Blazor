namespace W3Css.Blazor;

/// <summary>
/// Options used when creating a toast notification.
/// </summary>
public sealed class W3ToastOptions
{
    /// <summary>
    /// Optional toast title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Semantic color kind used by the toast.
    /// </summary>
    public W3AlertKind Kind { get; set; } = W3AlertKind.Info;

    /// <summary>
    /// Optional auto-dismiss duration. Set to <c>null</c> to use the provider default.
    /// </summary>
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// Optional action button text.
    /// </summary>
    public string? ActionText { get; set; }

    /// <summary>
    /// Optional callback invoked when the action button is selected.
    /// </summary>
    public Func<Task>? OnAction { get; set; }
}

