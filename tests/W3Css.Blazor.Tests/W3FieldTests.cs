using Bunit;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3FieldTests
{
    [Fact]
    public void FieldRendersStableLayoutHooksAndCustomClasses()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Field<string>>(parameters => parameters
            .Add(p => p.Label, "Name")
            .Add(p => p.ForId, "name")
            .Add(p => p.HelpText, "Required field")
            .Add(p => p.LabelClass, "field-label")
            .Add(p => p.HelpClass, "field-help")
            .Add(p => p.ValidationClass, "field-validation")
            .Add(p => p.Compact, true)
            .Add(p => p.Class, "field-shell")
            .Add(p => p.Style, "margin-bottom: 1rem;")
            .AddUnmatched("data-kind", "form-field")
            .AddChildContent("<input id=\"name\" class=\"w3-input\" />"));

        var field = cut.Find(".w3-field");
        var label = cut.Find("label");
        var help = cut.Find(".w3-field-help");

        Assert.Contains("w3-section", field.GetAttribute("class"));
        Assert.Contains("w3-field-compact", field.GetAttribute("class"));
        Assert.Contains("field-shell", field.GetAttribute("class"));
        Assert.Equal("margin-bottom: 1rem;", field.GetAttribute("style"));
        Assert.Equal("form-field", field.GetAttribute("data-kind"));
        Assert.Equal("name", label.GetAttribute("for"));
        Assert.Contains("w3-field-label", label.GetAttribute("class"));
        Assert.Contains("field-label", label.GetAttribute("class"));
        Assert.Contains("Required field", help.TextContent);
        Assert.Contains("field-help", help.GetAttribute("class"));
    }

    [Fact]
    public void FieldValidationMessageUsesStableAndCustomClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ValidationFormHost>();

        cut.InvokeAsync(() => cut.Instance.Validate());

        var validation = cut.Find(".w3-field-validation");

        Assert.Contains("The Name field is required.", validation.TextContent);
        Assert.Contains("w3-text-red", validation.GetAttribute("class"));
        Assert.Contains("w3-small", validation.GetAttribute("class"));
        Assert.Contains("field-validation", validation.GetAttribute("class"));
    }
}
