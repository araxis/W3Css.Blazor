using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3AnimateTests
{
    [Fact]
    public void AnimateRendersDirectionalAnimationColorAndRoundness()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Animate>(parameters => parameters
            .Add(p => p.Animation, W3Animation.Top)
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.ChildContent, "Animated"));

        var animate = cut.Find("div");
        var classes = animate.GetAttribute("class");

        Assert.Contains("w3-animate-top", classes);
        Assert.Contains("w3-teal", classes);
        Assert.Contains("w3-text-white", classes);
        Assert.Contains("w3-round", classes);
        Assert.Equal("Animated", animate.TextContent.Trim());
    }

    [Theory]
    [InlineData(W3Animation.Top, "w3-animate-top")]
    [InlineData(W3Animation.Bottom, "w3-animate-bottom")]
    [InlineData(W3Animation.Left, "w3-animate-left")]
    [InlineData(W3Animation.Right, "w3-animate-right")]
    [InlineData(W3Animation.Opacity, "w3-animate-opacity")]
    [InlineData(W3Animation.Zoom, "w3-animate-zoom")]
    [InlineData(W3Animation.Fading, "w3-animate-fading")]
    [InlineData(W3Animation.Spin, "w3-spin")]
    [InlineData(W3Animation.Input, "w3-animate-input")]
    public void AnimateMapsAnimationClasses(W3Animation animation, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Animate>(parameters => parameters
            .Add(p => p.Animation, animation)
            .Add(p => p.ChildContent, "Motion"));

        Assert.Contains(expectedClass, cut.Find("div").GetAttribute("class"));
    }

    [Fact]
    public void AnimateCanRenderInlineSpan()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Animate>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.Animation, W3Animation.Opacity)
            .Add(p => p.ChildContent, "Inline"));

        var animate = cut.Find("span");

        Assert.Contains("w3-animate-opacity", animate.GetAttribute("class"));
        Assert.Equal("Inline", animate.TextContent);
    }

    [Fact]
    public void AnimateRendersAdditionalClassAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Animate>(parameters => parameters
            .Add(p => p.Animation, W3Animation.Zoom)
            .Add(p => p.Class, "custom-animation")
            .Add(p => p.Style, "animation-duration: .4s;")
            .Add(p => p.ChildContent, "Zoom"));

        var animate = cut.Find("div");
        var classes = animate.GetAttribute("class");

        Assert.Contains("w3-animate-zoom", classes);
        Assert.Contains("custom-animation", classes);
        Assert.Equal("animation-duration: .4s;", animate.GetAttribute("style"));
    }

    [Fact]
    public void InputCanRenderInputAnimationClass()
    {
        using var context = new BunitContext();
        var value = "Search";
        var cut = context.Render<W3Input>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next ?? string.Empty))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Animation, W3Animation.Input)
            .Add(p => p.Placeholder, "Focus to expand"));

        var input = cut.Find("input");

        Assert.Contains("w3-input", input.GetAttribute("class"));
        Assert.Contains("w3-animate-input", input.GetAttribute("class"));
        Assert.Equal("Focus to expand", input.GetAttribute("placeholder"));
    }
}
