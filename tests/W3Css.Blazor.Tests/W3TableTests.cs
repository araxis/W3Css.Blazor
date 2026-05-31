using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3TableTests
{
    [Fact]
    public void RendersTableClassesAndContent()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Table>(parameters => parameters
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Striped, true)
            .Add(p => p.Bordered, true)
            .Add(p => p.Border, true)
            .Add(p => p.Hoverable, true)
            .Add(p => p.Centered, true)
            .Add(p => p.Class, "table-extra")
            .Add(p => p.Style, "min-width: 28rem;")
            .Add(p => p.ChildContent, "<tbody><tr><td>Badge</td></tr></tbody>"));

        var table = cut.Find("table");

        Assert.Contains("w3-table", table.GetAttribute("class"));
        Assert.Contains("w3-white", table.GetAttribute("class"));
        Assert.Contains("w3-text-black", table.GetAttribute("class"));
        Assert.Contains("w3-striped", table.GetAttribute("class"));
        Assert.Contains("w3-bordered", table.GetAttribute("class"));
        Assert.Contains("w3-border", table.GetAttribute("class"));
        Assert.Contains("w3-hoverable", table.GetAttribute("class"));
        Assert.Contains("w3-centered", table.GetAttribute("class"));
        Assert.Contains("table-extra", table.GetAttribute("class"));
        Assert.Equal("min-width: 28rem;", table.GetAttribute("style"));
        Assert.Contains("Badge", table.TextContent);
    }

    [Fact]
    public void WrapsTableWhenResponsive()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Table>(parameters => parameters
            .Add(p => p.Responsive, true)
            .Add(p => p.ChildContent, "<tbody><tr><td>Responsive</td></tr></tbody>")
            .AddUnmatched("aria-label", "Component matrix"));

        var wrapper = cut.Find("div");
        var table = cut.Find("table");

        Assert.Contains("w3-responsive", wrapper.GetAttribute("class"));
        Assert.Equal("Component matrix", table.GetAttribute("aria-label"));
        Assert.Contains("Responsive", table.TextContent);
    }
}
