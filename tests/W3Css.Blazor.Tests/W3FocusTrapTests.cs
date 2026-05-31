using Bunit;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3FocusTrapTests
{
    [Fact]
    public void FocusTrapRendersContainerAndChildContent()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3FocusTrap>(parameters => parameters
            .Add(p => p.Active, true)
            .Add(p => p.AutoFocus, false)
            .Add(p => p.RestoreFocus, true)
            .Add(p => p.InitialFocusSelector, "#first-field")
            .Add(p => p.AriaLabel, "Keyboard dialog")
            .Add(p => p.Class, "w3-border w3-padding")
            .Add(p => p.Style, "max-width: 20rem;")
            .AddChildContent("<input id=\"first-field\" /><button>Save</button>"));

        var trap = cut.Find(".w3-focus-trap");

        Assert.Contains("w3-border", trap.GetAttribute("class"));
        Assert.Contains("w3-padding", trap.GetAttribute("class"));
        Assert.Equal("max-width: 20rem;", trap.GetAttribute("style"));
        Assert.Equal("-1", trap.GetAttribute("tabindex"));
        Assert.Equal("Keyboard dialog", trap.GetAttribute("aria-label"));
        Assert.NotNull(trap.QuerySelector("#first-field"));
        Assert.Contains("Save", trap.TextContent);
    }

    [Fact]
    public void FocusTrapSupportsInactiveStateAndCustomTabIndex()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3FocusTrap>(parameters => parameters
            .Add(p => p.Active, false)
            .Add(p => p.AutoFocus, false)
            .Add(p => p.RestoreFocus, false)
            .Add(p => p.TabIndex, 0)
            .AddChildContent("<button>Open</button>"));

        var trap = cut.Find(".w3-focus-trap");

        Assert.Equal("0", trap.GetAttribute("tabindex"));
        Assert.Contains("Open", trap.TextContent);
    }
}
