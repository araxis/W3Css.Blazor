using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ContentExtraTests
{
    [Fact]
    public void ImageRendersResponsiveClassesAndAttributes()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Image>(parameters => parameters
            .Add(p => p.Src, "assets/sample.jpg")
            .Add(p => p.Alt, "Sample")
            .Add(p => p.Width, 320)
            .Add(p => p.Height, 180)
            .Add(p => p.Loading, "eager")
            .Add(p => p.Effect, W3ImageEffect.GrayscaleMin)
            .Add(p => p.HoverOpacity, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Border, true)
            .Add(p => p.Class, "image-extra")
            .Add(p => p.Style, "object-fit: contain;")
            .AddUnmatched("decoding", "async"));

        var image = cut.Find("img");

        Assert.Equal("assets/sample.jpg", image.GetAttribute("src"));
        Assert.Equal("Sample", image.GetAttribute("alt"));
        Assert.Equal("320", image.GetAttribute("width"));
        Assert.Equal("180", image.GetAttribute("height"));
        Assert.Equal("eager", image.GetAttribute("loading"));
        Assert.Equal("async", image.GetAttribute("decoding"));
        Assert.Contains("w3-image", image.GetAttribute("class"));
        Assert.Contains("w3-grayscale-min", image.GetAttribute("class"));
        Assert.Contains("w3-hover-opacity", image.GetAttribute("class"));
        Assert.Contains("w3-round", image.GetAttribute("class"));
        Assert.Contains("w3-border", image.GetAttribute("class"));
        Assert.Contains("image-extra", image.GetAttribute("class"));
        Assert.Equal("object-fit: contain;", image.GetAttribute("style"));
    }

    [Fact]
    public void ImageListRendersResponsiveGridAndItems()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ImageList>(parameters => parameters
            .Add(p => p.Label, "Gallery")
            .Add(p => p.Columns, 2)
            .Add(p => p.Gap, 6)
            .Add(p => p.MinItemWidth, "10rem")
            .Add(p => p.Dense, true)
            .Add(p => p.CaptionColor, W3Color.PaleBlue)
            .Add(p => p.CaptionTextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Large)
            .Add(p => p.Class, "image-list-extra")
            .Add(p => p.Style, "max-width: 44rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3ImageListItem>(0);
                builder.AddAttribute(1, nameof(W3ImageListItem.Src), "assets/one.svg");
                builder.AddAttribute(2, nameof(W3ImageListItem.Alt), "One");
                builder.AddAttribute(3, nameof(W3ImageListItem.Title), "Layout");
                builder.AddAttribute(4, nameof(W3ImageListItem.Subtitle), "Shells and grids");
                builder.AddAttribute(5, nameof(W3ImageListItem.HoverOpacity), true);
                builder.AddAttribute(6, nameof(W3ImageListItem.Effect), W3ImageEffect.GrayscaleMin);
                builder.CloseComponent();

                builder.OpenComponent<W3ImageListItem>(7);
                builder.AddAttribute(8, nameof(W3ImageListItem.Src), "assets/two.svg");
                builder.AddAttribute(9, nameof(W3ImageListItem.Alt), "Two");
                builder.AddAttribute(10, nameof(W3ImageListItem.AspectRatio), "1 / 1");
                builder.AddAttribute(11, nameof(W3ImageListItem.CaptionColor), W3Color.Teal);
                builder.AddAttribute(12, nameof(W3ImageListItem.CaptionTextColor), W3Color.White);
                builder.AddAttribute(13, nameof(W3ImageListItem.CaptionContent), (RenderFragment)(caption => caption.AddContent(0, "Custom caption")));
                builder.CloseComponent();
            }));

        var list = cut.Find("[role='list']");
        var items = cut.FindAll(".w3-image-list-item");

        Assert.Equal("Gallery", list.GetAttribute("aria-label"));
        Assert.Contains("w3-image-list-dense", list.GetAttribute("class"));
        Assert.Contains("image-list-extra", list.GetAttribute("class"));
        Assert.Equal("--w3-image-list-columns:2;--w3-image-list-gap:6px;--w3-image-list-min:10rem;max-width: 44rem", list.GetAttribute("style"));
        Assert.Equal(2, items.Count);
        Assert.Contains("w3-round-large", items[0].GetAttribute("class"));
        Assert.Contains("w3-border", items[0].GetAttribute("class"));
        Assert.Equal("assets/one.svg", items[0].QuerySelector("img")!.GetAttribute("src"));
        Assert.Contains("w3-grayscale-min", items[0].QuerySelector("img")!.GetAttribute("class"));
        Assert.Contains("w3-hover-opacity", items[0].QuerySelector("img")!.GetAttribute("class"));
        Assert.Contains("Layout", items[0].TextContent);
        Assert.Contains("w3-pale-blue", items[0].QuerySelector("figcaption")!.GetAttribute("class"));
        Assert.Contains("w3-teal", items[1].QuerySelector("figcaption")!.GetAttribute("class"));
        Assert.Contains("w3-text-white", items[1].QuerySelector("figcaption")!.GetAttribute("class"));
        Assert.Equal("--w3-image-list-aspect:1 / 1", items[1].GetAttribute("style"));
        Assert.Contains("Custom caption", items[1].TextContent);
    }

    [Fact]
    public void HighlighterHighlightsCaseInsensitiveMatches()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Highlighter>(parameters => parameters
            .Add(p => p.Text, "Data tables and data filters")
            .Add(p => p.Search, "data")
            .Add(p => p.MaxMatches, 1)
            .Add(p => p.Color, W3Color.PaleBlue)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "highlighter-extra")
            .Add(p => p.Style, "font-weight: 600;"));

        var root = cut.Find(".w3-highlighter");
        var mark = cut.Find("mark");

        Assert.Contains("highlighter-extra", root.GetAttribute("class"));
        Assert.Equal("font-weight: 600;", root.GetAttribute("style"));
        Assert.Single(cut.FindAll("mark"));
        Assert.Equal("Data", mark.TextContent);
        Assert.Contains("w3-pale-blue", mark.GetAttribute("class"));
        Assert.Contains("w3-text-black", mark.GetAttribute("class"));
        Assert.Contains("w3-round", mark.GetAttribute("class"));
        Assert.Contains("tables and data filters", root.TextContent);
    }

    [Fact]
    public void CodeRendersBlockAndInlineModes()
    {
        using var context = new BunitContext();
        var block = context.Render<W3Code>(parameters => parameters
            .Add(p => p.Code, "<W3Button>Save</W3Button>")
            .Add(p => p.Class, "code-block-extra")
            .Add(p => p.Style, "max-height: 12rem;"));
        var inline = context.Render<W3Code>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.Code, "w3-code")
            .Add(p => p.Class, "code-inline-extra")
            .Add(p => p.Style, "white-space: nowrap;"));

        Assert.Contains("w3-code", block.Find("pre").GetAttribute("class"));
        Assert.Contains("code-block-extra", block.Find("pre").GetAttribute("class"));
        Assert.Equal("max-height: 12rem;", block.Find("pre").GetAttribute("style"));
        Assert.Contains("<W3Button>Save</W3Button>", block.Find("pre").TextContent);
        Assert.Contains("w3-codespan", inline.Find("code").GetAttribute("class"));
        Assert.Contains("code-inline-extra", inline.Find("code").GetAttribute("class"));
        Assert.Equal("white-space: nowrap;", inline.Find("code").GetAttribute("style"));
        Assert.Equal("w3-code", inline.Find("code").TextContent.Trim());
    }

    [Fact]
    public void NoteRendersDedicatedNoteClass()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Note>(parameters => parameters
            .Add(p => p.Title, "Note")
            .Add(p => p.Border, true)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "note-extra")
            .Add(p => p.Style, "max-width: 32rem;")
            .Add(p => p.ChildContent, "Use this for supporting context."));

        var note = cut.Find("div");

        Assert.Contains("w3-panel", note.GetAttribute("class"));
        Assert.Contains("w3-note", note.GetAttribute("class"));
        Assert.Contains("w3-border", note.GetAttribute("class"));
        Assert.Contains("w3-round", note.GetAttribute("class"));
        Assert.Contains("note-extra", note.GetAttribute("class"));
        Assert.Equal("max-width: 32rem;", note.GetAttribute("style"));
        Assert.Equal("note", note.GetAttribute("role"));
        Assert.Contains("Use this for supporting context.", note.TextContent);
    }

    [Fact]
    public void QuoteRendersPanelAndCitation()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Quote>(parameters => parameters
            .Add(p => p.Cite, "W3.CSS")
            .Add(p => p.LeftBar, false)
            .Add(p => p.RightBar, true)
            .Add(p => p.Color, W3Color.PaleBlue)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.BorderColor, W3Color.Teal)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "quote-extra")
            .Add(p => p.Style, "max-width: 40rem;")
            .Add(p => p.ChildContent, "Simple is useful."));

        var quote = cut.Find("blockquote");

        Assert.Contains("w3-panel", quote.GetAttribute("class"));
        Assert.DoesNotContain("w3-leftbar", quote.GetAttribute("class"));
        Assert.Contains("w3-rightbar", quote.GetAttribute("class"));
        Assert.Contains("w3-pale-blue", quote.GetAttribute("class"));
        Assert.Contains("w3-text-black", quote.GetAttribute("class"));
        Assert.Contains("w3-border-teal", quote.GetAttribute("class"));
        Assert.Contains("w3-round", quote.GetAttribute("class"));
        Assert.Contains("quote-extra", quote.GetAttribute("class"));
        Assert.Equal("max-width: 40rem;", quote.GetAttribute("style"));
        Assert.Contains("Simple is useful.", quote.TextContent);
        Assert.Contains("W3.CSS", quote.TextContent);
    }
}
