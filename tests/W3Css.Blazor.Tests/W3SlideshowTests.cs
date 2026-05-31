using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3SlideshowTests
{
    [Fact]
    public void SlideshowRendersActiveSlideControlsAndIndicators()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Slideshow>(parameters => parameters
            .Add(p => p.ActiveValue, "one")
            .Add(p => p.Label, "Highlights")
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.ControlColor, W3Color.Black)
            .Add(p => p.ControlTextColor, W3Color.White)
            .Add(p => p.ControlClass, "control-extra")
            .Add(p => p.CaptionColor, W3Color.Black)
            .Add(p => p.CaptionTextColor, W3Color.White)
            .Add(p => p.CaptionClass, "caption-extra")
            .Add(p => p.ActiveIndicatorColor, W3Color.Teal)
            .Add(p => p.IndicatorTextColor, W3Color.White)
            .Add(p => p.IndicatorClass, "indicator-extra")
            .Add(p => p.IndicatorContainerClass, "indicator-list-extra")
            .Add(p => p.Class, "slideshow-extra")
            .Add(p => p.Style, "max-width: 36rem;")
            .Add(p => p.ChildContent, BuildSlides()));

        cut.WaitForAssertion(() => Assert.Contains("First content", cut.Markup));

        var carousel = cut.Find("[aria-roledescription='carousel']");
        var indicators = cut.FindAll(".w3-badge");

        Assert.Contains("w3-content", carousel.GetAttribute("class"));
        Assert.Contains("w3-display-container", carousel.GetAttribute("class"));
        Assert.Contains("w3-white", carousel.GetAttribute("class"));
        Assert.Contains("w3-text-black", carousel.GetAttribute("class"));
        Assert.Contains("slideshow-extra", carousel.GetAttribute("class"));
        Assert.Equal("max-width: 36rem;", carousel.GetAttribute("style"));
        Assert.Equal("Highlights", carousel.GetAttribute("aria-label"));
        Assert.Equal(3, indicators.Count);
        Assert.Contains("control-extra", cut.Find("button[aria-label='Previous slide']").GetAttribute("class"));
        Assert.Contains("control-extra", cut.Find("button[aria-label='Next slide']").GetAttribute("class"));
        Assert.Contains("indicator-list-extra", cut.Find(".w3-display-bottommiddle.w3-center").GetAttribute("class"));
        Assert.Contains("indicator-extra", indicators[0].GetAttribute("class"));
        Assert.Contains("w3-teal", indicators[0].GetAttribute("class"));
        Assert.Contains("caption-extra", cut.Find(".w3-display-bottommiddle.w3-container").GetAttribute("class"));
        Assert.Contains("First caption", cut.Markup);
    }

    [Fact]
    public void SlideshowChangesSlideFromButtons()
    {
        using var context = new BunitContext();
        var active = "one";
        var cut = context.Render<W3Slideshow>(parameters => parameters
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.ChildContent, BuildSlides()));

        cut.WaitForAssertion(() => Assert.Contains("First content", cut.Markup));

        cut.Find("button[aria-label='Next slide']").Click();

        cut.WaitForAssertion(() => Assert.Equal("two", active));
        Assert.Contains("Second content", cut.Markup);

        cut.Find("button[aria-label='Previous slide']").Click();

        cut.WaitForAssertion(() => Assert.Equal("one", active));
        Assert.Contains("First content", cut.Markup);
    }

    [Fact]
    public void SlideshowChangesSlideFromIndicatorAndKeyboard()
    {
        using var context = new BunitContext();
        var active = "one";
        var cut = context.Render<W3Slideshow>(parameters => parameters
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.ChildContent, BuildSlides()));

        cut.WaitForAssertion(() => Assert.Contains("First content", cut.Markup));

        cut.Find("button[aria-label='Show Third']").Click();

        cut.WaitForAssertion(() => Assert.Equal("three", active));
        Assert.Contains("Third content", cut.Markup);

        cut.Find("[aria-roledescription='carousel']").KeyDown(new KeyboardEventArgs { Key = "Home" });

        cut.WaitForAssertion(() => Assert.Equal("one", active));
        Assert.Contains("First content", cut.Markup);
    }

    [Fact]
    public void SlideshowCanRenderImageSlidesWithAnimation()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Slideshow>(parameters => parameters
            .Add(p => p.Animation, W3Animation.Fading)
            .Add(p => p.ShowIndicators, false)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3Slide>(0);
                builder.AddAttribute(1, nameof(W3Slide.Value), "image");
                builder.AddAttribute(2, nameof(W3Slide.Title), "Image");
                builder.AddAttribute(3, nameof(W3Slide.Src), "assets/w3css-blazor-mark.svg");
                builder.AddAttribute(4, nameof(W3Slide.Alt), "Project mark");
                builder.AddAttribute(5, nameof(W3Slide.Caption), "Image caption");
                builder.AddAttribute(6, nameof(W3Slide.Width), 96);
                builder.AddAttribute(7, nameof(W3Slide.Height), 96);
                builder.AddAttribute(8, nameof(W3Slide.Loading), "eager");
                builder.AddAttribute(9, nameof(W3Slide.Responsive), false);
                builder.AddAttribute(10, nameof(W3Slide.Class), "slide-extra");
                builder.AddAttribute(11, nameof(W3Slide.Style), "min-height: 10rem;");
                builder.AddAttribute(12, "data-slide-id", "image-slide");
                builder.AddAttribute(13, "aria-describedby", "image-caption");
                builder.CloseComponent();
            }));

        cut.WaitForElement("img");

        var slide = cut.Find("[aria-roledescription='slide']");
        var image = cut.Find("img");

        Assert.Contains("w3-animate-fading", slide.GetAttribute("class"));
        Assert.Contains("slide-extra", slide.GetAttribute("class"));
        Assert.Equal("min-height: 10rem;", slide.GetAttribute("style"));
        Assert.Equal("image-slide", slide.GetAttribute("data-slide-id"));
        Assert.Equal("image-caption", slide.GetAttribute("aria-describedby"));
        Assert.DoesNotContain("w3-image", image.GetAttribute("class"));
        Assert.Equal("Project mark", image.GetAttribute("alt"));
        Assert.Equal("96", image.GetAttribute("width"));
        Assert.Equal("96", image.GetAttribute("height"));
        Assert.Equal("eager", image.GetAttribute("loading"));
        Assert.Contains("Image caption", cut.Markup);
        Assert.DoesNotContain("w3-badge", cut.Markup);
    }

    private static RenderFragment BuildSlides()
    {
        return builder =>
        {
            builder.OpenComponent<W3Slide>(0);
            builder.AddAttribute(1, nameof(W3Slide.Value), "one");
            builder.AddAttribute(2, nameof(W3Slide.Title), "First");
            builder.AddAttribute(3, nameof(W3Slide.Caption), "First caption");
            builder.AddAttribute(4, nameof(W3Slide.ChildContent), (RenderFragment)(content => content.AddContent(0, "First content")));
            builder.CloseComponent();

            builder.OpenComponent<W3Slide>(5);
            builder.AddAttribute(6, nameof(W3Slide.Value), "two");
            builder.AddAttribute(7, nameof(W3Slide.Title), "Second");
            builder.AddAttribute(8, nameof(W3Slide.Caption), "Second caption");
            builder.AddAttribute(9, nameof(W3Slide.ChildContent), (RenderFragment)(content => content.AddContent(0, "Second content")));
            builder.CloseComponent();

            builder.OpenComponent<W3Slide>(10);
            builder.AddAttribute(11, nameof(W3Slide.Value), "three");
            builder.AddAttribute(12, nameof(W3Slide.Title), "Third");
            builder.AddAttribute(13, nameof(W3Slide.Caption), "Third caption");
            builder.AddAttribute(14, nameof(W3Slide.ChildContent), (RenderFragment)(content => content.AddContent(0, "Third content")));
            builder.CloseComponent();
        };
    }
}
