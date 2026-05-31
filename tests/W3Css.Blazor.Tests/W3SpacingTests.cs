using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3SpacingTests
{
    [Fact]
    public void SpacingRendersPaddingMarginSectionColorAndRoundness()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spacing>(parameters => parameters
            .Add(p => p.Section, true)
            .Add(p => p.Margin, W3Margin.All)
            .Add(p => p.Padding, W3Padding.Vertical32)
            .Add(p => p.Color, W3Color.PaleBlue)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.ChildContent, "Spaced"));

        var spacing = cut.Find("div");
        var classes = spacing.GetAttribute("class");

        Assert.Contains("w3-section", classes);
        Assert.Contains("w3-margin", classes);
        Assert.Contains("w3-padding-32", classes);
        Assert.Contains("w3-pale-blue", classes);
        Assert.Contains("w3-text-black", classes);
        Assert.Contains("w3-round", classes);
        Assert.Equal("Spaced", spacing.TextContent);
    }

    [Fact]
    public void SpacingRendersCombinedSideMargins()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spacing>(parameters => parameters
            .Add(p => p.Margin, W3Margin.Top | W3Margin.Bottom)
            .Add(p => p.Padding, W3Padding.Medium)
            .Add(p => p.ChildContent, "Side margins"));

        var spacing = cut.Find("div");
        var classes = spacing.GetAttribute("class");

        Assert.Contains("w3-margin-top", classes);
        Assert.Contains("w3-margin-bottom", classes);
        Assert.Contains("w3-padding", classes);
        Assert.DoesNotContain("w3-margin-right", classes);
        Assert.DoesNotContain("w3-margin-left", classes);
    }

    [Fact]
    public void SpacingRendersTopPaddingClass()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spacing>(parameters => parameters
            .Add(p => p.TopPadding, W3TopPadding.Top48)
            .Add(p => p.ChildContent, "Top padding"));

        var spacing = cut.Find("div");

        Assert.Contains("w3-padding-top-48", spacing.GetAttribute("class"));
    }

    [Fact]
    public void SpacingRendersAdditionalClassesAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spacing>(parameters => parameters
            .Add(p => p.Padding, W3Padding.Small)
            .Add(p => p.Class, "custom-shell")
            .Add(p => p.Style, "min-height: 4rem;")
            .Add(p => p.ChildContent, "Custom"));

        var spacing = cut.Find("div");
        var classes = spacing.GetAttribute("class");

        Assert.Contains("w3-padding-small", classes);
        Assert.Contains("custom-shell", classes);
        Assert.Equal("min-height: 4rem;", spacing.GetAttribute("style"));
    }

    [Theory]
    [InlineData(W3Padding.Small, "w3-padding-small")]
    [InlineData(W3Padding.Medium, "w3-padding")]
    [InlineData(W3Padding.Large, "w3-padding-large")]
    [InlineData(W3Padding.Vertical16, "w3-padding-16")]
    [InlineData(W3Padding.Vertical24, "w3-padding-24")]
    [InlineData(W3Padding.Vertical32, "w3-padding-32")]
    [InlineData(W3Padding.Vertical48, "w3-padding-48")]
    [InlineData(W3Padding.Vertical64, "w3-padding-64")]
    public void SpacingMapsPaddingClasses(W3Padding padding, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spacing>(parameters => parameters
            .Add(p => p.Padding, padding)
            .Add(p => p.ChildContent, "Padding"));

        var spacing = cut.Find("div");

        Assert.Contains(expectedClass, spacing.GetAttribute("class"));
    }

    [Theory]
    [InlineData(W3TopPadding.Top24, "w3-padding-top-24")]
    [InlineData(W3TopPadding.Top32, "w3-padding-top-32")]
    [InlineData(W3TopPadding.Top48, "w3-padding-top-48")]
    [InlineData(W3TopPadding.Top64, "w3-padding-top-64")]
    public void SpacingMapsTopPaddingClasses(W3TopPadding topPadding, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spacing>(parameters => parameters
            .Add(p => p.TopPadding, topPadding)
            .Add(p => p.ChildContent, "Top padding"));

        var spacing = cut.Find("div");

        Assert.Contains(expectedClass, spacing.GetAttribute("class"));
    }

    [Theory]
    [InlineData(W3Margin.All, "w3-margin")]
    [InlineData(W3Margin.Top, "w3-margin-top")]
    [InlineData(W3Margin.Right, "w3-margin-right")]
    [InlineData(W3Margin.Bottom, "w3-margin-bottom")]
    [InlineData(W3Margin.Left, "w3-margin-left")]
    public void SpacingMapsMarginClasses(W3Margin margin, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spacing>(parameters => parameters
            .Add(p => p.Margin, margin)
            .Add(p => p.ChildContent, "Margin"));

        var spacing = cut.Find("div");

        Assert.Contains(expectedClass, spacing.GetAttribute("class"));
    }
}
