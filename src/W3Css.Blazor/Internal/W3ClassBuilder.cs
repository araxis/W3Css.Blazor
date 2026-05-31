namespace W3Css.Blazor.Internal;

internal sealed class W3ClassBuilder
{
    private readonly List<string> classes = [];

    public W3ClassBuilder Add(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return this;
        }

        foreach (var cssClass in value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            classes.Add(cssClass);
        }

        return this;
    }

    public W3ClassBuilder AddIf(bool condition, string? value)
    {
        return condition ? Add(value) : this;
    }

    public override string ToString()
    {
        return string.Join(' ', classes.Distinct(StringComparer.Ordinal));
    }
}
