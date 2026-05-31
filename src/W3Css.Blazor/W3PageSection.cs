namespace W3Css.Blazor;

/// <summary>
/// A section descriptor for <c>W3PageContentNavigation</c>.
/// </summary>
public sealed class W3PageSection
{
    /// <summary>
    /// Creates a section descriptor.
    /// </summary>
    public W3PageSection()
    {
    }

    /// <summary>
    /// Creates a section descriptor with an id and title.
    /// </summary>
    public W3PageSection(string id, string title)
    {
        Id = id;
        Title = title;
    }

    /// <summary>
    /// The DOM id of the section element this entry links to.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The display label shown in the navigation.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Optional nesting level (1 = top). Used only for indentation.
    /// </summary>
    public int Level { get; set; } = 1;
}
