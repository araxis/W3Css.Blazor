using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3CardTests
{
    [Fact]
    public void RendersHeaderBodyAndFooter()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Card>(parameters => parameters
            .Add(p => p.Depth, W3CardDepth.Four)
            .Add(p => p.HeaderColor, W3Color.Teal)
            .Add(p => p.HeaderClass, "header-extra")
            .Add(p => p.BodyClass, "body-extra")
            .Add(p => p.FooterClass, "footer-extra")
            .Add(p => p.Header, "<h2>Header</h2>")
            .Add(p => p.ChildContent, "<p>Body</p>")
            .Add(p => p.Footer, "<small>Footer</small>"));

        var root = cut.Find("div");

        Assert.Contains("w3-card-4", root.GetAttribute("class"));
        Assert.Contains("Header", cut.Markup);
        Assert.Contains("Body", cut.Markup);
        Assert.Contains("Footer", cut.Markup);
        Assert.Contains("w3-teal", cut.Markup);
        Assert.Contains("header-extra", cut.Markup);
        Assert.Contains("body-extra", cut.Markup);
        Assert.Contains("footer-extra", cut.Markup);
    }

    [Fact]
    public void RendersAdditionalClassesAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Card>(parameters => parameters
            .Add(p => p.Class, "w3-margin-top custom-card")
            .Add(p => p.Style, "max-width:520px")
            .Add(p => p.ChildContent, "<p>Body</p>"));

        var root = cut.Find("div");

        Assert.Contains("w3-card", root.GetAttribute("class"));
        Assert.Contains("w3-margin-top", root.GetAttribute("class"));
        Assert.Contains("custom-card", root.GetAttribute("class"));
        Assert.Equal("max-width:520px", root.GetAttribute("style"));
    }

    [Fact]
    public void RendersOptionalActionRowInFooter()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Card>(parameters => parameters
            .Add(p => p.FooterColor, W3Color.LightGrey)
            .Add(p => p.FooterClass, "footer-extra")
            .Add(p => p.ActionsClass, "actions-extra")
            .Add(p => p.ActionsGap, 12)
            .Add(p => p.ChildContent, "<p>Body</p>")
            .Add(p => p.Footer, "<small>Updated today</small>")
            .Add(p => p.Actions, "<button class=\"w3-button\">Cancel</button><button class=\"w3-button w3-primary\">Save</button>"));

        var footer = cut.Find(".footer-extra");
        var content = cut.Find(".w3-card-footer-content");
        var actions = cut.Find(".w3-card-actions");

        Assert.Contains("w3-light-grey", footer.GetAttribute("class"));
        Assert.Contains("Updated today", content.TextContent);
        Assert.Contains("actions-extra", actions.GetAttribute("class"));
        Assert.Equal("--w3-card-actions-gap:12px", actions.GetAttribute("style"));
        Assert.Equal(2, actions.QuerySelectorAll("button").Length);
    }
}
