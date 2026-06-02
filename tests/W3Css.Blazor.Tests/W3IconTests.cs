using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3IconTests
{
    [Fact]
    public void IconRendersBuiltInSvgWithAccessibilityState()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Icon>(parameters => parameters
            .Add(p => p.IconName, W3IconName.Home)
            .Add(p => p.Size, W3Size.XXLarge)
            .Add(p => p.TextColor, W3Color.Teal)
            .Add(p => p.Label, "Home")
            .Add(p => p.Class, "custom-icon")
            .Add(p => p.Style, "line-height: 1;"));

        var icon = cut.Find("svg");
        var classes = icon.GetAttribute("class");

        Assert.Contains("w3-icon", classes);
        Assert.Contains("w3-icon-svg", classes);
        Assert.Contains("w3-xxlarge", classes);
        Assert.Contains("w3-text-teal", classes);
        Assert.Contains("custom-icon", classes);
        Assert.Equal("img", icon.GetAttribute("role"));
        Assert.Equal("Home", icon.GetAttribute("aria-label"));
        Assert.Equal("false", icon.GetAttribute("focusable"));
        Assert.Equal("line-height: 1;", icon.GetAttribute("style"));
        Assert.Contains("path", icon.InnerHtml);
    }

    [Fact]
    public void IconRendersLibraryClassesSizeAndHiddenState()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Icon>(parameters => parameters
            .Add(p => p.IconClass, "fa fa-home")
            .Add(p => p.Size, W3Size.XXLarge)
            .Add(p => p.TextColor, W3Color.Teal)
            .Add(p => p.Class, "custom-icon")
            .Add(p => p.Style, "line-height: 1;"));

        var icon = cut.Find("i");
        var classes = icon.GetAttribute("class");

        Assert.Contains("fa", classes);
        Assert.Contains("fa-home", classes);
        Assert.Contains("w3-xxlarge", classes);
        Assert.Contains("w3-text-teal", classes);
        Assert.Contains("custom-icon", classes);
        Assert.Equal("line-height: 1;", icon.GetAttribute("style"));
        Assert.Equal("true", icon.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void IconRendersAccessibleLabelAndChildContent()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Icon>(parameters => parameters
            .Add(p => p.IconClass, "material-icons")
            .Add(p => p.Label, "Home")
            .Add(p => p.Style, "font-size: 24px;")
            .Add(p => p.ChildContent, "home"));

        var icon = cut.Find("i");

        Assert.Contains("material-icons", icon.GetAttribute("class"));
        Assert.Equal("img", icon.GetAttribute("role"));
        Assert.Equal("Home", icon.GetAttribute("aria-label"));
        Assert.Null(icon.GetAttribute("aria-hidden"));
        Assert.Equal("font-size: 24px;", icon.GetAttribute("style"));
        Assert.Equal("home", icon.TextContent);
    }
}
