using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3InteractiveCompatibilityTests
{
    [Fact]
    public void MessageBoxShowsContentAndReportsYesResult()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        bool? result = null;
        var visible = true;

        var cut = context.Render<W3MessageBox>(parameters => parameters
            .Add(m => m.Visible, true)
            .Add(m => m.VisibleChanged, (bool v) => visible = v)
            .Add(m => m.Title, "Delete item?")
            .Add(m => m.Message, "This action cannot be undone.")
            .Add(m => m.YesText, "Delete")
            .Add(m => m.NoText, "Keep")
            .Add(m => m.CancelText, "Cancel")
            .Add(m => m.OnResult, (bool? r) => result = r));

        Assert.Contains("w3-show", cut.Find(".w3-modal").GetAttribute("class"));
        Assert.Equal("w3-modal-title", cut.Find("[role='dialog']").GetAttribute("aria-labelledby"));
        Assert.Null(cut.Find("[role='dialog']").GetAttribute("aria-label"));
        Assert.Contains("Delete item?", cut.Markup);
        Assert.Contains("This action cannot be undone.", cut.Markup);
        Assert.Contains("w3-modal-actions", cut.Find(".w3-modal-actions").GetAttribute("class"));
        Assert.All(
            cut.FindAll(".w3-message-box-action"),
            button =>
            {
                Assert.DoesNotContain("w3-bar-item", button.GetAttribute("class"));
                Assert.DoesNotContain("w3-margin-right", button.GetAttribute("class"));
            });

        cut.FindAll("button").First(button => button.TextContent.Trim() == "Delete").Click();

        Assert.True(result);
        Assert.False(visible);
    }

    [Fact]
    public void MessageBoxForwardsExplicitAccessibleLabelsToModal()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var labelledBox = context.Render<W3MessageBox>(parameters => parameters
            .Add(m => m.Visible, true)
            .Add(m => m.AriaLabel, "Export finished")
            .Add(m => m.Message, "Your export is ready.")
            .Add(m => m.YesText, "Got it"));

        var labelledDialog = labelledBox.Find("[role='dialog']");
        Assert.Equal("Export finished", labelledDialog.GetAttribute("aria-label"));
        Assert.Null(labelledDialog.GetAttribute("aria-labelledby"));

        var referencedBox = context.Render<W3MessageBox>(parameters => parameters
            .Add(m => m.Visible, true)
            .Add(m => m.AriaLabelledBy, "message-heading")
            .Add(m => m.MessageContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<h2 id=\"message-heading\">Archive item?</h2>")))
            .Add(m => m.YesText, "Archive"));

        var referencedDialog = referencedBox.Find("[role='dialog']");
        Assert.Equal("message-heading", referencedDialog.GetAttribute("aria-labelledby"));
        Assert.Null(referencedDialog.GetAttribute("aria-label"));
    }

    [Fact]
    public void MessageBoxReportsNoAndCancelResults()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        bool? noResult = true;
        var noBox = context.Render<W3MessageBox>(parameters => parameters
            .Add(m => m.Visible, true)
            .Add(m => m.Message, "Save changes?")
            .Add(m => m.YesText, "Yes")
            .Add(m => m.NoText, "No")
            .Add(m => m.OnResult, (bool? r) => noResult = r));

        noBox.FindAll("button").First(button => button.TextContent.Trim() == "No").Click();
        Assert.False(noResult);

        bool? cancelResult = true;
        var cancelBox = context.Render<W3MessageBox>(parameters => parameters
            .Add(m => m.Visible, true)
            .Add(m => m.Message, "Discard draft?")
            .Add(m => m.YesText, "Discard")
            .Add(m => m.CancelText, "Cancel")
            .Add(m => m.OnResult, (bool? r) => cancelResult = r));

        cancelBox.FindAll("button").First(button => button.TextContent.Trim() == "Cancel").Click();
        Assert.Null(cancelResult);
    }

    [Fact]
    public void MessageBoxHidesOptionalButtonsWhenTextIsNull()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3MessageBox>(parameters => parameters
            .Add(m => m.Visible, true)
            .Add(m => m.Message, "Export complete.")
            .Add(m => m.YesText, "Got it"));

        var labels = cut.FindAll("button").Select(button => button.TextContent.Trim()).ToList();

        Assert.Contains("Got it", labels);
        Assert.DoesNotContain("Keep", labels);
        Assert.DoesNotContain("Cancel", labels);
    }

    [Fact]
    public void MessageBoxForwardsAdditionalAttributesToModal()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3MessageBox>(parameters => parameters
            .Add(m => m.Visible, true)
            .Add(m => m.Message, "Confirm the action.")
            .Add(m => m.YesText, "Confirm")
            .AddUnmatched("data-testid", "confirm-message"));

        Assert.NotNull(cut.Find("[data-testid='confirm-message']"));
    }

    [Fact]
    public void ToggleGroupSingleSelectMarksPressedAndRaisesChange()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        string? selected = "list";

        var cut = context.Render<W3ToggleGroup<string>>(parameters => parameters
            .Add(g => g.SelectedValue, "list")
            .Add(g => g.SelectedValueChanged, (string? value) => selected = value)
            .Add(g => g.Label, "View mode")
            .Add(g => g.ChildContent, Items("list", "board", "calendar")));

        var group = cut.Find("[role='group']");
        Assert.Contains("w3-toggle-group", group.GetAttribute("class"));
        Assert.Equal("View mode", group.GetAttribute("aria-label"));

        var buttons = cut.FindAll("button");
        Assert.Equal(3, buttons.Count);
        Assert.Equal("true", buttons.First(b => b.TextContent.Trim() == "list").GetAttribute("aria-pressed"));
        Assert.Equal("false", buttons.First(b => b.TextContent.Trim() == "board").GetAttribute("aria-pressed"));

        cut.FindAll("button").First(b => b.TextContent.Trim() == "board").Click();
        Assert.Equal("board", selected);
    }

    [Fact]
    public void ToggleGroupMultiSelectAccumulatesAndRemovesValues()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        IReadOnlyCollection<string> values = new List<string>();

        var cut = context.Render<W3ToggleGroup<string>>(parameters => parameters
            .Add(g => g.MultiSelect, true)
            .Add(g => g.SelectedValues, values)
            .Add(g => g.SelectedValuesChanged, (IReadOnlyCollection<string> v) => values = v)
            .Add(g => g.ChildContent, Items("open", "mine", "urgent")));

        cut.FindAll("button").First(b => b.TextContent.Trim() == "open").Click();
        Assert.Contains("open", values);

        cut.FindAll("button").First(b => b.TextContent.Trim() == "urgent").Click();
        Assert.Contains("open", values);
        Assert.Contains("urgent", values);

        cut.FindAll("button").First(b => b.TextContent.Trim() == "open").Click();
        Assert.DoesNotContain("open", values);
        Assert.Contains("urgent", values);
    }

    private static RenderFragment Items(params string[] values) => builder =>
    {
        for (var i = 0; i < values.Length; i++)
        {
            var value = values[i];
            builder.OpenComponent<W3ToggleItem<string>>(i * 3);
            builder.AddAttribute(i * 3 + 1, "Value", value);
            builder.AddAttribute(i * 3 + 2, "ChildContent", (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, value)));
            builder.CloseComponent();
        }
    };
}
