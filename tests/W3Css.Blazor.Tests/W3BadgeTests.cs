using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3BadgeTests
{
    [Fact]
    public void RendersBadgeClassesAndContent()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Badge>(parameters => parameters
            .Add(p => p.Color, W3Color.Red)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Size, W3Size.Small)
            .Add(p => p.Class, "badge-extra")
            .Add(p => p.Style, "min-width: 2rem;")
            .Add(p => p.ChildContent, "4"));

        var badge = cut.Find("span");

        Assert.Contains("w3-badge", badge.GetAttribute("class"));
        Assert.Contains("w3-red", badge.GetAttribute("class"));
        Assert.Contains("w3-text-white", badge.GetAttribute("class"));
        Assert.Contains("w3-small", badge.GetAttribute("class"));
        Assert.Contains("badge-extra", badge.GetAttribute("class"));
        Assert.Equal("min-width: 2rem;", badge.GetAttribute("style"));
        Assert.Equal("4", badge.TextContent.Trim());
    }
}
