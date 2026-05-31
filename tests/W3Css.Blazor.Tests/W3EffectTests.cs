using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3EffectTests
{
    [Fact]
    public void EffectRendersOpacityColorAndRoundness()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Effect>(parameters => parameters
            .Add(p => p.Effect, W3EffectStyle.OpacityMin)
            .Add(p => p.Color, W3Color.PaleBlue)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.ChildContent, "Opacity"));

        var effect = cut.Find("div");
        var classes = effect.GetAttribute("class");

        Assert.Contains("w3-opacity-min", classes);
        Assert.Contains("w3-pale-blue", classes);
        Assert.Contains("w3-text-black", classes);
        Assert.Contains("w3-round", classes);
        Assert.Equal("Opacity", effect.TextContent.Trim());
    }

    [Fact]
    public void EffectRendersFilterClasses()
    {
        using var context = new BunitContext();
        var grayscale = context.Render<W3Effect>(parameters => parameters
            .Add(p => p.Effect, W3EffectStyle.GrayscaleMax)
            .Add(p => p.ChildContent, "Gray"));
        var sepia = context.Render<W3Effect>(parameters => parameters
            .Add(p => p.Effect, W3EffectStyle.SepiaMin)
            .Add(p => p.ChildContent, "Sepia"));

        Assert.Contains("w3-grayscale-max", grayscale.Find("div").GetAttribute("class"));
        Assert.Contains("w3-sepia-min", sepia.Find("div").GetAttribute("class"));
    }

    [Fact]
    public void EffectRendersHoverEffectAdditionalClassAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Effect>(parameters => parameters
            .Add(p => p.HoverEffect, W3HoverEffect.Shadow)
            .Add(p => p.Class, "custom-effect")
            .Add(p => p.Style, "transition: opacity .2s;")
            .Add(p => p.ChildContent, "Hover"));

        var effect = cut.Find("div");
        var classes = effect.GetAttribute("class");

        Assert.Contains("w3-hover-shadow", classes);
        Assert.Contains("custom-effect", classes);
        Assert.Equal("transition: opacity .2s;", effect.GetAttribute("style"));
    }

    [Fact]
    public void EffectCanRenderInlineSpan()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Effect>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.Effect, W3EffectStyle.OpacityOff)
            .Add(p => p.HoverEffect, W3HoverEffect.Opacity)
            .Add(p => p.ChildContent, "Inline"));

        var effect = cut.Find("span");
        var classes = effect.GetAttribute("class");

        Assert.Contains("w3-opacity-off", classes);
        Assert.Contains("w3-hover-opacity", classes);
        Assert.Equal("Inline", effect.TextContent);
    }

    [Fact]
    public void EffectMapsHoverNoneClass()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Effect>(parameters => parameters
            .Add(p => p.HoverEffect, W3HoverEffect.NoEffect)
            .Add(p => p.ChildContent, "No hover"));

        var effect = cut.Find("div");

        Assert.Contains("w3-hover-none", effect.GetAttribute("class"));
    }

    [Theory]
    [InlineData(W3EffectStyle.Opacity, "w3-opacity")]
    [InlineData(W3EffectStyle.OpacityMin, "w3-opacity-min")]
    [InlineData(W3EffectStyle.OpacityMax, "w3-opacity-max")]
    [InlineData(W3EffectStyle.OpacityOff, "w3-opacity-off")]
    [InlineData(W3EffectStyle.Grayscale, "w3-grayscale")]
    [InlineData(W3EffectStyle.GrayscaleMin, "w3-grayscale-min")]
    [InlineData(W3EffectStyle.GrayscaleMax, "w3-grayscale-max")]
    [InlineData(W3EffectStyle.Sepia, "w3-sepia")]
    [InlineData(W3EffectStyle.SepiaMin, "w3-sepia-min")]
    [InlineData(W3EffectStyle.SepiaMax, "w3-sepia-max")]
    public void EffectMapsEffectClasses(W3EffectStyle effectStyle, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Effect>(parameters => parameters
            .Add(p => p.Effect, effectStyle)
            .Add(p => p.ChildContent, "Effect"));

        var effect = cut.Find("div");

        Assert.Contains(expectedClass, effect.GetAttribute("class"));
    }

    [Theory]
    [InlineData(W3HoverEffect.Opacity, "w3-hover-opacity")]
    [InlineData(W3HoverEffect.OpacityOff, "w3-hover-opacity-off")]
    [InlineData(W3HoverEffect.Grayscale, "w3-hover-grayscale")]
    [InlineData(W3HoverEffect.Sepia, "w3-hover-sepia")]
    [InlineData(W3HoverEffect.Shadow, "w3-hover-shadow")]
    [InlineData(W3HoverEffect.NoEffect, "w3-hover-none")]
    public void EffectMapsHoverEffectClasses(W3HoverEffect hoverEffect, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Effect>(parameters => parameters
            .Add(p => p.HoverEffect, hoverEffect)
            .Add(p => p.ChildContent, "Hover"));

        var effect = cut.Find("div");

        Assert.Contains(expectedClass, effect.GetAttribute("class"));
    }
}
