using Bunit;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3SwipeAreaTests
{
    [Fact]
    public void RendersSurfaceClassesAndAttributes()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3SwipeArea>(parameters => parameters
            .Add(c => c.Border, true)
            .Add(c => c.Round, W3Round.Large)
            .Add(c => c.Color, W3Color.PaleBlue)
            .Add(c => c.TextColor, W3Color.Black)
            .Add(c => c.AriaLabel, "Card gestures")
            .Add(c => c.Class, "custom-swipe")
            .Add(c => c.Style, "min-height: 4rem;")
            .Add(c => c.ChildContent, "Swipe me"));

        var root = cut.Find(".w3-swipe-area");
        Assert.Contains("w3-border", root.GetAttribute("class"));
        Assert.Contains("w3-round-large", root.GetAttribute("class"));
        Assert.Contains("w3-pale-blue", root.GetAttribute("class"));
        Assert.Contains("w3-text-black", root.GetAttribute("class"));
        Assert.Contains("custom-swipe", root.GetAttribute("class"));
        Assert.Contains("touch-action: pan-y", root.GetAttribute("style"));
        Assert.Contains("min-height: 4rem", root.GetAttribute("style"));
        Assert.Equal("region", root.GetAttribute("role"));
        Assert.Equal("Card gestures", root.GetAttribute("aria-label"));
        Assert.Equal("Swipe me", root.TextContent.Trim());
    }

    [Fact]
    public void RaisesGeneralAndDirectionalCallbackForAcceptedSwipe()
    {
        using var context = new BunitContext();
        W3SwipeEventArgs? generalSwipe = null;
        W3SwipeEventArgs? leftSwipe = null;

        var cut = context.Render<W3SwipeArea>(parameters => parameters
            .Add(c => c.Threshold, 40)
            .Add(c => c.OnSwipe, args => generalSwipe = args)
            .Add(c => c.OnSwipeLeft, args => leftSwipe = args)
            .Add(c => c.ChildContent, "Swipe"));

        var root = cut.Find(".w3-swipe-area");
        root.TriggerEvent("onpointerdown", new PointerEventArgs { PointerId = 2, ClientX = 180, ClientY = 20 });
        root.TriggerEvent("onpointerup", new PointerEventArgs { PointerId = 2, ClientX = 80, ClientY = 28 });

        Assert.NotNull(generalSwipe);
        Assert.NotNull(leftSwipe);
        Assert.Equal(W3SwipeDirection.Left, generalSwipe.Direction);
        Assert.Equal(generalSwipe, leftSwipe);
        Assert.Equal(-100, generalSwipe.DeltaX);
        Assert.Equal(8, generalSwipe.DeltaY);
        Assert.True(generalSwipe.Distance > 100);
    }

    [Fact]
    public void IgnoresGesturesBelowThresholdOrOutsideRestraint()
    {
        using var context = new BunitContext();
        var swipeCount = 0;

        var cut = context.Render<W3SwipeArea>(parameters => parameters
            .Add(c => c.Threshold, 60)
            .Add(c => c.Restraint, 15)
            .Add(c => c.OnSwipe, _ => swipeCount++)
            .Add(c => c.ChildContent, "Swipe"));

        var root = cut.Find(".w3-swipe-area");
        root.TriggerEvent("onpointerdown", new PointerEventArgs { PointerId = 1, ClientX = 0, ClientY = 0 });
        root.TriggerEvent("onpointerup", new PointerEventArgs { PointerId = 1, ClientX = 40, ClientY = 0 });
        root.TriggerEvent("onpointerdown", new PointerEventArgs { PointerId = 1, ClientX = 0, ClientY = 0 });
        root.TriggerEvent("onpointerup", new PointerEventArgs { PointerId = 1, ClientX = 90, ClientY = 40 });

        Assert.Equal(0, swipeCount);
    }

    [Fact]
    public void IgnoresDisabledAndCancelledGestures()
    {
        using var context = new BunitContext();
        var swipeCount = 0;

        var disabled = context.Render<W3SwipeArea>(parameters => parameters
            .Add(c => c.Disabled, true)
            .Add(c => c.OnSwipe, _ => swipeCount++)
            .Add(c => c.ChildContent, "Disabled"));

        var disabledRoot = disabled.Find(".w3-swipe-area");
        disabledRoot.TriggerEvent("onpointerdown", new PointerEventArgs { PointerId = 7, ClientX = 0, ClientY = 0 });
        disabledRoot.TriggerEvent("onpointerup", new PointerEventArgs { PointerId = 7, ClientX = 100, ClientY = 0 });

        var active = context.Render<W3SwipeArea>(parameters => parameters
            .Add(c => c.OnSwipe, _ => swipeCount++)
            .Add(c => c.ChildContent, "Active"));

        var activeRoot = active.Find(".w3-swipe-area");
        activeRoot.TriggerEvent("onpointerdown", new PointerEventArgs { PointerId = 8, ClientX = 0, ClientY = 0 });
        activeRoot.TriggerEvent("onpointercancel", new PointerEventArgs { PointerId = 8 });
        activeRoot.TriggerEvent("onpointerup", new PointerEventArgs { PointerId = 8, ClientX = 100, ClientY = 0 });

        Assert.Equal(0, swipeCount);
        Assert.Contains("w3-disabled", disabledRoot.GetAttribute("class"));
        Assert.Equal("true", disabledRoot.GetAttribute("aria-disabled"));
    }
}
