using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3DisplayUtilityTests
{
    [Fact]
    public void DisplayContainerAndDisplayRenderPositionedOverlay()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3DisplayContainer>(parameters => parameters
            .Add(p => p.Color, W3Color.LightGrey)
            .Add(p => p.Class, "w3-border custom-display-container")
            .Add(p => p.Style, "max-width:360px")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3Display>(0);
                builder.AddAttribute(1, nameof(W3Display.Position), W3DisplayPosition.BottomRight);
                builder.AddAttribute(2, nameof(W3Display.ShowOnHover), true);
                builder.AddAttribute(3, nameof(W3Display.Color), W3Color.Black);
                builder.AddAttribute(4, nameof(W3Display.TextColor), W3Color.White);
                builder.AddAttribute(5, nameof(W3Display.Class), "w3-padding custom-display");
                builder.AddAttribute(6, nameof(W3Display.ChildContent), (RenderFragment)(content => content.AddContent(0, "Caption")));
                builder.CloseComponent();
            }));

        var outer = cut.Find(".w3-display-container");
        var display = cut.Find(".w3-display-bottomright");

        Assert.Contains("w3-light-grey", outer.GetAttribute("class"));
        Assert.Contains("w3-border", outer.GetAttribute("class"));
        Assert.Contains("custom-display-container", outer.GetAttribute("class"));
        Assert.Equal("max-width:360px", outer.GetAttribute("style"));
        Assert.Contains("w3-display-hover", display.GetAttribute("class"));
        Assert.Contains("w3-black", display.GetAttribute("class"));
        Assert.Contains("w3-text-white", display.GetAttribute("class"));
        Assert.Contains("w3-padding", display.GetAttribute("class"));
        Assert.Contains("custom-display", display.GetAttribute("class"));
        Assert.Equal("Caption", display.TextContent);
    }

    [Fact]
    public void DisplaySupportsVisibilityAndCustomPosition()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Display>(parameters => parameters
            .Add(p => p.Position, W3DisplayPosition.Custom)
            .Add(p => p.Visibility, W3Visibility.ShowInlineBlock)
            .Add(p => p.Class, "custom-position")
            .Add(p => p.Style, "left: 12px; top: 20px;")
            .Add(p => p.ChildContent, "Custom"));

        var display = cut.Find("div");

        Assert.Contains("w3-display-position", display.GetAttribute("class"));
        Assert.Contains("w3-show-inline-block", display.GetAttribute("class"));
        Assert.Contains("custom-position", display.GetAttribute("class"));
        Assert.Equal("left: 12px; top: 20px;", display.GetAttribute("style"));
    }

    [Theory]
    [InlineData(W3Visibility.Hide, "w3-hide")]
    [InlineData(W3Visibility.Show, "w3-show")]
    [InlineData(W3Visibility.ShowBlock, "w3-show-block")]
    [InlineData(W3Visibility.ShowInlineBlock, "w3-show-inline-block")]
    [InlineData(W3Visibility.HideSmall, "w3-hide-small")]
    [InlineData(W3Visibility.HideMedium, "w3-hide-medium")]
    [InlineData(W3Visibility.HideLarge, "w3-hide-large")]
    public void DisplayContainerMapsVisibilityClasses(W3Visibility visibility, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3DisplayContainer>(parameters => parameters
            .Add(p => p.Visibility, visibility)
            .Add(p => p.ChildContent, "Visible content"));

        var container = cut.Find("div");

        Assert.Contains("w3-display-container", container.GetAttribute("class"));
        Assert.Contains(expectedClass, container.GetAttribute("class"));
        Assert.Equal("Visible content", container.TextContent);
    }

    [Fact]
    public void CellRowAndCellRenderAlignmentAndMobileClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3CellRow>(parameters => parameters
            .Add(p => p.Class, "w3-white custom-cell-row")
            .Add(p => p.Style, "table-layout:fixed")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3Cell>(0);
                builder.AddAttribute(1, nameof(W3Cell.Alignment), W3CellAlignment.Middle);
                builder.AddAttribute(2, nameof(W3Cell.Mobile), true);
                builder.AddAttribute(3, nameof(W3Cell.Color), W3Color.PaleBlue);
                builder.AddAttribute(4, nameof(W3Cell.Class), "custom-cell");
                builder.AddAttribute(5, nameof(W3Cell.Style), "width:96px");
                builder.AddAttribute(6, nameof(W3Cell.ChildContent), (RenderFragment)(content => content.AddContent(0, "Middle cell")));
                builder.CloseComponent();
            }));

        var row = cut.Find(".w3-cell-row");
        var cell = cut.Find(".w3-cell");

        Assert.NotNull(row);
        Assert.Contains("w3-white", row.GetAttribute("class"));
        Assert.Contains("custom-cell-row", row.GetAttribute("class"));
        Assert.Equal("table-layout:fixed", row.GetAttribute("style"));
        Assert.Contains("w3-cell-middle", cell.GetAttribute("class"));
        Assert.Contains("w3-mobile", cell.GetAttribute("class"));
        Assert.Contains("w3-pale-blue", cell.GetAttribute("class"));
        Assert.Contains("custom-cell", cell.GetAttribute("class"));
        Assert.Equal("width:96px", cell.GetAttribute("style"));
        Assert.Equal("Middle cell", cell.TextContent);
    }
}
