using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3OverlayComponentTests
{
    [Fact]
    public void OverlayRendersOnlyWhenVisibleAndAppliesCustomStyle()
    {
        using var context = new BunitContext();

        var openOverlay = context.Render<W3Overlay>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.Contained, true)
            .Add(p => p.Style, "opacity:0.5")
            .Add(p => p.Class, "overlay-test"));

        var overlay = openOverlay.Find(".w3-overlay");

        Assert.NotNull(overlay);
        Assert.Contains("w3-overlay", overlay.GetAttribute("class"));
        Assert.Contains("w3-show", overlay.GetAttribute("class"));
        Assert.Contains("overlay-test", overlay.GetAttribute("class"));
        Assert.Equal("position:absolute!important;opacity:0.5", overlay.GetAttribute("style"));

        var hiddenOverlay = context.Render<W3Overlay>(parameters => parameters
            .Add(p => p.Visible, false)
            .Add(p => p.Contained, true)
            .Add(p => p.Style, "opacity:0.5")
            .Add(p => p.Class, "overlay-test"));

        Assert.Empty(hiddenOverlay.FindAll("div"));
    }

    [Fact]
    public void OverlayClosesOnClickWhenAutoCloseIsEnabled()
    {
        using var context = new BunitContext();
        var visible = true;
        var clicked = 0;

        var cut = context.Render<W3Overlay>(parameters => parameters
            .Add(p => p.Visible, visible)
            .Add(p => p.VisibleChanged, EventCallback.Factory.Create<bool>(this, value => visible = value))
            .Add(p => p.OnClick, EventCallback.Factory.Create(this, () => clicked++))
            .Add(p => p.Contained, true));

        cut.Find(".w3-overlay").Click();

        Assert.False(visible);
        Assert.Equal(1, clicked);
    }

    [Fact]
    public void OverlayDoesNotAutoCloseWhenAutoCloseDisabled()
    {
        using var context = new BunitContext();
        var visible = true;
        var clicked = 0;

        var cut = context.Render<W3Overlay>(parameters => parameters
            .Add(p => p.Visible, visible)
            .Add(p => p.VisibleChanged, EventCallback.Factory.Create<bool>(this, value => visible = value))
            .Add(p => p.AutoClose, false)
            .Add(p => p.OnClick, EventCallback.Factory.Create(this, () => clicked++))
            .Add(p => p.Contained, true));

        cut.Find(".w3-overlay").Click();

        Assert.True(visible);
        Assert.Equal(1, clicked);
    }
}
