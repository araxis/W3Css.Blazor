using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3CompositionPrimitiveTests
{
    [Fact]
    public void DividerRendersPlainAndLabeledSeparators()
    {
        using var context = new BunitContext();
        var plain = context.Render<W3Divider>(parameters => parameters
            .Add(p => p.BorderColor, W3Color.Teal)
            .Add(p => p.Strong, true)
            .Add(p => p.Class, "divider-extra")
            .Add(p => p.Style, "max-width: 20rem;"));
        var labeled = context.Render<W3Divider>(parameters => parameters
            .Add(p => p.TextColor, W3Color.DarkGrey)
            .Add(p => p.BorderColor, W3Color.Grey)
            .Add(p => p.ChildContent, "Advanced options"));

        var separator = plain.Find("hr");
        var labeledSeparator = labeled.Find("div");

        Assert.Equal("separator", separator.GetAttribute("role"));
        Assert.Equal("horizontal", separator.GetAttribute("aria-orientation"));
        Assert.Contains("w3-divider", separator.GetAttribute("class"));
        Assert.Contains("w3-divider-strong", separator.GetAttribute("class"));
        Assert.Contains("w3-margin-top", separator.GetAttribute("class"));
        Assert.Contains("w3-margin-bottom", separator.GetAttribute("class"));
        Assert.Contains("w3-border-teal", separator.GetAttribute("class"));
        Assert.Contains("divider-extra", separator.GetAttribute("class"));
        Assert.Equal("max-width: 20rem;", separator.GetAttribute("style"));

        Assert.Equal("separator", labeledSeparator.GetAttribute("role"));
        Assert.Contains("w3-text-dark-grey", labeledSeparator.GetAttribute("class"));
        Assert.Contains("w3-border-grey", labeledSeparator.GetAttribute("class"));
        Assert.Contains("Advanced options", labeledSeparator.TextContent);
    }

    [Fact]
    public void StackRendersDirectionalLayoutClassesAndGap()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Stack>(parameters => parameters
            .Add(p => p.Horizontal, true)
            .Add(p => p.Wrap, true)
            .Add(p => p.AlignCenter, true)
            .Add(p => p.JustifyCenter, true)
            .Add(p => p.JustifyEnd, true)
            .Add(p => p.JustifyBetween, true)
            .Add(p => p.Gap, 10)
            .Add(p => p.Color, W3Color.PaleBlue)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "stack-extra")
            .Add(p => p.Style, "max-width: 40rem;")
            .Add(p => p.ChildContent, "<span>One</span><span>Two</span>"));

        var stack = cut.Find("div");

        Assert.Contains("w3-stack", stack.GetAttribute("class"));
        Assert.Contains("w3-stack-horizontal", stack.GetAttribute("class"));
        Assert.Contains("w3-stack-wrap", stack.GetAttribute("class"));
        Assert.Contains("w3-stack-align-center", stack.GetAttribute("class"));
        Assert.Contains("w3-stack-justify-center", stack.GetAttribute("class"));
        Assert.Contains("w3-stack-justify-end", stack.GetAttribute("class"));
        Assert.Contains("w3-stack-justify-between", stack.GetAttribute("class"));
        Assert.Contains("w3-pale-blue", stack.GetAttribute("class"));
        Assert.Contains("w3-text-black", stack.GetAttribute("class"));
        Assert.Contains("stack-extra", stack.GetAttribute("class"));
        Assert.Equal("--w3-stack-gap:10px;max-width: 40rem", stack.GetAttribute("style"));
        Assert.Contains("One", stack.TextContent);
        Assert.Contains("Two", stack.TextContent);
    }

    [Fact]
    public void ToolbarRendersMainContentActionsAndAccessibility()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Toolbar>(parameters => parameters
            .Add(p => p.Label, "Dashboard actions")
            .Add(p => p.Dense, true)
            .Add(p => p.Wrap, true)
            .Add(p => p.Border, true)
            .Add(p => p.Card, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Gap, 6)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "toolbar-extra")
            .Add(p => p.Style, "margin-bottom: 1rem;")
            .Add(p => p.ChildContent, "Filters")
            .Add(p => p.Actions, "Export"));

        var toolbar = cut.Find("[role='toolbar']");

        Assert.Equal("Dashboard actions", toolbar.GetAttribute("aria-label"));
        Assert.Contains("w3-toolbar", toolbar.GetAttribute("class"));
        Assert.Contains("w3-toolbar-dense", toolbar.GetAttribute("class"));
        Assert.Contains("w3-toolbar-wrap", toolbar.GetAttribute("class"));
        Assert.Contains("w3-border", toolbar.GetAttribute("class"));
        Assert.Contains("w3-card-2", toolbar.GetAttribute("class"));
        Assert.Contains("w3-round", toolbar.GetAttribute("class"));
        Assert.Contains("w3-white", toolbar.GetAttribute("class"));
        Assert.Contains("w3-text-black", toolbar.GetAttribute("class"));
        Assert.Contains("toolbar-extra", toolbar.GetAttribute("class"));
        Assert.Equal("--w3-toolbar-gap:6px;margin-bottom: 1rem", toolbar.GetAttribute("style"));
        Assert.Contains("Filters", cut.Find(".w3-toolbar-content").TextContent);
        Assert.Contains("Export", cut.Find(".w3-toolbar-actions").TextContent);
    }

    [Fact]
    public void ButtonGroupRendersGroupedButtons()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ButtonGroup>(parameters => parameters
            .Add(p => p.Label, "View mode")
            .Add(p => p.Vertical, true)
            .Add(p => p.Connected, false)
            .Add(p => p.Border, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Gap, 4)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "group-extra")
            .Add(p => p.Style, "min-width: 10rem;")
            .Add(p => p.ChildContent, "<button class=\"w3-button\">List</button><button class=\"w3-button\">Board</button>"));

        var group = cut.Find("[role='group']");

        Assert.Equal("View mode", group.GetAttribute("aria-label"));
        Assert.Contains("w3-button-group", group.GetAttribute("class"));
        Assert.Contains("w3-button-group-vertical", group.GetAttribute("class"));
        Assert.DoesNotContain("w3-button-group-connected", group.GetAttribute("class"));
        Assert.Contains("w3-border", group.GetAttribute("class"));
        Assert.Contains("w3-round", group.GetAttribute("class"));
        Assert.Contains("w3-white", group.GetAttribute("class"));
        Assert.Contains("w3-text-black", group.GetAttribute("class"));
        Assert.Contains("group-extra", group.GetAttribute("class"));
        Assert.Equal("--w3-button-group-gap:4px;min-width: 10rem", group.GetAttribute("style"));
        Assert.Equal(2, cut.FindAll("button").Count);
    }

    [Fact]
    public void ActionRowRendersWrappingAlignedActionGroup()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ActionRow>(parameters => parameters
            .Add(p => p.Label, "Form actions")
            .Add(p => p.Wrap, true)
            .Add(p => p.JustifyStart, true)
            .Add(p => p.Border, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Gap, 12)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "actions-extra")
            .Add(p => p.Style, "margin-top: 1rem;")
            .Add(p => p.ChildContent, "<button class=\"w3-button\">Cancel</button><button class=\"w3-button w3-teal\">Save</button>"));

        var row = cut.Find("[role='group']");

        Assert.Equal("Form actions", row.GetAttribute("aria-label"));
        Assert.Contains("w3-action-row", row.GetAttribute("class"));
        Assert.Contains("w3-action-row-wrap", row.GetAttribute("class"));
        Assert.Contains("w3-action-row-start", row.GetAttribute("class"));
        Assert.Contains("w3-border", row.GetAttribute("class"));
        Assert.Contains("w3-round", row.GetAttribute("class"));
        Assert.Contains("w3-white", row.GetAttribute("class"));
        Assert.Contains("w3-text-black", row.GetAttribute("class"));
        Assert.Contains("actions-extra", row.GetAttribute("class"));
        Assert.Equal("--w3-action-row-gap:12px;margin-top: 1rem", row.GetAttribute("style"));
        Assert.Equal(2, cut.FindAll("button").Count);
    }

    [Fact]
    public void ActionRowUsesLabelledByWhenProvided()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ActionRow>(parameters => parameters
            .Add(p => p.Label, "Ignored label")
            .Add(p => p.AriaLabelledBy, "actions-heading")
            .Add(p => p.JustifyCenter, true)
            .Add(p => p.ChildContent, "Save"));

        var row = cut.Find(".w3-action-row");

        Assert.Null(row.GetAttribute("aria-label"));
        Assert.Equal("actions-heading", row.GetAttribute("aria-labelledby"));
        Assert.Contains("w3-action-row-center", row.GetAttribute("class"));
    }
}
