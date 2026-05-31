using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ProgressCircularTests
{
    [Fact]
    public void ProgressCircularRendersDeterminateStateWithTrackAndIndicatorStyles()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ProgressCircular>(parameters => parameters
            .Add(p => p.Value, 75)
            .Add(p => p.Max, 100)
            .Add(p => p.Size, 64)
            .Add(p => p.Thickness, 6)
            .Add(p => p.ShowLabel, true)
            .Add(p => p.Label, "Uploading")
            .Add(p => p.Color, W3Color.Indigo)
            .Add(p => p.TrackColor, W3Color.LightGrey)
            .Add(p => p.TextColor, W3Color.Black));

        var progress = cut.Find("[role='progressbar']");
        var track = cut.Find(".w3-progress-circular-track");
        var indicator = cut.Find(".w3-progress-circular-indicator");
        var label = cut.Find(".w3-progress-circular-label");
        var indicatorStyle = indicator.GetAttribute("style") ?? string.Empty;

        Assert.Contains("w3-progress-circular", progress.GetAttribute("class"));
        Assert.Contains("w3-progress-circular-determinate", progress.GetAttribute("class"));
        Assert.Contains("w3-progress-circular-label", label.GetAttribute("class"));
        Assert.Equal("Uploading", label.TextContent);
        Assert.Contains("w3-text-black", progress.GetAttribute("class"));
        Assert.Contains("w3-text-indigo", indicator.GetAttribute("class"));
        Assert.Contains("w3-text-light-grey", track.GetAttribute("class"));
        Assert.Contains("stroke-dasharray:100", indicatorStyle);
        Assert.Contains("stroke-dashoffset:25", indicatorStyle);
        Assert.Equal("width: 64px; height: 64px;", progress.GetAttribute("style"));
    }

    [Fact]
    public void ProgressCircularRendersIndeterminateStateAsSpinningRing()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ProgressCircular>(parameters => parameters
            .Add(p => p.Indeterminate, true)
            .Add(p => p.Size, 32)
            .Add(p => p.Thickness, 4));

        var progress = cut.Find("[role='progressbar']");
        var circles = cut.FindAll("circle");
        var indicator = cut.Find(".w3-progress-circular-indicator");

        Assert.Contains("w3-progress-circular-indeterminate", progress.GetAttribute("class"));
        Assert.DoesNotContain("w3-progress-circular-determinate", progress.GetAttribute("class"));
        Assert.Equal(2, circles.Count);
        Assert.Equal("indeterminate", progress.GetAttribute("aria-valuetext"));
        Assert.DoesNotContain("stroke-dasharray", indicator.GetAttribute("style") ?? string.Empty);
    }

    [Fact]
    public void ProgressCircularDefaultsToComputedLabelWithoutCustomLabel()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ProgressCircular>(parameters => parameters
            .Add(p => p.Value, 33)
            .Add(p => p.Max, 100)
            .Add(p => p.ShowLabel, true));

        var label = cut.Find(".w3-progress-circular-label");

        Assert.Equal("33%", label.TextContent);
    }
}
