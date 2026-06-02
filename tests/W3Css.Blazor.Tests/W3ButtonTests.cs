using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ButtonTests
{
    [Fact]
    public void RendersW3ButtonClassesAndContent()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Button>(parameters => parameters
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Size, W3Size.Large)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Border, true)
            .Add(p => p.Block, true)
            .Add(p => p.Type, "submit")
            .Add(p => p.Class, "button-extra")
            .Add(p => p.Style, "min-width: 8rem;")
            .Add(p => p.ChildContent, "Save"));

        var button = cut.Find("button");

        Assert.Equal("submit", button.GetAttribute("type"));
        Assert.Contains("w3-button", button.GetAttribute("class"));
        Assert.Contains("w3-teal", button.GetAttribute("class"));
        Assert.Contains("w3-text-white", button.GetAttribute("class"));
        Assert.Contains("w3-large", button.GetAttribute("class"));
        Assert.Contains("w3-round", button.GetAttribute("class"));
        Assert.Contains("w3-border", button.GetAttribute("class"));
        Assert.Contains("w3-block", button.GetAttribute("class"));
        Assert.Contains("button-extra", button.GetAttribute("class"));
        Assert.Equal("min-width: 8rem;", button.GetAttribute("style"));
        Assert.Equal("Save", button.TextContent.Trim());
    }

    [Fact]
    public void SupportsDisabledAndAdditionalAttributes()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Button>(parameters => parameters
            .Add(p => p.Disabled, true)
            .Add(p => p.ChildContent, "Delete")
            .AddUnmatched("aria-label", "Delete item"));

        var button = cut.Find("button");

        Assert.True(button.HasAttribute("disabled"));
        Assert.Equal("Delete item", button.GetAttribute("aria-label"));
        Assert.Contains("w3-disabled", button.GetAttribute("class"));
    }

    [Fact]
    public void InvokesClickCallback()
    {
        using var context = new BunitContext();
        var clicked = false;
        var cut = context.Render<W3Button>(parameters => parameters
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .Add(p => p.ChildContent, "Run"));

        cut.Find("button").Click();

        Assert.True(clicked);
    }

    [Fact]
    public void IconButtonRendersAccessibleIconAction()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3IconButton>(parameters => parameters
            .Add(p => p.Label, "Refresh data")
            .Add(p => p.Title, "Refresh")
            .Add(p => p.IconClass, "fa fa-refresh")
            .Add(p => p.IconElementClass, "custom-icon")
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Size, W3Size.Large)
            .Add(p => p.Round, W3Round.Large)
            .Add(p => p.Border, true)
            .Add(p => p.Circle, true)
            .Add(p => p.Class, "toolbar-action")
            .Add(p => p.Style, "margin-right: 4px;"));

        var button = cut.Find("button");
        var icon = cut.Find("i");

        Assert.Equal("button", button.GetAttribute("type"));
        Assert.Equal("Refresh data", button.GetAttribute("aria-label"));
        Assert.Equal("Refresh", button.GetAttribute("title"));
        Assert.Null(button.GetAttribute("aria-pressed"));
        Assert.Contains("w3-button", button.GetAttribute("class"));
        Assert.Contains("w3-icon-button", button.GetAttribute("class"));
        Assert.Contains("w3-icon-button-circle", button.GetAttribute("class"));
        Assert.Contains("w3-circle", button.GetAttribute("class"));
        Assert.Contains("w3-border", button.GetAttribute("class"));
        Assert.Contains("w3-teal", button.GetAttribute("class"));
        Assert.Contains("w3-text-white", button.GetAttribute("class"));
        Assert.Contains("w3-large", button.GetAttribute("class"));
        Assert.Contains("w3-round-large", button.GetAttribute("class"));
        Assert.Contains("toolbar-action", button.GetAttribute("class"));
        Assert.Equal("margin-right: 4px;", button.GetAttribute("style"));

        Assert.Contains("fa", icon.GetAttribute("class"));
        Assert.Contains("fa-refresh", icon.GetAttribute("class"));
        Assert.Contains("w3-icon-button-icon", icon.GetAttribute("class"));
        Assert.Contains("custom-icon", icon.GetAttribute("class"));
        Assert.Equal("true", icon.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void IconButtonRendersBuiltInIconName()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3IconButton>(parameters => parameters
            .Add(p => p.Label, "Save")
            .Add(p => p.IconName, W3IconName.Save)
            .Add(p => p.IconElementClass, "custom-icon"));

        var icon = cut.Find("svg");

        Assert.Equal("Save", cut.Find("button").GetAttribute("aria-label"));
        Assert.Contains("w3-icon-svg", icon.GetAttribute("class"));
        Assert.Contains("w3-icon-button-icon", icon.GetAttribute("class"));
        Assert.Contains("custom-icon", icon.GetAttribute("class"));
        Assert.Equal("true", icon.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void IconButtonSupportsTogglePressedDisabledAndClickCallback()
    {
        using var context = new BunitContext();
        var clicked = false;

        var cut = context.Render<W3IconButton>(parameters => parameters
            .Add(p => p.Label, "Bold")
            .Add(p => p.Toggle, true)
            .Add(p => p.Pressed, true)
            .Add(p => p.Disabled, true)
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .Add(p => p.ChildContent, "B"));

        var button = cut.Find("button");

        Assert.Equal("Bold", button.GetAttribute("aria-label"));
        Assert.Equal("true", button.GetAttribute("aria-pressed"));
        Assert.True(button.HasAttribute("disabled"));
        Assert.Contains("w3-disabled", button.GetAttribute("class"));
        Assert.Contains("w3-icon-button-pressed", button.GetAttribute("class"));
        Assert.Equal("B", cut.Find("i").TextContent);

        button.Click();

        Assert.False(clicked);
    }

    [Fact]
    public void ToggleIconButtonCanTrackInternalPressedStateAndEmitAriaPressed()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3ToggleIconButton>(parameters => parameters
            .Add(p => p.Label, "Bold")
            .Add(p => p.IconClass, "fa fa-bold"));

        var button = cut.Find("button");

        Assert.Equal("false", button.GetAttribute("aria-pressed"));
        Assert.Null(button.GetAttribute("disabled"));

        button.Click();
        Assert.Equal("true", button.GetAttribute("aria-pressed"));

        button.Click();
        Assert.Equal("false", button.GetAttribute("aria-pressed"));
    }

    [Fact]
    public void ToggleIconButtonRespectsPressedParameterWhenBoundMode()
    {
        using var context = new BunitContext();
        var pressed = false;
        var pressedChanges = 0;

        var cut = context.Render<W3ToggleIconButton>(parameters => parameters
            .Add(p => p.Label, "Track changes")
            .Add(p => p.IconClass, "fa fa-flash")
            .Add(p => p.Pressed, pressed)
            .Add(p => p.PressedChanged, EventCallback.Factory.Create<bool>(this, _ => pressedChanges++)));

        var button = cut.Find("button");

        Assert.Equal("false", button.GetAttribute("aria-pressed"));

        button.Click();
        Assert.Equal(1, pressedChanges);
        Assert.Equal("false", button.GetAttribute("aria-pressed"));
    }

    [Fact]
    public void ToggleIconButtonDoesNotToggleWhenDisabled()
    {
        using var context = new BunitContext();
        var clicked = false;

        var cut = context.Render<W3ToggleIconButton>(parameters => parameters
            .Add(p => p.Label, "Underline")
            .Add(p => p.IconClass, "fa fa-underline")
            .Add(p => p.Disabled, true)
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true)));

        var button = cut.Find("button");

        Assert.True(button.HasAttribute("disabled"));
        Assert.Equal("false", button.GetAttribute("aria-pressed"));

        button.Click();

        Assert.False(clicked);
        Assert.Equal("false", button.GetAttribute("aria-pressed"));
    }

    [Fact]
    public void ToggleIconButtonCanCycleThroughMultipleStates()
    {
        using var context = new BunitContext();
        var states = new[]
        {
            new W3ToggleIconButtonState("Light", "Theme mode: Light", "L"),
            new W3ToggleIconButtonState("Dark", "Theme mode: Dark", "D"),
            new W3ToggleIconButtonState("Auto", "Theme mode: Auto", "A")
        };

        var cut = context.Render<W3ToggleIconButton>(parameters => parameters
            .Add(p => p.States, states)
            .Add(p => p.Value, "Light")
            .Add(p => p.Border, true));

        var button = cut.Find("button");

        Assert.Equal("Theme mode: Light", button.GetAttribute("aria-label"));
        Assert.Null(button.GetAttribute("aria-pressed"));
        Assert.DoesNotContain("w3-icon-button-pressed", button.GetAttribute("class"));
        Assert.Equal("L", cut.Find("i").TextContent.Trim());

        button.Click();
        Assert.Equal("Theme mode: Dark", button.GetAttribute("aria-label"));
        Assert.Equal("D", cut.Find("i").TextContent.Trim());

        button.Click();
        Assert.Equal("Theme mode: Auto", button.GetAttribute("aria-label"));
        Assert.Equal("A", cut.Find("i").TextContent.Trim());
    }

    [Fact]
    public void ToggleIconButtonCanCycleThroughBuiltInStateIcons()
    {
        using var context = new BunitContext();
        var states = new[]
        {
            new W3ToggleIconButtonState("Light", "Theme mode: Light", W3IconName.Sun),
            new W3ToggleIconButtonState("Dark", "Theme mode: Dark", W3IconName.Moon),
            new W3ToggleIconButtonState("Auto", "Theme mode: Auto", W3IconName.Monitor)
        };

        var cut = context.Render<W3ToggleIconButton>(parameters => parameters
            .Add(p => p.States, states)
            .Add(p => p.Value, "Light"));

        Assert.Single(cut.FindAll("svg.w3-icon-svg"));
        Assert.Equal("Theme mode: Light", cut.Find("button").GetAttribute("aria-label"));

        cut.Find("button").Click();

        Assert.Single(cut.FindAll("svg.w3-icon-svg"));
        Assert.Equal("Theme mode: Dark", cut.Find("button").GetAttribute("aria-label"));

        cut.Find("button").Click();

        Assert.Single(cut.FindAll("svg.w3-icon-svg"));
        Assert.Equal("Theme mode: Auto", cut.Find("button").GetAttribute("aria-label"));
    }

    [Fact]
    public void FabRendersAccessibleFloatingActionButton()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Fab>(parameters => parameters
            .Add(p => p.Label, "Create task")
            .Add(p => p.Title, "Create")
            .Add(p => p.IconClass, "fa fa-plus")
            .Add(p => p.IconElementClass, "fab-icon-extra")
            .Add(p => p.Position, W3FabPosition.BottomRight)
            .Add(p => p.Offset, 18)
            .Add(p => p.Color, W3Color.Green)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Depth, W3CardDepth.Two)
            .Add(p => p.Class, "board-fab")
            .Add(p => p.Style, "margin: 0;"));

        var button = cut.Find("button");
        var icon = cut.Find("i");

        Assert.Equal("button", button.GetAttribute("type"));
        Assert.Equal("Create task", button.GetAttribute("aria-label"));
        Assert.Equal("Create", button.GetAttribute("title"));
        Assert.Contains("w3-button", button.GetAttribute("class"));
        Assert.Contains("w3-fab", button.GetAttribute("class"));
        Assert.Contains("w3-circle", button.GetAttribute("class"));
        Assert.Contains("w3-fab-absolute", button.GetAttribute("class"));
        Assert.Contains("w3-fab-bottom-right", button.GetAttribute("class"));
        Assert.Contains("w3-card-2", button.GetAttribute("class"));
        Assert.Contains("w3-green", button.GetAttribute("class"));
        Assert.Contains("w3-text-white", button.GetAttribute("class"));
        Assert.Contains("board-fab", button.GetAttribute("class"));
        Assert.Equal("--w3-fab-offset:18px;margin: 0", button.GetAttribute("style"));

        Assert.Contains("fa-plus", icon.GetAttribute("class"));
        Assert.Contains("w3-fab-icon", icon.GetAttribute("class"));
        Assert.Contains("fab-icon-extra", icon.GetAttribute("class"));
        Assert.Equal("true", icon.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void FabSupportsExtendedMiniFixedDisabledAndClickCallback()
    {
        using var context = new BunitContext();
        var clicked = false;

        var cut = context.Render<W3Fab>(parameters => parameters
            .Add(p => p.Label, "Create workflow")
            .Add(p => p.IconContent, "add")
            .Add(p => p.Extended, true)
            .Add(p => p.Mini, true)
            .Add(p => p.Fixed, true)
            .Add(p => p.Position, W3FabPosition.TopLeft)
            .Add(p => p.Disabled, true)
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .Add(p => p.ChildContent, "New workflow"));

        var button = cut.Find("button");

        Assert.Equal("Create workflow", button.GetAttribute("aria-label"));
        Assert.True(button.HasAttribute("disabled"));
        Assert.Contains("w3-fab-extended", button.GetAttribute("class"));
        Assert.Contains("w3-round-xxlarge", button.GetAttribute("class"));
        Assert.Contains("w3-fab-mini", button.GetAttribute("class"));
        Assert.Contains("w3-fab-fixed", button.GetAttribute("class"));
        Assert.Contains("w3-fab-top-left", button.GetAttribute("class"));
        Assert.Contains("w3-disabled", button.GetAttribute("class"));
        Assert.Equal("add", cut.Find("i").TextContent);
        Assert.Equal("New workflow", cut.Find(".w3-fab-label").TextContent);

        button.Click();

        Assert.False(clicked);
    }
}
