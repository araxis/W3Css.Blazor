using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3AlertTests
{
    [Fact]
    public void RendersSemanticAlertKind()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Alert>(parameters => parameters
            .Add(p => p.Kind, W3AlertKind.Danger)
            .Add(p => p.Title, "Saved")
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Role, "alert")
            .Add(p => p.Class, "alert-extra")
            .Add(p => p.Style, "margin-bottom: 0;")
            .Add(p => p.ChildContent, "Profile updated"));

        var alert = cut.Find("div");

        Assert.Contains("w3-panel", alert.GetAttribute("class"));
        Assert.Contains("w3-danger", alert.GetAttribute("class"));
        Assert.Contains("w3-round", alert.GetAttribute("class"));
        Assert.Contains("alert-extra", alert.GetAttribute("class"));
        Assert.Equal("margin-bottom: 0;", alert.GetAttribute("style"));
        Assert.Equal("alert", alert.GetAttribute("role"));
        Assert.Contains("Saved", alert.TextContent);
        Assert.Contains("Profile updated", alert.TextContent);
    }
}
