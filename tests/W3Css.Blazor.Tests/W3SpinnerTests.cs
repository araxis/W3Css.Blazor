using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3SpinnerTests
{
    [Fact]
    public void SpinnerRendersAccessibleStatusWithSpinVisual()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spinner>(parameters => parameters
            .Add(p => p.Label, "Loading rows")
            .Add(p => p.Class, "spinner-root")
            .Add(p => p.Style, "min-height: 2rem;"));

        var spinner = cut.Find("[role='status']");
        var visual = cut.Find(".w3-spinner-visual");

        Assert.Contains("w3-spinner", spinner.GetAttribute("class"));
        Assert.Contains("spinner-root", spinner.GetAttribute("class"));
        Assert.Contains("w3-text-teal", spinner.GetAttribute("class"));
        Assert.Contains("w3-large", spinner.GetAttribute("class"));
        Assert.Equal("min-height: 2rem;", spinner.GetAttribute("style"));
        Assert.Equal("polite", spinner.GetAttribute("aria-live"));
        Assert.Equal("Loading rows", spinner.GetAttribute("aria-label"));
        Assert.Contains("w3-spin", visual.GetAttribute("class"));
        Assert.Equal(string.Empty, spinner.TextContent.Trim());
    }

    [Fact]
    public void SpinnerCanShowLabelInlineAndCentered()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spinner>(parameters => parameters
            .Add(p => p.Label, "Saving")
            .Add(p => p.ShowLabel, true)
            .Add(p => p.Inline, true)
            .Add(p => p.Centered, true)
            .Add(p => p.Size, W3Size.XLarge)
            .Add(p => p.TextColor, W3Color.Indigo));

        var spinner = cut.Find("span[role='status']");

        Assert.Contains("Saving", spinner.TextContent);
        Assert.Null(spinner.GetAttribute("aria-label"));
        Assert.Contains("w3-spinner-inline", spinner.GetAttribute("class"));
        Assert.Contains("w3-spinner-centered", spinner.GetAttribute("class"));
        Assert.Contains("w3-xlarge", spinner.GetAttribute("class"));
        Assert.Contains("w3-text-indigo", spinner.GetAttribute("class"));
    }
}
