using Bunit;
using Microsoft.AspNetCore.Components;
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
    [InlineData(W3Color.Info, "w3-info", "w3-text-info", "w3-border-info")]
    [InlineData(W3Color.Success, "w3-success", "w3-text-success", "w3-border-success")]
    [InlineData(W3Color.Warning, "w3-warning", "w3-text-warning", "w3-border-warning")]
    [InlineData(W3Color.Danger, "w3-danger", "w3-text-danger", "w3-border-danger")]
    [InlineData(W3Color.Note, "w3-note", "w3-text-note", "w3-border-note")]
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
                Success = "#009944",
                OnSuccess = "#001a08",
                Danger = "#cc2244",
                OnDanger = "#fff8f8",
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
        Assert.Contains("--w3-success:#009944", markup);
        Assert.Contains("--w3-on-success:#001a08", markup);
        Assert.Contains("--w3-danger:#cc2244", markup);
        Assert.Contains("--w3-on-danger:#fff8f8", markup);
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
            .Add(p => p.Theme, new W3Theme { Primary = "#111111", Dark = new W3Theme { Primary = "#eeeeee", Warning = "#ffee77" } })
            .Add(p => p.Dark, true)
            .Add(p => p.ChildContent, "x"));

        Assert.Equal("true", cut.Find("div.w3-theme-root").GetAttribute("data-w3-dark"));
        Assert.Contains(".w3-theme-root[data-w3-dark='true']{", cut.Markup);
        Assert.Contains("--w3-primary:#eeeeee", cut.Markup);
        Assert.Contains("--w3-warning:#ffee77", cut.Markup);
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
    public void AlertsCanUseProviderSemanticStatusTokens()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3ThemeProvider>(parameters => parameters
            .Add(p => p.Theme, new W3Theme { Danger = "#b00020", OnDanger = "#ffffff" })
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3Alert>(0);
                builder.AddAttribute(1, nameof(W3Alert.Kind), W3AlertKind.Danger);
                builder.AddAttribute(2, nameof(W3Alert.ChildContent), (RenderFragment)(content => content.AddContent(3, "Check this")));
                builder.CloseComponent();
            }));

        Assert.Contains("--w3-danger:#b00020", cut.Markup);
        Assert.Contains("w3-danger", cut.Find("[role=\"status\"]").GetAttribute("class"));
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
        Assert.Contains("--w3-success: #008a00;", css);
        Assert.Contains(".w3-success { background-color: var(--w3-success) !important; color: var(--w3-on-success) !important; }", css);
        Assert.Contains(".w3-border-danger { border-color: var(--w3-danger) !important; }", css);
        Assert.Contains(":focus-visible", css);
        Assert.Contains("outline: var(--w3-focus-width) solid var(--w3-focus-color) !important;", css);
        Assert.Contains("[aria-disabled=\"true\"]", css);
        Assert.Contains(".w3-theme-root[data-w3-dark=\"true\"] .w3-striped tbody tr:nth-child(even)", css);
        Assert.Contains(".w3-theme-root[data-w3-dark=\"true\"] .w3-hoverable tbody tr:hover", css);
        Assert.Contains(".w3-theme-root[data-w3-dark=\"true\"] .w3-hoverable.w3-striped tbody tr:nth-child(even):hover", css);
        Assert.Contains("background-color: var(--w3-border) !important;", css);
        Assert.Contains("color: var(--w3-on-surface) !important;", css);
    }
}
