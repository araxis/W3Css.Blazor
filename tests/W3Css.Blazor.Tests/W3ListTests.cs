using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ListTests
{
    [Fact]
    public void RendersListWithOptionsAndItems()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3List>(parameters => parameters
            .Add(p => p.Border, true)
            .Add(p => p.Hoverable, true)
            .Add(p => p.Centered, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "list-extra")
            .Add(p => p.Style, "max-width: 30rem;")
            .Add(p => p.ChildContent, "<li>Buttons</li><li>Cards</li>"));

        var list = cut.Find("ul");

        Assert.Contains("w3-ul", list.GetAttribute("class"));
        Assert.Contains("w3-border", list.GetAttribute("class"));
        Assert.Contains("w3-hoverable", list.GetAttribute("class"));
        Assert.Contains("w3-center", list.GetAttribute("class"));
        Assert.Contains("w3-white", list.GetAttribute("class"));
        Assert.Contains("w3-text-black", list.GetAttribute("class"));
        Assert.Contains("list-extra", list.GetAttribute("class"));
        Assert.Equal("max-width: 30rem;", list.GetAttribute("style"));
        Assert.Contains("Buttons", list.TextContent);
        Assert.Contains("Cards", list.TextContent);
    }
}
