using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using W3Css.Blazor.Internal;

namespace W3Css.Blazor;

/// <summary>
/// Shared parameters for validation-friendly W3.CSS form components.
/// </summary>
public abstract class W3FormComponentBase<TValue> : InputBase<TValue>
{
    /// <summary>
    /// Additional CSS classes for the rendered form control.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Inline styles for the rendered form control.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>
    /// Background color class.
    /// </summary>
    [Parameter]
    public W3Color Color { get; set; } = W3Color.None;

    /// <summary>
    /// Text color class.
    /// </summary>
    [Parameter]
    public W3Color TextColor { get; set; } = W3Color.None;

    /// <summary>
    /// Rounded corner class.
    /// </summary>
    [Parameter]
    public W3Round Round { get; set; } = W3Round.None;

    /// <summary>
    /// Adds the W3.CSS border class.
    /// </summary>
    [Parameter]
    public bool Border { get; set; }

    /// <summary>
    /// Disables the form control.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Builds the final CSS class list for a form control.
    /// </summary>
    protected string BuildControlClass(params string?[] classes)
    {
        var builder = new W3ClassBuilder();

        foreach (var cssClass in classes)
        {
            builder.Add(cssClass);
        }

        builder
            .Add(Color.ToBackgroundClass())
            .Add(TextColor.ToTextClass())
            .Add(Round.ToClass())
            .AddIf(Border, "w3-border")
            .Add(CssClass)
            .Add(Class);

        return builder.ToString();
    }
}
