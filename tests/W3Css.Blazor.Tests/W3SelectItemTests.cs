using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3SelectItemTests
{
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
