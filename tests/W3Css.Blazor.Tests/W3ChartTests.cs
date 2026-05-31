using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;
using W3Css.Blazor.Internal;

namespace W3Css.Blazor.Tests;

public sealed class W3ChartTests
{
    private static readonly string[] None = [];

    [Fact]
    public void BarGeometryProducesOneBarPerSeriesPerCategory()
    {
        var series = new[]
        {
            new W3ChartSeries("A", new double[] { 1, 2, 3, 4 }),
            new W3ChartSeries("B", new double[] { 4, 3, 2, 1 })
        };

        var layout = W3ChartGeometry.Build(W3ChartType.Bar, series, new[] { "Q1", "Q2", "Q3", "Q4" }, None, 420, 260, true, "#fff");

        Assert.Equal(8, layout.Bars.Count);
        Assert.Empty(layout.Slices);
        Assert.Equal(2, layout.Legend.Count);
        Assert.NotNull(layout.AxisX);
        Assert.NotEmpty(layout.Grid);
        Assert.All(layout.Bars, bar => Assert.True(bar.Height >= 0));
    }

    [Fact]
    public void LineGeometryProducesPolylinePerSeries()
    {
        var series = new[] { new W3ChartSeries("A", new double[] { 1, 2, 3, 4 }) };

        var layout = W3ChartGeometry.Build(W3ChartType.Line, series, new[] { "Q1", "Q2", "Q3", "Q4" }, None, 420, 260, true, "#fff");

        Assert.Single(layout.Lines);
        Assert.Contains(",", layout.Lines[0].Points);
        Assert.Empty(layout.Bars);
    }

    [Fact]
    public void PieGeometryProducesSlicePerPositiveValueWithoutHole()
    {
        var series = new[] { new W3ChartSeries("T", new double[] { 42, 30, 18, 10 }) };

        var layout = W3ChartGeometry.Build(W3ChartType.Pie, series, new[] { "a", "b", "c", "d" }, None, 260, 260, false, "#fff");

        Assert.Equal(4, layout.Slices.Count);
        Assert.Equal(4, layout.Legend.Count);
        Assert.Null(layout.Hole);
        Assert.All(layout.Slices, slice => Assert.False(slice.FullCircle));
    }

    [Fact]
    public void DonutAddsHoleAndSingleValueIsFullCircle()
    {
        var donut = W3ChartGeometry.Build(W3ChartType.Donut, new[] { new W3ChartSeries("T", new double[] { 1, 2, 3 }) }, None, None, 260, 260, false, "#fff");
        Assert.NotNull(donut.Hole);

        var single = W3ChartGeometry.Build(W3ChartType.Pie, new[] { new W3ChartSeries("T", new double[] { 5 }) }, None, None, 260, 260, false, "#fff");
        Assert.Single(single.Slices);
        Assert.True(single.Slices[0].FullCircle);
    }

    [Fact]
    public void EmptySeriesProducesNoMarksAndDoesNotThrow()
    {
        var layout = W3ChartGeometry.Build(W3ChartType.Bar, Array.Empty<W3ChartSeries>(), None, None, 420, 260, true, "#fff");

        Assert.Empty(layout.Bars);
        Assert.Empty(layout.Slices);
    }

    [Fact]
    public void NullSeriesDataIsTreatedAsEmptyAndDoesNotThrow()
    {
        var withNullData = new[] { new W3ChartSeries { Name = "X", Data = null! } };

        // Null data is treated as empty: no categories are inferred, so nothing is plotted (and no NRE).
        var bar = W3ChartGeometry.Build(W3ChartType.Bar, withNullData, None, None, 420, 260, true, "#fff");
        Assert.Empty(bar.Bars);

        var pie = W3ChartGeometry.Build(W3ChartType.Pie, withNullData, None, None, 260, 260, false, "#fff");
        Assert.Empty(pie.Slices);

        // With an explicit label the guard still avoids a throw (the value reads as 0).
        var barWithLabel = W3ChartGeometry.Build(W3ChartType.Bar, withNullData, new[] { "a" }, None, 420, 260, true, "#fff");
        Assert.All(barWithLabel.Bars, b => Assert.Equal(0, b.Value));
    }

    [Fact]
    public void RendersSvgBarsAndLegend()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Chart>(parameters => parameters
            .Add(c => c.ChartType, W3ChartType.Bar)
            .Add(c => c.Labels, new[] { "Q1", "Q2" })
            .Add(c => c.Series, new[] { new W3ChartSeries("A", new double[] { 3, 5 }) }));

        var svg = cut.Find("svg");
        Assert.Equal("img", svg.GetAttribute("role"));
        Assert.Equal(2, cut.FindAll("rect").Count);
        Assert.NotEmpty(cut.FindAll(".w3-chart-legend-item"));
    }

    [Fact]
    public void RendersDonutSlicesAndHole()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Chart>(parameters => parameters
            .Add(c => c.ChartType, W3ChartType.Donut)
            .Add(c => c.Series, new[] { new W3ChartSeries("T", new double[] { 2, 3, 5 }) }));

        Assert.Equal(3, cut.FindAll("path").Count);
        Assert.NotEmpty(cut.FindAll("circle"));
    }
}
