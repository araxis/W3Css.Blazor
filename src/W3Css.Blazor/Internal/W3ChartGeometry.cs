using System.Globalization;

namespace W3Css.Blazor.Internal;

/// <summary>
/// Pure geometry for <c>W3Chart</c>: turns series/values into SVG primitives.
/// Kept dependency-free and side-effect-free so it can be unit-tested directly.
/// </summary>
internal static class W3ChartGeometry
{
    public static readonly IReadOnlyList<string> DefaultPalette =
    [
        "#2196f3", "#4caf50", "#ff9800", "#9c27b0",
        "#f44336", "#00bcd4", "#cddc39", "#795548"
    ];

    public static W3ChartLayout Build(
        W3ChartType type,
        IReadOnlyList<W3ChartSeries> series,
        IReadOnlyList<string> labels,
        IReadOnlyList<string> palette,
        double width,
        double height,
        bool showAxisLabels,
        string holeColor)
    {
        var colors = palette is { Count: > 0 } ? palette : DefaultPalette;

        return type switch
        {
            W3ChartType.Pie or W3ChartType.Donut => BuildPie(type, series, labels, colors, width, height, holeColor),
            W3ChartType.Line => BuildCartesian(W3ChartType.Line, series, labels, colors, width, height, showAxisLabels),
            _ => BuildCartesian(W3ChartType.Bar, series, labels, colors, width, height, showAxisLabels)
        };
    }

    private static W3ChartLayout BuildCartesian(
        W3ChartType type,
        IReadOnlyList<W3ChartSeries> series,
        IReadOnlyList<string> labels,
        IReadOnlyList<string> colors,
        double width,
        double height,
        bool showAxisLabels)
    {
        var bars = new List<W3ChartBar>();
        var lines = new List<W3ChartLine>();
        var axisLabels = new List<W3ChartAxisLabel>();
        var grid = new List<W3ChartGridLine>();
        var legend = new List<W3ChartLegendItem>();

        var categories = labels.Count > 0
            ? labels.Count
            : series.Count > 0 ? series.Max(s => s.Data?.Count ?? 0) : 0;

        const double left = 44, right = 16, top = 16;
        var bottom = showAxisLabels ? 34 : 16;
        var plotW = width - left - right;
        var plotH = height - top - bottom;
        var baseY = top + plotH;

        var max = series.SelectMany(s => s.Data ?? (IReadOnlyList<double>)[]).DefaultIfEmpty(0).Max();
        if (max <= 0)
        {
            max = 1;
        }

        // Y gridlines + labels at 0, mid, max.
        foreach (var fraction in new[] { 0d, 0.5d, 1d })
        {
            var y = baseY - fraction * plotH;
            grid.Add(new W3ChartGridLine(left, y, left + plotW, y));
            axisLabels.Add(new W3ChartAxisLabel(left - 6, y + 4, Format(max * fraction), "end"));
        }

        for (var s = 0; s < series.Count; s++)
        {
            legend.Add(new W3ChartLegendItem(ResolveColor(series[s].Color, colors, s), series[s].Name));
        }

        if (categories > 0)
        {
            if (type == W3ChartType.Bar)
            {
                var groupW = plotW / categories;
                var innerPad = groupW * 0.15;
                var barAreaW = groupW - innerPad * 2;
                var barW = barAreaW / Math.Max(1, series.Count);

                for (var i = 0; i < categories; i++)
                {
                    for (var s = 0; s < series.Count; s++)
                    {
                        var v = ValueAt(series[s].Data, i);
                        var h = v / max * plotH;
                        var x = left + i * groupW + innerPad + s * barW;
                        var y = baseY - h;
                        bars.Add(new W3ChartBar(x, y, Math.Max(0, barW - 2), Math.Max(0, h), ResolveColor(series[s].Color, colors, s), LabelAt(labels, i), v));
                    }
                }
            }
            else
            {
                var step = categories > 1 ? plotW / (categories - 1) : 0;

                for (var s = 0; s < series.Count; s++)
                {
                    var points = new List<string>(categories);
                    for (var i = 0; i < categories; i++)
                    {
                        var v = ValueAt(series[s].Data, i);
                        var x = left + (categories > 1 ? i * step : plotW / 2);
                        var y = baseY - v / max * plotH;
                        points.Add($"{Format(x)},{Format(y)}");
                    }

                    lines.Add(new W3ChartLine(string.Join(' ', points), ResolveColor(series[s].Color, colors, s), series[s].Name));
                }
            }

            if (showAxisLabels)
            {
                var groupW = plotW / categories;
                var step = categories > 1 ? plotW / (categories - 1) : 0;
                for (var i = 0; i < categories; i++)
                {
                    var x = type == W3ChartType.Bar
                        ? left + i * groupW + groupW / 2
                        : left + (categories > 1 ? i * step : plotW / 2);
                    axisLabels.Add(new W3ChartAxisLabel(x, baseY + 18, LabelAt(labels, i), "middle"));
                }
            }
        }

        return new W3ChartLayout
        {
            Width = width,
            Height = height,
            Bars = bars,
            Lines = lines,
            Grid = grid,
            Labels = axisLabels,
            Legend = legend,
            AxisX = new W3ChartGridLine(left, baseY, left + plotW, baseY),
            AxisY = new W3ChartGridLine(left, top, left, baseY)
        };
    }

