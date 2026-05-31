using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3PaperTests
{
    [Fact]
    public void PaperRendersElevationAndSurfaceClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Paper>(parameters => parameters
            .Add(p => p.Elevation, 2)
            .Add(p => p.Outlined, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "paper-elevated")
            .Add(p => p.Style, "max-width: 24rem;")
            .Add(p => p.ChildContent, "<p>Paper surface</p>"));

        var root = cut.Find("div");
        var classes = root.GetAttribute("class");

        Assert.Contains("w3-card-2", classes);
        Assert.Contains("w3-round", classes);
        Assert.Contains("w3-white", classes);
        Assert.Contains("w3-text-black", classes);
        Assert.Contains("w3-border", classes);
        Assert.Contains("paper-elevated", classes);
        Assert.Equal("max-width: 24rem;", root.GetAttribute("style"));
    }

    [Fact]
    public void PaperCanRenderFlatOutlinedSquareSurface()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Paper>(parameters => parameters
            .Add(p => p.Elevation, 0)
            .Add(p => p.Square, true)
            .Add(p => p.Outlined, true)
            .Add(p => p.Style, "min-height: 5rem;")
            .Add(p => p.Class, "paper-flat")
            .Add(p => p.ChildContent, "<p>Flat surface</p>"));

        var root = cut.Find("div");
        var classes = root.GetAttribute("class");

        Assert.DoesNotContain("w3-card", classes);
        Assert.DoesNotContain("w3-round", classes);
        Assert.Contains("w3-border", classes);
        Assert.Contains("paper-flat", classes);
        Assert.Equal("min-height: 5rem;", root.GetAttribute("style"));
    }
}
