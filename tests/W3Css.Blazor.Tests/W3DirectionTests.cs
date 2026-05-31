using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3DirectionTests
{
    [Fact]
    public void DirectionRendersRightToLeftClassAndStyling()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Direction>(parameters => parameters
            .Add(p => p.Direction, W3TextDirection.RightToLeft)
            .Add(p => p.Color, W3Color.PaleYellow)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "custom-direction")
            .Add(p => p.Style, "unicode-bidi: isolate;")
            .Add(p => p.ChildContent, "RTL sample"));

        var wrapper = cut.Find("div");
        var classes = wrapper.GetAttribute("class");

        Assert.Contains("w3-rtl", classes);
        Assert.Contains("w3-pale-yellow", classes);
        Assert.Contains("w3-text-black", classes);
        Assert.Contains("w3-round", classes);
        Assert.Contains("custom-direction", classes);
        Assert.Equal("unicode-bidi: isolate;", wrapper.GetAttribute("style"));
        Assert.Equal("RTL sample", wrapper.TextContent.Trim());
    }

    [Fact]
    public void DirectionCanRenderInlineLeftToRightSpan()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Direction>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.Direction, W3TextDirection.LeftToRight)
            .Add(p => p.Size, W3Size.Large)
            .Add(p => p.ChildContent, "LTR"));

        var wrapper = cut.Find("span");
        var classes = wrapper.GetAttribute("class");

        Assert.Contains("w3-ltr", classes);
        Assert.Contains("w3-large", classes);
        Assert.Equal("LTR", wrapper.TextContent);
    }

    [Theory]
    [InlineData(W3TextDirection.LeftToRight, "w3-ltr")]
    [InlineData(W3TextDirection.RightToLeft, "w3-rtl")]
    public void DirectionMapsDirectionClasses(W3TextDirection direction, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Direction>(parameters => parameters
            .Add(p => p.Direction, direction)
            .Add(p => p.ChildContent, "Directional"));

        var wrapper = cut.Find("div");

        Assert.Contains(expectedClass, wrapper.GetAttribute("class"));
    }
}
