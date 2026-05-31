using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ListItemTests
{
    [Fact]
    public void RendersListItemAsLinkWithStartAndEndContent()
    {
        using var context = new BunitContext();
        var clicked = false;

        var cut = context.Render<W3ListItem>(parameters => parameters
            .Add(p => p.Text, "Inbox")
            .Add(p => p.Href, "components/list-item")
            .Add(p => p.IconClass, "fa fa-envelope")
            .Add(p => p.Divider, true)
            .Add(p => p.Class, "list-item-shell")
            .Add(p => p.Style, "max-width: 32rem;")
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .Add(p => p.EndContent, (RenderFragment)(end => end.AddContent(0, "9"))));

        var root = cut.Find("li");
        var link = cut.Find("a");

        Assert.Contains("w3-list-item", root.GetAttribute("class"));
        Assert.Contains("w3-border-bottom", root.GetAttribute("class"));
        Assert.Contains("list-item-shell", root.GetAttribute("class"));
        Assert.Equal("max-width: 32rem;", root.GetAttribute("style"));
        Assert.Equal("components/list-item", link.GetAttribute("href"));
        Assert.Contains("w3-list-item-action", link.GetAttribute("class"));
        Assert.Contains("fa-envelope", cut.Find(".w3-list-item-icon").GetAttribute("class"));
        Assert.Equal("9", cut.Find(".w3-list-item-end").TextContent);

        link.Click();
        Assert.True(clicked);
    }

    [Fact]
    public void RendersStartContentWhenTextNotProvided()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3ListItem>(parameters => parameters
            .Add(p => p.Class, "start-content-item")
            .Add(p => p.StartContent, (RenderFragment)(content =>
            {
                content.OpenElement(0, "span");
                content.AddAttribute(1, "class", "w3-badge");
                content.AddContent(2, "New");
                content.CloseElement();
            }))
            .Add(p => p.EndContent, (RenderFragment)(content =>
            {
                content.OpenElement(0, "span");
                content.AddAttribute(1, "class", "w3-tag");
                content.AddContent(2, "new");
                content.CloseElement();
            })));

        var start = cut.Find(".w3-list-item-start");
        var end = cut.Find(".w3-list-item-end");

        Assert.Contains("start-content-item", cut.Find("li").GetAttribute("class"));
        Assert.Equal("New", start.TextContent);
        Assert.Equal("new", end.TextContent);
    }

    [Fact]
    public void RendersDisabledStateWithoutNavigation()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3ListItem>(parameters => parameters
            .Add(p => p.Text, "Unavailable")
            .Add(p => p.Disabled, true)
            .Add(p => p.Href, "components/list-item"));

        var item = cut.Find("span[role='button']");
        var anchor = cut.FindAll("a");

        Assert.Equal("button", item.GetAttribute("role"));
        Assert.Equal("true", item.GetAttribute("aria-disabled"));
        Assert.Empty(anchor);
        Assert.Contains("w3-disabled", item.GetAttribute("class"));
        Assert.Equal("listitem", cut.Find("li").GetAttribute("role"));
    }
}
