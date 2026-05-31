using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ContentPrimitiveTests
{
    [Fact]
    public void AvatarRendersInitialsImageAndCustomContent()
    {
        using var context = new BunitContext();
        var initials = context.Render<W3Avatar>(parameters => parameters
            .Add(p => p.Initials, "AL")
            .Add(p => p.Label, "Ada Lovelace")
            .Add(p => p.Size, 48)
            .Add(p => p.Color, W3Color.Teal)
            .Add(p => p.TextColor, W3Color.White)
            .Add(p => p.Border, true)
            .Add(p => p.Class, "avatar-extra")
            .Add(p => p.Style, "flex: 0 0 auto;"));
        var image = context.Render<W3Avatar>(parameters => parameters
            .Add(p => p.Src, "assets/avatar.png")
            .Add(p => p.Alt, "Profile")
            .Add(p => p.Loading, "eager")
            .Add(p => p.Circle, false)
            .Add(p => p.Round, W3Round.Large));
        var custom = context.Render<W3Avatar>(parameters => parameters
            .Add(p => p.ChildContent, builder => builder.AddContent(0, "!")));

        var avatar = initials.Find("span.w3-avatar");

        Assert.Equal("img", avatar.GetAttribute("role"));
        Assert.Equal("Ada Lovelace", avatar.GetAttribute("aria-label"));
        Assert.Contains("w3-circle", avatar.GetAttribute("class"));
        Assert.Contains("w3-border", avatar.GetAttribute("class"));
        Assert.Contains("w3-teal", avatar.GetAttribute("class"));
        Assert.Contains("w3-text-white", avatar.GetAttribute("class"));
        Assert.Contains("avatar-extra", avatar.GetAttribute("class"));
        Assert.Equal("--w3-avatar-size:48px;flex: 0 0 auto", avatar.GetAttribute("style"));
        Assert.Equal("AL", initials.Find(".w3-avatar-initials").TextContent);

        Assert.Equal("assets/avatar.png", image.Find("img").GetAttribute("src"));
        Assert.Equal("Profile", image.Find("img").GetAttribute("alt"));
        Assert.Equal("eager", image.Find("img").GetAttribute("loading"));
        Assert.Contains("w3-round-large", image.Find("span.w3-avatar").GetAttribute("class"));
        Assert.DoesNotContain("w3-circle", image.Find("span.w3-avatar").GetAttribute("class"));
        Assert.Contains("!", custom.Find("span.w3-avatar").TextContent);
    }

    [Fact]
    public void SkeletonRendersLinesAndCirclePlaceholders()
    {
        using var context = new BunitContext();
        var lines = context.Render<W3Skeleton>(parameters => parameters
            .Add(p => p.Lines, 3)
            .Add(p => p.Width, "70%")
            .Add(p => p.Height, "12px")
            .Add(p => p.Label, "Loading rows")
            .Add(p => p.Animate, false)
            .Add(p => p.Color, W3Color.PaleBlue)
            .Add(p => p.Class, "skeleton-extra")
            .Add(p => p.Style, "margin-bottom: 1rem;"));
        var circle = context.Render<W3Skeleton>(parameters => parameters
            .Add(p => p.Circle, true)
            .Add(p => p.Width, "48px")
            .Add(p => p.Height, "48px"));

        var skeleton = lines.Find("[role='status']");

        Assert.Equal("Loading rows", skeleton.GetAttribute("aria-label"));
        Assert.Equal("true", skeleton.GetAttribute("aria-busy"));
        Assert.Contains("w3-skeleton", skeleton.GetAttribute("class"));
        Assert.Contains("w3-pale-blue", skeleton.GetAttribute("class"));
        Assert.Contains("skeleton-extra", skeleton.GetAttribute("class"));
        Assert.DoesNotContain("w3-skeleton-animate", skeleton.GetAttribute("class"));
        Assert.Equal("--w3-skeleton-width:70%;--w3-skeleton-height:12px;margin-bottom: 1rem", skeleton.GetAttribute("style"));
        Assert.Equal(3, lines.FindAll(".w3-skeleton-line").Count);
        Assert.Contains("w3-skeleton-circle", circle.Find("[role='status']").GetAttribute("class"));
        Assert.Single(circle.FindAll(".w3-skeleton-shape"));
    }

    [Fact]
    public void ChipSetSupportsSingleSelection()
    {
        using var context = new BunitContext();
        string? selected = "list";
        string? selectedFromChip = null;
        var cut = context.Render<W3ChipSet<string>>(parameters => parameters
            .Add(p => p.Label, "View mode")
            .Add(p => p.SelectedValue, selected)
            .Add(p => p.SelectedValueChanged, value => selected = value)
            .Add(p => p.ActiveColor, W3Color.Indigo)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3Chip<string>>(0);
                builder.AddAttribute(1, nameof(W3Chip<string>.Value), "list");
                builder.AddAttribute(2, nameof(W3Chip<string>.ChildContent), (RenderFragment)(child => child.AddContent(0, "List")));
                builder.CloseComponent();

                builder.OpenComponent<W3Chip<string>>(3);
                builder.AddAttribute(4, nameof(W3Chip<string>.Value), "board");
                builder.AddAttribute(5, nameof(W3Chip<string>.OnSelected), EventCallback.Factory.Create<string>(this, value => selectedFromChip = value));
                builder.AddAttribute(6, nameof(W3Chip<string>.ChildContent), (RenderFragment)(child => child.AddContent(0, "Board")));
                builder.CloseComponent();
            }));

        var chips = cut.FindAll("button[role='option']");

        Assert.Equal("View mode", cut.Find("[role='listbox']").GetAttribute("aria-label"));
        Assert.Equal("false", cut.Find("[role='listbox']").GetAttribute("aria-multiselectable"));
        Assert.Equal("true", chips[0].GetAttribute("aria-selected"));
        Assert.Contains("w3-indigo", chips[0].GetAttribute("class"));
        Assert.Equal("false", chips[1].GetAttribute("aria-selected"));

        chips[1].Click();

        chips = cut.FindAll("button[role='option']");
        Assert.Equal("board", selected);
        Assert.Equal("board", selectedFromChip);
        Assert.Equal("false", chips[0].GetAttribute("aria-selected"));
        Assert.Equal("true", chips[1].GetAttribute("aria-selected"));
    }

    [Fact]
    public void ChipSetSupportsMultiSelectionAndDisabledState()
    {
        using var context = new BunitContext();
        IReadOnlyCollection<string> selected = ["active"];
        var cut = context.Render<W3ChipSet<string>>(parameters => parameters
            .Add(p => p.MultiSelect, true)
            .Add(p => p.SelectedValues, selected)
            .Add(p => p.SelectedValuesChanged, values => selected = values)
            .Add(p => p.Disabled, false)
            .Add(p => p.Gap, 4)
            .Add(p => p.Class, "chip-set-extra")
            .Add(p => p.Style, "max-width: 32rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3Chip<string>>(0);
                builder.AddAttribute(1, nameof(W3Chip<string>.Value), "active");
                builder.AddAttribute(2, nameof(W3Chip<string>.IconClass), "fa fa-check");
                builder.AddAttribute(3, nameof(W3Chip<string>.ChildContent), (RenderFragment)(child => child.AddContent(0, "Active")));
                builder.CloseComponent();

                builder.OpenComponent<W3Chip<string>>(4);
                builder.AddAttribute(5, nameof(W3Chip<string>.Value), "review");
                builder.AddAttribute(6, nameof(W3Chip<string>.Disabled), true);
                builder.AddAttribute(7, nameof(W3Chip<string>.ChildContent), (RenderFragment)(child => child.AddContent(0, "Review")));
                builder.CloseComponent();
            }));

        var chipSet = cut.Find("[role='listbox']");
        var chips = cut.FindAll("button[role='option']");

        Assert.Equal("true", chipSet.GetAttribute("aria-multiselectable"));
        Assert.Contains("chip-set-extra", chipSet.GetAttribute("class"));
        Assert.Equal("--w3-chip-set-gap:4px;max-width: 32rem", chipSet.GetAttribute("style"));
        Assert.Equal("true", chips[0].GetAttribute("aria-selected"));
        Assert.Contains("fa-check", chips[0].InnerHtml);
        Assert.True(chips[1].HasAttribute("disabled"));

        chips[0].Click();

        Assert.Empty(selected);
        Assert.Equal("false", cut.FindAll("button[role='option']")[0].GetAttribute("aria-selected"));
    }
}
