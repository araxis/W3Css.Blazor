namespace W3Css.Blazor;

/// <summary>
/// Gesture information emitted by <see cref="Components.W3SwipeArea"/>.
/// </summary>
public sealed class W3SwipeEventArgs : EventArgs
{
    /// <summary>
    /// Creates a swipe event payload.
    /// </summary>
    public W3SwipeEventArgs(W3SwipeDirection direction, double deltaX, double deltaY, double distance, TimeSpan duration)
    {
        Direction = direction;
        DeltaX = deltaX;
        DeltaY = deltaY;
        Distance = distance;
        Duration = duration;
    }

    /// <summary>
    /// Dominant gesture direction.
    /// </summary>
    public W3SwipeDirection Direction { get; }

    /// <summary>
    /// Horizontal movement from start to end in CSS pixels.
    /// </summary>
    public double DeltaX { get; }

    /// <summary>
    /// Vertical movement from start to end in CSS pixels.
    /// </summary>
    public double DeltaY { get; }

    /// <summary>
    /// Straight-line distance in CSS pixels.
    /// </summary>
    public double Distance { get; }

    /// <summary>
    /// Gesture duration.
    /// </summary>
    public TimeSpan Duration { get; }
}
