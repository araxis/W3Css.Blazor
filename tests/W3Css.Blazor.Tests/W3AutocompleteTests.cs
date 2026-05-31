using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3AutocompleteTests
{
    [Fact]
    public void AutocompleteFiltersOptionsAndSelectsClickedItem()
    {
        using var context = new BunitContext();
        string? value = null;
        var cut = context.Render<W3Autocomplete<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Items, ["Ada Lovelace", "Grace Hopper", "Linus Torvalds"])
            .Add(p => p.Id, "owner-autocomplete")
            .Add(p => p.Placeholder, "Search people")
            .Add(p => p.Border, true)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.ActiveColor, W3Color.PaleGreen)
            .Add(p => p.MenuColor, W3Color.PaleBlue)
            .Add(p => p.MenuTextColor, W3Color.Black)
            .Add(p => p.InputClass, "autocomplete-input")
            .Add(p => p.InputStyle, "min-width: 12rem;")
            .Add(p => p.MenuClass, "autocomplete-menu-extra")
            .Add(p => p.Class, "autocomplete-root")
            .Add(p => p.Style, "max-width: 20rem;"));

        var root = cut.Find(".w3-autocomplete");
        var input = cut.Find("input");

        Assert.Contains("autocomplete-root", root.GetAttribute("class"));
        Assert.Equal("max-width: 20rem;", root.GetAttribute("style"));
        Assert.Equal("owner-autocomplete", input.GetAttribute("id"));
        Assert.Equal("combobox", input.GetAttribute("role"));
        Assert.Equal("Search people", input.GetAttribute("placeholder"));
        Assert.Contains("w3-input", input.GetAttribute("class"));
        Assert.Contains("w3-border", input.GetAttribute("class"));
        Assert.Contains("w3-white", input.GetAttribute("class"));
        Assert.Contains("w3-text-black", input.GetAttribute("class"));
        Assert.Contains("w3-round", input.GetAttribute("class"));
        Assert.Contains("autocomplete-input", input.GetAttribute("class"));
        Assert.Equal("min-width: 12rem;", input.GetAttribute("style"));

        input.Input("gr");

        var menu = cut.Find(".w3-autocomplete-menu");
        Assert.Contains("w3-pale-blue", menu.GetAttribute("class"));
        Assert.Contains("w3-text-black", menu.GetAttribute("class"));
        Assert.Contains("autocomplete-menu-extra", menu.GetAttribute("class"));
        Assert.Single(cut.FindAll(".w3-autocomplete-option"));
        Assert.Contains("w3-pale-green", cut.Find(".w3-autocomplete-option").GetAttribute("class"));
        Assert.Contains("Grace Hopper", cut.Markup);

        cut.Find(".w3-autocomplete-option").Click();

        Assert.Equal("Grace Hopper", value);
        Assert.Equal("Grace Hopper", cut.Find("input").GetAttribute("value"));
        Assert.Empty(cut.FindAll(".w3-autocomplete-menu"));
    }

    [Fact]
    public void AutocompleteCanSelectActiveItemFromKeyboard()
    {
        using var context = new BunitContext();
        string? value = null;
        var cut = context.Render<W3Autocomplete<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Items, ["Alpha", "Beta", "Gamma"]));

        var input = cut.Find("input");

        input.Input("a");
        input.KeyDown(new KeyboardEventArgs { Key = "ArrowDown" });
        input.KeyDown(new KeyboardEventArgs { Key = "Enter" });

        Assert.Equal("Beta", value);
        Assert.Empty(cut.FindAll(".w3-autocomplete-menu"));
    }

    [Fact]
    public void AutocompleteSupportsTextSelectorTemplateAndNoResults()
    {
        using var context = new BunitContext();
        WorkspaceOption? value = null;
        var options = new[]
        {
            new WorkspaceOption("atlas", "Atlas", "Design"),
            new WorkspaceOption("cygnus", "Cygnus", "Engineering")
        };

        var cut = context.Render<W3Autocomplete<WorkspaceOption>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<WorkspaceOption?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Items, options)
            .Add(p => p.TextSelector, option => option.Name)
            .Add(p => p.ItemTemplate, option => builder =>
            {
                builder.OpenElement(0, "span");
                builder.AddContent(1, option.Name);
                builder.AddContent(2, " ");
                builder.OpenElement(3, "small");
                builder.AddContent(4, option.Team);
                builder.CloseElement();
                builder.CloseElement();
            }));

        cut.Find("input").Input("atlas");

        Assert.Contains("Design", cut.Markup);

        cut.Find(".w3-autocomplete-option").Click();

        Assert.Equal(options[0], value);
        Assert.Equal("Atlas", cut.Find("input").GetAttribute("value"));

        cut.Find("input").Input("missing");

        Assert.Contains("No results", cut.Markup);
    }

    private sealed record WorkspaceOption(string Id, string Name, string Team);
}
