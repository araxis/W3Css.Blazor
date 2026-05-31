using Microsoft.AspNetCore.Components;

namespace W3Css.Blazor;

/// <summary>
/// Shared parameters for W3Css.Blazor components.
/// </summary>
public abstract class W3ComponentBase : ComponentBase
{
    /// <summary>
    /// Additional CSS classes for the rendered element.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Inline styles for the rendered element.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>
    /// Attributes passed to the rendered element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the final CSS class list for a component.
    /// </summary>
    protected string BuildClass(params string?[] classes)
    {
        var builder = new Internal.W3ClassBuilder();

        foreach (var cssClass in classes)
        {
            builder.Add(cssClass);
        }

        builder.Add(Class);

        return builder.ToString();
    }
}
