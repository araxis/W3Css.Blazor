using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3PageContentNavigationTests
{
    [Fact]
    public async Task RendersSectionLinksAndHighlightsActiveSection()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        string? raised = null;
        var cut = context.Render<W3PageContentNavigation>(parameters => parameters
            .Add(c => c.Title, "On this page")
            .Add(c => c.ActiveSectionChanged, (string? id) => raised = id)
            .Add(c => c.Sections, new[]
            {
                new W3PageSection("overview", "Overview"),
                new W3PageSection("usage", "Usage")
            }));

        var nav = cut.Find("nav");
        Assert.Equal("On this page", nav.GetAttribute("aria-label"));
        Assert.Contains("w3-page-content-nav", nav.GetAttribute("class"));

        var links = cut.FindAll("a");
        Assert.Equal(2, links.Count);
        Assert.Equal("#overview", links[0].GetAttribute("href"));
        Assert.Equal("#usage", links[1].GetAttribute("href"));
        Assert.All(cut.FindAll("a"), a => Assert.Equal("false", a.GetAttribute("aria-current")));

        await cut.InvokeAsync(() => cut.Instance.SetActive("usage"));

        var usageLink = cut.FindAll("a")[1];
        Assert.Equal("true", usageLink.GetAttribute("aria-current"));
        Assert.Contains("w3-text-teal", usageLink.GetAttribute("class"));
        Assert.Single(cut.FindAll("li.w3-page-content-nav-active"));
        Assert.Equal("usage", raised);
    }

    [Fact]
    public void EmptySectionsRenderNoLinks()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3PageContentNavigation>();

        Assert.Empty(cut.FindAll("a"));
        Assert.NotNull(cut.Find("nav"));
    }
}
