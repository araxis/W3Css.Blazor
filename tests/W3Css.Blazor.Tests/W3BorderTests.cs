using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3BorderTests
{
    [Fact]
    public void BorderRendersAllSidesColorAndRoundness()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Border>(parameters => parameters
            .Add(p => p.BorderColor, W3Color.Teal)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.ChildContent, "Bordered"));

        var border = cut.Find("div");

        Assert.Contains("w3-border", border.GetAttribute("class"));
        Assert.Contains("w3-border-teal", border.GetAttribute("class"));
        Assert.Contains("w3-round", border.GetAttribute("class"));
        Assert.Contains("w3-white", border.GetAttribute("class"));
        Assert.Equal("Bordered", border.TextContent);
    }

    [Fact]
    public void BorderRendersCombinedSideClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Border>(parameters => parameters
            .Add(p => p.Border, W3BorderSide.Top | W3BorderSide.Bottom)
            .Add(p => p.BorderColor, W3Color.Blue)
            .Add(p => p.ChildContent, "Sides"));

        var border = cut.Find("div");

        Assert.Contains("w3-border-top", border.GetAttribute("class"));
        Assert.Contains("w3-border-bottom", border.GetAttribute("class"));
        Assert.Contains("w3-border-blue", border.GetAttribute("class"));
        Assert.DoesNotContain("w3-border-right", border.GetAttribute("class"));
        Assert.DoesNotContain("w3-border-left", border.GetAttribute("class"));
    }

    [Fact]
    public void BorderRemoveOverridesSideClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Border>(parameters => parameters
            .Add(p => p.Border, W3BorderSide.Remove | W3BorderSide.Top)
            .Add(p => p.ChildContent, "Removed"));

        var border = cut.Find("div");

        Assert.Contains("w3-border-0", border.GetAttribute("class"));
        Assert.DoesNotContain("w3-border-top", border.GetAttribute("class"));
    }

    [Fact]
    public void BorderRendersBarClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Border>(parameters => parameters
            .Add(p => p.Border, W3BorderSide.None)
            .Add(p => p.Bar, W3BorderBar.Left | W3BorderBar.Right)
            .Add(p => p.BorderColor, W3Color.Green)
            .Add(p => p.ChildContent, "Bars"));

        var border = cut.Find("div");

        Assert.Contains("w3-leftbar", border.GetAttribute("class"));
        Assert.Contains("w3-rightbar", border.GetAttribute("class"));
        Assert.Contains("w3-border-green", border.GetAttribute("class"));
        Assert.DoesNotContain("w3-border ", border.GetAttribute("class"));
    }

    [Fact]
    public void BorderRendersAdditionalClassesStyleAndTextColor()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Border>(parameters => parameters
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Class, "w3-padding custom-border")
            .Add(p => p.Style, "min-height:48px")
            .Add(p => p.ChildContent, "Styled"));

        var border = cut.Find("div");

        Assert.Contains("w3-border", border.GetAttribute("class"));
        Assert.Contains("w3-text-white", border.GetAttribute("class"));
        Assert.Contains("w3-padding", border.GetAttribute("class"));
        Assert.Contains("custom-border", border.GetAttribute("class"));
        Assert.Equal("min-height:48px", border.GetAttribute("style"));
        Assert.Equal("Styled", border.TextContent);
    }

    [Theory]
    [InlineData(W3Round.Small, "w3-round-small")]
    [InlineData(W3Round.Medium, "w3-round")]
    [InlineData(W3Round.Large, "w3-round-large")]
    [InlineData(W3Round.XLarge, "w3-round-xlarge")]
    [InlineData(W3Round.XXLarge, "w3-round-xxlarge")]
    public void BorderMapsRoundClasses(W3Round round, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Border>(parameters => parameters
            .Add(p => p.Round, round)
            .Add(p => p.ChildContent, "Rounded"));

        var border = cut.Find("div");

        Assert.Contains("w3-border", border.GetAttribute("class"));
        Assert.Contains(expectedClass, border.GetAttribute("class"));
    }
}
