using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3HoverColorTests
{
    [Fact]
    public void HoverColorRendersBackgroundTextAndBorderHoverClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3HoverColor>(parameters => parameters
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Border, true)
            .Add(p => p.BorderColor, W3Color.LightGrey)
            .Add(p => p.HoverColor, W3Color.Teal)
            .Add(p => p.HoverTextColor, W3Color.White)
            .Add(p => p.HoverBorderColor, W3Color.Red)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "custom-hover")
            .Add(p => p.Style, "cursor: pointer;")
            .Add(p => p.ChildContent, "Hover me"));

        var wrapper = cut.Find("div");
        var classes = wrapper.GetAttribute("class");

        Assert.Contains("w3-white", classes);
        Assert.Contains("w3-text-black", classes);
        Assert.Contains("w3-border", classes);
        Assert.Contains("w3-border-light-grey", classes);
        Assert.Contains("w3-hover-teal", classes);
        Assert.Contains("w3-hover-text-white", classes);
        Assert.Contains("w3-hover-border-red", classes);
        Assert.Contains("w3-round", classes);
        Assert.Contains("custom-hover", classes);
        Assert.Equal("cursor: pointer;", wrapper.GetAttribute("style"));
        Assert.Equal("Hover me", wrapper.TextContent.Trim());
    }

    [Fact]
    public void HoverColorCanRenderInlineSpan()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3HoverColor>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.HoverTextColor, W3Color.Red)
            .Add(p => p.Size, W3Size.Large)
            .Add(p => p.ChildContent, "Inline"));

        var wrapper = cut.Find("span");
        var classes = wrapper.GetAttribute("class");

        Assert.Contains("w3-hover-text-red", classes);
        Assert.Contains("w3-large", classes);
        Assert.Equal("Inline", wrapper.TextContent);
    }
}
