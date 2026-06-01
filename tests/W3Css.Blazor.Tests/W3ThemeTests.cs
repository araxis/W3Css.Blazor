using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;
using W3Css.Blazor.Internal;

namespace W3Css.Blazor.Tests;

public sealed class W3ThemeTests
{
    [Theory]
    [InlineData(W3Color.Primary, "w3-primary", "w3-text-primary", "w3-border-primary")]
    [InlineData(W3Color.Secondary, "w3-secondary", "w3-text-secondary", "w3-border-secondary")]
    [InlineData(W3Color.Accent, "w3-accent", "w3-text-accent", "w3-border-accent")]
    [InlineData(W3Color.Surface, "w3-surface", "w3-text-surface", "w3-border-surface")]
    public void ThemeTokensMapToTokenClasses(W3Color color, string bg, string text, string border)
    {
        Assert.Equal(bg, color.ToBackgroundClass());
        Assert.Equal(text, color.ToTextClass());
        Assert.Equal(border, color.ToBorderClass());
    }

    [Fact]
    public void ProviderEmitsCssVariablesFromTheme()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3ThemeProvider>(parameters => parameters
            .Add(p => p.Theme, new W3Theme
            {
                Primary = "#abcdef",
                Surface = "#101010",
                Radius = "9px",
                FocusColor = "#fedcba",
                FocusWidth = "3px",
                FocusOffset = "4px"
            })
            .Add(p => p.ChildContent, "content"));

        var markup = cut.Markup;
        Assert.Contains(".w3-theme-root{", markup);
        Assert.Contains("--w3-primary:#abcdef", markup);
        Assert.Contains("--w3-surface:#101010", markup);
        Assert.Contains("--w3-radius:9px", markup);
        Assert.Contains("--w3-focus-color:#fedcba", markup);
        Assert.Contains("--w3-focus-width:3px", markup);
        Assert.Contains("--w3-focus-offset:4px", markup);

        var root = cut.Find("div.w3-theme-root");
        Assert.False(root.HasAttribute("data-w3-dark"));
        Assert.Contains("content", root.TextContent);
    }

    [Fact]
    public void ProviderTogglesDarkAttributeAndEmitsDarkVariables()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3ThemeProvider>(parameters => parameters
            .Add(p => p.Theme, new W3Theme { Primary = "#111111", Dark = new W3Theme { Primary = "#eeeeee" } })
            .Add(p => p.Dark, true)
            .Add(p => p.ChildContent, "x"));

        Assert.Equal("true", cut.Find("div.w3-theme-root").GetAttribute("data-w3-dark"));
        Assert.Contains(".w3-theme-root[data-w3-dark='true']{", cut.Markup);
        Assert.Contains("--w3-primary:#eeeeee", cut.Markup);
    }

    [Fact]
    public void ButtonAdoptsPrimaryTokenClass()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Button>(parameters => parameters
            .Add(b => b.Color, W3Color.Primary)
            .Add(b => b.ChildContent, "Save"));

        Assert.Contains("w3-primary", cut.Find("button").GetAttribute("class"));
    }

    [Fact]
    public void ThemeStylesheetDefinesTokenizedFocusVisibleRules()
    {
        var css = File.ReadAllText(Path.Combine(
            AppContext.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "..",
            "src",
            "W3Css.Blazor",
            "wwwroot",
            "w3-theme.css"));

        Assert.Contains("--w3-focus-color: var(--w3-primary);", css);
        Assert.Contains(":focus-visible", css);
        Assert.Contains("outline: var(--w3-focus-width) solid var(--w3-focus-color) !important;", css);
        Assert.Contains("[aria-disabled=\"true\"]", css);
    }
}
