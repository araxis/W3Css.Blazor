using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3PanelTests
{
    [Fact]
    public void RendersPanelWithBorderAndLeftBar()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Panel>(parameters => parameters
            .Add(p => p.Color, W3Color.PaleYellow)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Border, true)
            .Add(p => p.LeftBar, true)
            .Add(p => p.ChildContent, "Read the release notes."));

        var panel = cut.Find("div");

        Assert.Contains("w3-panel", panel.GetAttribute("class"));
        Assert.Contains("w3-panel-surface", panel.GetAttribute("class"));
        Assert.Contains("w3-pale-yellow", panel.GetAttribute("class"));
        Assert.Contains("w3-text-black", panel.GetAttribute("class"));
        Assert.Contains("w3-round", panel.GetAttribute("class"));
        Assert.Contains("w3-border", panel.GetAttribute("class"));
        Assert.Contains("w3-leftbar", panel.GetAttribute("class"));
        Assert.Equal("Read the release notes.", panel.TextContent.Trim());
    }

    [Fact]
    public void RendersAdditionalClassesAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Panel>(parameters => parameters
            .Add(p => p.Class, "w3-margin-top w3-padding")
            .Add(p => p.Style, "max-width:560px")
            .Add(p => p.ChildContent, "Panel content"));

        var panel = cut.Find("div");

        Assert.Contains("w3-panel", panel.GetAttribute("class"));
        Assert.Contains("w3-panel-surface", panel.GetAttribute("class"));
        Assert.Contains("w3-margin-top", panel.GetAttribute("class"));
        Assert.Contains("w3-padding", panel.GetAttribute("class"));
        Assert.Equal("max-width:560px", panel.GetAttribute("style"));
    }
}
