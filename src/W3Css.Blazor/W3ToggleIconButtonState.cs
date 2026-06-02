namespace W3Css.Blazor;

/// <summary>
/// Describes one value in a multi-state <see cref="Components.W3ToggleIconButton"/>.
/// </summary>
public sealed class W3ToggleIconButtonState
{
    /// <summary>
    /// Initializes a new instance of the <see cref="W3ToggleIconButtonState"/> class.
    /// </summary>
    public W3ToggleIconButtonState()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="W3ToggleIconButtonState"/> class.
    /// </summary>
    /// <param name="value">State value used for binding and cycling.</param>
    /// <param name="label">Accessible label for this state.</param>
    /// <param name="iconText">Optional text rendered inside the icon when no icon class is supplied.</param>
    /// <param name="iconClass">Optional icon CSS class for this state.</param>
    /// <param name="title">Optional tooltip title for this state.</param>
    public W3ToggleIconButtonState(string value, string label, string? iconText = null, string? iconClass = null, string? title = null)
    {
        Value = value;
        Label = label;
        IconText = iconText;
        IconClass = iconClass;
        Title = title;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="W3ToggleIconButtonState"/> class.
    /// </summary>
    /// <param name="value">State value used for binding and cycling.</param>
    /// <param name="label">Accessible label for this state.</param>
    /// <param name="iconName">Built-in icon name for this state.</param>
    /// <param name="title">Optional tooltip title for this state.</param>
    public W3ToggleIconButtonState(string value, string label, W3IconName iconName, string? title = null)
    {
        Value = value;
        Label = label;
        IconName = iconName;
        Title = title;
    }

    /// <summary>
    /// State value used for binding and cycling.
    /// </summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>
    /// Accessible label for this state.
    /// </summary>
    public string Label { get; init; } = string.Empty;

    /// <summary>
    /// Optional tooltip title for this state.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Built-in icon name for this state.
    /// </summary>
    public W3IconName IconName { get; init; } = W3IconName.None;

    /// <summary>
    /// Optional icon CSS class for this state.
    /// </summary>
    public string? IconClass { get; init; }

    /// <summary>
    /// Optional additional icon element class for this state.
    /// </summary>
    public string? IconElementClass { get; init; }

    /// <summary>
    /// Optional text rendered inside the icon when no icon class is supplied.
    /// </summary>
    public string? IconText { get; init; }
}