    private static W3ChartLayout BuildPie(
        W3ChartType type,
        IReadOnlyList<W3ChartSeries> series,
        IReadOnlyList<string> labels,
        IReadOnlyList<string> colors,
        double width,
        double height,
        string holeColor)
    {
        var slices = new List<W3ChartSlice>();
        var legend = new List<W3ChartLegendItem>();

        var values = series.Count > 0 ? series[0].Data ?? [] : [];
        var total = values.Where(v => v > 0).Sum();

        var cx = width / 2;
        var cy = height / 2;
        var r = Math.Min(width, height) / 2 - 8;

        if (total > 0)
        {
            var angle = -Math.PI / 2;
            for (var i = 0; i < values.Count; i++)
            {
                var v = values[i];
                if (v <= 0)
                {
                    continue;
                }

                var color = colors[i % colors.Count];
                var fraction = v / total;
                var sweep = fraction * Math.PI * 2;
                var label = LabelAt(labels, i);
                legend.Add(new W3ChartLegendItem(color, string.IsNullOrEmpty(label) ? $"Slice {i + 1}" : label));

                if (fraction >= 0.999999)
                {
                    slices.Add(new W3ChartSlice(string.Empty, true, cx, cy, r, color, label, v, 100));
                    break;
                }

                var end = angle + sweep;
                var x1 = cx + r * Math.Cos(angle);
                var y1 = cy + r * Math.Sin(angle);
                var x2 = cx + r * Math.Cos(end);
                var y2 = cy + r * Math.Sin(end);
                var largeArc = sweep > Math.PI ? 1 : 0;
                var path = $"M {Format(cx)} {Format(cy)} L {Format(x1)} {Format(y1)} A {Format(r)} {Format(r)} 0 {largeArc} 1 {Format(x2)} {Format(y2)} Z";
                slices.Add(new W3ChartSlice(path, false, cx, cy, r, color, label, v, Math.Round(fraction * 100, 1)));
                angle = end;
            }
        }

        return new W3ChartLayout
        {
            Width = width,
            Height = height,
            Slices = slices,
            Legend = legend,
            Hole = type == W3ChartType.Donut ? new W3ChartHole(cx, cy, r * 0.6, holeColor) : null
        };
    }

    private static double ValueAt(IReadOnlyList<double> data, int index) =>
        data is not null && index >= 0 && index < data.Count ? data[index] : 0;

    private static string LabelAt(IReadOnlyList<string> labels, int index) =>
        index >= 0 && index < labels.Count ? labels[index] : string.Empty;

    private static string ResolveColor(string? explicitColor, IReadOnlyList<string> colors, int index) =>
        string.IsNullOrWhiteSpace(explicitColor) ? colors[index % colors.Count] : explicitColor!;

    private static string Format(double value) =>
        Math.Round(value, 2).ToString(CultureInfo.InvariantCulture);
}

internal sealed record W3ChartBar(double X, double Y, double Width, double Height, string Color, string Label, double Value);

internal sealed record W3ChartLine(string Points, string Color, string Name);

internal sealed record W3ChartSlice(string Path, bool FullCircle, double Cx, double Cy, double R, string Color, string Label, double Value, double Percent);

internal sealed record W3ChartGridLine(double X1, double Y1, double X2, double Y2);

internal sealed record W3ChartHole(double Cx, double Cy, double R, string Color);

internal sealed record W3ChartAxisLabel(double X, double Y, string Text, string Anchor);

internal sealed record W3ChartLayout
{
    public double Width { get; init; }
    public double Height { get; init; }
    public IReadOnlyList<W3ChartBar> Bars { get; init; } = [];
    public IReadOnlyList<W3ChartLine> Lines { get; init; } = [];
    public IReadOnlyList<W3ChartSlice> Slices { get; init; } = [];
    public IReadOnlyList<W3ChartGridLine> Grid { get; init; } = [];
    public IReadOnlyList<W3ChartAxisLabel> Labels { get; init; } = [];
    public IReadOnlyList<W3ChartLegendItem> Legend { get; init; } = [];
    public W3ChartGridLine? AxisX { get; init; }
    public W3ChartGridLine? AxisY { get; init; }
    public W3ChartHole? Hole { get; init; }
}

internal sealed record W3ChartLegendItem(string Color, string Name);
