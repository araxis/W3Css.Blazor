namespace W3Css.Blazor;

/// <summary>
/// A toast notification rendered by <c>W3ToastProvider</c>.
/// </summary>
/// <param name="Id">Unique toast identifier.</param>
/// <param name="Message">Primary toast message text.</param>
/// <param name="Title">Optional toast title.</param>
/// <param name="Kind">Semantic toast kind.</param>
/// <param name="Duration">Optional display duration override.</param>
/// <param name="ActionText">Optional action button text.</param>
/// <param name="OnAction">Optional callback raised when the action is selected.</param>
public sealed record W3ToastMessage(
    Guid Id,
    string Message,
    string? Title,
    W3AlertKind Kind,
    TimeSpan? Duration,
    string? ActionText,
    Func<Task>? OnAction);

