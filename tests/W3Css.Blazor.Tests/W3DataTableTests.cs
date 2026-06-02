using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3DataTableTests
{
    [Fact]
    public void DataTableRendersColumnsRowsPagingAndCount()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Items, Rows)
            .Add(p => p.Id, "projects")
            .Add(p => p.AriaLabel, "Project backlog")
            .Add(p => p.PageSize, 2)
            .Add(p => p.SearchLabel, "Filter projects")
            .Add(p => p.SearchPlaceholder, "Find project")
            .Add(p => p.SearchInputClass, "search-extra")
            .Add(p => p.ToolbarColor, W3Color.Blue)
            .Add(p => p.ToolbarTextColor, W3Color.White)
            .Add(p => p.HeaderColor, W3Color.Teal)
            .Add(p => p.HeaderTextColor, W3Color.White)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Class, "data-table-extra")
            .Add(p => p.Style, "max-width: 56rem;")
            .Add(p => p.ChildContent, ColumnDefinitions()));

        var root = cut.Find("section");
        var toolbar = cut.Find(".w3-data-table-toolbar");
        var searchInput = cut.Find("input[type='search']");
        var table = cut.Find("table");

        Assert.Contains("w3-data-table", root.GetAttribute("class"));
        Assert.Contains("data-table-extra", root.GetAttribute("class"));
        Assert.Equal("max-width: 56rem;", root.GetAttribute("style"));
        Assert.Contains("w3-blue", toolbar.GetAttribute("class"));
        Assert.Contains("w3-text-white", toolbar.GetAttribute("class"));
        Assert.Equal("projects-search", searchInput.GetAttribute("id"));
        Assert.Equal("Find project", searchInput.GetAttribute("placeholder"));
        Assert.Contains("search-extra", searchInput.GetAttribute("class"));
        Assert.Equal("projects-search", cut.Find("label").GetAttribute("for"));
        Assert.Contains("Filter projects", cut.Find("label").TextContent);
        Assert.Equal("Project backlog", table.GetAttribute("aria-label"));
        Assert.Contains("w3-table", table.GetAttribute("class"));
        Assert.Contains("w3-white", table.GetAttribute("class"));
        Assert.Contains("w3-text-black", table.GetAttribute("class"));
        Assert.Contains("w3-striped", table.GetAttribute("class"));
        Assert.Contains("w3-teal", cut.Find("thead").GetAttribute("class"));
        Assert.Contains("w3-text-white", cut.Find("thead").GetAttribute("class"));
        Assert.Contains("project-header", cut.FindAll("thead th")[0].GetAttribute("class"));
        Assert.Equal("width: 45%;", cut.FindAll("thead th")[0].GetAttribute("style"));
        Assert.Contains("project-cell", cut.FindAll("tbody td")[0].GetAttribute("class"));
        Assert.Equal("font-weight: 600;", cut.FindAll("tbody td")[0].GetAttribute("style"));
        Assert.Contains("Project", cut.Markup);
        Assert.Contains("Ada", cut.Markup);
        Assert.Contains("Grace", cut.Markup);
        Assert.DoesNotContain("Linus", cut.Markup);
        Assert.DoesNotContain("Internal", cut.Markup);
        Assert.Contains("1-2 of 3", cut.Markup);
        Assert.NotEmpty(cut.FindAll("nav[aria-label='Data table pagination']"));
    }

    [Fact]
    public void DataTableSortsByColumn()
    {
        using var context = new BunitContext();
        var rows = new[]
        {
            new ProjectRow("Zephyr", "Grace", 4),
            new ProjectRow("Atlas", "Ada", 9)
        };
        var cut = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Items, rows)
            .Add(p => p.Pageable, false)
            .Add(p => p.ChildContent, ColumnDefinitions()));

        var firstText = cut.FindAll("tbody tr")[0].TextContent;
        Assert.Contains("Zephyr", firstText);

        cut.FindAll("button").Single(button => button.TextContent.Contains("Project")).Click();

        firstText = cut.FindAll("tbody tr")[0].TextContent;
        Assert.Contains("Atlas", firstText);
        Assert.Equal("ascending", cut.Find("th[aria-sort]").GetAttribute("aria-sort"));

        cut.FindAll("button").Single(button => button.TextContent.Contains("Project")).Click();

        firstText = cut.FindAll("tbody tr")[0].TextContent;
        Assert.Contains("Zephyr", firstText);
        Assert.Equal("descending", cut.Find("th[aria-sort]").GetAttribute("aria-sort"));
    }

    [Fact]
    public void DataTableFiltersFromSearchInput()
    {
        using var context = new BunitContext();
        string? searchText = null;
        var cut = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Items, Rows)
            .Add(p => p.PageSize, 2)
            .Add(p => p.SearchTextChanged, value => searchText = value)
            .Add(p => p.ChildContent, ColumnDefinitions()));

        cut.Find("input[type='search']").Input("linus");

        Assert.Equal("linus", searchText);
        Assert.Contains("Linus", cut.Markup);
        Assert.DoesNotContain("Ada", cut.Markup);
        Assert.Contains("1-1 of 1", cut.Markup);
    }

    [Fact]
    public void DataTableSupportsLoadingAndEmptyStates()
    {
        using var context = new BunitContext();
        var loading = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Loading, true)
            .Add(p => p.LoadingText, "Fetching rows")
            .Add(p => p.LoadingColor, W3Color.PaleYellow)
            .Add(p => p.LoadingTextColor, W3Color.Black)
            .Add(p => p.LoadingContent, builder => builder.AddContent(0, "Custom loading"))
            .Add(p => p.ChildContent, ColumnDefinitions()));

        Assert.Contains("Custom loading", loading.Find("[role='status']").TextContent);
        Assert.Contains("w3-pale-yellow", loading.Find("[role='status']").GetAttribute("class"));
        Assert.Contains("w3-text-black", loading.Find("[role='status']").GetAttribute("class"));

        var empty = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Items, Array.Empty<ProjectRow>())
            .Add(p => p.EmptyText, "Nothing here")
            .Add(p => p.EmptyDescription, "Try another filter")
            .Add(p => p.ShowEmptyIcon, false)
            .Add(p => p.EmptyContent, builder => builder.AddContent(0, "Custom empty"))
            .Add(p => p.ChildContent, ColumnDefinitions()));

        Assert.Contains("Custom empty", empty.Find("tbody").TextContent);
        Assert.DoesNotContain("Nothing here", empty.Find("tbody").TextContent);

        var defaultEmpty = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Items, Array.Empty<ProjectRow>())
            .Add(p => p.EmptyText, "Nothing here")
            .Add(p => p.EmptyDescription, "Try another filter")
            .Add(p => p.ShowEmptyIcon, false)
            .Add(p => p.ChildContent, ColumnDefinitions()));

        Assert.Contains("w3-empty-state", defaultEmpty.Find(".w3-empty-state").GetAttribute("class"));
        Assert.Contains("Nothing here", defaultEmpty.Find(".w3-empty-state").TextContent);
        Assert.Contains("Try another filter", defaultEmpty.Find(".w3-empty-state").TextContent);
        Assert.Empty(defaultEmpty.FindAll(".w3-empty-state-icon"));
    }

    [Fact]
    public void DataTableSupportsErrorState()
    {
        using var context = new BunitContext();
        var error = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Error, true)
            .Add(p => p.ErrorText, "Rows could not load")
            .Add(p => p.ErrorDescription, "Try again")
            .Add(p => p.ErrorColor, W3Color.PaleRed)
            .Add(p => p.ErrorTextColor, W3Color.Black)
            .Add(p => p.ChildContent, ColumnDefinitions()));

        var state = error.Find(".w3-empty-state");

        Assert.Equal("alert", state.GetAttribute("role"));
        Assert.Equal("assertive", state.GetAttribute("aria-live"));
        Assert.Contains("Rows could not load", state.TextContent);
        Assert.Contains("Try again", state.TextContent);
        Assert.Contains("w3-pale-red", state.GetAttribute("class"));
        Assert.Empty(error.FindAll("table"));

        var customError = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Error, true)
            .Add(p => p.ErrorContent, builder => builder.AddContent(0, "Custom error"))
            .Add(p => p.ChildContent, ColumnDefinitions()));

        Assert.Contains("Custom error", customError.Find("[role='alert']").TextContent);
        Assert.Empty(customError.FindAll(".w3-empty-state"));
    }

    [Fact]
    public void DataTableSupportsSelectionAndRowActions()
    {
        using var context = new BunitContext();
        ProjectRow? selected = null;
        var cut = context.Render<W3DataTable<ProjectRow>>(parameters => parameters
            .Add(p => p.Items, Rows)
            .Add(p => p.Selectable, true)
            .Add(p => p.SelectedColor, W3Color.PaleGreen)
            .Add(p => p.SelectedItemChanged, value => selected = value)
            .Add(p => p.RowActionsHeader, "Tools")
            .Add(p => p.RowActionsHeaderClass, "tools-header")
            .Add(p => p.RowActionsCellClass, "tools-cell")
            .Add(p => p.RowActionsClass, "tools-actions")
            .Add(p => p.RowActionsGap, 12)
            .Add(p => p.RowActions, row => builder =>
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "type", "button");
                builder.AddContent(2, $"Open {row.Name}");
                builder.CloseElement();
                builder.OpenElement(3, "button");
                builder.AddAttribute(4, "type", "button");
                builder.AddContent(5, $"Archive {row.Name}");
                builder.CloseElement();
            })
            .Add(p => p.ChildContent, ColumnDefinitions()));

        Assert.Contains("Open Apollo", cut.Markup);
        Assert.Contains("Tools", cut.Find("thead").TextContent);
        Assert.Contains("tools-header", cut.Find(".w3-data-table-actions-header").GetAttribute("class"));
        Assert.Contains("tools-cell", cut.Find(".w3-data-table-actions-cell").GetAttribute("class"));
        Assert.Contains("tools-actions", cut.Find(".w3-data-table-actions").GetAttribute("class"));
        Assert.Equal("--w3-data-table-actions-gap:12px", cut.Find(".w3-data-table-actions").GetAttribute("style"));
        Assert.Equal(2, cut.Find(".w3-data-table-actions").QuerySelectorAll("button").Length);

        cut.FindAll("tbody tr")[1].Click();

        Assert.NotNull(selected);
        Assert.Equal("Borealis", selected.Name);
        Assert.Equal("true", cut.FindAll("tbody tr")[1].GetAttribute("aria-selected"));
        Assert.Contains("w3-pale-green", cut.FindAll("tbody tr")[1].GetAttribute("class"));
    }

    [Fact]
    public void DataTableFooterKeepsHorizontalPadding()
    {
        var css = File.ReadAllText(Path.Combine(
            GetRepositoryRoot(),
            "src",
            "W3Css.Blazor",
            "Components",
            "W3DataTable.razor.css"));

        Assert.Contains(".w3-data-table-footer", css);
        Assert.Contains("padding: 0.75rem;", css);
    }

    private static RenderFragment ColumnDefinitions()
    {
        return builder =>
        {
            builder.OpenComponent<W3DataColumn<ProjectRow>>(0);
            builder.AddAttribute(1, nameof(W3DataColumn<ProjectRow>.Title), "Project");
            builder.AddAttribute(2, nameof(W3DataColumn<ProjectRow>.CellText), (Func<ProjectRow, object?>)(row => row.Name));
            builder.AddAttribute(3, nameof(W3DataColumn<ProjectRow>.SortKey), (Func<ProjectRow, object?>)(row => row.Name));
            builder.AddAttribute(4, nameof(W3DataColumn<ProjectRow>.HeaderClass), "project-header");
            builder.AddAttribute(5, nameof(W3DataColumn<ProjectRow>.CellClass), "project-cell");
            builder.AddAttribute(6, nameof(W3DataColumn<ProjectRow>.HeaderStyle), "width: 45%;");
            builder.AddAttribute(7, nameof(W3DataColumn<ProjectRow>.CellStyle), "font-weight: 600;");
            builder.CloseComponent();

            builder.OpenComponent<W3DataColumn<ProjectRow>>(8);
            builder.AddAttribute(9, nameof(W3DataColumn<ProjectRow>.Title), "Owner");
            builder.AddAttribute(10, nameof(W3DataColumn<ProjectRow>.CellText), (Func<ProjectRow, object?>)(row => row.Owner));
            builder.AddAttribute(11, nameof(W3DataColumn<ProjectRow>.SearchValue), (Func<ProjectRow, string?>)(row => row.Owner));
            builder.CloseComponent();

            builder.OpenComponent<W3DataColumn<ProjectRow>>(12);
            builder.AddAttribute(13, nameof(W3DataColumn<ProjectRow>.Title), "Tasks");
            builder.AddAttribute(14, nameof(W3DataColumn<ProjectRow>.CellText), (Func<ProjectRow, object?>)(row => row.Tasks));
            builder.AddAttribute(15, nameof(W3DataColumn<ProjectRow>.SortKey), (Func<ProjectRow, object?>)(row => row.Tasks));
            builder.AddAttribute(16, nameof(W3DataColumn<ProjectRow>.Align), W3TextAlignment.Right);
            builder.CloseComponent();

            builder.OpenComponent<W3DataColumn<ProjectRow>>(17);
            builder.AddAttribute(18, nameof(W3DataColumn<ProjectRow>.Title), "Internal");
            builder.AddAttribute(19, nameof(W3DataColumn<ProjectRow>.Visible), false);
            builder.AddAttribute(20, nameof(W3DataColumn<ProjectRow>.CellText), (Func<ProjectRow, object?>)(_ => "Secret"));
            builder.CloseComponent();
        };
    }

    private static readonly ProjectRow[] Rows =
    [
        new("Apollo", "Ada", 12),
        new("Borealis", "Grace", 7),
        new("Cygnus", "Linus", 4)
    ];

    private static string GetRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "W3Css.Blazor.slnx")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Could not locate the repository root.");
    }

    private sealed record ProjectRow(string Name, string Owner, int Tasks);
}
