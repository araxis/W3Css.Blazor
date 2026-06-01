using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3TimelineRatingTests
{
    [Fact]
    public void TimelineRendersSemanticListAndItems()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Timeline>(parameters => parameters
            .Add(p => p.Label, "Release history")
            .Add(p => p.Dense, true)
            .Add(p => p.Gap, 12)
            .Add(p => p.LineColor, W3Color.PaleBlue)
            .Add(p => p.MarkerColor, W3Color.Indigo)
            .Add(p => p.MarkerTextColor, W3Color.White)
            .Add(p => p.ContentColor, W3Color.White)
            .Add(p => p.Class, "timeline-extra")
            .Add(p => p.Style, "max-width: 36rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3TimelineItem>(0);
                builder.AddAttribute(1, nameof(W3TimelineItem.Marker), "1");
                builder.AddAttribute(2, nameof(W3TimelineItem.Title), "Scope locked");
                builder.AddAttribute(3, nameof(W3TimelineItem.Subtitle), "Design system");
                builder.AddAttribute(4, nameof(W3TimelineItem.Time), "09:00");
                builder.AddAttribute(5, nameof(W3TimelineItem.DateTime), "2026-05-26T09:00:00");
                builder.AddAttribute(6, nameof(W3TimelineItem.Active), true);
                builder.AddAttribute(7, nameof(W3TimelineItem.ContentColor), W3Color.PaleBlue);
                builder.AddAttribute(8, nameof(W3TimelineItem.ChildContent), (RenderFragment)(child => child.AddContent(0, "Backlog grouped.")));
                builder.CloseComponent();

                builder.OpenComponent<W3TimelineItem>(9);
                builder.AddAttribute(10, nameof(W3TimelineItem.IconClass), "fa fa-check");
                builder.AddAttribute(11, nameof(W3TimelineItem.Title), "Verified");
                builder.AddAttribute(12, nameof(W3TimelineItem.Completed), true);
                builder.CloseComponent();
            }));

        var list = cut.Find("ol[role='list']");
        var items = cut.FindAll(".w3-timeline-item");
        var firstMarker = items[0].QuerySelector(".w3-timeline-marker")!;
        var firstContent = items[0].QuerySelector(".w3-timeline-content")!;
        var firstLine = items[0].QuerySelector(".w3-timeline-line")!;

        Assert.Equal("Release history", list.GetAttribute("aria-label"));
        Assert.Contains("w3-timeline-dense", list.GetAttribute("class"));
        Assert.Contains("timeline-extra", list.GetAttribute("class"));
        Assert.Equal("--w3-timeline-gap:12px;--w3-timeline-content-padding:8px 10px;max-width: 36rem", list.GetAttribute("style"));
        Assert.Equal(2, items.Count);
        Assert.Contains("w3-timeline-item-active", items[0].GetAttribute("class"));
        Assert.Contains("w3-timeline-item-completed", items[1].GetAttribute("class"));
        Assert.Contains("w3-circle", firstMarker.GetAttribute("class"));
        Assert.Contains("w3-indigo", firstMarker.GetAttribute("class"));
        Assert.Contains("w3-text-white", firstMarker.GetAttribute("class"));
        Assert.Contains("w3-border-pale-blue", firstLine.GetAttribute("class"));
        Assert.Contains("w3-pale-blue", firstContent.GetAttribute("class"));
        Assert.Contains("w3-leftbar", firstContent.GetAttribute("class"));
        Assert.Contains("w3-border-indigo", firstContent.GetAttribute("class"));
        Assert.Equal("Scope locked", items[0].QuerySelector(".w3-timeline-title")!.TextContent);
        Assert.Equal("2026-05-26T09:00:00", items[0].QuerySelector("time")!.GetAttribute("datetime"));
        Assert.Contains("Backlog grouped.", items[0].TextContent);
        Assert.Contains("fa-check", items[1].InnerHtml);
    }

    [Fact]
    public void RatingSupportsSelectionAndClearableState()
    {
        using var context = new BunitContext();
        var value = 2;
        var cut = context.Render<W3Rating>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, next => value = next)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Max, 5)
            .Add(p => p.Label, "Satisfaction")
            .Add(p => p.Required, true)
            .Add(p => p.Clearable, true)
            .Add(p => p.ShowValue, true)
            .Add(p => p.Gap, 4)
            .Add(p => p.Class, "rating-extra")
            .Add(p => p.Style, "margin: 0;"));

        var group = cut.Find("[role='radiogroup']");
        var buttons = cut.FindAll("button[role='radio']");

        Assert.Equal("Satisfaction", group.GetAttribute("aria-label"));
        Assert.Equal("true", group.GetAttribute("aria-required"));
        Assert.Equal("false", group.GetAttribute("aria-readonly"));
        Assert.Equal("false", group.GetAttribute("aria-disabled"));
        Assert.Contains("rating-extra", group.GetAttribute("class"));
        Assert.Equal("--w3-rating-gap:4px;margin: 0", group.GetAttribute("style"));
        Assert.Equal(5, buttons.Count);
        Assert.Equal("true", buttons[1].GetAttribute("aria-checked"));
        Assert.Contains("w3-text-amber", buttons[0].GetAttribute("class"));
        Assert.Contains("w3-text-grey", buttons[4].GetAttribute("class"));
        Assert.Contains("2 / 5", cut.Find(".w3-rating-value").TextContent);

        buttons[2].Click();

        buttons = cut.FindAll("button[role='radio']");
        Assert.Equal(3, value);
        Assert.Equal("true", buttons[2].GetAttribute("aria-checked"));
        Assert.Contains("3 / 5", cut.Find(".w3-rating-value").TextContent);

        buttons[2].Click();

        Assert.Equal(0, value);
        Assert.All(cut.FindAll("button[role='radio']"), button => Assert.Equal("false", button.GetAttribute("aria-checked")));
        Assert.Contains("0 / 5", cut.Find(".w3-rating-value").TextContent);
    }

    [Fact]
    public void RatingSupportsKeyboardAdjustment()
    {
        using var context = new BunitContext();
        var value = 2;
        var cut = context.Render<W3Rating>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, next => value = next)
            .Add(p => p.Max, 5)
            .Add(p => p.Clearable, true)
            .Add(p => p.ShowValue, true));

        var ratingButton = cut.Find("button[role='radio']");

        ratingButton.KeyDown(new KeyboardEventArgs { Key = "ArrowRight" });
        Assert.Equal(3, value);
        Assert.Equal("true", cut.FindAll("button[role='radio']")[2].GetAttribute("aria-checked"));

        ratingButton.KeyDown(new KeyboardEventArgs { Key = "ArrowLeft" });
        Assert.Equal(2, value);

        ratingButton.KeyDown(new KeyboardEventArgs { Key = "End" });
        Assert.Equal(5, value);
        Assert.Contains("5 / 5", cut.Find(".w3-rating-value").TextContent);

        ratingButton.KeyDown(new KeyboardEventArgs { Key = "Home" });
        Assert.Equal(1, value);

        ratingButton.KeyDown(new KeyboardEventArgs { Key = "Delete" });
        Assert.Equal(0, value);
        Assert.Contains("0 / 5", cut.Find(".w3-rating-value").TextContent);
    }

    [Fact]
    public void RatingKeyboardDecreaseStopsAtOneWhenNotClearable()
    {
        using var context = new BunitContext();
        var value = 1;
        var cut = context.Render<W3Rating>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, next => value = next)
            .Add(p => p.Max, 5));

        cut.Find("button[role='radio']").KeyDown(new KeyboardEventArgs { Key = "ArrowLeft" });
        Assert.Equal(1, value);

        cut.Find("button[role='radio']").KeyDown(new KeyboardEventArgs { Key = "Backspace" });
        Assert.Equal(1, value);
    }

    [Fact]
    public void RatingSupportsReadOnlyCustomIcons()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Rating>(parameters => parameters
            .Add(p => p.Value, 4)
            .Add(p => p.ReadOnly, true)
            .Add(p => p.IconClass, "fa fa-heart")
            .Add(p => p.EmptyIconClass, "fa fa-heart-o")
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.EmptyColor, W3Color.LightGrey));

        var group = cut.Find("[role='radiogroup']");
        var buttons = cut.FindAll("button[role='radio']");

        Assert.Equal("true", group.GetAttribute("aria-readonly"));
        Assert.Contains("w3-rating-readonly", group.GetAttribute("class"));
        Assert.All(buttons, button => Assert.True(button.HasAttribute("disabled")));
        Assert.Contains("fa-heart", buttons[3].InnerHtml);
        Assert.Contains("w3-text-teal", buttons[3].GetAttribute("class"));
        Assert.Contains("fa-heart-o", buttons[4].InnerHtml);
        Assert.Contains("w3-text-light-grey", buttons[4].GetAttribute("class"));
    }
}
