using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ContainerTests
{
    [Fact]
    public void RendersContainerWithColorClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Container>(parameters => parameters
            .Add(p => p.Color, W3Color.Paper)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.ChildContent, "Content"));

        var container = cut.Find("div");

        Assert.Contains("w3-container", container.GetAttribute("class"));
        Assert.Contains("w3-paper", container.GetAttribute("class"));
        Assert.Contains("w3-text-black", container.GetAttribute("class"));
        Assert.Equal("Content", container.TextContent);
    }

    [Fact]
    public void RendersAdditionalClassesAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Container>(parameters => parameters
            .Add(p => p.Class, "w3-border w3-round")
            .Add(p => p.Style, "max-width:480px")
            .Add(p => p.ChildContent, "Content"));

        var container = cut.Find("div");

        Assert.Contains("w3-container", container.GetAttribute("class"));
        Assert.Contains("w3-border", container.GetAttribute("class"));
        Assert.Contains("w3-round", container.GetAttribute("class"));
        Assert.Equal("max-width:480px", container.GetAttribute("style"));
    }
}
