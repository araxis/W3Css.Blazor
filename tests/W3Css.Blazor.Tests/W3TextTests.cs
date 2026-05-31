using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3TextTests
{
    [Fact]
    public void TextRendersAlignmentStyleFontSizeAndTextColor()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.Alignment, W3TextAlignment.Center)
            .Add(p => p.TextStyle, W3TextStyle.Wide | W3TextStyle.Bold)
            .Add(p => p.Font, W3FontFamily.Serif)
            .Add(p => p.Size, W3Size.Large)
            .Add(p => p.TextColor, W3Color.Teal)
            .Add(p => p.ChildContent, "Styled text"));

        var text = cut.Find("div");
        var classes = text.GetAttribute("class");

        Assert.Contains("w3-center", classes);
        Assert.Contains("w3-wide", classes);
        Assert.Contains("w3-bold", classes);
        Assert.Contains("w3-serif", classes);
        Assert.Contains("w3-large", classes);
        Assert.Contains("w3-text-teal", classes);
        Assert.Equal("Styled text", text.TextContent.Trim());
    }

    [Fact]
    public void TextCanRenderInlineSpan()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.TextStyle, W3TextStyle.Italic)
            .Add(p => p.Font, W3FontFamily.Monospace)
            .Add(p => p.ChildContent, "Inline text"));

        var text = cut.Find("span");
        var classes = text.GetAttribute("class");

        Assert.Contains("w3-italic", classes);
        Assert.Contains("w3-monospace", classes);
        Assert.Equal("Inline text", text.TextContent);
    }

    [Fact]
    public void TextRendersJustifyBackgroundAdditionalClassAndStyle()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.Alignment, W3TextAlignment.Justify)
            .Add(p => p.Color, W3Color.PaleYellow)
            .Add(p => p.Class, "custom-text")
            .Add(p => p.Style, "letter-spacing: .04em;")
            .Add(p => p.ChildContent, "Justified text"));

        var text = cut.Find("div");
        var classes = text.GetAttribute("class");

        Assert.Contains("w3-justify", classes);
        Assert.Contains("w3-pale-yellow", classes);
        Assert.Contains("custom-text", classes);
        Assert.Equal("letter-spacing: .04em;", text.GetAttribute("style"));
    }

    [Fact]
    public void TextMapsRightAlignmentAndSansSerifFont()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.Alignment, W3TextAlignment.Right)
            .Add(p => p.Font, W3FontFamily.SansSerif)
            .Add(p => p.ChildContent, "Right aligned"));

        var text = cut.Find("div");
        var classes = text.GetAttribute("class");

        Assert.Contains("w3-right-align", classes);
        Assert.Contains("w3-sans-serif", classes);
    }

    [Theory]
    [InlineData(W3TextAlignment.Left, "w3-left-align")]
    [InlineData(W3TextAlignment.Right, "w3-right-align")]
    [InlineData(W3TextAlignment.Center, "w3-center")]
    [InlineData(W3TextAlignment.Justify, "w3-justify")]
    public void TextMapsAlignmentClasses(W3TextAlignment alignment, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.Alignment, alignment)
            .Add(p => p.ChildContent, "Aligned"));

        var text = cut.Find("div");

        Assert.Contains(expectedClass, text.GetAttribute("class"));
    }

    [Theory]
    [InlineData(W3TextStyle.Wide, "w3-wide")]
    [InlineData(W3TextStyle.Bold, "w3-bold")]
    [InlineData(W3TextStyle.Italic, "w3-italic")]
    public void TextMapsStyleClasses(W3TextStyle textStyle, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.TextStyle, textStyle)
            .Add(p => p.ChildContent, "Styled"));

        var text = cut.Find("div");

        Assert.Contains(expectedClass, text.GetAttribute("class"));
    }

    [Theory]
    [InlineData(W3FontFamily.Serif, "w3-serif")]
    [InlineData(W3FontFamily.SansSerif, "w3-sans-serif")]
    [InlineData(W3FontFamily.Cursive, "w3-cursive")]
    [InlineData(W3FontFamily.Monospace, "w3-monospace")]
    public void TextMapsFontClasses(W3FontFamily font, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.Font, font)
            .Add(p => p.ChildContent, "Font"));

        var text = cut.Find("div");

        Assert.Contains(expectedClass, text.GetAttribute("class"));
    }

    [Theory]
    [InlineData(W3Size.Tiny, "w3-tiny")]
    [InlineData(W3Size.Small, "w3-small")]
    [InlineData(W3Size.Large, "w3-large")]
    [InlineData(W3Size.XLarge, "w3-xlarge")]
    [InlineData(W3Size.XXLarge, "w3-xxlarge")]
    [InlineData(W3Size.XXXLarge, "w3-xxxlarge")]
    [InlineData(W3Size.Jumbo, "w3-jumbo")]
    public void TextMapsSizeClasses(W3Size size, string expectedClass)
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Text>(parameters => parameters
            .Add(p => p.Size, size)
            .Add(p => p.ChildContent, "Sized"));

        var text = cut.Find("div");

        Assert.Contains(expectedClass, text.GetAttribute("class"));
    }
}
