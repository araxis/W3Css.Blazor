using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3StepperTests
{
    [Fact]
    public void StepperRendersStepsActivePanelAndProgress()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = context.Render<W3Stepper>(parameters => parameters
            .Add(p => p.Label, "Checkout")
            .Add(p => p.ActiveValue, "shipping")
            .Add(p => p.CompletedValues, ["cart"])
            .Add(p => p.ShowProgress, true)
            .Add(p => p.ShowProgressLabel, true)
            .Add(p => p.ProgressLabelText, "Half done")
            .Add(p => p.Border, true)
            .Add(p => p.Card, true)
            .Add(p => p.CardDepth, W3CardDepth.Two)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.PanelColor, W3Color.PaleBlue)
            .Add(p => p.PanelTextColor, W3Color.Black)
            .Add(p => p.ProgressColor, W3Color.Green)
            .Add(p => p.ProgressTextColor, W3Color.White)
            .Add(p => p.ProgressTrackColor, W3Color.LightGrey)
            .Add(p => p.Class, "checkout-stepper")
            .Add(p => p.Style, "max-width: 48rem;")
            .Add(p => p.ChildContent, StepDefinitions()));

        cut.WaitForAssertion(() => Assert.Equal(3, cut.FindAll(".w3-stepper-item").Count));

        var nav = cut.Find("nav[aria-label='Checkout']");
        var activeItem = cut.Find(".w3-stepper-active");
        var activeButton = cut.Find("button[aria-current='step']");
        var progress = cut.Find("[role='progressbar']");

        Assert.Contains("w3-stepper", cut.Find("section").GetAttribute("class"));
        Assert.Contains("w3-border", cut.Find("section").GetAttribute("class"));
        Assert.Contains("w3-card-2", cut.Find("section").GetAttribute("class"));
        Assert.Contains("w3-round", cut.Find("section").GetAttribute("class"));
        Assert.Contains("w3-white", cut.Find("section").GetAttribute("class"));
        Assert.Contains("w3-text-black", cut.Find("section").GetAttribute("class"));
        Assert.Contains("checkout-stepper", cut.Find("section").GetAttribute("class"));
        Assert.Equal("max-width: 48rem;", cut.Find("section").GetAttribute("style"));
        Assert.NotNull(nav);
        Assert.Contains("w3-stepper-nav-after-progress", nav.GetAttribute("class"));
        Assert.Contains("Shipping details", activeButton.TextContent);
        Assert.Equal("w3-step-panel-shipping", activeButton.GetAttribute("aria-controls"));
        Assert.Contains("w3-primary", activeItem.GetAttribute("class"));
        Assert.DoesNotContain("w3-button", activeButton.GetAttribute("class"));
        var panel = cut.Find("[role='region']");
        Assert.Contains("w3-pale-blue", panel.GetAttribute("class"));
        Assert.Contains("w3-text-black", panel.GetAttribute("class"));
        Assert.Contains("Shipping panel", panel.TextContent);
        Assert.Equal("50", progress.GetAttribute("aria-valuenow"));
        Assert.Contains("Half done", progress.TextContent);
        Assert.Contains("w3-green", cut.Find(".w3-progress-bar").GetAttribute("class"));
        Assert.Contains("w3-stepper-completed", cut.FindAll(".w3-stepper-item")[0].GetAttribute("class"));
    }

    [Fact]
    public void StepperActivatesClickedStep()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var active = "cart";
        string? activated = null;
        var cut = context.Render<W3Stepper>(parameters => parameters
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.StepActivated, EventCallback.Factory.Create<string>(this, value => activated = value))
            .Add(p => p.ChildContent, StepDefinitions()));

        cut.WaitForAssertion(() => Assert.Equal(3, cut.FindAll(".w3-stepper-item").Count));

        cut.FindAll("button").Single(button => button.TextContent.Contains("Review")).Click();

        Assert.Equal("review", active);
        Assert.Equal("review", activated);
        Assert.Contains("Review panel", cut.Find("[role='region']").TextContent);
    }

    [Fact]
    public void StepperSupportsKeyboardNavigationAcrossActivatableSteps()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var active = "cart";
        var cut = context.Render<W3Stepper>(parameters => parameters
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3Step>(0);
                builder.AddAttribute(1, nameof(W3Step.Value), "cart");
                builder.AddAttribute(2, nameof(W3Step.Title), "Cart");
                builder.AddAttribute(3, nameof(W3Step.ChildContent), (RenderFragment)(content => content.AddContent(0, "Cart panel")));
                builder.CloseComponent();

                builder.OpenComponent<W3Step>(4);
                builder.AddAttribute(5, nameof(W3Step.Value), "shipping");
                builder.AddAttribute(6, nameof(W3Step.Title), "Shipping");
                builder.AddAttribute(7, nameof(W3Step.Disabled), true);
                builder.AddAttribute(8, nameof(W3Step.ChildContent), (RenderFragment)(content => content.AddContent(0, "Shipping panel")));
                builder.CloseComponent();

                builder.OpenComponent<W3Step>(9);
                builder.AddAttribute(10, nameof(W3Step.Value), "review");
                builder.AddAttribute(11, nameof(W3Step.Title), "Review");
                builder.AddAttribute(12, nameof(W3Step.ChildContent), (RenderFragment)(content => content.AddContent(0, "Review panel")));
                builder.CloseComponent();
            }));

        cut.WaitForAssertion(() => Assert.Equal(3, cut.FindAll(".w3-stepper-item").Count));

        cut.Find("button[aria-current='step']").KeyDown(new KeyboardEventArgs { Key = "ArrowRight" });

        Assert.Equal("review", active);
        Assert.Contains("Review panel", cut.Find("[role='region']").TextContent);

        cut.Find("button[aria-current='step']").KeyDown(new KeyboardEventArgs { Key = "ArrowLeft" });

        Assert.Equal("cart", active);
        Assert.Contains("Cart panel", cut.Find("[role='region']").TextContent);

        cut.Find("button[aria-current='step']").KeyDown(new KeyboardEventArgs { Key = "End" });
        Assert.Equal("review", active);

        cut.Find("button[aria-current='step']").KeyDown(new KeyboardEventArgs { Key = "Home" });
        Assert.Equal("cart", active);

        var stepButtons = cut.FindAll(".w3-stepper-button");
        Assert.Equal("0", stepButtons[0].GetAttribute("tabindex"));
        Assert.Equal("-1", stepButtons[2].GetAttribute("tabindex"));
    }

    [Fact]
    public void LinearStepperDisablesFutureIncompleteStepsAndSupportsErrorState()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var active = "shipping";
        var cut = context.Render<W3Stepper>(parameters => parameters
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.CompletedValues, ["cart"])
            .Add(p => p.Linear, true)
            .Add(p => p.Vertical, true)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3Step>(0);
                builder.AddAttribute(1, nameof(W3Step.Value), "cart");
                builder.AddAttribute(2, nameof(W3Step.Title), "Cart");
                builder.CloseComponent();

                builder.OpenComponent<W3Step>(3);
                builder.AddAttribute(4, nameof(W3Step.Value), "shipping");
                builder.AddAttribute(5, nameof(W3Step.Title), "Shipping");
                builder.CloseComponent();

                builder.OpenComponent<W3Step>(6);
                builder.AddAttribute(7, nameof(W3Step.Value), "review");
                builder.AddAttribute(8, nameof(W3Step.Title), "Review");
                builder.AddAttribute(9, nameof(W3Step.Error), true);
                builder.CloseComponent();
            }));

        cut.WaitForAssertion(() => Assert.Equal(3, cut.FindAll(".w3-stepper-item").Count));

        var reviewButton = cut.FindAll("button").Single(button => button.TextContent.Contains("Review"));

        Assert.True(reviewButton.HasAttribute("disabled"));
        Assert.Equal("true", reviewButton.GetAttribute("aria-disabled"));
        Assert.Contains("w3-stepper-vertical", cut.Find("section").GetAttribute("class"));
        Assert.Contains("w3-stepper-error", cut.FindAll(".w3-stepper-item")[2].GetAttribute("class"));

        Assert.Equal("shipping", active);
    }

    [Fact]
    public void LinearStepperKeyboardRespectsFutureIncompleteSteps()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        var active = "shipping";
        var cut = context.Render<W3Stepper>(parameters => parameters
            .Add(p => p.ActiveValue, active)
            .Add(p => p.ActiveValueChanged, EventCallback.Factory.Create<string>(this, value => active = value))
            .Add(p => p.CompletedValues, ["cart"])
            .Add(p => p.Linear, true)
            .Add(p => p.ChildContent, StepDefinitions()));

        cut.WaitForAssertion(() => Assert.Equal(3, cut.FindAll(".w3-stepper-item").Count));

        cut.Find("button[aria-current='step']").KeyDown(new KeyboardEventArgs { Key = "End" });

        Assert.Equal("shipping", active);
        Assert.Contains("Shipping panel", cut.Find("[role='region']").TextContent);
    }

    private static RenderFragment StepDefinitions()
    {
        return builder =>
        {
            builder.OpenComponent<W3Step>(0);
            builder.AddAttribute(1, nameof(W3Step.Value), "cart");
            builder.AddAttribute(2, nameof(W3Step.Title), "Cart");
            builder.AddAttribute(3, nameof(W3Step.Description), "Confirm items");
            builder.AddAttribute(4, nameof(W3Step.ChildContent), (RenderFragment)(content => content.AddContent(0, "Cart panel")));
            builder.CloseComponent();

            builder.OpenComponent<W3Step>(5);
            builder.AddAttribute(6, nameof(W3Step.Value), "shipping");
            builder.AddAttribute(7, nameof(W3Step.Title), "Shipping");
            builder.AddAttribute(8, nameof(W3Step.Description), "Shipping details");
            builder.AddAttribute(9, nameof(W3Step.ChildContent), (RenderFragment)(content => content.AddContent(0, "Shipping panel")));
            builder.CloseComponent();

            builder.OpenComponent<W3Step>(10);
            builder.AddAttribute(11, nameof(W3Step.Value), "review");
            builder.AddAttribute(12, nameof(W3Step.Title), "Review");
            builder.AddAttribute(13, nameof(W3Step.Description), "Final check");
            builder.AddAttribute(14, nameof(W3Step.ChildContent), (RenderFragment)(content => content.AddContent(0, "Review panel")));
            builder.CloseComponent();
        };
    }
}
