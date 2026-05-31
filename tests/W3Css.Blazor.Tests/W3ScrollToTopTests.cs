using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ScrollToTopTests
{
    [Fact]
    public async Task StartsHiddenAndShowsWhenSetVisible()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3ScrollToTop>(parameters => parameters
            .Add(c => c.Circle, true)
            .Add(c => c.Color, W3Color.Teal)
            .Add(c => c.TextColor, W3Color.White));

        var button = cut.Find("button");
        Assert.Contains("w3-scroll-to-top", button.GetAttribute("class"));
        Assert.Contains("w3-scroll-to-top-hidden", button.GetAttribute("class"));
        Assert.Contains("w3-circle", button.GetAttribute("class"));
        Assert.Contains("w3-teal", button.GetAttribute("class"));
        Assert.Equal("true", button.GetAttribute("aria-hidden"));
        Assert.Equal("-1", button.GetAttribute("tabindex"));
        Assert.Equal("Scroll to top", button.GetAttribute("aria-label"));

        await cut.InvokeAsync(() => cut.Instance.SetVisible(true));

        button = cut.Find("button");
        Assert.Contains("w3-scroll-to-top-visible", button.GetAttribute("class"));
        Assert.DoesNotContain("w3-scroll-to-top-hidden", button.GetAttribute("class"));
        Assert.Equal("false", button.GetAttribute("aria-hidden"));
        Assert.Equal("0", button.GetAttribute("tabindex"));
    }

    [Fact]
    public async Task NonCircleUsesRoundClassAndClickIsHandled()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3ScrollToTop>(parameters => parameters
            .Add(c => c.Circle, false)
            .Add(c => c.Round, W3Round.Large)
            .Add(c => c.Color, W3Color.Blue)
            .Add(c => c.ChildContent, "Top"));

        var button = cut.Find("button");
        Assert.Contains("w3-round-large", button.GetAttribute("class"));
        Assert.DoesNotContain("w3-circle", button.GetAttribute("class"));
        Assert.Contains("w3-blue", button.GetAttribute("class"));
        Assert.Contains("Top", button.TextContent);

        // Loose JS interop: clicking invokes the scrollToTop module call without throwing.
        await cut.InvokeAsync(() => cut.Instance.SetVisible(true));
        button.Click();

        Assert.Contains(context.JSInterop.Invocations, i => i.Identifier == "import");
    }
}
