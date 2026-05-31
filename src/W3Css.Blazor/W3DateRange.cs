namespace W3Css.Blazor;

/// <summary>
/// Represents a MudBlazor-style date range value used by the date range picker wrapper.
/// </summary>
public sealed class W3DateRange
{
    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateOnly? Start { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateOnly? End { get; set; }

    /// <summary>
    /// Initializes a new date range.
    /// </summary>
    public W3DateRange()
    {
    }

    /// <summary>
    /// Initializes a new date range.
    /// </summary>
    public W3DateRange(DateOnly? start, DateOnly? end)
    {
        Start = start;
        End = end;
    }

    /// <summary>
    /// Initializes a new date range with optional normalization when start date is later than end date.
    /// </summary>
    public W3DateRange(DateOnly? start, DateOnly? end, bool normalize)
    {
        if (normalize && start.HasValue && end.HasValue && start > end)
        {
            Start = end;
            End = start;
        }
        else
        {
            Start = start;
            End = end;
        }
    }
}
