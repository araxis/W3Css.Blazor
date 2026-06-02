using Bunit;
using Microsoft.Extensions.DependencyInjection;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ToastTests
{
    [Fact]
    public void ToastRendersSemanticPanelAndDismissAction()
    {
        using var context = new BunitContext();
        var dismissed = false;

        var cut = context.Render<W3Toast>(parameters => parameters
            .Add(p => p.Title, "Saved")
            .Add(p => p.Kind, W3AlertKind.Success)
            .Add(p => p.Round, W3Round.Large)
            .Add(p => p.Card, true)
            .Add(p => p.Animate, true)
            .Add(p => p.CloseButtonLabel, "Dismiss saved toast")
            .Add(p => p.Class, "toast-extra")
            .Add(p => p.Style, "max-width: 24rem;")
            .Add(p => p.OnDismiss, () => dismissed = true)
            .Add(p => p.ChildContent, "Project settings were saved."));

        var toast = cut.Find(".w3-toast");

        Assert.Contains("w3-panel", toast.GetAttribute("class"));
        Assert.Contains("w3-success", toast.GetAttribute("class"));
        Assert.Contains("w3-round-large", toast.GetAttribute("class"));
        Assert.Contains("w3-toast-dismissible", toast.GetAttribute("class"));
        Assert.Contains("w3-card-4", toast.GetAttribute("class"));
        Assert.Contains("w3-animate-right", toast.GetAttribute("class"));
        Assert.Contains("toast-extra", toast.GetAttribute("class"));
        Assert.Equal("max-width: 24rem;", toast.GetAttribute("style"));
        Assert.Equal("status", toast.GetAttribute("role"));
        Assert.Equal("polite", toast.GetAttribute("aria-live"));
        Assert.Equal("Dismiss saved toast", cut.Find(".w3-toast-close").GetAttribute("aria-label"));
        Assert.Empty(cut.FindAll(".w3-toast-actions .w3-toast-close"));
        Assert.Equal("button", toast.Children[1].TagName.ToLowerInvariant());
        Assert.Contains("Saved", toast.TextContent);
        Assert.Contains("Project settings were saved.", toast.TextContent);

        cut.Find(".w3-toast-close").Click();

        Assert.True(dismissed);
    }

    [Fact]
    public void ToastServiceAddsDismissesAndClearsMessages()
    {
        var service = new W3ToastService();
        var changes = 0;
        service.ToastsChanged += () => changes++;

        var toast = service.Show("Saved from service.", new W3ToastOptions
        {
            Title = "Saved",
            Kind = W3AlertKind.Success,
            ActionText = "Undo"
        });

        Assert.Single(service.Toasts);
        Assert.Equal(toast.Id, service.Toasts[0].Id);
        Assert.Equal("Saved", service.Toasts[0].Title);
        Assert.Equal(W3AlertKind.Success, service.Toasts[0].Kind);
        Assert.Equal("Undo", service.Toasts[0].ActionText);

        service.Dismiss(toast.Id);

        Assert.Empty(service.Toasts);

        service.ShowWarning("Review required.", "Warning");
        service.Clear();

        Assert.Empty(service.Toasts);
        Assert.Equal(4, changes);
    }

    [Fact]
    public void ToastProviderRendersServiceMessagesAndRunsActions()
    {
        using var context = new BunitContext();
        context.Services.AddW3CssBlazor();
        var service = context.Services.GetRequiredService<W3ToastService>();
        var actionRan = false;

        var cut = context.Render<W3ToastProvider>(parameters => parameters
            .Add(p => p.DefaultDuration, (TimeSpan?)null)
            .Add(p => p.Position, W3ToastPosition.TopRight)
            .Add(p => p.MaxVisible, 3)
            .Add(p => p.AriaLive, "assertive")
            .Add(p => p.Label, "Workspace notifications")
            .Add(p => p.Class, "toast-provider-extra")
            .Add(p => p.Style, "z-index: 20;"));

        service.Show("Draft restore point created.", new W3ToastOptions
        {
            Title = "Undo available",
            Kind = W3AlertKind.Info,
            ActionText = "Undo",
            OnAction = () =>
            {
                actionRan = true;
                return Task.CompletedTask;
            }
        });

        cut.WaitForAssertion(() =>
        {
            var provider = cut.Find(".w3-toast-provider");
            Assert.Contains("w3-toast-top-right", provider.GetAttribute("class"));
            Assert.Contains("toast-provider-extra", provider.GetAttribute("class"));
            Assert.Equal("z-index: 20;", provider.GetAttribute("style"));
            Assert.Equal("assertive", provider.GetAttribute("aria-live"));
            Assert.Equal("Workspace notifications", provider.GetAttribute("aria-label"));
            Assert.Contains("Undo available", cut.Markup);
            Assert.Contains("Draft restore point created.", cut.Markup);
        });

        cut.Find(".w3-toast-action").Click();

        Assert.True(actionRan);
        cut.WaitForAssertion(() => Assert.Empty(service.Toasts));
    }
}
