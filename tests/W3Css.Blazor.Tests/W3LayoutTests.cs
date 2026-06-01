using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3LayoutTests
{
    [Fact]
    public void RowCanRenderPaddedLayout()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Row>(parameters => parameters
            .Add(p => p.Padding, true)
            .Add(p => p.Color, W3Color.LightGrey)
            .Add(p => p.ChildContent, "Columns"));

        var row = cut.Find("div");

        Assert.Contains("w3-row-padding", row.GetAttribute("class"));
        Assert.Contains("w3-light-grey", row.GetAttribute("class"));
        Assert.Equal("Columns", row.TextContent);
    }

    [Fact]
    public void RowRendersAdditionalClassesAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Row>(parameters => parameters
            .Add(p => p.Class, "w3-margin-top custom-row")
            .Add(p => p.Style, "gap:12px")
            .Add(p => p.ChildContent, "Columns"));

        var row = cut.Find("div");

        Assert.Contains("w3-row", row.GetAttribute("class"));
        Assert.Contains("w3-margin-top", row.GetAttribute("class"));
        Assert.Contains("custom-row", row.GetAttribute("class"));
        Assert.Equal("gap:12px", row.GetAttribute("style"));
    }

    [Fact]
    public void ColumnMapsBreakpointSpans()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Column>(parameters => parameters
            .Add(p => p.Small, 12)
            .Add(p => p.Medium, 6)
            .Add(p => p.Large, 4)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.ChildContent, "Column"));

        var column = cut.Find("div");

        Assert.Contains("w3-col", column.GetAttribute("class"));
        Assert.Contains("s12", column.GetAttribute("class"));
        Assert.Contains("m6", column.GetAttribute("class"));
        Assert.Contains("l4", column.GetAttribute("class"));
        Assert.Contains("w3-text-black", column.GetAttribute("class"));
    }

    [Fact]
    public void ColumnCanRenderRestColumn()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Column>(parameters => parameters
            .Add(p => p.Rest, true)
            .Add(p => p.Small, 6)
            .Add(p => p.ChildContent, "Rest"));

        var column = cut.Find("div");
        var classes = column.GetAttribute("class");

        Assert.Contains("w3-rest", classes);
        Assert.DoesNotContain("w3-col", classes);
        Assert.DoesNotContain("s6", classes);
    }

    [Fact]
    public void ColumnRendersAdditionalClassesAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Column>(parameters => parameters
            .Add(p => p.Small, 6)
            .Add(p => p.Class, "w3-margin-bottom custom-column")
            .Add(p => p.Style, "min-height:48px")
            .Add(p => p.ChildContent, "Column"));

        var column = cut.Find("div");

        Assert.Contains("w3-col", column.GetAttribute("class"));
        Assert.Contains("s6", column.GetAttribute("class"));
        Assert.Contains("w3-margin-bottom", column.GetAttribute("class"));
        Assert.Contains("custom-column", column.GetAttribute("class"));
        Assert.Equal("min-height:48px", column.GetAttribute("style"));
    }

    [Fact]
    public void GridAndFlexRenderLayoutClasses()
    {
        using var context = new BunitContext();
        var grid = context.Render<W3Grid>(parameters => parameters
            .Add(p => p.Padding, true)
            .Add(p => p.Class, "w3-margin-top custom-grid")
            .Add(p => p.Style, "grid-template-columns:1fr")
            .Add(p => p.ChildContent, "<span>Grid item</span>"));
        var flex = context.Render<W3Flex>(parameters => parameters
            .Add(p => p.Class, "w3-mobile")
            .Add(p => p.Style, "gap:8px")
            .Add(p => p.ChildContent, "<span>Flex item</span>"));

        Assert.Contains("w3-grid-padding", grid.Find("div").GetAttribute("class"));
        Assert.Contains("w3-margin-top", grid.Find("div").GetAttribute("class"));
        Assert.Contains("custom-grid", grid.Find("div").GetAttribute("class"));
        Assert.Equal("grid-template-columns:1fr", grid.Find("div").GetAttribute("style"));
        Assert.Contains("Grid item", grid.Find("div").TextContent);
        Assert.Contains("w3-flex", flex.Find("div").GetAttribute("class"));
        Assert.Contains("w3-mobile", flex.Find("div").GetAttribute("class"));
        Assert.Equal("gap:8px", flex.Find("div").GetAttribute("style"));
    }

    [Fact]
    public void BarAndBarItemRenderNavigationClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Bar>(parameters => parameters
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Class, "main-bar")
            .Add(p => p.Style, "min-height: 3rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BarItem>(0);
                builder.AddAttribute(1, nameof(W3BarItem.Href), "components");
                builder.AddAttribute(2, nameof(W3BarItem.Mobile), true);
                builder.AddAttribute(3, nameof(W3BarItem.Color), W3Color.White);
                builder.AddAttribute(4, nameof(W3BarItem.TextColor), W3Color.Teal);
                builder.AddAttribute(5, nameof(W3BarItem.Class), "nav-link");
                builder.AddAttribute(6, nameof(W3BarItem.Style), "font-weight: 700;");
                builder.AddAttribute(7, nameof(W3BarItem.ChildContent), (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, "Components")));
                builder.CloseComponent();
            }));

        var bar = cut.Find("div");
        var item = cut.Find("a");

        Assert.Contains("w3-bar", bar.GetAttribute("class"));
        Assert.Contains("w3-teal", bar.GetAttribute("class"));
        Assert.Contains("w3-text-white", bar.GetAttribute("class"));
        Assert.Contains("main-bar", bar.GetAttribute("class"));
        Assert.Equal("min-height: 3rem;", bar.GetAttribute("style"));
        Assert.Contains("w3-bar-item", item.GetAttribute("class"));
        Assert.Contains("w3-button", item.GetAttribute("class"));
        Assert.Contains("w3-mobile", item.GetAttribute("class"));
        Assert.Contains("w3-white", item.GetAttribute("class"));
        Assert.Contains("w3-text-teal", item.GetAttribute("class"));
        Assert.Contains("nav-link", item.GetAttribute("class"));
        Assert.Equal("font-weight: 700;", item.GetAttribute("style"));
        Assert.Equal("components", item.GetAttribute("href"));
    }

    [Fact]
    public void AppBarRendersTitleMenuNavigationAndActions()
    {
        using var context = new BunitContext();
        var menuClicked = false;
        var cut = context.Render<W3AppBar>(parameters => parameters
            .Add(p => p.Title, "Studio")
            .Add(p => p.Subtitle, "Design workspace")
            .Add(p => p.NavigationLabel, "Workspace sections")
            .Add(p => p.ShowMenuButton, true)
            .Add(p => p.MenuButtonAriaLabel, "Open workspace navigation")
            .Add(p => p.MenuText, "Open")
            .Add(p => p.MenuExpanded, false)
            .Add(p => p.MenuControls, "sidebar")
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Card, true)
            .Add(p => p.Class, "app-bar-extra")
            .Add(p => p.Style, "min-height: 3.5rem;")
            .Add(p => p.LeadingClass, "leading-extra")
            .Add(p => p.TitleClass, "title-extra")
            .Add(p => p.NavClass, "nav-extra")
            .Add(p => p.ActionsClass, "actions-extra")
            .Add(p => p.MenuButtonClass, "menu-extra")
            .Add(p => p.Leading, builder => builder.AddContent(0, "Brand"))
            .Add(p => p.OnMenuClick, EventCallback.Factory.Create<MouseEventArgs>(this, _ => menuClicked = true))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BarItem>(0);
                builder.AddAttribute(1, nameof(W3BarItem.Href), "components/app-bar");
                builder.AddAttribute(2, nameof(W3BarItem.ChildContent), (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, "App Bar")));
                builder.CloseComponent();
            })
            .Add(p => p.Actions, builder =>
            {
                builder.OpenComponent<W3BarItem>(0);
                builder.AddAttribute(1, nameof(W3BarItem.ChildContent), (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, "Search")));
                builder.CloseComponent();
            }));

        var header = cut.Find("header");
        var menuButton = cut.Find("button");

        Assert.Contains("w3-bar", header.GetAttribute("class"));
        Assert.Contains("w3-app-bar", header.GetAttribute("class"));
        Assert.Contains("w3-app-bar-has-leading", header.GetAttribute("class"));
        Assert.Contains("w3-app-bar-has-title", header.GetAttribute("class"));
        Assert.Contains("w3-app-bar-has-nav", header.GetAttribute("class"));
        Assert.Contains("w3-app-bar-has-actions", header.GetAttribute("class"));
        Assert.Contains("w3-teal", header.GetAttribute("class"));
        Assert.Contains("w3-text-white", header.GetAttribute("class"));
        Assert.Contains("w3-card-4", header.GetAttribute("class"));
        Assert.Contains("app-bar-extra", header.GetAttribute("class"));
        Assert.Equal("min-height: 3.5rem;", header.GetAttribute("style"));
        Assert.Contains("Brand", cut.Markup);
        Assert.Contains("leading-extra", cut.Find(".w3-app-bar-leading").GetAttribute("class"));
        Assert.Contains("title-extra", cut.Find(".w3-app-bar-title").GetAttribute("class"));
        Assert.Contains("Studio", cut.Markup);
        Assert.Contains("Design workspace", cut.Markup);
        Assert.Contains("w3-app-bar-nav", cut.Find("nav").GetAttribute("class"));
        Assert.Contains("nav-extra", cut.Find("nav").GetAttribute("class"));
        Assert.Equal("Workspace sections", cut.Find("nav").GetAttribute("aria-label"));
        Assert.Contains("actions-extra", cut.Find(".w3-app-bar-actions").GetAttribute("class"));
        Assert.Contains("menu-extra", menuButton.GetAttribute("class"));
        Assert.Equal("Open workspace navigation", menuButton.GetAttribute("aria-label"));
        Assert.Contains("Open", menuButton.TextContent);
        Assert.Equal("false", menuButton.GetAttribute("aria-expanded"));
        Assert.Equal("sidebar", menuButton.GetAttribute("aria-controls"));

        menuButton.Click();

        Assert.True(menuClicked);
    }

    [Fact]
    public void AppBarSupportsPlacementAndLinkedTitle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3AppBar>(parameters => parameters
            .Add(p => p.Title, "Field App")
            .Add(p => p.TitleHref, "components/app-bar")
            .Add(p => p.TitleTarget, "_blank")
            .Add(p => p.Position, W3AppBarPosition.Sticky)
            .Add(p => p.Mobile, true)
            .Add(p => p.Dense, true)
            .Add(p => p.Prominent, true)
            .Add(p => p.Border, true)
            .Add(p => p.Card, true)
            .Add(p => p.CardDepth, W3CardDepth.Two)
            .Add(p => p.Round, W3Round.Medium));

        var header = cut.Find("header");
        var title = cut.Find("a.w3-app-bar-title-link");

        Assert.Contains("w3-app-bar-sticky", header.GetAttribute("class"));
        Assert.Contains("w3-app-bar-mobile", header.GetAttribute("class"));
        Assert.Contains("w3-app-bar-dense", header.GetAttribute("class"));
        Assert.Contains("w3-app-bar-prominent", header.GetAttribute("class"));
        Assert.Contains("w3-border", header.GetAttribute("class"));
        Assert.Contains("w3-card-2", header.GetAttribute("class"));
        Assert.Contains("w3-round", header.GetAttribute("class"));
        Assert.Equal("components/app-bar", title.GetAttribute("href"));
        Assert.Equal("_blank", title.GetAttribute("target"));
    }

    [Fact]
    public void DisabledBarLinkRemovesHref()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3BarItem>(parameters => parameters
            .Add(p => p.Href, "components")
            .Add(p => p.Disabled, true)
            .Add(p => p.ChildContent, "Disabled"));

        var item = cut.Find("a");

        Assert.Contains("w3-disabled", item.GetAttribute("class"));
        Assert.Equal("true", item.GetAttribute("aria-disabled"));
        Assert.Null(item.GetAttribute("href"));
    }

    [Fact]
    public void BarItemInvokesButtonClick()
    {
        using var context = new BunitContext();
        var clicked = false;
        var cut = context.Render<W3BarItem>(parameters => parameters
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .Add(p => p.ChildContent, "Menu"));

        cut.Find("button").Click();

        Assert.True(clicked);
    }

    [Fact]
    public void BarItemCanRenderStaticLabel()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3BarItem>(parameters => parameters
            .Add(p => p.Button, false)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.ChildContent, "Studio"));

        var item = cut.Find("span");

        Assert.Contains("w3-bar-item", item.GetAttribute("class"));
        Assert.Contains("w3-text-white", item.GetAttribute("class"));
        Assert.DoesNotContain("w3-button", item.GetAttribute("class"));
        Assert.Equal("Studio", item.TextContent);
        Assert.Empty(cut.FindAll("button"));
    }

    [Fact]
    public void ResponsiveWrapsContent()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Responsive>(parameters => parameters
            .Add(p => p.Class, "wide-scroll")
            .Add(p => p.Style, "max-width: 32rem;")
            .Add(p => p.ChildContent, "<table><tbody><tr><td>Wide</td></tr></tbody></table>"));

        var wrapper = cut.Find("div");

        Assert.Contains("w3-responsive", wrapper.GetAttribute("class"));
        Assert.Contains("wide-scroll", wrapper.GetAttribute("class"));
        Assert.Equal("max-width: 32rem;", wrapper.GetAttribute("style"));
        Assert.Contains("Wide", wrapper.TextContent);
    }
}
