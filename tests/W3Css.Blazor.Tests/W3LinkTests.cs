using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3LinkTests
{
    [Fact]
    public void RendersAnchorByDefaultWithHrefAndClasses()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Link>(parameters => parameters
            .Add(p => p.Text, "Go")
            .Add(p => p.Href, "components")
            .Add(p => p.TextColor, W3Color.Teal));

        var anchor = cut.Find("a");

        Assert.Equal("components", anchor.GetAttribute("href"));
        Assert.Contains("w3-link", anchor.GetAttribute("class"));
        Assert.Contains("w3-text-teal", anchor.GetAttribute("class"));
    }

    [Fact]
    public void RendersButtonWhenButtonModeIsEnabled()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Link>(parameters => parameters
            .Add(p => p.Text, "Upload")
            .Add(p => p.Href, (string?)null)
            .Add(p => p.Button, true)
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.TextColor, W3Color.White));

        var button = cut.Find("button");

        Assert.Contains("w3-button", button.GetAttribute("class"));
        Assert.Contains("w3-teal", button.GetAttribute("class"));
        Assert.Contains("w3-text-white", button.GetAttribute("class"));
    }

    [Fact]
    public void DisabledModeRendersNonInteractiveSpan()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Link>(parameters => parameters
            .Add(p => p.Text, "Unavailable")
            .Add(p => p.Href, "components")
            .Add(p => p.Disabled, true));

        Assert.Empty(cut.FindAll("a"));
        Assert.Single(cut.FindAll("span"));
        var span = cut.Find("span");
        Assert.Equal("true", span.GetAttribute("aria-disabled"));
        Assert.Contains("Unavailable", span.TextContent);
    }

    [Fact]
    public void CallsOnClickForLinkMode()
    {
        using var context = new BunitContext();
        var clicked = false;

        var cut = context.Render<W3Link>(parameters => parameters
            .Add(p => p.Text, "Click me")
            .Add(p => p.Button, true)
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true)));

        var command = cut.Find("button");
        command.Click();
        Assert.True(clicked);
    }

    [Fact]
    public void AppliesUnderlineFalseViaStyleOverride()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Link>(parameters => parameters
            .Add(p => p.Text, "No underline")
            .Add(p => p.Underline, false));

        Assert.Contains("text-decoration: none;", cut.Find("span").GetAttribute("style"));
    }
}
