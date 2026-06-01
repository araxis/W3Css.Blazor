using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3NativeInputTests
{
    [Fact]
    public void TextAreaRendersNativeAttributesAndUpdatesOnChange()
    {
        using var context = new BunitContext();
        var value = "Initial note";
        var cut = context.Render<W3TextArea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next ?? string.Empty))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "team-note")
            .Add(p => p.Placeholder, "Write a note")
            .Add(p => p.Rows, 5)
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "note-control")
            .Add(p => p.Style, "min-height: 8rem;")
            .AddUnmatched("aria-label", "Team note"));

        var textArea = cut.Find("textarea");

        Assert.Equal("team-note", textArea.GetAttribute("id"));
        Assert.Contains("w3-input", textArea.GetAttribute("class"));
        Assert.Contains("w3-border", textArea.GetAttribute("class"));
        Assert.Contains("w3-white", textArea.GetAttribute("class"));
        Assert.Contains("w3-text-black", textArea.GetAttribute("class"));
        Assert.Contains("w3-round", textArea.GetAttribute("class"));
        Assert.Contains("note-control", textArea.GetAttribute("class"));
        Assert.Equal("Write a note", textArea.GetAttribute("placeholder"));
        Assert.Equal("5", textArea.GetAttribute("rows"));
        Assert.Equal("min-height: 8rem;", textArea.GetAttribute("style"));
        Assert.Equal("Team note", textArea.GetAttribute("aria-label"));
        Assert.Equal("Initial note", textArea.TextContent);

        textArea.Change("Updated note");

        Assert.Equal("Updated note", value);
    }

    [Fact]
    public void TextAreaCanUpdateOnInput()
    {
        using var context = new BunitContext();
        var value = string.Empty;
        var cut = context.Render<W3TextArea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next ?? string.Empty))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.UpdateOnInput, true));

        cut.Find("textarea").Input("Live draft");

        Assert.Equal("Live draft", value);
    }

    [Fact]
    public void NumberInputRendersNativeAttributesAndUpdatesIntegerValue()
    {
        using var context = new BunitContext();
        var value = 2;
        var cut = context.Render<W3NumberInput<int>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<int>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "seat-count")
            .Add(p => p.Placeholder, "Seats")
            .Add(p => p.Min, 1)
            .Add(p => p.Max, 12)
            .Add(p => p.Step, "1")
            .Add(p => p.Required, true)
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "numeric-control")
            .Add(p => p.Style, "max-width: 8rem;"));

        var input = cut.Find("input");

        Assert.Equal("number", input.GetAttribute("type"));
        Assert.Equal("seat-count", input.GetAttribute("id"));
        Assert.Contains("w3-input", input.GetAttribute("class"));
        Assert.Contains("w3-border", input.GetAttribute("class"));
        Assert.Contains("w3-white", input.GetAttribute("class"));
        Assert.Contains("w3-text-black", input.GetAttribute("class"));
        Assert.Contains("w3-round", input.GetAttribute("class"));
        Assert.Contains("numeric-control", input.GetAttribute("class"));
        Assert.Equal("Seats", input.GetAttribute("placeholder"));
        Assert.Equal("1", input.GetAttribute("min"));
        Assert.Equal("12", input.GetAttribute("max"));
        Assert.Equal("1", input.GetAttribute("step"));
        Assert.True(input.HasAttribute("required"));
        Assert.Equal("true", input.GetAttribute("aria-required"));
        Assert.Equal("max-width: 8rem;", input.GetAttribute("style"));

        input.Change("8");

        Assert.Equal(8, value);
    }

    [Fact]
    public void NumberInputCanUpdateOnInputForDecimalValue()
    {
        using var context = new BunitContext();
        var value = 1.25m;
        var cut = context.Render<W3NumberInput<decimal>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<decimal>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.UpdateOnInput, true)
            .Add(p => p.Step, "0.01"));

        cut.Find("input").Input("2.50");

        Assert.Equal(2.50m, value);
    }

    [Fact]
    public void DateInputFormatsBoundsAndUpdatesDateOnlyValue()
    {
        using var context = new BunitContext();
        DateOnly? value = new DateOnly(2026, 6, 15);
        var cut = context.Render<W3DateInput>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<DateOnly?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "start-date")
            .Add(p => p.Min, new DateOnly(2026, 1, 1))
            .Add(p => p.Max, new DateOnly(2026, 12, 31))
            .Add(p => p.Required, true)
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "date-control")
            .Add(p => p.Style, "max-width: 12rem;"));

        var input = cut.Find("input");

        Assert.Equal("date", input.GetAttribute("type"));
        Assert.Equal("start-date", input.GetAttribute("id"));
        Assert.Contains("w3-input", input.GetAttribute("class"));
        Assert.Contains("w3-border", input.GetAttribute("class"));
        Assert.Contains("w3-white", input.GetAttribute("class"));
        Assert.Contains("w3-text-black", input.GetAttribute("class"));
        Assert.Contains("w3-round", input.GetAttribute("class"));
        Assert.Contains("date-control", input.GetAttribute("class"));
        Assert.Equal("2026-06-15", input.GetAttribute("value"));
        Assert.Equal("2026-01-01", input.GetAttribute("min"));
        Assert.Equal("2026-12-31", input.GetAttribute("max"));
        Assert.True(input.HasAttribute("required"));
        Assert.Equal("true", input.GetAttribute("aria-required"));
        Assert.Equal("max-width: 12rem;", input.GetAttribute("style"));

        input.Change("2026-07-04");

        Assert.Equal(new DateOnly(2026, 7, 4), value);
    }

    [Fact]
    public void TimeInputFormatsBoundsAndUpdatesTimeOnlyValue()
    {
        using var context = new BunitContext();
        TimeOnly? value = new TimeOnly(9, 30);
        var cut = context.Render<W3TimeInput>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<TimeOnly?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "start-time")
            .Add(p => p.Min, new TimeOnly(8, 0))
            .Add(p => p.Max, new TimeOnly(18, 0))
            .Add(p => p.Step, "900")
            .Add(p => p.Required, true)
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "time-control")
            .Add(p => p.Style, "max-width: 10rem;"));

        var input = cut.Find("input");

        Assert.Equal("time", input.GetAttribute("type"));
        Assert.Equal("start-time", input.GetAttribute("id"));
        Assert.Contains("w3-input", input.GetAttribute("class"));
        Assert.Contains("w3-border", input.GetAttribute("class"));
        Assert.Contains("w3-white", input.GetAttribute("class"));
        Assert.Contains("w3-text-black", input.GetAttribute("class"));
        Assert.Contains("w3-round", input.GetAttribute("class"));
        Assert.Contains("time-control", input.GetAttribute("class"));
        Assert.Equal("09:30", input.GetAttribute("value"));
        Assert.Equal("08:00", input.GetAttribute("min"));
        Assert.Equal("18:00", input.GetAttribute("max"));
        Assert.Equal("900", input.GetAttribute("step"));
        Assert.True(input.HasAttribute("required"));
        Assert.Equal("true", input.GetAttribute("aria-required"));
        Assert.Equal("max-width: 10rem;", input.GetAttribute("style"));

        input.Change("10:45");

        Assert.Equal(new TimeOnly(10, 45), value);
    }

    [Fact]
    public async Task ValueInputsReportRequiredValidationStateThroughFieldMessages()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ValueInputValidationHost>();

        var isValid = await cut.InvokeAsync(() => cut.Instance.Validate());

        Assert.False(isValid);
        Assert.Contains("Enter a seat count from 1 to 12.", cut.Markup);
        Assert.Contains("Choose a start date.", cut.Markup);
        Assert.Contains("Choose a start time.", cut.Markup);

        var inputs = cut.FindAll("input");

        Assert.All(inputs, input =>
        {
            Assert.Equal("true", input.GetAttribute("aria-invalid"));
            Assert.Contains("invalid", input.GetAttribute("class"));
            Assert.True(input.HasAttribute("required"));
            Assert.Equal("true", input.GetAttribute("aria-required"));
        });

        inputs[0].Change("4");
        inputs[1].Change("2026-06-15");
        inputs[2].Change("09:30");

        isValid = await cut.InvokeAsync(() => cut.Instance.Validate());

        Assert.True(isValid);
        Assert.DoesNotContain("Enter a seat count from 1 to 12.", cut.Markup);
        Assert.DoesNotContain("Choose a start date.", cut.Markup);
        Assert.DoesNotContain("Choose a start time.", cut.Markup);
    }

    [Fact]
    public void SliderRendersRangeAttributesAndUpdatesOnInputByDefault()
    {
        using var context = new BunitContext();
        var value = 40;
        var cut = context.Render<W3Slider<int>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<int>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "volume-slider")
            .Add(p => p.Label, "Volume")
            .Add(p => p.ValueText, "40 percent")
            .Add(p => p.Min, 0)
            .Add(p => p.Max, 100)
            .Add(p => p.Step, "5")
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "slider-control")
            .Add(p => p.Style, "accent-color: teal;"));

        var input = cut.Find("input");

        Assert.Equal("range", input.GetAttribute("type"));
        Assert.Equal("volume-slider", input.GetAttribute("id"));
        Assert.Equal("Volume", input.GetAttribute("aria-label"));
        Assert.Equal("40 percent", input.GetAttribute("aria-valuetext"));
        Assert.Contains("w3-input", input.GetAttribute("class"));
        Assert.Contains("w3-border", input.GetAttribute("class"));
        Assert.Contains("w3-white", input.GetAttribute("class"));
        Assert.Contains("w3-text-black", input.GetAttribute("class"));
        Assert.Contains("w3-round", input.GetAttribute("class"));
        Assert.Contains("slider-control", input.GetAttribute("class"));
        Assert.Equal("0", input.GetAttribute("min"));
        Assert.Equal("100", input.GetAttribute("max"));
        Assert.Equal("5", input.GetAttribute("step"));
        Assert.Equal("accent-color: teal;", input.GetAttribute("style"));

        input.Input("75");

        Assert.Equal(75, value);
    }

    [Fact]
    public void SliderCanUpdateOnChangeWhenRequested()
    {
        using var context = new BunitContext();
        var value = 1.5m;
        var cut = context.Render<W3Slider<decimal>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<decimal>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Min, 0)
            .Add(p => p.Max, 5)
            .Add(p => p.Step, "0.5")
            .Add(p => p.UpdateOnInput, false));

        cut.Find("input").Change("3.5");

        Assert.Equal(3.5m, value);
    }

    [Fact]
    public void SwitchRendersAccessibleToggleAndUpdatesValue()
    {
        using var context = new BunitContext();
        var value = false;
        var cut = context.Render<W3Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<bool>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "notification-switch")
            .Add(p => p.Label, "Enable notifications")
            .Add(p => p.OnColor, W3Color.Green)
            .Add(p => p.Border, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.LabelClass, "label-extra")
            .Add(p => p.TrackClass, "track-extra")
            .Add(p => p.Required, true)
            .Add(p => p.Class, "switch-extra")
            .Add(p => p.Style, "gap: 0.75rem;"));

        var label = cut.Find("label");
        var input = cut.Find("input");
        var track = cut.Find(".w3-switch-track");
        var labelText = cut.Find(".w3-switch-label");

        Assert.Contains("Enable notifications", label.TextContent);
        Assert.Contains("w3-switch", label.GetAttribute("class"));
        Assert.Contains("switch-extra", label.GetAttribute("class"));
        Assert.Equal("gap: 0.75rem;", label.GetAttribute("style"));
        Assert.Equal("checkbox", input.GetAttribute("type"));
        Assert.Equal("notification-switch", input.GetAttribute("id"));
        Assert.Equal("switch", input.GetAttribute("role"));
        Assert.Equal("false", input.GetAttribute("aria-checked"));
        Assert.True(input.HasAttribute("required"));
        Assert.Equal("true", input.GetAttribute("aria-required"));
        Assert.Contains("w3-light-grey", track.GetAttribute("class"));
        Assert.Contains("w3-border", track.GetAttribute("class"));
        Assert.Contains("w3-round", track.GetAttribute("class"));
        Assert.Contains("track-extra", track.GetAttribute("class"));
        Assert.Contains("w3-text-black", labelText.GetAttribute("class"));
        Assert.Contains("label-extra", labelText.GetAttribute("class"));

        input.Change(true);

        Assert.True(value);
        Assert.Contains("w3-switch-on", cut.Find("label").GetAttribute("class"));
        Assert.Equal("true", cut.Find("input").GetAttribute("aria-checked"));
        Assert.Contains("w3-green", cut.Find(".w3-switch-track").GetAttribute("class"));
    }

    [Fact]
    public void ColorInputRendersNativeColorAttributesAndUpdatesValue()
    {
        using var context = new BunitContext();
        var value = "#008080";
        var cut = context.Render<W3ColorInput>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next ?? string.Empty))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "accent-color")
            .Add(p => p.FullWidth, true)
            .Add(p => p.Required, true)
            .Add(p => p.Label, "Accent color")
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "color-control")
            .Add(p => p.Style, "max-width: 14rem;"));

        var input = cut.Find("input");

        Assert.Equal("color", input.GetAttribute("type"));
        Assert.Equal("accent-color", input.GetAttribute("id"));
        Assert.True(input.HasAttribute("required"));
        Assert.Equal("true", input.GetAttribute("aria-required"));
        Assert.Equal("Accent color", input.GetAttribute("aria-label"));
        Assert.Contains("w3-input", input.GetAttribute("class"));
        Assert.Contains("w3-color-input", input.GetAttribute("class"));
        Assert.Contains("w3-color-input-block", input.GetAttribute("class"));
        Assert.Contains("w3-border", input.GetAttribute("class"));
        Assert.Contains("w3-white", input.GetAttribute("class"));
        Assert.Contains("w3-text-black", input.GetAttribute("class"));
        Assert.Contains("w3-round", input.GetAttribute("class"));
        Assert.Contains("color-control", input.GetAttribute("class"));
        Assert.Equal("max-width: 14rem;", input.GetAttribute("style"));

        input.Input("#336699");

        Assert.Equal("#336699", value);
    }

    [Fact]
    public void FileInputRendersNativeFileAttributes()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3FileInput>(parameters => parameters
            .Add(p => p.Id, "asset-file")
            .Add(p => p.Name, "asset")
            .Add(p => p.Accept, ".png,.jpg")
            .Add(p => p.Multiple, true)
            .Add(p => p.Required, true)
            .Add(p => p.AriaLabel, "Asset file")
            .Add(p => p.Border, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "file-control")
            .Add(p => p.Style, "max-width: 24rem;")
            .Add(p => p.ShowSelectedFiles, true));

        var input = cut.Find("input");

        Assert.Equal("file", input.GetAttribute("type"));
        Assert.Equal("asset-file", input.GetAttribute("id"));
        Assert.Equal("asset", input.GetAttribute("name"));
        Assert.Equal(".png,.jpg", input.GetAttribute("accept"));
        Assert.True(input.HasAttribute("multiple"));
        Assert.True(input.HasAttribute("required"));
        Assert.Equal("true", input.GetAttribute("aria-required"));
        Assert.Equal("Asset file", input.GetAttribute("aria-label"));
        Assert.Contains("w3-input", input.GetAttribute("class"));
        Assert.Contains("w3-file-input", input.GetAttribute("class"));
        Assert.Contains("w3-border", input.GetAttribute("class"));
        Assert.Contains("w3-round", input.GetAttribute("class"));
        Assert.Contains("w3-white", input.GetAttribute("class"));
        Assert.Contains("w3-text-black", input.GetAttribute("class"));
        Assert.Contains("file-control", input.GetAttribute("class"));
        Assert.Equal("max-width: 24rem;", input.GetAttribute("style"));
    }

    [Fact]
    public async Task RemainingInputsReportValidationStateThroughFieldMessages()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3InputUxValidationHost>();

        var isValid = await cut.InvokeAsync(() => cut.Instance.Validate());

        Assert.False(isValid);
        Assert.Contains("Choose a volume.", cut.Markup);
        Assert.Contains("Enable sync to continue.", cut.Markup);
        Assert.Contains("Choose a satisfaction score.", cut.Markup);
        Assert.Contains("Choose an accent color.", cut.Markup);
        Assert.Contains("Select a reviewer.", cut.Markup);

        var slider = cut.Find("#required-slider");
        var switchInput = cut.Find("#required-switch");
        var rating = cut.Find("[role='radiogroup'][aria-label='Satisfaction score']");
        var color = cut.Find("#required-color");
        var autocomplete = cut.Find("#required-reviewer");

        Assert.Equal("true", slider.GetAttribute("aria-invalid"));
        Assert.Contains("invalid", slider.GetAttribute("class"));
        Assert.Equal("true", switchInput.GetAttribute("aria-invalid"));
        Assert.Equal("true", switchInput.GetAttribute("aria-required"));
        Assert.Contains("invalid", cut.Find("label.w3-switch").GetAttribute("class"));
        Assert.Equal("true", rating.GetAttribute("aria-invalid"));
        Assert.Equal("true", rating.GetAttribute("aria-required"));
        Assert.Contains("invalid", rating.GetAttribute("class"));
        Assert.Equal("true", color.GetAttribute("aria-invalid"));
        Assert.Equal("true", color.GetAttribute("aria-required"));
        Assert.Equal("true", autocomplete.GetAttribute("aria-invalid"));
        Assert.Equal("true", autocomplete.GetAttribute("aria-required"));

        slider.Input("50");
        switchInput.Change(true);
        cut.FindAll("button[role='radio']")[2].Click();
        color.Input("#336699");
        autocomplete.Input("Ada");
        cut.Find(".w3-autocomplete-option").Click();

        isValid = await cut.InvokeAsync(() => cut.Instance.Validate());

        Assert.True(isValid);
        Assert.DoesNotContain("Enable sync to continue.", cut.Markup);
        Assert.DoesNotContain("Choose a satisfaction score.", cut.Markup);
        Assert.DoesNotContain("Choose an accent color.", cut.Markup);
        Assert.DoesNotContain("Select a reviewer.", cut.Markup);
    }
}
