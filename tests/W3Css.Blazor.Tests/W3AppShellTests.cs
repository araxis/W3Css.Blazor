using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3AppShellTests
{
    [Fact]
    public void AppShellRendersSemanticRegionsAndOffsetsMainContent()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3AppShell>(parameters => parameters
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.HeaderColor, W3Color.Teal)
            .Add(p => p.HeaderTextColor, W3Color.White)
            .Add(p => p.SidebarColor, W3Color.LightGrey)
            .Add(p => p.SidebarTextColor, W3Color.Black)
            .Add(p => p.FooterColor, W3Color.Black)
            .Add(p => p.FooterTextColor, W3Color.White)
            .Add(p => p.SidebarWidth, 280)
            .Add(p => p.SidebarLabel, "Workspace navigation")
            .Add(p => p.MainId, "content")
            .Add(p => p.Class, "shell-extra")
            .Add(p => p.Style, "min-height: 32rem;")
            .Add(p => p.HeaderClass, "shell-header-extra")
            .Add(p => p.HeaderStyle, "min-height: 3rem;")
            .Add(p => p.BodyClass, "shell-body-extra")
            .Add(p => p.BodyStyle, "background: white;")
            .Add(p => p.SidebarClass, "shell-sidebar-extra")
            .Add(p => p.SidebarStyle, "overflow:auto;")
            .Add(p => p.MainClass, "shell-main-extra")
            .Add(p => p.MainStyle, "padding-top: 1rem;")
            .Add(p => p.FooterClass, "shell-footer-extra")
            .Add(p => p.FooterStyle, "min-height: 2rem;")
            .Add(p => p.Header, Header("Header content"))
            .Add(p => p.Sidebar, Header("Sidebar content"))
            .Add(p => p.ChildContent, Header("Main content"))
            .Add(p => p.Footer, Header("Footer content")));

        var root = cut.Find(".w3-app-shell");
        var header = cut.Find("header");
        var sidebar = cut.Find("aside");
        var main = cut.Find("main");
        var footer = cut.Find("footer");

        Assert.Contains("Header content", header.TextContent);
        Assert.Contains("Sidebar content", sidebar.TextContent);
        Assert.Contains("Main content", main.TextContent);
        Assert.Contains("Footer content", footer.TextContent);
        Assert.Contains("w3-white", root.GetAttribute("class"));
        Assert.Contains("w3-text-black", root.GetAttribute("class"));
        Assert.Contains("w3-app-shell-has-sidebar", root.GetAttribute("class"));
        Assert.Contains("shell-extra", root.GetAttribute("class"));
        Assert.Equal("min-height: 32rem", root.GetAttribute("style"));
        Assert.Contains("w3-teal", header.GetAttribute("class"));
        Assert.Contains("w3-text-white", header.GetAttribute("class"));
        Assert.Contains("shell-header-extra", header.GetAttribute("class"));
        Assert.Equal("min-height: 3rem", header.GetAttribute("style"));
        Assert.Contains("shell-body-extra", cut.Find(".w3-app-shell-body").GetAttribute("class"));
        Assert.Equal("background: white;", cut.Find(".w3-app-shell-body").GetAttribute("style"));
        Assert.Contains("w3-sidebar", sidebar.GetAttribute("class"));
        Assert.Contains("w3-collapse", sidebar.GetAttribute("class"));
        Assert.Contains("w3-light-grey", sidebar.GetAttribute("class"));
        Assert.Contains("w3-text-black", sidebar.GetAttribute("class"));
        Assert.Contains("shell-sidebar-extra", sidebar.GetAttribute("class"));
        Assert.Equal("Workspace navigation", sidebar.GetAttribute("aria-label"));
        Assert.Contains("width:280px", sidebar.GetAttribute("style"));
        Assert.Contains("overflow:auto", sidebar.GetAttribute("style"));
        Assert.Contains("margin-left:280px", main.GetAttribute("style"));
        Assert.Contains("padding-top: 1rem", main.GetAttribute("style"));
        Assert.Contains("shell-main-extra", main.GetAttribute("class"));
        Assert.Equal("content", main.GetAttribute("id"));
        Assert.Contains("w3-black", footer.GetAttribute("class"));
        Assert.Contains("w3-text-white", footer.GetAttribute("class"));
        Assert.Contains("shell-footer-extra", footer.GetAttribute("class"));
        Assert.Equal("min-height: 2rem", footer.GetAttribute("style"));
        Assert.NotNull(root);
    }

    [Fact]
    public void AppShellOverlayClosesSidebar()
    {
        using var context = new BunitContext();
        var open = true;
        var cut = context.Render<W3AppShell>(parameters => parameters
            .Add(p => p.SidebarOpen, open)
            .Add(p => p.SidebarOverlay, true)
            .Add(p => p.OverlayClass, "overlay-extra")
            .Add(p => p.OverlayStyle, "cursor:pointer;")
            .Add(p => p.SidebarOpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(p => p.Sidebar, Header("Menu"))
            .Add(p => p.ChildContent, Header("Content")));

        Assert.Contains("w3-show", cut.Find("aside").GetAttribute("class"));
        Assert.Contains("overlay-extra", cut.Find(".w3-overlay").GetAttribute("class"));
        Assert.Equal("cursor:pointer", cut.Find(".w3-overlay").GetAttribute("style"));

        cut.Find(".w3-overlay").Click();

        Assert.False(open);
        Assert.Contains("w3-hide", cut.Find("aside").GetAttribute("class"));
        Assert.Contains("display:none!important", cut.Find("aside").GetAttribute("style"));
        Assert.Empty(cut.FindAll(".w3-overlay"));
    }

    [Fact]
    public void AppShellCanPlaceSidebarOnRight()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3AppShell>(parameters => parameters
            .Add(p => p.SidebarPosition, W3SidebarPosition.Right)
            .Add(p => p.SidebarWidth, 220)
            .Add(p => p.Sidebar, Header("Right menu"))
            .Add(p => p.ChildContent, Header("Content")));

        Assert.Contains("right:0", cut.Find("aside").GetAttribute("style"));
        Assert.Contains("margin-right:220px", cut.Find("main").GetAttribute("style"));
    }

    [Fact]
    public void AppShellClosedSidebarOverridesCollapseDisplay()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3AppShell>(parameters => parameters
            .Add(p => p.SidebarOpen, false)
            .Add(p => p.Sidebar, Header("Menu"))
            .Add(p => p.ChildContent, Header("Content")));

        var sidebar = cut.Find("aside");

        Assert.Contains("w3-hide", sidebar.GetAttribute("class"));
        Assert.Contains("w3-collapse", sidebar.GetAttribute("class"));
        Assert.Contains("display:none!important", sidebar.GetAttribute("style"));
        Assert.Null(cut.Find("main").GetAttribute("style"));
    }

    [Fact]
    public void AppShellCanRenderContainedPreview()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3AppShell>(parameters => parameters
            .Add(p => p.Contained, true)
            .Add(p => p.Sidebar, Header("Menu"))
            .Add(p => p.ChildContent, Header("Content")));

        Assert.Contains("position:relative", cut.Find(".w3-app-shell").GetAttribute("style"));
        Assert.Contains("position:absolute!important", cut.Find("aside").GetAttribute("style"));
        Assert.Empty(cut.FindAll(".w3-overlay"));
    }

    [Fact]
    public void AppShellCanRenderContainedOverlayWhenEnabled()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3AppShell>(parameters => parameters
            .Add(p => p.Contained, true)
            .Add(p => p.SidebarOverlay, true)
            .Add(p => p.Sidebar, Header("Menu"))
            .Add(p => p.ChildContent, Header("Content")));

        Assert.Contains("position:absolute!important", cut.Find(".w3-overlay").GetAttribute("style"));
    }

    private static RenderFragment Header(string text)
    {
        return builder => builder.AddContent(0, text);
    }
}
