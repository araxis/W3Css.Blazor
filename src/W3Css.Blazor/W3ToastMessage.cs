namespace W3Css.Blazor;

/// <summary>
/// A toast notification rendered by <c>W3ToastProvider</c>.
/// </summary>
public sealed record W3ToastMessage(
    Guid Id,
    string Message,
    string? Title,
    W3AlertKind Kind,
    TimeSpan? Duration,
    string? ActionText,
    Func<Task>? OnAction);

