using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ColorClassMapTests
{
    [Fact]
    public void TextColorDoesNotEmitClassesMissingFromBundledStylesheet()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.TextColor, W3Color.Crimson)
            .Add(p => p.ChildContent, "Unsupported text color"));

        var text = cut.Find("div");
        var classes = text.GetAttribute("class") ?? string.Empty;

        Assert.DoesNotContain("w3-text-crimson", classes);
    }

    [Fact]
    public void BorderColorDoesNotEmitClassesMissingFromBundledStylesheet()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Border>(parameters => parameters
            .Add(p => p.BorderColor, W3Color.Cobalt)
            .Add(p => p.ChildContent, "Unsupported border color"));

        var border = cut.Find("div");
        var classes = border.GetAttribute("class") ?? string.Empty;

        Assert.Contains("w3-border", classes);
        Assert.DoesNotContain("w3-border-cobalt", classes);
    }

    [Fact]
    public void HoverColorKeepsExtendedBackgroundAndSkipsMissingTextAndBorderClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3HoverColor>(parameters => parameters
            .Add(p => p.HoverColor, W3Color.Cobalt)
            .Add(p => p.HoverTextColor, W3Color.Crimson)
            .Add(p => p.HoverBorderColor, W3Color.Cobalt)
            .Add(p => p.ChildContent, "Hover sample"));

        var hover = cut.Find("div");
        var classes = hover.GetAttribute("class") ?? string.Empty;

        Assert.Contains("w3-hover-cobalt", classes);
        Assert.DoesNotContain("w3-hover-text-crimson", classes);
        Assert.DoesNotContain("w3-hover-border-cobalt", classes);
    }
}
