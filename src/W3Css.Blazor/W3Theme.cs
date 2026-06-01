namespace W3Css.Blazor;

/// <summary>
/// A design-token theme for W3Css.Blazor. Because W3.CSS itself ships no CSS
/// variables, the library owns this token layer: a <c>W3ThemeProvider</c> turns a
/// <see cref="W3Theme"/> into CSS custom properties, and components opt in via the
/// themed <see cref="W3Color"/> tokens (<c>Primary</c>, <c>Secondary</c>,
/// <c>Surface</c>, <c>Accent</c>). Change one theme → the whole app re-skins.
/// </summary>
public sealed record W3Theme
{
    /// <summary>Primary brand color.</summary>
    public string Primary { get; init; } = "#1976d2";

    /// <summary>Foreground color used on top of <see cref="Primary"/>.</summary>
    public string OnPrimary { get; init; } = "#ffffff";

    /// <summary>Secondary brand color.</summary>
    public string Secondary { get; init; } = "#7b1fa2";

    /// <summary>Foreground color used on top of <see cref="Secondary"/>.</summary>
    public string OnSecondary { get; init; } = "#ffffff";

    /// <summary>Accent / highlight color.</summary>
    public string Accent { get; init; } = "#ff9800";

    /// <summary>Foreground color used on top of <see cref="Accent"/>.</summary>
    public string OnAccent { get; init; } = "#000000";

    /// <summary>Surface (card/panel) background color.</summary>
    public string Surface { get; init; } = "#ffffff";

    /// <summary>Foreground color used on top of <see cref="Surface"/>.</summary>
    public string OnSurface { get; init; } = "#1a1a1a";

    /// <summary>Page background color (the surface behind cards/panels).</summary>
    public string Background { get; init; } = "#f6f7f9";

    /// <summary>Foreground color used on top of <see cref="Background"/>.</summary>
    public string OnBackground { get; init; } = "#1f2933";

    /// <summary>Themed border color.</summary>
    public string Border { get; init; } = "#e0e0e0";

    /// <summary>Default corner radius exposed as <c>--w3-radius</c>.</summary>
    public string Radius { get; init; } = "4px";

    /// <summary>Keyboard focus ring color exposed as <c>--w3-focus-color</c>.</summary>
    public string FocusColor { get; init; } = "var(--w3-primary)";

    /// <summary>Keyboard focus ring width exposed as <c>--w3-focus-width</c>.</summary>
    public string FocusWidth { get; init; } = "2px";

    /// <summary>Keyboard focus ring offset exposed as <c>--w3-focus-offset</c>.</summary>
    public string FocusOffset { get; init; } = "2px";

    /// <summary>Base font family applied within the theme scope.</summary>
    public string FontFamily { get; init; } = "inherit";

    /// <summary>
    /// Optional dark-mode variant. When null, a sensible default dark theme is used.
    /// </summary>
    public W3Theme? Dark { get; init; }

    /// <summary>
    /// The default light theme.
    /// </summary>
    public static W3Theme Default { get; } = new();

    /// <summary>
    /// A sensible default dark theme, used when a theme has no explicit <see cref="Dark"/> variant.
    /// </summary>
    public static W3Theme DefaultDark { get; } = new()
    {
        Primary = "#90caf9",
        OnPrimary = "#0d1b2a",
        Secondary = "#ce93d8",
        OnSecondary = "#1a1a1a",
        Accent = "#ffb74d",
        OnAccent = "#1a1a1a",
        Surface = "#1e1e1e",
        OnSurface = "#f5f5f5",
        Background = "#15171a",
        OnBackground = "#e6e8eb",
        Border = "#3a3a3a",
        FocusColor = "var(--w3-primary)"
    };
}
