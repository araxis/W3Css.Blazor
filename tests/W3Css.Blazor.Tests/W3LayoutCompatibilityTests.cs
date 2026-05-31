using Bunit;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3LayoutCompatibilityTests
{
    [Fact]
    public void SpacerRendersFlexFillerElement()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Spacer>(parameters => parameters
            .Add(p => p.Class, "spacer-extra")
            .Add(p => p.Style, "min-width: 1rem;"));

        var spacer = cut.Find("div");

        Assert.Contains("w3-spacer", spacer.GetAttribute("class"));
        Assert.Contains("spacer-extra", spacer.GetAttribute("class"));
        Assert.Equal("min-width: 1rem;", spacer.GetAttribute("style"));
        Assert.Equal("true", spacer.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void FooterRendersSemanticFooterWithColorBorderAndFixedClasses()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Footer>(parameters => parameters
            .Add(p => p.Fixed, true)
            .Add(p => p.Wrap, true)
            .Add(p => p.Border, true)
            .Add(p => p.Gap, 24)
            .Add(p => p.Color, W3Color.DarkGrey)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Class, "footer-extra")
            .Add(p => p.Style, "z-index: 5;")
            .Add(p => p.ChildContent, "© 2026"));

        var footer = cut.Find("footer");

        Assert.Contains("w3-footer", footer.GetAttribute("class"));
        Assert.Contains("w3-bottom", footer.GetAttribute("class"));
        Assert.Contains("w3-footer-wrap", footer.GetAttribute("class"));
        Assert.Contains("w3-border-top", footer.GetAttribute("class"));
        Assert.Contains("w3-dark-grey", footer.GetAttribute("class"));
        Assert.Contains("w3-text-white", footer.GetAttribute("class"));
        Assert.Contains("footer-extra", footer.GetAttribute("class"));
        Assert.Equal("--w3-footer-gap:24px;z-index: 5", footer.GetAttribute("style"));
        Assert.Contains("© 2026", footer.TextContent);
    }

    [Fact]
    public void AvatarGroupAppliesOverlapVariableAndOutlineClass()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3AvatarGroup>(parameters => parameters
            .Add(p => p.Spacing, 18)
            .Add(p => p.Outlined, true)
            .Add(p => p.Label, "Project members")
            .Add(p => p.Class, "group-extra")
            .Add(p => p.Style, "margin: 4px;")
            .Add(p => p.ChildContent, "<span class=\"w3-avatar\">A</span><span class=\"w3-avatar\">B</span>"));

        var group = cut.Find("[role='group']");

        Assert.Equal("Project members", group.GetAttribute("aria-label"));
        Assert.Contains("w3-avatar-group", group.GetAttribute("class"));
        Assert.Contains("w3-avatar-group-outlined", group.GetAttribute("class"));
        Assert.Contains("group-extra", group.GetAttribute("class"));
        Assert.Equal("--w3-avatar-group-overlap:-18px;margin: 4px", group.GetAttribute("style"));
        Assert.Equal(2, cut.FindAll(".w3-avatar").Count);
    }

    [Fact]
    public void AvatarGroupWithoutOutlineOmitsOutlineClass()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3AvatarGroup>(parameters => parameters
            .Add(p => p.Outlined, false));

        var group = cut.Find("[role='group']");

        Assert.Contains("w3-avatar-group", group.GetAttribute("class"));
        Assert.DoesNotContain("w3-avatar-group-outlined", group.GetAttribute("class"));
    }
}
