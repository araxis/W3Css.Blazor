using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3CollapseTests
{
    [Fact]
    public void CollapseHidesAndShowsContentBasedOnExpandedState()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Collapse>(parameters => parameters
            .Add(p => p.Expanded, true)
            .Add(p => p.Border, true)
            .Add(p => p.Animate, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.ChildContent, "Expanded content"));

        var root = cut.Find("div");

        Assert.Contains("w3-collapse-content", root.GetAttribute("class"));
        Assert.Contains("w3-show", root.GetAttribute("class"));
        Assert.Contains("w3-animate-opacity", root.GetAttribute("class"));
        Assert.Contains("w3-border", root.GetAttribute("class"));
        Assert.Contains("w3-round", root.GetAttribute("class"));
        Assert.Contains("w3-white", root.GetAttribute("class"));
        Assert.Contains("w3-text-black", root.GetAttribute("class"));
        Assert.Contains("Expanded content", cut.Markup);

        cut = context.Render<W3Collapse>(parameters => parameters
            .Add(p => p.Expanded, false)
            .Add(p => p.Border, true)
            .Add(p => p.Animate, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.ChildContent, "Expanded content"));

        root = cut.Find("div");

        Assert.Contains("w3-hide", root.GetAttribute("class"));
        Assert.DoesNotContain("w3-show", root.GetAttribute("class"));
        Assert.DoesNotContain("Expanded content", cut.Markup);
    }

    [Fact]
    public void CollapsePrefersIsExpandedWhenProvided()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Collapse>(parameters => parameters
            .Add(p => p.Expanded, true)
            .Add(p => p.IsExpanded, false)
            .Add(p => p.ChildContent, "Alias-content"));

        Assert.Contains("w3-hide", cut.Find("div").GetAttribute("class"));
        Assert.DoesNotContain("w3-show", cut.Find("div").GetAttribute("class"));
        Assert.DoesNotContain("Alias-content", cut.Markup);
    }

    [Fact]
    public async Task CollapseToggleApiReturnsAliasStateWhenUsed()
    {
        using var context = new BunitContext();

        var isExpanded = true;
        var cut = context.Render<W3Collapse>(parameters => parameters
            .Add(p => p.IsExpanded, isExpanded)
            .Add(p => p.IsExpandedChanged, value => isExpanded = value)
            .Add(p => p.ChildContent, "Alias output"));

        await cut.Instance.SetExpandedAsync(false);

        Assert.False(isExpanded);
    }
}
