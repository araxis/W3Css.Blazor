using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3SelectItemTests
{
    [Fact]
    public void SelectRendersPlaceholderBeforeItemsAndDisablesItByDefault()
    {
        using var context = new BunitContext();

        var value = string.Empty;
        var cut = context.Render<W3Select<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Placeholder, "Choose a size")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<W3SelectItem<string>>(0);
                builder.AddAttribute(1, "Value", "small");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.AddContent(0, "Small");
                }));
                builder.CloseComponent();
            })));

        var options = cut.FindAll("option");

        Assert.Equal(2, options.Count);
        Assert.Equal(string.Empty, options[0].GetAttribute("value"));
        Assert.True(options[0].HasAttribute("disabled"));
        Assert.Equal("Choose a size", options[0].TextContent.Trim());
        Assert.Equal("small", options[1].GetAttribute("value"));
        Assert.Equal("Small", options[1].TextContent.Trim());
    }

    [Fact]
    public void SelectCanRenderEnabledPlaceholderWhenRequested()
    {
        using var context = new BunitContext();

        var value = string.Empty;
        var cut = context.Render<W3Select<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Placeholder, "Any size")
            .Add(p => p.PlaceholderDisabled, false));

        var option = cut.Find("option");

        Assert.Equal(string.Empty, option.GetAttribute("value"));
        Assert.False(option.HasAttribute("disabled"));
        Assert.Equal("Any size", option.TextContent.Trim());
    }

    [Fact]
    public void SelectBinderUpdatesValueWhenSelectionChanges()
    {
        using var context = new BunitContext();

        var value = string.Empty;
        var cut = context.Render<W3Select<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string>(this, selected => value = selected))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Placeholder, "Choose a size")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<W3SelectItem<string>>(0);
                builder.AddAttribute(1, "Value", "medium");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.AddContent(0, "Medium");
                }));
                builder.CloseComponent();
            })));

        cut.Find("select").Change("medium");

        Assert.Equal("medium", value);
    }

    [Fact]
    public void SelectItemRendersAsOptionWithStringifiedValueAndChildLabel()
    {
        using var context = new BunitContext();

        var value = 2;
        var cut = context.Render<W3Select<int>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<W3SelectItem<int>>(0);
                builder.AddAttribute(1, "Value", 1);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.AddContent(0, "One");
                }));
                builder.CloseComponent();

                builder.OpenComponent<W3SelectItem<int>>(3);
                builder.AddAttribute(4, "Value", 2);
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.AddContent(0, "Two");
                }));
                builder.CloseComponent();
            })));

        var options = cut.FindAll("option");

        Assert.Equal(2, options.Count);
        Assert.Contains("value=\"1\"", options[0].OuterHtml);
        Assert.Contains("value=\"2\"", options[1].OuterHtml);
        Assert.Equal("One", options[0].TextContent);
        Assert.Equal("Two", options[1].TextContent);
        Assert.Contains("1", options[0].GetAttribute("value"));
        Assert.Contains("2", options[1].GetAttribute("value"));
    }

    [Fact]
    public void SelectItemSupportsDisabledAndFallbackContent()
    {
        using var context = new BunitContext();

        var value = 2;
        var cut = context.Render<W3Select<int>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<W3SelectItem<int>>(0);
                builder.AddAttribute(1, "Value", 3);
                builder.AddAttribute(2, "Disabled", true);
                builder.CloseComponent();
            })));

        var option = cut.Find("option");

        Assert.Equal("3", option.GetAttribute("value"));
        Assert.Equal("3", option.TextContent.Trim());
        Assert.True(option.HasAttribute("disabled"));
    }
}
