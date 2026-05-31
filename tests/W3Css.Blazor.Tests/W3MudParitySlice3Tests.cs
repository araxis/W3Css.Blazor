using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3MudParitySlice3Tests
{
    [Fact]
    public void MaskFormatsDigitsAndDelimitersFromPattern()
    {
        using var context = new BunitContext();
        string? value = null;

        var cut = context.Render<W3Mask>(parameters => parameters
            .Add(m => m.Mask, "(000) 000-0000")
            .Add(m => m.Value, value)
            .Add(m => m.ValueChanged, (string? v) => value = v)
            .Add(m => m.ValueExpression, () => value));

        cut.Find("input").Input("1234567890");

        Assert.Equal("(123) 456-7890", value);
    }

    [Fact]
    public void MaskSkipsCharactersThatDoNotMatchTokenClass()
    {
        using var context = new BunitContext();
        string? value = null;

        var cut = context.Render<W3Mask>(parameters => parameters
            .Add(m => m.Mask, "aa-00")
            .Add(m => m.Value, value)
            .Add(m => m.ValueChanged, (string? v) => value = v)
            .Add(m => m.ValueExpression, () => value));

        cut.Find("input").Input("ab12cd");

        Assert.Equal("ab-12", value);
    }

    [Fact]
    public void ColorInputDefaultsToBareNativeInput()
    {
        using var context = new BunitContext();
        string? value = "#000000";

        var cut = context.Render<W3ColorInput>(parameters => parameters
            .Add(c => c.Value, "#000000")
            .Add(c => c.ValueChanged, (string? v) => value = v)
            .Add(c => c.ValueExpression, () => value));

        Assert.Single(cut.FindAll("input[type='color']"));
        Assert.Empty(cut.FindAll(".w3-color-input-swatch"));
    }

    [Fact]
    public void ColorInputWithPaletteSelectsSwatchAndAcceptsHex()
    {
        using var context = new BunitContext();
        string? value = "#000000";

        var cut = context.Render<W3ColorInput>(parameters => parameters
            .Add(c => c.ShowPalette, true)
            .Add(c => c.Palette, new[] { "#ff0000", "#00ff00" })
            .Add(c => c.Value, "#000000")
            .Add(c => c.ValueChanged, (string? v) => value = v)
            .Add(c => c.ValueExpression, () => value));

        var swatches = cut.FindAll(".w3-color-input-swatch");
        Assert.Equal(2, swatches.Count);

        swatches[0].Click();
        Assert.Equal("#ff0000", value);

        cut.Find(".w3-color-input-hex").Change("#abcdef");
        Assert.Equal("#abcdef", value);

        cut.Find(".w3-color-input-hex").Change("not-a-color");
        Assert.Equal("#abcdef", value);
    }

    [Fact]
    public void TabsSupportCloseableAndAddableButtons()
    {
        using var context = new BunitContext();
        string? closed = null;
        var added = 0;

        var cut = context.Render<W3Tabs>(parameters => parameters
            .Add(t => t.ActiveValue, "a")
            .Add(t => t.ShowAddButton, true)
            .Add(t => t.Closeable, true)
            .Add(t => t.OnAddTab, () => added++)
            .Add(t => t.OnCloseTab, (string v) => closed = v)
            .Add(t => t.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<W3TabPanel>(0);
                builder.AddAttribute(1, "Value", "a");
                builder.AddAttribute(2, "Title", "Alpha");
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(b => b.AddContent(0, "A body")));
                builder.CloseComponent();

                builder.OpenComponent<W3TabPanel>(4);
                builder.AddAttribute(5, "Value", "b");
                builder.AddAttribute(6, "Title", "Beta");
                builder.AddAttribute(7, "ChildContent", (RenderFragment)(b => b.AddContent(0, "B body")));
                builder.CloseComponent();
            })));

        cut.FindAll("button").First(b => b.GetAttribute("aria-label") == "Add tab").Click();
        Assert.Equal(1, added);

        cut.FindAll("button").First(b => b.GetAttribute("aria-label") == "Close Alpha").Click();
        Assert.Equal("a", closed);
    }
}
