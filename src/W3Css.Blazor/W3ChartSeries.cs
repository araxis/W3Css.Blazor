namespace W3Css.Blazor;

/// <summary>
/// A named data series for <c>W3Chart</c>. For Pie/Donut charts the first series'
/// <see cref="Data"/> values become the slices.
/// </summary>
public sealed class W3ChartSeries
{
    /// <summary>
    /// Creates an empty series.
    /// </summary>
    public W3ChartSeries()
    {
    }

    /// <summary>
    /// Creates a series with a name and data.
    /// </summary>
    public W3ChartSeries(string name, IReadOnlyList<double> data, string? color = null)
    {
        Name = name;
        Data = data;
        Color = color;
    }

    /// <summary>
    /// Series name shown in the legend.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Data points for the series.
    /// </summary>
    public IReadOnlyList<double> Data { get; set; } = [];

    /// <summary>
    /// Optional explicit color (hex). Falls back to the chart palette when null.
    /// </summary>
    public string? Color { get; set; }
}
