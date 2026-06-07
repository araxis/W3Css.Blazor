using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3NavigationTests
{
    [Fact]
    public void TabsRenderActivePanelAndSwitchOnClick()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var active = "overview";
        var cut = context.Render<W3Tabs>(parameters => parameters
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.Border, true)
            .Add(p => p.PanelBorder, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.ActiveColor, W3Color.Teal)
            .Add(p => p.ActiveTextColor, W3Color.White)
            .Add(p => p.PanelColor, W3Color.PaleBlue)
            .Add(p => p.PanelTextColor, W3Color.Black)
            .Add(p => p.Class, "tabs-shell")
            .Add(p => p.Style, "max-width: 32rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3TabPanel>(0);
                builder.AddAttribute(1, nameof(W3TabPanel.Value), "overview");
                builder.AddAttribute(2, nameof(W3TabPanel.Title), "Overview");
                builder.AddAttribute(3, nameof(W3TabPanel.ChildContent), (RenderFragment)(content => content.AddContent(0, "Overview content")));
                builder.CloseComponent();
                builder.OpenComponent<W3TabPanel>(4);
                builder.AddAttribute(5, nameof(W3TabPanel.Value), "details");
                builder.AddAttribute(6, nameof(W3TabPanel.Title), "Details");
                builder.AddAttribute(7, nameof(W3TabPanel.ChildContent), (RenderFragment)(content => content.AddContent(0, "Details content")));
                builder.CloseComponent();
            }));

        cut.WaitForAssertion(() => Assert.Contains("Overview content", cut.Markup));

        var root = cut.Find("section");
        var tablist = cut.Find("[role='tablist']");
        var activeButton = cut.FindAll("button")[0];
        var panel = cut.Find("[role='tabpanel']");

        Assert.Contains("tabs-shell", root.GetAttribute("class"));
        Assert.Equal("max-width: 32rem;", root.GetAttribute("style"));
        Assert.Contains("w3-border", tablist.GetAttribute("class"));
        Assert.Contains("w3-white", tablist.GetAttribute("class"));
        Assert.Contains("w3-text-black", tablist.GetAttribute("class"));
        Assert.Contains("w3-teal", activeButton.GetAttribute("class"));
        Assert.Contains("w3-text-white", activeButton.GetAttribute("class"));
        Assert.Contains("w3-border", panel.GetAttribute("class"));
        Assert.Contains("w3-pale-blue", panel.GetAttribute("class"));
        Assert.Contains("w3-text-black", panel.GetAttribute("class"));

        cut.FindAll("button")[1].Click();

        cut.WaitForAssertion(() => Assert.Equal("details", active));
        Assert.Contains("Details content", cut.Markup);
    }

    [Fact]
    public void TabsSupportKeyboardNavigationAcrossEnabledTabs()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var active = "overview";
        var cut = context.Render<W3Tabs>(parameters => parameters
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3TabPanel>(0);
                builder.AddAttribute(1, nameof(W3TabPanel.Value), "overview");
                builder.AddAttribute(2, nameof(W3TabPanel.Title), "Overview");
                builder.AddAttribute(3, nameof(W3TabPanel.ChildContent), (RenderFragment)(content => content.AddContent(0, "Overview content")));
                builder.CloseComponent();
                builder.OpenComponent<W3TabPanel>(4);
                builder.AddAttribute(5, nameof(W3TabPanel.Value), "disabled");
                builder.AddAttribute(6, nameof(W3TabPanel.Title), "Disabled");
                builder.AddAttribute(7, nameof(W3TabPanel.Disabled), true);
                builder.AddAttribute(8, nameof(W3TabPanel.ChildContent), (RenderFragment)(content => content.AddContent(0, "Disabled content")));
                builder.CloseComponent();
                builder.OpenComponent<W3TabPanel>(9);
                builder.AddAttribute(10, nameof(W3TabPanel.Value), "details");
                builder.AddAttribute(11, nameof(W3TabPanel.Title), "Details");
                builder.AddAttribute(12, nameof(W3TabPanel.ChildContent), (RenderFragment)(content => content.AddContent(0, "Details content")));
                builder.CloseComponent();
            }));

        var overviewTab = cut.Find("button[role='tab']");
        overviewTab.KeyDown(new KeyboardEventArgs { Key = "ArrowRight" });

        Assert.Equal("details", active);
        Assert.Contains("Details content", cut.Markup);

        overviewTab.KeyDown(new KeyboardEventArgs { Key = "ArrowLeft" });

        Assert.Equal("overview", active);
        Assert.Contains("Overview content", cut.Markup);

        overviewTab.KeyDown(new KeyboardEventArgs { Key = "End" });
        Assert.Equal("details", active);

        overviewTab.KeyDown(new KeyboardEventArgs { Key = "Home" });
        Assert.Equal("overview", active);

        var tabButtons = cut.FindAll("button[role='tab']");
        Assert.Equal("0", tabButtons[0].GetAttribute("tabindex"));
        Assert.Equal("-1", tabButtons[2].GetAttribute("tabindex"));
    }

    [Fact]
    public void TabsDeleteKeyClosesCloseableFocusedTab()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        string? closed = null;
        var cut = context.Render<W3Tabs>(parameters => parameters
            .Add(p => p.ActiveValue, "overview")
            .Add(p => p.ShowCloseButtons, true)
            .Add(p => p.Closeable, true)
            .Add(p => p.OnCloseTab, EventCallback.Factory.Create<string>(this, value => closed = value))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3TabPanel>(0);
                builder.AddAttribute(1, nameof(W3TabPanel.Value), "overview");
                builder.AddAttribute(2, nameof(W3TabPanel.Title), "Overview");
                builder.AddAttribute(3, nameof(W3TabPanel.ChildContent), (RenderFragment)(content => content.AddContent(0, "Overview content")));
                builder.CloseComponent();
                builder.OpenComponent<W3TabPanel>(4);
                builder.AddAttribute(5, nameof(W3TabPanel.Value), "details");
                builder.AddAttribute(6, nameof(W3TabPanel.Title), "Details");
                builder.AddAttribute(7, nameof(W3TabPanel.ChildContent), (RenderFragment)(content => content.AddContent(0, "Details content")));
                builder.CloseComponent();
            }));

        cut.Find("button[role='tab']").KeyDown(new KeyboardEventArgs { Key = "Delete" });

        Assert.Equal("overview", closed);
    }

    [Fact]
    public void AccordionRendersWrapperClasses()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = context.Render<W3Accordion>(parameters => parameters
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "accordion-shell")
            .Add(p => p.Style, "max-width: 30rem;")
            .Add(p => p.ChildContent, "Accordion content"));

        var root = cut.Find("div");

        Assert.Contains("w3-section", root.GetAttribute("class"));
        Assert.Contains("w3-white", root.GetAttribute("class"));
        Assert.Contains("w3-text-black", root.GetAttribute("class"));
        Assert.Contains("accordion-shell", root.GetAttribute("class"));
        Assert.Equal("max-width: 30rem;", root.GetAttribute("style"));
        Assert.Contains("Accordion content", root.TextContent);
    }

    [Fact]
    public void AccordionItemTogglesExpandedState()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var expanded = false;
        var cut = context.Render<W3AccordionItem>(parameters => parameters
            .Add(p => p.Title, "Section")
            .Add(p => p.Expanded, expanded)
            .Add(p => p.ExpandedChanged, EventCallback.Factory.Create<bool>(this, value => expanded = value))
            .Add(p => p.ButtonColor, W3Color.Teal)
            .Add(p => p.ButtonTextColor, W3Color.White)
            .Add(p => p.ContentColor, W3Color.White)
            .Add(p => p.ContentTextColor, W3Color.Black)
            .Add(p => p.Class, "accordion-item")
            .Add(p => p.Style, "margin-bottom: 1rem;")
            .Add(p => p.ChildContent, "Hidden content"));

        var root = cut.Find("div");
        var button = cut.Find("button");

        Assert.Contains("accordion-item", root.GetAttribute("class"));
        Assert.Equal("margin-bottom: 1rem;", root.GetAttribute("style"));
        Assert.Contains("w3-teal", button.GetAttribute("class"));
        Assert.Contains("w3-text-white", button.GetAttribute("class"));
        Assert.DoesNotContain("Hidden content", cut.Markup);

        button.Click();

        Assert.True(expanded);
        Assert.Contains("Hidden content", cut.Markup);
        Assert.Equal("true", cut.Find("button").GetAttribute("aria-expanded"));
        Assert.Contains("w3-white", cut.Find(".w3-container").GetAttribute("class"));
        Assert.Contains("w3-text-black", cut.Find(".w3-container").GetAttribute("class"));
    }

    [Fact]
    public void DropdownTogglesContentVisibility()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var open = false;
        var cut = context.Render<W3Dropdown>(parameters => parameters
            .Add(p => p.Label, "Menu")
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.ButtonColor, W3Color.Teal)
            .Add(p => p.ButtonTextColor, W3Color.White)
            .Add(p => p.ContentColor, W3Color.White)
            .Add(p => p.ContentTextColor, W3Color.Black)
            .Add(p => p.Class, "dropdown-shell")
            .Add(p => p.Style, "min-width: 12rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BarItem>(0);
                builder.AddAttribute(1, nameof(W3BarItem.Href), "components");
                builder.AddAttribute(2, nameof(W3BarItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Components")));
                builder.CloseComponent();
            }));

        var root = cut.Find(".w3-dropdown-click");
        var button = cut.Find("button");
        var content = cut.Find(".w3-dropdown-content");

        Assert.Contains("dropdown-shell", root.GetAttribute("class"));
        Assert.Equal("min-width: 12rem;", root.GetAttribute("style"));
        Assert.Contains("w3-teal", button.GetAttribute("class"));
        Assert.Contains("w3-text-white", button.GetAttribute("class"));
        Assert.Equal("true", button.GetAttribute("aria-haspopup"));
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
        Assert.Contains("w3-white", content.GetAttribute("class"));
        Assert.Contains("w3-text-black", content.GetAttribute("class"));
        Assert.Equal(button.GetAttribute("aria-controls"), content.GetAttribute("id"));
        Assert.DoesNotContain("w3-show", content.GetAttribute("class"));

        button.Click();

        Assert.True(open);
        Assert.Contains("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));
    }

    [Fact]
    public void DropdownClosesFromOutsideClickLayer()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var open = true;
        var cut = context.Render<W3Dropdown>(parameters => parameters
            .Add(p => p.Label, "Menu")
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.ChildContent, "Menu content"));

        Assert.Contains("w3-dropdown-open", cut.Find(".w3-dropdown-click").GetAttribute("class"));
        Assert.Contains("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));

        cut.Find(".w3-dropdown-close-layer").Click();

        Assert.False(open);
        Assert.DoesNotContain("w3-dropdown-open", cut.Find(".w3-dropdown-click").GetAttribute("class"));
        Assert.DoesNotContain("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));
    }

    [Fact]
    public void DropdownSupportsKeyboardOpenAndEscapeClose()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var open = false;
        var cut = context.Render<W3Dropdown>(parameters => parameters
            .Add(p => p.Label, "Menu")
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.ChildContent, "Menu content"));

        cut.Find("button").KeyDown(new KeyboardEventArgs { Key = "ArrowDown" });

        cut.WaitForAssertion(() =>
        {
            Assert.True(open);
            Assert.Contains("w3-dropdown-open", cut.Find(".w3-dropdown-click").GetAttribute("class"));
            Assert.Contains("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));
            Assert.Equal("true", cut.Find("button").GetAttribute("aria-expanded"));
        });

        cut.Find(".w3-dropdown-content").KeyDown(new KeyboardEventArgs { Key = "Escape" });

        cut.WaitForAssertion(() =>
        {
            Assert.False(open);
            Assert.DoesNotContain("w3-dropdown-open", cut.Find(".w3-dropdown-click").GetAttribute("class"));
            Assert.DoesNotContain("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));
            Assert.Equal("false", cut.Find("button").GetAttribute("aria-expanded"));
        });
    }

    [Fact]
    public void MenuRendersCommandItemsAndClosesOnSelection()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var open = false;
        var clicked = false;

        var cut = context.Render<W3Menu>(parameters => parameters
            .Add(p => p.Label, "Actions")
            .Add(p => p.MenuLabel, "Asset actions")
            .Add(p => p.IconClass, "fa fa-ellipsis-v")
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.TriggerColor, W3Color.Teal)
            .Add(p => p.TriggerTextColor, W3Color.White)
            .Add(p => p.TriggerBorder, true)
            .Add(p => p.Placement, W3MenuPlacement.BottomEnd)
            .Add(p => p.MinWidth, 220)
            .Add(p => p.PanelClass, "menu-panel-extra")
            .Add(p => p.Class, "menu-shell")
            .Add(p => p.Style, "margin-left: 1rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3MenuItem>(0);
                builder.AddAttribute(1, nameof(W3MenuItem.IconClass), "fa fa-pencil");
                builder.AddAttribute(2, nameof(W3MenuItem.Text), "Rename");
                builder.AddAttribute(3, nameof(W3MenuItem.Description), "Change title");
                builder.AddAttribute(4, nameof(W3MenuItem.Shortcut), "R");
                builder.AddAttribute(5, nameof(W3MenuItem.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true));
                builder.CloseComponent();

                builder.OpenComponent<W3MenuDivider>(6);
                builder.CloseComponent();

                builder.OpenComponent<W3MenuItem>(7);
                builder.AddAttribute(8, nameof(W3MenuItem.Text), "Delete");
                builder.AddAttribute(9, nameof(W3MenuItem.Destructive), true);
                builder.CloseComponent();

                builder.OpenComponent<W3MenuItem>(10);
                builder.AddAttribute(11, nameof(W3MenuItem.Text), "Locked");
                builder.AddAttribute(12, nameof(W3MenuItem.Disabled), true);
                builder.CloseComponent();
            }));

        var root = cut.Find(".w3-menu");
        var trigger = cut.Find(".w3-menu-trigger");
        var panel = cut.Find("[role='menu']");

        Assert.Contains("menu-shell", root.GetAttribute("class"));
        Assert.Equal("--w3-menu-min-width:220px;margin-left: 1rem", root.GetAttribute("style"));
        Assert.Equal("menu", trigger.GetAttribute("aria-haspopup"));
        Assert.Equal("Actions", trigger.GetAttribute("aria-label"));
        Assert.Equal("false", trigger.GetAttribute("aria-expanded"));
        Assert.Contains("w3-teal", trigger.GetAttribute("class"));
        Assert.Contains("w3-text-white", trigger.GetAttribute("class"));
        Assert.Contains("w3-border", trigger.GetAttribute("class"));
        Assert.Contains("fa-ellipsis-v", cut.Find(".w3-menu-trigger-icon").GetAttribute("class"));
        Assert.Equal(trigger.GetAttribute("aria-controls"), panel.GetAttribute("id"));
        Assert.Equal("Asset actions", panel.GetAttribute("aria-label"));
        Assert.Contains("w3-menu-bottom-end", panel.GetAttribute("class"));
        Assert.DoesNotContain("w3-show", panel.GetAttribute("class"));

        trigger.Click();

        cut.WaitForAssertion(() =>
        {
            Assert.True(open);
            Assert.Equal("true", cut.Find(".w3-menu-trigger").GetAttribute("aria-expanded"));
            Assert.Contains("w3-show", cut.Find("[role='menu']").GetAttribute("class"));
        });

        panel = cut.Find("[role='menu']");
        var items = cut.FindAll("[role='menuitem']");

        Assert.Contains("w3-show", panel.GetAttribute("class"));
        Assert.Contains("menu-panel-extra", panel.GetAttribute("class"));
        Assert.Equal(3, items.Count);
        Assert.Contains("Change title", cut.Markup);
        Assert.Contains("R", cut.Find("kbd").TextContent);
        Assert.Contains("w3-menu-divider", cut.Find("[role='separator']").GetAttribute("class"));
        Assert.Contains("w3-text-red", items[1].GetAttribute("class"));
        Assert.Equal("true", items[2].GetAttribute("aria-disabled"));

        items[0].Click();

        Assert.True(clicked);
        cut.WaitForAssertion(() =>
        {
            Assert.False(open);
            Assert.DoesNotContain("w3-show", cut.Find("[role='menu']").GetAttribute("class"));
        });
    }

    [Fact]
    public void MenuItemSupportsHrefDenseEndContentAndCloseOptOut()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var open = false;
        var clicked = false;

        var cut = context.Render<W3Menu>(parameters => parameters
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3MenuItem>(0);
                builder.AddAttribute(1, nameof(W3MenuItem.Href), "components/versions");
                builder.AddAttribute(2, nameof(W3MenuItem.Target), "_blank");
                builder.AddAttribute(3, nameof(W3MenuItem.Text), "Version history");
                builder.AddAttribute(4, nameof(W3MenuItem.Dense), true);
                builder.AddAttribute(5, nameof(W3MenuItem.CloseOnClick), false);
                builder.AddAttribute(6, nameof(W3MenuItem.Color), W3Color.White);
                builder.AddAttribute(7, nameof(W3MenuItem.TextColor), W3Color.Black);
                builder.AddAttribute(8, nameof(W3MenuItem.HoverTextColor), W3Color.Teal);
                builder.AddAttribute(9, nameof(W3MenuItem.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true));
                builder.AddAttribute(10, nameof(W3MenuItem.EndContent), (RenderFragment)(end => end.AddContent(0, "New")));
                builder.CloseComponent();
            }));

        cut.Find(".w3-menu-trigger").Click();

        cut.WaitForAssertion(() => Assert.True(open));

        var item = cut.Find("[role='menuitem']");

        Assert.Equal("A", item.TagName);
        Assert.Equal("components/versions", item.GetAttribute("href"));
        Assert.Equal("_blank", item.GetAttribute("target"));
        Assert.Contains("w3-menu-item-dense", item.GetAttribute("class"));
        Assert.Contains("w3-white", item.GetAttribute("class"));
        Assert.Contains("w3-text-black", item.GetAttribute("class"));
        Assert.Contains("w3-hover-light-grey", item.GetAttribute("class"));
        Assert.Contains("w3-hover-text-teal", item.GetAttribute("class"));
        Assert.Equal("New", cut.Find(".w3-menu-item-end").TextContent);

        item.Click();

        Assert.True(clicked);
        Assert.True(open);
        Assert.Contains("w3-show", cut.Find("[role='menu']").GetAttribute("class"));
    }

    [Fact]
    public void MenuSupportsKeyboardNavigationAcrossSelectableItems()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var open = false;
        var cut = context.Render<W3Menu>(parameters => parameters
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3MenuItem>(0);
                builder.AddAttribute(1, nameof(W3MenuItem.Text), "Rename");
                builder.CloseComponent();

                builder.OpenComponent<W3MenuItem>(2);
                builder.AddAttribute(3, nameof(W3MenuItem.Text), "Duplicate");
                builder.CloseComponent();

                builder.OpenComponent<W3MenuDivider>(4);
                builder.CloseComponent();

                builder.OpenComponent<W3MenuItem>(5);
                builder.AddAttribute(6, nameof(W3MenuItem.Text), "Delete");
                builder.AddAttribute(7, nameof(W3MenuItem.Destructive), true);
                builder.CloseComponent();

                builder.OpenComponent<W3MenuItem>(8);
                builder.AddAttribute(9, nameof(W3MenuItem.Text), "Locked");
                builder.AddAttribute(10, nameof(W3MenuItem.Disabled), true);
                builder.CloseComponent();
            }));

        cut.Find(".w3-menu-trigger").KeyDown(new KeyboardEventArgs { Key = "ArrowDown" });

        cut.WaitForAssertion(() =>
        {
            Assert.True(open);
            var items = cut.FindAll("[role='menuitem']");
            Assert.Equal("0", items[0].GetAttribute("tabindex"));
            Assert.Equal("-1", items[1].GetAttribute("tabindex"));
            Assert.Equal("-1", items[2].GetAttribute("tabindex"));
            Assert.Null(items[3].GetAttribute("tabindex"));
        });

        cut.FindAll("[role='menuitem']")[0].KeyDown(new KeyboardEventArgs { Key = "ArrowDown" });

        cut.WaitForAssertion(() =>
        {
            var items = cut.FindAll("[role='menuitem']");
            Assert.Equal("-1", items[0].GetAttribute("tabindex"));
            Assert.Equal("0", items[1].GetAttribute("tabindex"));
        });

        cut.FindAll("[role='menuitem']")[1].KeyDown(new KeyboardEventArgs { Key = "End" });

        cut.WaitForAssertion(() =>
        {
            var items = cut.FindAll("[role='menuitem']");
            Assert.Equal("0", items[2].GetAttribute("tabindex"));
            Assert.Null(items[3].GetAttribute("tabindex"));
        });

        cut.FindAll("[role='menuitem']")[2].KeyDown(new KeyboardEventArgs { Key = "ArrowDown" });

        cut.WaitForAssertion(() =>
        {
            var items = cut.FindAll("[role='menuitem']");
            Assert.Equal("0", items[0].GetAttribute("tabindex"));
            Assert.Equal("-1", items[2].GetAttribute("tabindex"));
        });

        cut.FindAll("[role='menuitem']")[0].KeyDown(new KeyboardEventArgs { Key = "Escape" });

        cut.WaitForAssertion(() =>
        {
            Assert.False(open);
            Assert.DoesNotContain("w3-show", cut.Find("[role='menu']").GetAttribute("class"));
        });
    }

    [Fact]
    public void MenuSupportsIconOnlyTriggerWithAccessibleLabel()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3Menu>(parameters => parameters
            .Add(p => p.Label, "Landing page actions")
            .Add(p => p.IconClass, "fa fa-ellipsis-v")
            .Add(p => p.IconOnly, true)
            .Add(p => p.FixedPosition, true)
            .Add(p => p.ShowCaret, false)
            .Add(p => p.TriggerBorder, true)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3MenuItem>(0);
                builder.AddAttribute(1, nameof(W3MenuItem.Text), "Open");
                builder.CloseComponent();
            }));

        var trigger = cut.Find(".w3-menu-trigger");

        Assert.Equal("Landing page actions", trigger.GetAttribute("aria-label"));
        Assert.Contains("w3-menu-trigger-icon-only", trigger.GetAttribute("class"));
        Assert.Contains("w3-menu-panel-fixed", cut.Find("[role='menu']").GetAttribute("class"));
        Assert.Contains("fa-ellipsis-v", cut.Find(".w3-menu-trigger-icon").GetAttribute("class"));
        Assert.DoesNotContain("Landing page actions", trigger.TextContent);
        Assert.DoesNotContain("w3-menu-caret", cut.Markup);
    }

    [Fact]
    public void NavbarRendersActiveMobileItemsAndChangesActiveValue()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var active = "home";
        var cut = context.Render<W3Navbar>(parameters => parameters
            .Add(p => p.Label, "Primary")
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.Mobile, true)
            .Add(p => p.Border, true)
            .Add(p => p.Card, true)
            .Add(p => p.CardDepth, W3CardDepth.Two)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.Black)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.ActiveColor, W3Color.Green)
            .Add(p => p.ActiveTextColor, W3Color.White)
            .Add(p => p.Class, "navbar-shell")
            .Add(p => p.Style, "max-width: 40rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3NavbarItem>(0);
                builder.AddAttribute(1, nameof(W3NavbarItem.Value), "home");
                builder.AddAttribute(2, nameof(W3NavbarItem.Href), "home");
                builder.AddAttribute(3, nameof(W3NavbarItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Home")));
                builder.CloseComponent();

                builder.OpenComponent<W3NavbarItem>(4);
                builder.AddAttribute(5, nameof(W3NavbarItem.Value), "docs");
                builder.AddAttribute(6, nameof(W3NavbarItem.Href), "docs");
                builder.AddAttribute(7, nameof(W3NavbarItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Docs")));
                builder.CloseComponent();
            }));

        var nav = cut.Find("nav");
        var links = cut.FindAll("a");

        Assert.Equal("Primary", nav.GetAttribute("aria-label"));
        Assert.Contains("w3-bar", nav.GetAttribute("class"));
        Assert.Contains("w3-border", nav.GetAttribute("class"));
        Assert.Contains("w3-card-2", nav.GetAttribute("class"));
        Assert.Contains("w3-round", nav.GetAttribute("class"));
        Assert.Contains("w3-black", nav.GetAttribute("class"));
        Assert.Contains("w3-text-white", nav.GetAttribute("class"));
        Assert.Contains("navbar-shell", nav.GetAttribute("class"));
        Assert.Equal("max-width: 40rem;", nav.GetAttribute("style"));
        Assert.Contains("w3-mobile", links[0].GetAttribute("class"));
        Assert.Contains("w3-green", links[0].GetAttribute("class"));
        Assert.Equal("page", links[0].GetAttribute("aria-current"));

        links[1].Click();

        cut.WaitForAssertion(() => Assert.Equal("docs", active));
        var updatedLinks = cut.FindAll("a");
        Assert.Null(updatedLinks[0].GetAttribute("aria-current"));
        Assert.Equal("page", updatedLinks[1].GetAttribute("aria-current"));
    }

    [Fact]
    public void NavbarItemCanRenderDisabledButton()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var clicked = false;
        var cut = context.Render<W3NavbarItem>(parameters => parameters
            .Add(p => p.Value, "save")
            .Add(p => p.Disabled, true)
            .Add(p => p.Type, "submit")
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "save-item")
            .Add(p => p.Style, "min-width: 6rem;")
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .Add(p => p.ChildContent, "Save"));

        var button = cut.Find("button");

        Assert.True(button.HasAttribute("disabled"));
        Assert.Equal("submit", button.GetAttribute("type"));
        Assert.Contains("w3-disabled", button.GetAttribute("class"));
        Assert.Contains("w3-white", button.GetAttribute("class"));
        Assert.Contains("w3-text-black", button.GetAttribute("class"));
        Assert.Contains("save-item", button.GetAttribute("class"));
        Assert.Equal("min-width: 6rem;", button.GetAttribute("style"));

        button.Click();

        Assert.False(clicked);
    }

    [Fact]
    public void NavbarComposesDropdownWithMobileItems()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var open = false;
        var cut = context.Render<W3Navbar>(parameters => parameters
            .Add(p => p.Mobile, true)
            .Add(p => p.ActiveValue, "reports")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3NavbarItem>(0);
                builder.AddAttribute(1, nameof(W3NavbarItem.Value), "home");
                builder.AddAttribute(2, nameof(W3NavbarItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Home")));
                builder.CloseComponent();

                builder.OpenComponent<W3Dropdown>(3);
                builder.AddAttribute(4, nameof(W3Dropdown.Label), "More");
                builder.AddAttribute(5, nameof(W3Dropdown.Mobile), true);
                builder.AddAttribute(6, nameof(W3Dropdown.Open), open);
                builder.AddAttribute(7, nameof(W3Dropdown.OpenChanged), EventCallback.Factory.Create<bool>(this, value => open = value));
                builder.AddAttribute(8, nameof(W3Dropdown.ChildContent), (RenderFragment)(content =>
                {
                    content.OpenComponent<W3NavbarItem>(0);
                    content.AddAttribute(1, nameof(W3NavbarItem.Value), "reports");
                    content.AddAttribute(2, nameof(W3NavbarItem.Href), "reports");
                    content.AddAttribute(3, nameof(W3NavbarItem.ChildContent), (RenderFragment)(item => item.AddContent(0, "Reports")));
                    content.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        Assert.Contains("w3-mobile", cut.Find(".w3-dropdown-click").GetAttribute("class"));
        Assert.Contains("w3-mobile", cut.Find(".w3-dropdown-content a").GetAttribute("class"));
        Assert.Contains("w3-primary", cut.Find(".w3-dropdown-content a").GetAttribute("class"));

        cut.Find(".w3-dropdown-click > button").Click();

        Assert.True(open);
        Assert.Contains("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));
    }

    [Fact]
    public void NavMenuRendersGroupedLinksHeaderAndActiveItem()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = context.Render<W3NavMenu>(parameters => parameters
            .Add(p => p.Label, "Workspace")
            .Add(p => p.Border, true)
            .Add(p => p.Card, true)
            .Add(p => p.CardDepth, W3CardDepth.Two)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.ActiveColor, W3Color.Teal)
            .Add(p => p.ActiveTextColor, W3Color.White)
            .Add(p => p.HoverTextColor, W3Color.Black)
            .Add(p => p.Class, "workspace-menu")
            .Add(p => p.Style, "max-width: 18rem;")
            .Add(p => p.Header, (RenderFragment)(header => header.AddContent(0, "Studio")))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3NavMenuGroup>(0);
                builder.AddAttribute(1, nameof(W3NavMenuGroup.Title), "Build");
                builder.AddAttribute(2, nameof(W3NavMenuGroup.ChildContent), (RenderFragment)(group =>
                {
                    group.OpenComponent<W3NavMenuItem>(0);
                    group.AddAttribute(1, nameof(W3NavMenuItem.Href), "components");
                    group.AddAttribute(2, nameof(W3NavMenuItem.IconClass), "fa fa-th-large");
                    group.AddAttribute(3, nameof(W3NavMenuItem.Text), "Components");
                    group.CloseComponent();

                    group.OpenComponent<W3NavMenuItem>(4);
                    group.AddAttribute(5, nameof(W3NavMenuItem.Href), "components/data-table");
                    group.AddAttribute(6, nameof(W3NavMenuItem.Badge), "3");
                    group.AddAttribute(7, nameof(W3NavMenuItem.Active), true);
                    group.AddAttribute(8, nameof(W3NavMenuItem.ChildContent), (RenderFragment)(item => item.AddContent(0, "Data Tools")));
                    group.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        var nav = cut.Find("nav");
        var activeItem = cut.Find(".w3-nav-menu-item-active");

        Assert.Equal("Workspace", nav.GetAttribute("aria-label"));
        Assert.Contains("w3-nav-menu", nav.GetAttribute("class"));
        Assert.Contains("w3-bar-block", nav.GetAttribute("class"));
        Assert.Contains("w3-border", nav.GetAttribute("class"));
        Assert.Contains("w3-card-2", nav.GetAttribute("class"));
        Assert.Contains("w3-round", nav.GetAttribute("class"));
        Assert.Contains("w3-white", nav.GetAttribute("class"));
        Assert.Contains("w3-text-black", nav.GetAttribute("class"));
        Assert.Contains("workspace-menu", nav.GetAttribute("class"));
        Assert.Equal("max-width: 18rem;", nav.GetAttribute("style"));
        Assert.Contains("Studio", cut.Markup);
        Assert.Contains("fa", cut.Find(".w3-nav-menu-item-icon").GetAttribute("class"));
        Assert.Contains("w3-teal", activeItem.GetAttribute("class"));
        Assert.Contains("w3-text-white", activeItem.GetAttribute("class"));
        Assert.Equal("page", activeItem.GetAttribute("aria-current"));
        Assert.Contains("3", cut.Find(".w3-nav-menu-item-badge").TextContent);
    }

    [Fact]
    public void NavMenuGroupTogglesAndButtonItemClicks()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var expanded = false;
        var clicked = false;
        var cut = context.Render<W3NavMenu>(parameters => parameters
            .Add(p => p.Compact, true)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3NavMenuGroup>(0);
                builder.AddAttribute(1, nameof(W3NavMenuGroup.Title), "Commands");
                builder.AddAttribute(2, nameof(W3NavMenuGroup.Expanded), expanded);
                builder.AddAttribute(3, nameof(W3NavMenuGroup.ExpandedChanged), EventCallback.Factory.Create<bool>(this, value => expanded = value));
                builder.AddAttribute(4, nameof(W3NavMenuGroup.ChildContent), (RenderFragment)(group =>
                {
                    group.OpenComponent<W3NavMenuItem>(0);
                    group.AddAttribute(1, nameof(W3NavMenuItem.Text), "Run report");
                    group.AddAttribute(2, nameof(W3NavMenuItem.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true));
                    group.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        Assert.DoesNotContain("Run report", cut.Markup);

        cut.Find(".w3-nav-menu-group-button").Click();

        Assert.True(expanded);
        Assert.Contains("Run report", cut.Markup);

        cut.FindAll("button").Single(button => button.TextContent.Contains("Run report", StringComparison.Ordinal)).Click();

        Assert.True(clicked);
        Assert.Contains("w3-nav-menu-item-compact", cut.Find(".w3-nav-menu-item").GetAttribute("class"));
    }

    [Fact]
    public void NavMenuItemDisabledLinkRendersNonInteractiveText()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = context.Render<W3NavMenuItem>(parameters => parameters
            .Add(p => p.Href, "components")
            .Add(p => p.Disabled, true)
            .Add(p => p.Text, "Archived"));

        var item = cut.Find(".w3-nav-menu-item");

        Assert.Equal("SPAN", item.TagName);
        Assert.Equal("true", item.GetAttribute("aria-disabled"));
        Assert.Contains("w3-disabled", item.GetAttribute("class"));
        Assert.Empty(cut.FindAll("a"));
    }

    [Fact]
    public void NavMenuItemEmptyHrefTargetsApplicationRoot()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var navigation = context.Services.GetRequiredService<NavigationManager>();
        var cut = context.Render<W3NavMenuItem>(parameters => parameters
            .Add(p => p.Href, string.Empty)
            .Add(p => p.Match, NavLinkMatch.All)
            .Add(p => p.Text, "Dashboard"));

        var item = cut.Find("a");

        Assert.Equal(navigation.BaseUri, item.GetAttribute("href"));
        Assert.Equal("page", item.GetAttribute("aria-current"));
        Assert.Contains("w3-nav-menu-item-active", item.GetAttribute("class"));
        Assert.Empty(cut.FindAll("button"));

        navigation.NavigateTo("customers");

        cut.WaitForAssertion(() =>
        {
            var updatedItem = cut.Find("a");
            Assert.Null(updatedItem.GetAttribute("aria-current"));
            Assert.DoesNotContain("w3-nav-menu-item-active", updatedItem.GetAttribute("class"));
        });
    }

    [Fact]
    public void BottomNavigationRendersActiveItemAndChangesValue()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var value = "home";
        var clicked = false;
        var cut = context.Render<W3BottomNavigation>(parameters => parameters
            .Add(p => p.Label, "Mobile destinations")
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next ?? string.Empty))
            .Add(p => p.Contained, true)
            .Add(p => p.Dense, true)
            .Add(p => p.Border, true)
            .Add(p => p.Card, true)
            .Add(p => p.CardDepth, W3CardDepth.Two)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.ActiveColor, W3Color.Teal)
            .Add(p => p.ActiveTextColor, W3Color.White)
            .Add(p => p.HoverTextColor, W3Color.Teal)
            .Add(p => p.Class, "mobile-nav")
            .Add(p => p.Style, "max-width: 24rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BottomNavigationItem>(0);
                builder.AddAttribute(1, nameof(W3BottomNavigationItem.Value), "home");
                builder.AddAttribute(2, nameof(W3BottomNavigationItem.Text), "Home");
                builder.AddAttribute(3, nameof(W3BottomNavigationItem.IconClass), "fa fa-home");
                builder.CloseComponent();

                builder.OpenComponent<W3BottomNavigationItem>(4);
                builder.AddAttribute(5, nameof(W3BottomNavigationItem.Value), "search");
                builder.AddAttribute(6, nameof(W3BottomNavigationItem.Text), "Search");
                builder.AddAttribute(7, nameof(W3BottomNavigationItem.IconClass), "fa fa-search");
                builder.AddAttribute(8, nameof(W3BottomNavigationItem.Badge), "2");
                builder.AddAttribute(9, nameof(W3BottomNavigationItem.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true));
                builder.CloseComponent();
            }));

        var nav = cut.Find("nav");
        var buttons = cut.FindAll("button");

        Assert.Equal("Mobile destinations", nav.GetAttribute("aria-label"));
        Assert.Contains("w3-bottom-navigation", nav.GetAttribute("class"));
        Assert.Contains("w3-bottom-navigation-contained", nav.GetAttribute("class"));
        Assert.Contains("w3-bottom-navigation-equal", nav.GetAttribute("class"));
        Assert.Contains("w3-bottom-navigation-dense", nav.GetAttribute("class"));
        Assert.Contains("w3-border", nav.GetAttribute("class"));
        Assert.Contains("w3-card-2", nav.GetAttribute("class"));
        Assert.Contains("w3-round", nav.GetAttribute("class"));
        Assert.Contains("w3-white", nav.GetAttribute("class"));
        Assert.Contains("w3-text-black", nav.GetAttribute("class"));
        Assert.Contains("mobile-nav", nav.GetAttribute("class"));
        Assert.Equal("max-width: 24rem;", nav.GetAttribute("style"));
        Assert.Contains("w3-bottom-navigation-item-active", buttons[0].GetAttribute("class"));
        Assert.Contains("w3-teal", buttons[0].GetAttribute("class"));
        Assert.Equal("true", buttons[0].GetAttribute("aria-pressed"));
        Assert.Contains("fa-home", cut.Find(".w3-bottom-navigation-item-icon").GetAttribute("class"));
        Assert.Contains("2", cut.Find(".w3-bottom-navigation-item-badge").TextContent);

        buttons[1].Click();

        Assert.True(clicked);
        Assert.Equal("search", value);
        Assert.Contains("w3-bottom-navigation-item-active", cut.FindAll("button")[1].GetAttribute("class"));
        Assert.Equal("true", cut.FindAll("button")[1].GetAttribute("aria-pressed"));
    }

    [Fact]
    public void BottomNavigationSupportsLinksHiddenLabelsAndDisabledItems()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = context.Render<W3BottomNavigation>(parameters => parameters
            .Add(p => p.ShowLabels, false)
            .Add(p => p.Value, "components")
            .Add(p => p.Color, W3Color.Black)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.ActiveColor, W3Color.Amber)
            .Add(p => p.ActiveTextColor, W3Color.Black)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BottomNavigationItem>(0);
                builder.AddAttribute(1, nameof(W3BottomNavigationItem.Value), "components");
                builder.AddAttribute(2, nameof(W3BottomNavigationItem.Href), "components");
                builder.AddAttribute(3, nameof(W3BottomNavigationItem.Target), "_blank");
                builder.AddAttribute(4, nameof(W3BottomNavigationItem.Text), "Components");
                builder.AddAttribute(5, nameof(W3BottomNavigationItem.IconClass), "fa fa-th-large");
                builder.AddAttribute(6, nameof(W3BottomNavigationItem.Badge), "9");
                builder.CloseComponent();

                builder.OpenComponent<W3BottomNavigationItem>(7);
                builder.AddAttribute(8, nameof(W3BottomNavigationItem.Text), "Locked");
                builder.AddAttribute(9, nameof(W3BottomNavigationItem.IconClass), "fa fa-lock");
                builder.AddAttribute(10, nameof(W3BottomNavigationItem.Disabled), true);
                builder.CloseComponent();
            }));

        var nav = cut.Find("nav");
        var link = cut.Find("a");
        var disabled = cut.Find("span[aria-disabled='true']");

        Assert.Contains("w3-black", nav.GetAttribute("class"));
        Assert.Contains("w3-text-white", nav.GetAttribute("class"));
        Assert.Equal("components", link.GetAttribute("href"));
        Assert.Equal("_blank", link.GetAttribute("target"));
        Assert.Equal("Components", link.GetAttribute("aria-label"));
        Assert.Equal("page", link.GetAttribute("aria-current"));
        Assert.Contains("w3-amber", link.GetAttribute("class"));
        Assert.Contains("w3-text-black", link.GetAttribute("class"));
        Assert.Contains("w3-bottom-navigation-item-label-hidden", cut.Find(".w3-bottom-navigation-item-label").GetAttribute("class"));
        Assert.Contains("9", cut.Find(".w3-bottom-navigation-item-badge").TextContent);
        Assert.Equal("true", disabled.GetAttribute("aria-disabled"));
        Assert.Contains("w3-disabled", disabled.GetAttribute("class"));
        Assert.Contains("w3-bottom-navigation-item-disabled", disabled.GetAttribute("class"));
    }

    [Fact]
    public void BottomNavigationSupportsKeyboardNavigationAcrossSelectableItems()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var value = "home";
        var cut = context.Render<W3BottomNavigation>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next ?? string.Empty))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BottomNavigationItem>(0);
                builder.AddAttribute(1, nameof(W3BottomNavigationItem.Value), "home");
                builder.AddAttribute(2, nameof(W3BottomNavigationItem.Text), "Home");
                builder.CloseComponent();

                builder.OpenComponent<W3BottomNavigationItem>(3);
                builder.AddAttribute(4, nameof(W3BottomNavigationItem.Text), "Disabled");
                builder.AddAttribute(5, nameof(W3BottomNavigationItem.Disabled), true);
                builder.CloseComponent();

                builder.OpenComponent<W3BottomNavigationItem>(6);
                builder.AddAttribute(7, nameof(W3BottomNavigationItem.Value), "search");
                builder.AddAttribute(8, nameof(W3BottomNavigationItem.Text), "Search");
                builder.CloseComponent();

                builder.OpenComponent<W3BottomNavigationItem>(9);
                builder.AddAttribute(10, nameof(W3BottomNavigationItem.Value), "settings");
                builder.AddAttribute(11, nameof(W3BottomNavigationItem.Text), "Settings");
                builder.CloseComponent();
            }));

        cut.Find("[aria-pressed='true']").KeyDown(new KeyboardEventArgs { Key = "ArrowRight" });

        Assert.Equal("search", value);
        Assert.Contains("Search", cut.Find("[aria-pressed='true']").TextContent);

        cut.Find("[aria-pressed='true']").KeyDown(new KeyboardEventArgs { Key = "ArrowLeft" });
        Assert.Equal("home", value);

        cut.Find("[aria-pressed='true']").KeyDown(new KeyboardEventArgs { Key = "End" });
        Assert.Equal("settings", value);

        cut.Find("[aria-pressed='true']").KeyDown(new KeyboardEventArgs { Key = "ArrowRight" });
        Assert.Equal("home", value);

        cut.Find("[aria-pressed='true']").KeyDown(new KeyboardEventArgs { Key = "Home" });
        Assert.Equal("home", value);
    }

    [Fact]
    public void BreadcrumbRendersSemanticLinksAndCurrentItem()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = context.Render<W3Breadcrumb>(parameters => parameters
            .Add(p => p.Label, "Project path")
            .Add(p => p.Separator, ">")
            .Add(p => p.ListClass, "breadcrumb-list-extra")
            .Add(p => p.Border, true)
            .Add(p => p.Card, true)
            .Add(p => p.CardDepth, W3CardDepth.Two)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.LightGrey)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "breadcrumb-shell")
            .Add(p => p.Style, "max-width: 36rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BreadcrumbItem>(0);
                builder.AddAttribute(1, nameof(W3BreadcrumbItem.Href), "components");
                builder.AddAttribute(2, nameof(W3BreadcrumbItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Components")));
                builder.CloseComponent();

                builder.OpenComponent<W3BreadcrumbItem>(3);
                builder.AddAttribute(4, nameof(W3BreadcrumbItem.Href), "components/navbar");
                builder.AddAttribute(5, nameof(W3BreadcrumbItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Navigation")));
                builder.CloseComponent();

                builder.OpenComponent<W3BreadcrumbItem>(6);
                builder.AddAttribute(7, nameof(W3BreadcrumbItem.Current), true);
                builder.AddAttribute(8, nameof(W3BreadcrumbItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Breadcrumb")));
                builder.CloseComponent();
            }));

        var nav = cut.Find("nav");
        var items = cut.FindAll("li");

        Assert.Equal("Project path", nav.GetAttribute("aria-label"));
        Assert.Contains("w3-breadcrumb", nav.GetAttribute("class"));
        Assert.Contains("w3-border", nav.GetAttribute("class"));
        Assert.Contains("w3-card-2", nav.GetAttribute("class"));
        Assert.Contains("w3-light-grey", nav.GetAttribute("class"));
        Assert.Contains("w3-text-black", nav.GetAttribute("class"));
        Assert.Contains("w3-round", nav.GetAttribute("class"));
        Assert.Contains("breadcrumb-shell", nav.GetAttribute("class"));
        Assert.Equal("max-width: 36rem;", nav.GetAttribute("style"));
        Assert.Contains("breadcrumb-list-extra", cut.Find("ol").GetAttribute("class"));
        Assert.Equal(3, items.Count);
        Assert.All(items.Skip(1), item => Assert.Equal(">", item.GetAttribute("data-separator")));
        Assert.Equal(2, cut.FindAll("a").Count);
        Assert.Equal("page", cut.Find("[aria-current='page']").GetAttribute("aria-current"));
    }

    [Fact]
    public void BreadcrumbItemCanRenderDisabledTextAndButtonLinks()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var clicked = false;
        var cut = context.Render<W3Breadcrumb>(parameters => parameters
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BreadcrumbItem>(0);
                builder.AddAttribute(1, nameof(W3BreadcrumbItem.Href), "components");
                builder.AddAttribute(2, nameof(W3BreadcrumbItem.Button), true);
                builder.AddAttribute(3, nameof(W3BreadcrumbItem.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true));
                builder.AddAttribute(4, nameof(W3BreadcrumbItem.LinkClass), "breadcrumb-link-extra");
                builder.AddAttribute(5, nameof(W3BreadcrumbItem.Class), "breadcrumb-item-extra");
                builder.AddAttribute(6, nameof(W3BreadcrumbItem.Style), "padding-inline: 0.25rem;");
                builder.AddAttribute(7, nameof(W3BreadcrumbItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Components")));
                builder.CloseComponent();

                builder.OpenComponent<W3BreadcrumbItem>(8);
                builder.AddAttribute(9, nameof(W3BreadcrumbItem.Disabled), true);
                builder.AddAttribute(10, nameof(W3BreadcrumbItem.Text), "Disabled");
                builder.AddAttribute(11, nameof(W3BreadcrumbItem.TextClass), "breadcrumb-text-extra");
                builder.CloseComponent();
            }));

        var link = cut.Find("a");
        var disabled = cut.FindAll("li")[1];

        Assert.Contains("w3-button", link.GetAttribute("class"));
        Assert.Contains("breadcrumb-link-extra", link.GetAttribute("class"));
        Assert.Contains("breadcrumb-item-extra", cut.FindAll("li")[0].GetAttribute("class"));
        Assert.Equal("padding-inline: 0.25rem;", cut.FindAll("li")[0].GetAttribute("style"));
        Assert.Contains("w3-disabled", disabled.GetAttribute("class"));
        Assert.Equal("true", disabled.GetAttribute("aria-disabled"));
        Assert.Contains("breadcrumb-text-extra", disabled.QuerySelector("span")?.GetAttribute("class"));

        link.Click();

        Assert.True(clicked);
    }

    [Fact]
    public void PaginationChangesPage()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var page = 2;
        var cut = context.Render<W3Pagination>(parameters => parameters
            .Add(p => p.PageCount, 4)
            .Add(p => p.CurrentPage, page)
            .Add(p => p.CurrentPageChanged, EventCallback.Factory.Create<int>(this, value => page = value))
            .Add(p => p.Center, true)
            .Add(p => p.PreviousText, "Back")
            .Add(p => p.NextText, "Forward")
            .Add(p => p.AriaLabel, "Result pages")
            .Add(p => p.ActiveColor, W3Color.Teal)
            .Add(p => p.ActiveTextColor, W3Color.White)
            .Add(p => p.Class, "pager-shell")
            .Add(p => p.Style, "margin-top: 1rem;"));

        var nav = cut.Find("nav");

        Assert.Equal("Result pages", nav.GetAttribute("aria-label"));
        Assert.Contains("w3-center", nav.GetAttribute("class"));
        Assert.Contains("pager-shell", nav.GetAttribute("class"));
        Assert.Equal("margin-top: 1rem;", nav.GetAttribute("style"));
        Assert.Equal("Back", cut.FindAll("button")[0].TextContent);
        Assert.Equal("Forward", cut.FindAll("button")[^1].TextContent);
        Assert.Contains("w3-text-white", cut.Find("[aria-current='page']").GetAttribute("class"));

        cut.FindAll("button")[3].Click();

        Assert.Equal(3, page);
        Assert.Contains("aria-current=\"page\"", cut.Markup);
    }

    [Fact]
    public void PaginationSupportsKeyboardNavigation()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var page = 2;
        var cut = context.Render<W3Pagination>(parameters => parameters
            .Add(p => p.PageCount, 4)
            .Add(p => p.CurrentPage, page)
            .Add(p => p.CurrentPageChanged, EventCallback.Factory.Create<int>(this, value => page = value)));

        var activeButton = cut.Find("[aria-current='page']");

        activeButton.KeyDown(new KeyboardEventArgs { Key = "ArrowRight" });
        Assert.Equal(3, page);
        Assert.Equal("3", cut.Find("[aria-current='page']").TextContent);

        cut.Find("[aria-current='page']").KeyDown(new KeyboardEventArgs { Key = "ArrowLeft" });
        Assert.Equal(2, page);

        cut.Find("[aria-current='page']").KeyDown(new KeyboardEventArgs { Key = "End" });
        Assert.Equal(4, page);

        cut.Find("[aria-current='page']").KeyDown(new KeyboardEventArgs { Key = "ArrowRight" });
        Assert.Equal(4, page);

        cut.Find("[aria-current='page']").KeyDown(new KeyboardEventArgs { Key = "Home" });
        Assert.Equal(1, page);

        cut.Find("[aria-current='page']").KeyDown(new KeyboardEventArgs { Key = "ArrowLeft" });
        Assert.Equal(1, page);
    }

    [Fact]
    public void ProgressRendersClampedWidthAndLabel()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = context.Render<W3Progress>(parameters => parameters
            .Add(p => p.Value, 125)
            .Add(p => p.Max, 100)
            .Add(p => p.ShowLabel, true)
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.TrackColor, W3Color.LightGrey)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "progress-track")
            .Add(p => p.Style, "height: 1.75rem;"));

        var track = cut.Find("[role='progressbar']");
        var bar = cut.Find(".w3-container");

        Assert.Equal("100", track.GetAttribute("aria-valuenow"));
        Assert.Contains("w3-round", track.GetAttribute("class"));
        Assert.Contains("w3-light-grey", track.GetAttribute("class"));
        Assert.Contains("progress-track", track.GetAttribute("class"));
        Assert.Equal("height: 1.75rem;", track.GetAttribute("style"));
        Assert.Contains("w3-teal", bar.GetAttribute("class"));
        Assert.Contains("w3-text-white", bar.GetAttribute("class"));
        Assert.Contains("width:100%", bar.GetAttribute("style"));
        Assert.Contains("100%", bar.TextContent);
    }

    [Fact]
    public void ProgressUsesEmptyBarClassAtZero()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = context.Render<W3Progress>(parameters => parameters
            .Add(p => p.Value, 0)
            .Add(p => p.ShowLabel, true));

        var bar = cut.Find(".w3-progress-bar");

        Assert.Contains("width:0%", bar.GetAttribute("style"));
        Assert.Contains("w3-progress-bar-empty", bar.GetAttribute("class"));
        Assert.Contains("0%", bar.TextContent);
    }
}
