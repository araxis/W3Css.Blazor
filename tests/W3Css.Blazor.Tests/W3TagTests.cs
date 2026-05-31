using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3TagTests
{
    [Fact]
    public void RendersTagClassesAndAttributes()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Tag>(parameters => parameters
            .Add(p => p.Color, W3Color.Black)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Size, W3Size.Large)
            .Add(p => p.Round, W3Round.Large)
            .Add(p => p.Class, "tag-extra")
            .Add(p => p.Style, "letter-spacing: 0;")
            .Add(p => p.ChildContent, "Stable")
            .AddUnmatched("data-kind", "release"));

        var tag = cut.Find("span");

        Assert.Contains("w3-tag", tag.GetAttribute("class"));
        Assert.Contains("w3-black", tag.GetAttribute("class"));
        Assert.Contains("w3-text-white", tag.GetAttribute("class"));
        Assert.Contains("w3-large", tag.GetAttribute("class"));
        Assert.Contains("w3-round-large", tag.GetAttribute("class"));
        Assert.Contains("tag-extra", tag.GetAttribute("class"));
        Assert.Equal("letter-spacing: 0;", tag.GetAttribute("style"));
        Assert.Equal("release", tag.GetAttribute("data-kind"));
        Assert.Equal("Stable", tag.TextContent.Trim());
    }
}
