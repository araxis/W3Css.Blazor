using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3DateRangePickerTests
{
    [Fact]
    public void DateRangePickerRendersTwoDateInputsAndForwardsSharedAttributes()
    {
        using var context = new BunitContext();
        var value = new W3DateRange(new DateOnly(2026, 6, 1), new DateOnly(2026, 6, 30));
        var cut = context.Render<W3DateRangePicker>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<W3DateRange?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.FromLabel, "Start")
            .Add(p => p.ToLabel, "End")
            .Add(p => p.Min, new DateOnly(2026, 1, 1))
            .Add(p => p.Max, new DateOnly(2026, 12, 31))
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "date-range-control")
            .Add(p => p.Style, "max-width: 24rem;"));

        var labels = cut.FindAll("label");
        Assert.Equal(2, cut.FindAll("input[type='date']").Count);
        Assert.Equal("Start", labels[0].TextContent);
        Assert.Equal("End", labels[1].TextContent);

        var wrapper = cut.Find(".date-range-control");
        Assert.Contains("w3-row", wrapper.GetAttribute("class"));
        Assert.Contains("date-range-control", wrapper.GetAttribute("class"));

        var startInput = cut.FindAll("input[type='date']")[0];
        var endInput = cut.FindAll("input[type='date']")[1];
        Assert.Contains("w3-input", startInput.GetAttribute("class"));
        Assert.Contains("w3-border", startInput.GetAttribute("class"));
        Assert.Contains("w3-white", startInput.GetAttribute("class"));
        Assert.Contains("w3-text-black", startInput.GetAttribute("class"));
        Assert.Contains("w3-round", startInput.GetAttribute("class"));
        Assert.Equal("2026-06-01", startInput.GetAttribute("value"));
        Assert.Equal("2026-01-01", startInput.GetAttribute("min"));
        Assert.Equal("2026-12-31", startInput.GetAttribute("max"));

        Assert.Contains("w3-input", endInput.GetAttribute("class"));
        Assert.Contains("w3-border", endInput.GetAttribute("class"));
        Assert.Contains("w3-white", endInput.GetAttribute("class"));
        Assert.Contains("w3-text-black", endInput.GetAttribute("class"));
        Assert.Contains("w3-round", endInput.GetAttribute("class"));
        Assert.Equal("2026-06-30", endInput.GetAttribute("value"));
        Assert.Equal("max-width: 24rem;", wrapper.GetAttribute("style"));
    }

    [Fact]
    public void DateRangePickerImmediateAliasMapsToUpdateOnInputForBothInputs()
    {
        using var context = new BunitContext();
        var value = new W3DateRange(new DateOnly(2026, 6, 15), new DateOnly(2026, 6, 20));
        var cut = context.Render<W3DateRangePicker>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<W3DateRange?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Immediate, true)
            .Add(p => p.Min, new DateOnly(2026, 1, 1))
            .Add(p => p.Max, new DateOnly(2026, 12, 31)));

        var inputs = cut.FindAll("input[type='date']");
        inputs[0].Input("2026-07-01");
        inputs[1].Input("2026-07-15");

        Assert.Equal(new DateOnly(2026, 7, 15), value.End);
        Assert.Equal(new DateOnly(2026, 7, 1), value.Start);
    }

    [Fact]
    public void DateRangePickerClearingBothInputsInvokesNullValue()
    {
        using var context = new BunitContext();
        var value = new W3DateRange(new DateOnly(2026, 6, 10), new DateOnly(2026, 6, 20));
        var cut = context.Render<W3DateRangePicker>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<W3DateRange?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value));

        var inputs = cut.FindAll("input[type='date']");
        inputs[0].Change(string.Empty);
        inputs[1].Change(string.Empty);

        Assert.Null(value);
    }
}
