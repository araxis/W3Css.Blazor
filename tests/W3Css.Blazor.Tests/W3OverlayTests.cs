using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3OverlayTests
{
    [Fact]
    public void ModalRendersOpenStateAndClosesFromButton()
    {
        using var context = new BunitContext();
        var open = true;
        var cut = context.Render<W3Modal>(parameters => parameters
            .Add(p => p.Id, "details")
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.Title, "Details")
            .Add(p => p.Animate, true)
            .Add(p => p.ContentColor, W3Color.White)
            .Add(p => p.ContentTextColor, W3Color.Black)
            .Add(p => p.ContentClass, "modal-content-extra")
            .Add(p => p.ContentStyle, "max-width: 32rem;")
            .Add(p => p.Class, "modal-extra")
            .Add(p => p.Style, "padding-top: 4rem;")
            .Add(p => p.ChildContent, "Modal body"));

        var modal = cut.Find("[role='dialog']");
        var content = cut.Find(".w3-modal-content");

        Assert.Contains("w3-modal", modal.GetAttribute("class"));
        Assert.Equal("details", modal.GetAttribute("id"));
        Assert.Contains("w3-show", modal.GetAttribute("class"));
        Assert.Contains("modal-extra", modal.GetAttribute("class"));
        Assert.Equal("padding-top: 4rem;", modal.GetAttribute("style"));
        Assert.Contains("w3-card-4", content.GetAttribute("class"));
        Assert.Contains("w3-animate-zoom", content.GetAttribute("class"));
        Assert.Contains("w3-white", content.GetAttribute("class"));
        Assert.Contains("w3-text-black", content.GetAttribute("class"));
        Assert.Contains("modal-content-extra", content.GetAttribute("class"));
        Assert.Equal("max-width: 32rem;", content.GetAttribute("style"));
        Assert.Equal("true", modal.GetAttribute("aria-modal"));
        Assert.Equal("false", modal.GetAttribute("aria-hidden"));
        Assert.Equal("details-title", modal.GetAttribute("aria-labelledby"));
        Assert.Null(modal.GetAttribute("aria-label"));

        cut.Find("button").Click();

        Assert.False(open);
    }

    [Fact]
    public void ModalSupportsExplicitAccessibleLabelWithoutTitle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Modal>(parameters => parameters
            .Add(p => p.Open, true)
            .Add(p => p.AriaLabel, "Preferences")
            .Add(p => p.ChildContent, "Modal body"));

        var modal = cut.Find("[role='dialog']");

        Assert.Equal("Preferences", modal.GetAttribute("aria-label"));
        Assert.Null(modal.GetAttribute("aria-labelledby"));
        Assert.Equal("true", modal.GetAttribute("aria-modal"));
        Assert.Equal("false", modal.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void ModalSupportsCustomLabelledByReference()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Modal>(parameters => parameters
            .Add(p => p.Open, true)
            .Add(p => p.AriaLabelledBy, "custom-modal-heading")
            .Add(p => p.Header, (RenderFragment)(header => header.AddMarkupContent(0, "<h2 id=\"custom-modal-heading\">Preferences</h2>")))
            .Add(p => p.ChildContent, "Modal body"));

        var modal = cut.Find("[role='dialog']");

        Assert.Equal("custom-modal-heading", modal.GetAttribute("aria-labelledby"));
        Assert.Null(modal.GetAttribute("aria-label"));
    }

    [Fact]
    public void ModalCanCloseFromEscape()
    {
        using var context = new BunitContext();
        var open = true;
        var cut = context.Render<W3Modal>(parameters => parameters
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.ChildContent, "Modal body"));

        cut.Find("[role='dialog']").KeyDown(new KeyboardEventArgs { Key = "Escape" });

        Assert.False(open);
    }

    [Fact]
    public void TooltipRendersHoverText()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Tooltip>(parameters => parameters
            .Add(p => p.Text, "More details")
            .Add(p => p.Color, W3Color.Black)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Round, W3Round.Large)
            .Add(p => p.TextClass, "tooltip-text-extra")
            .Add(p => p.TooltipStyle, "min-width: 10rem;")
            .Add(p => p.Class, "tooltip-root-extra")
            .Add(p => p.Style, "display: inline-flex;")
            .Add(p => p.ChildContent, "Hover me"));

        var tooltip = cut.Find(".w3-tooltip");
        var text = cut.Find(".w3-text");

        Assert.Contains("Hover me", tooltip.TextContent);
        Assert.Contains("tooltip-root-extra", tooltip.GetAttribute("class"));
        Assert.Equal("display: inline-flex;", tooltip.GetAttribute("style"));
        Assert.Contains("More details", text.TextContent);
        Assert.Contains("w3-tag", text.GetAttribute("class"));
        Assert.Contains("w3-black", text.GetAttribute("class"));
        Assert.Contains("w3-text-white", text.GetAttribute("class"));
        Assert.Contains("w3-round-large", text.GetAttribute("class"));
        Assert.Contains("tooltip-text-extra", text.GetAttribute("class"));
        Assert.Equal("min-width: 10rem;", text.GetAttribute("style"));
    }

    [Fact]
    public void PopoverTogglesAndClosesFromOutsideLayer()
    {
        using var context = new BunitContext();
        var open = false;
        var cut = context.Render<W3Popover>(parameters => parameters
            .Add(p => p.Label, "Open filters")
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.Placement, W3PopoverPlacement.Right)
            .Add(p => p.Width, "16rem")
            .Add(p => p.TriggerColor, W3Color.Teal)
            .Add(p => p.TriggerTextColor, W3Color.White)
            .Add(p => p.ContentColor, W3Color.White)
            .Add(p => p.ContentTextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Large)
            .Add(p => p.TriggerClass, "popover-trigger-extra")
            .Add(p => p.ContentClass, "popover-content-extra")
            .Add(p => p.ContentStyle, "max-width: 20rem;")
            .Add(p => p.Class, "popover-extra")
            .Add(p => p.Style, "margin-left: 2rem;")
            .Add(p => p.TriggerContent, (RenderFragment)(trigger => trigger.AddContent(0, "Filters")))
            .Add(p => p.ChildContent, "Filter body"));

        var root = cut.Find(".w3-popover");
        var button = cut.Find("button");
        var content = cut.Find("[role='dialog']");

        Assert.Contains("w3-popover-right", root.GetAttribute("class"));
        Assert.Contains("popover-extra", root.GetAttribute("class"));
        Assert.Equal("margin-left: 2rem", root.GetAttribute("style"));
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
        Assert.Equal("dialog", button.GetAttribute("aria-haspopup"));
        Assert.Contains("w3-teal", button.GetAttribute("class"));
        Assert.Contains("w3-text-white", button.GetAttribute("class"));
        Assert.Contains("popover-trigger-extra", button.GetAttribute("class"));
        Assert.Equal("true", content.GetAttribute("aria-hidden"));
        Assert.Equal(button.GetAttribute("aria-controls"), content.GetAttribute("id"));
        Assert.DoesNotContain("w3-show", content.GetAttribute("class"));

        button.Click();

        Assert.True(open);
        root = cut.Find(".w3-popover");
        content = cut.Find("[role='dialog']");
        Assert.Contains("w3-popover-open", root.GetAttribute("class"));
        Assert.Equal("false", content.GetAttribute("aria-hidden"));
        Assert.Contains("w3-show", content.GetAttribute("class"));
        Assert.Contains("w3-card-4", content.GetAttribute("class"));
        Assert.Contains("w3-round-large", content.GetAttribute("class"));
        Assert.Contains("popover-content-extra", content.GetAttribute("class"));
        Assert.Equal("width:16rem;max-width: 20rem", content.GetAttribute("style"));
        Assert.Contains("Filter body", content.TextContent);

        cut.Find(".w3-popover-close-layer").Click();

        Assert.False(open);
    }

    [Fact]
    public void PopoverClosesWithEscapeAndReturnsTriggerFocus()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var open = true;
        var cut = context.Render<W3Popover>(parameters => parameters
            .Add(p => p.Label, "Open filters")
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.ChildContent, "Filter body"));

        Assert.Contains("w3-popover-open", cut.Find(".w3-popover").GetAttribute("class"));
        Assert.Equal("true", cut.Find("button").GetAttribute("aria-expanded"));

        cut.Find("[role='dialog']").KeyDown(new KeyboardEventArgs { Key = "Escape" });

        cut.WaitForAssertion(() =>
        {
            Assert.False(open);
            Assert.DoesNotContain("w3-popover-open", cut.Find(".w3-popover").GetAttribute("class"));
            Assert.DoesNotContain("w3-show", cut.Find("[role='dialog']").GetAttribute("class"));
            Assert.Equal("false", cut.Find("button").GetAttribute("aria-expanded"));
            Assert.Equal("true", cut.Find("[role='dialog']").GetAttribute("aria-hidden"));
        });
    }

    [Fact]
    public void SidebarRendersOverlayAndClosesFromOverlayClick()
    {
        using var context = new BunitContext();
        var open = true;
        var cut = context.Render<W3Sidebar>(parameters => parameters
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.Width, 260)
            .Add(p => p.Collapse, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Label, "Primary sidebar")
            .Add(p => p.Header, (RenderFragment)(header => header.AddContent(0, "Menu header")))
            .Add(p => p.Class, "sidebar-extra")
            .Add(p => p.Style, "top: 1rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3BarItem>(0);
                builder.AddAttribute(1, nameof(W3BarItem.Href), "components");
                builder.AddAttribute(2, nameof(W3BarItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Components")));
                builder.CloseComponent();
            }));

        var sidebar = cut.Find("aside");
        var overlay = cut.Find(".w3-overlay");

        Assert.Contains("w3-sidebar", sidebar.GetAttribute("class"));
        Assert.Contains("w3-show", sidebar.GetAttribute("class"));
        Assert.Contains("w3-collapse", sidebar.GetAttribute("class"));
        Assert.Contains("w3-white", sidebar.GetAttribute("class"));
        Assert.Contains("w3-text-black", sidebar.GetAttribute("class"));
        Assert.Contains("sidebar-extra", sidebar.GetAttribute("class"));
        Assert.Contains("width:260px", sidebar.GetAttribute("style"));
        Assert.Contains("left:0", sidebar.GetAttribute("style"));
        Assert.Contains("top: 1rem", sidebar.GetAttribute("style"));
        Assert.Contains("Menu header", sidebar.TextContent);
        Assert.Equal("false", sidebar.GetAttribute("aria-hidden"));
        Assert.Equal("Primary sidebar", sidebar.GetAttribute("aria-label"));
        Assert.Equal("0", sidebar.GetAttribute("tabindex"));

        overlay.Click();

        Assert.False(open);
    }

    [Fact]
    public void SidebarClosesWithEscapeWhenFocusedInside()
    {
        using var context = new BunitContext();
        var open = true;
        var cut = context.Render<W3Sidebar>(parameters => parameters
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.ChildContent, "Navigation"));

        cut.Find("aside").KeyDown(new KeyboardEventArgs { Key = "Escape" });

        cut.WaitForAssertion(() =>
        {
            Assert.False(open);
            var sidebar = cut.Find("aside");
            Assert.Contains("w3-hide", sidebar.GetAttribute("class"));
            Assert.Equal("true", sidebar.GetAttribute("aria-hidden"));
            Assert.Null(sidebar.GetAttribute("tabindex"));
        });
    }

    [Fact]
    public void SidebarCanRenderOnRight()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Sidebar>(parameters => parameters
            .Add(p => p.Position, W3SidebarPosition.Right)
            .Add(p => p.ChildContent, "Right side"));

        Assert.Contains("right:0", cut.Find("aside").GetAttribute("style"));
    }

    [Fact]
    public void DrawerRendersTemporarySurfaceAndClosesFromOverlay()
    {
        using var context = new BunitContext();
        var open = true;
        var cut = context.Render<W3Drawer>(parameters => parameters
            .Add(p => p.Id, "workspace-drawer")
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.Title, "Workspace")
            .Add(p => p.Width, 320)
            .Add(p => p.Position, W3SidebarPosition.Right)
            .Add(p => p.Contained, true)
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.HeaderClass, "drawer-header-extra")
            .Add(p => p.BodyClass, "drawer-body-extra")
            .Add(p => p.FooterClass, "drawer-footer-extra")
            .Add(p => p.Class, "drawer-extra")
            .Add(p => p.Style, "top: 2rem;")
            .Add(p => p.ChildContent, "Drawer body")
            .Add(p => p.Footer, (RenderFragment)(footer => footer.AddContent(0, "Drawer footer"))));

        var drawer = cut.Find("aside");
        var overlay = cut.Find(".w3-drawer-overlay");
        var header = cut.Find(".w3-drawer-header");
        var body = cut.Find(".w3-drawer-body");
        var footer = cut.Find(".w3-drawer-footer");

        Assert.Contains("w3-sidebar", drawer.GetAttribute("class"));
        Assert.Contains("w3-drawer", drawer.GetAttribute("class"));
        Assert.Contains("w3-drawer-temporary", drawer.GetAttribute("class"));
        Assert.Contains("w3-drawer-right", drawer.GetAttribute("class"));
        Assert.Contains("w3-drawer-contained", drawer.GetAttribute("class"));
        Assert.Contains("w3-show", drawer.GetAttribute("class"));
        Assert.Contains("w3-animate-right", drawer.GetAttribute("class"));
        Assert.Contains("w3-card-4", drawer.GetAttribute("class"));
        Assert.Contains("w3-border", drawer.GetAttribute("class"));
        Assert.Contains("w3-white", drawer.GetAttribute("class"));
        Assert.Contains("w3-text-black", drawer.GetAttribute("class"));
        Assert.Contains("drawer-extra", drawer.GetAttribute("class"));
        Assert.Contains("width:320px", drawer.GetAttribute("style"));
        Assert.Contains("right:0", drawer.GetAttribute("style"));
        Assert.Contains("top: 2rem", drawer.GetAttribute("style"));
        Assert.Equal("dialog", drawer.GetAttribute("role"));
        Assert.Equal("true", drawer.GetAttribute("aria-modal"));
        Assert.Equal("false", drawer.GetAttribute("aria-hidden"));
        Assert.Equal("workspace-drawer-title", drawer.GetAttribute("aria-labelledby"));
        Assert.Null(drawer.GetAttribute("aria-label"));
        Assert.Contains("Workspace", header.TextContent);
        Assert.Contains("drawer-header-extra", header.GetAttribute("class"));
        Assert.Contains("drawer-body-extra", body.GetAttribute("class"));
        Assert.Contains("Drawer body", body.TextContent);
        Assert.Contains("drawer-footer-extra", footer.GetAttribute("class"));
        Assert.Contains("Drawer footer", footer.TextContent);
        Assert.Contains("w3-drawer-overlay-contained", overlay.GetAttribute("class"));

        overlay.Click();

        Assert.False(open);
    }

    [Fact]
    public void DrawerSupportsExplicitAccessibleLabels()
    {
        using var context = new BunitContext();
        var labelled = context.Render<W3Drawer>(parameters => parameters
            .Add(p => p.Open, true)
            .Add(p => p.AriaLabel, "Workspace settings")
            .Add(p => p.ChildContent, "Settings"));

        var labelledDrawer = labelled.Find("aside");
        Assert.Equal("Workspace settings", labelledDrawer.GetAttribute("aria-label"));
        Assert.Null(labelledDrawer.GetAttribute("aria-labelledby"));

        var referenced = context.Render<W3Drawer>(parameters => parameters
            .Add(p => p.Open, true)
            .Add(p => p.AriaLabelledBy, "drawer-heading")
            .Add(p => p.Header, (RenderFragment)(header => header.AddMarkupContent(0, "<h2 id=\"drawer-heading\">Inspector</h2>")))
            .Add(p => p.ChildContent, "Inspector body"));

        var referencedDrawer = referenced.Find("aside");
        Assert.Equal("drawer-heading", referencedDrawer.GetAttribute("aria-labelledby"));
        Assert.Null(referencedDrawer.GetAttribute("aria-label"));
    }

    [Fact]
    public void DrawerSupportsPersistentFooterAndEscapeClose()
    {
        using var context = new BunitContext();
        var open = true;
        var cut = context.Render<W3Drawer>(parameters => parameters
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.Variant, W3DrawerVariant.Persistent)
            .Add(p => p.Overlay, false)
            .Add(p => p.Title, "Inspector")
            .Add(p => p.Footer, (RenderFragment)(footer => footer.AddContent(0, "Apply changes")))
            .Add(p => p.ChildContent, "Inspector body"));

        var drawer = cut.Find("aside");

        Assert.Contains("w3-drawer-persistent", drawer.GetAttribute("class"));
        Assert.Equal("complementary", drawer.GetAttribute("role"));
        Assert.Null(drawer.GetAttribute("aria-modal"));
        Assert.Empty(cut.FindAll(".w3-drawer-overlay"));
        Assert.Contains("Apply changes", cut.Markup);

        drawer.KeyDown(new KeyboardEventArgs { Key = "Escape" });

        Assert.False(open);
    }

    [Fact]
    public void ModalRendersActionsFooterInABar()
    {
        using var context = new BunitContext();
        var open = true;

        var cut = context.Render<W3Modal>(parameters => parameters
            .Add(p => p.Open, open)
            .Add(p => p.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.Id, "confirm")
            .Add(p => p.Title, "Delete draft")
            .Add(p => p.ShowCloseButton, true)
            .Add(p => p.ChildContent, (RenderFragment)(c => c.AddContent(0, "Delete this item permanently?")))
            .Add(p => p.Actions, (RenderFragment)(actions =>
            {
                actions.AddMarkupContent(0, "<button class=\"w3-button w3-red\">Delete</button>");
            })));

        var modal = cut.Find("[role='dialog']");
        var footer = cut.Find(".w3-bar");

        Assert.Contains("w3-modal", modal.GetAttribute("class"));
        Assert.Contains("w3-card-4", cut.Find(".w3-modal-content").GetAttribute("class"));
        Assert.Contains("w3-bar", footer.GetAttribute("class"));
        Assert.Contains("Delete this item permanently?", cut.Markup);
        Assert.Contains("Delete", footer.TextContent);
    }
}
