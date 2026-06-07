using Bunit;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3TooltipTests
{
    [Fact]
    public void TooltipExposesAccessibleDescription()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Tooltip>(parameters => parameters
            .Add(p => p.Text, "Saves your changes")
            .Add(p => p.ChildContent, "Save"));

        var wrapper = cut.Find(".w3-tooltip");
        var text = cut.Find("[role='tooltip']");
        var describedBy = wrapper.GetAttribute("aria-describedby");

        Assert.False(string.IsNullOrEmpty(describedBy));
        Assert.Equal(describedBy, text.GetAttribute("id"));
        Assert.Contains("Saves your changes", text.TextContent);
    }
}
