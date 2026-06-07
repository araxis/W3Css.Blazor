using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3TreeViewTests
{
    [Fact]
    public void TreeViewRendersTreeSemanticsAndTogglesExpansion()
    {
        using var context = new BunitContext();
        IReadOnlyCollection<string> expanded = [];
        var cut = context.Render<W3TreeView<TreeNode>>(parameters => parameters
            .Add(p => p.Items, Nodes)
            .Add(p => p.Label, "Documentation tree")
            .Add(p => p.Border, true)
            .Add(p => p.Card, true)
            .Add(p => p.CardDepth, W3CardDepth.Four)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.Dense, false)
            .Add(p => p.ShowLines, false)
            .Add(p => p.ToggleColor, W3Color.Amber)
            .Add(p => p.ItemClass, "tree-item-extra")
            .Add(p => p.LabelClass, "tree-label-extra")
            .Add(p => p.ToggleClass, "tree-toggle-extra")
            .Add(p => p.Class, "tree-extra")
            .Add(p => p.Style, "max-width: 34rem;")
            .Add(p => p.TextSelector, node => node.Name)
            .Add(p => p.ValueSelector, node => node.Id)
            .Add(p => p.ChildrenSelector, node => node.Children)
            .Add(p => p.ExpandedValues, expanded)
            .Add(p => p.ExpandedValuesChanged, EventCallback.Factory.Create<IReadOnlyCollection<string>>(this, values => expanded = values)));

        var tree = cut.Find("ul[role='tree']");

        Assert.Equal("Documentation tree", tree.GetAttribute("aria-label"));
        Assert.Contains("w3-tree-view", tree.GetAttribute("class"));
        Assert.Contains("w3-border", tree.GetAttribute("class"));
        Assert.Contains("w3-card-4", tree.GetAttribute("class"));
        Assert.Contains("w3-round", tree.GetAttribute("class"));
        Assert.Contains("w3-white", tree.GetAttribute("class"));
        Assert.Contains("w3-text-black", tree.GetAttribute("class"));
        Assert.Contains("w3-tree-view-lines-disabled", tree.GetAttribute("class"));
        Assert.Contains("tree-extra", tree.GetAttribute("class"));
        Assert.DoesNotContain("w3-tree-view-dense", tree.GetAttribute("class"));
        Assert.Equal("max-width: 34rem;", tree.GetAttribute("style"));
        Assert.Contains("tree-item-extra", cut.Find("li[role='treeitem']").GetAttribute("class"));
        Assert.Contains("tree-label-extra", cut.Find(".w3-tree-view-label").GetAttribute("class"));
        Assert.Contains("tree-toggle-extra", cut.Find(".w3-tree-view-toggle").GetAttribute("class"));
        Assert.Contains("w3-amber", cut.Find(".w3-tree-view-toggle").GetAttribute("class"));
        Assert.DoesNotContain("API Reference", cut.Markup);
        Assert.Equal("false", cut.Find("li[role='treeitem']").GetAttribute("aria-expanded"));

        cut.Find("button[aria-label='Expand Components']").Click();

        Assert.Contains("components", expanded);
        Assert.Contains("API Reference", cut.Markup);
        Assert.NotEmpty(cut.FindAll("ul[role='group']"));
        Assert.Equal("true", cut.Find("li[role='treeitem']").GetAttribute("aria-expanded"));
    }

    [Fact]
    public void TreeViewSupportsSelectionLinksTemplatesAndDisabledNodes()
    {
        using var context = new BunitContext();
        string? selected = null;
        TreeNode? selectedNode = null;
        var cut = context.Render<W3TreeView<TreeNode>>(parameters => parameters
            .Add(p => p.Items, Nodes)
            .Add(p => p.TextSelector, node => node.Name)
            .Add(p => p.ValueSelector, node => node.Id)
            .Add(p => p.ChildrenSelector, node => node.Children)
            .Add(p => p.HrefSelector, node => node.Href)
            .Add(p => p.TargetSelector, node => node.Href is null ? null : "_self")
            .Add(p => p.DisabledSelector, node => node.Disabled)
            .Add(p => p.ExpandedValues, ["components"])
            .Add(p => p.SelectedValue, selected)
            .Add(p => p.SelectedValueChanged, EventCallback.Factory.Create<string?>(this, value => selected = value))
            .Add(p => p.ItemSelected, EventCallback.Factory.Create<TreeNode>(this, value => selectedNode = value))
            .Add(p => p.ItemTemplate, node => builder =>
            {
                builder.OpenElement(0, "span");
                builder.AddContent(1, node.Name);
                builder.AddContent(2, " ");
                builder.OpenElement(3, "small");
                builder.AddContent(4, node.Kind);
                builder.CloseElement();
                builder.CloseElement();
            }));

        Assert.Contains("Guide", cut.Markup);
        Assert.Equal("components/data-table", cut.Find("a[href='components/data-table']").GetAttribute("href"));
        Assert.Equal("_self", cut.Find("a[href='components/data-table']").GetAttribute("target"));

        cut.Find("a[href='components/data-table']").Click();

        Assert.Equal("api", selected);
        Assert.Equal("API Reference", selectedNode?.Name);
        var selectedItem = cut.Find("a[href='components/data-table']").ParentElement?.ParentElement;
        Assert.Equal("true", selectedItem?.GetAttribute("aria-selected"));

        var disabled = cut.Find(".w3-tree-view-disabled");
        Assert.Equal("true", disabled.GetAttribute("aria-disabled"));
        Assert.Contains("w3-tree-view-disabled", disabled.GetAttribute("class"));
    }

    [Fact]
    public void TreeViewCanExpandParentsFromItemClickAndRenderEmptyState()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3TreeView<TreeNode>>(parameters => parameters
            .Add(p => p.Items, Nodes)
            .Add(p => p.TextSelector, node => node.Name)
            .Add(p => p.ValueSelector, node => node.Id)
            .Add(p => p.ChildrenSelector, node => node.Children)
            .Add(p => p.ExpandOnItemClick, true));

        cut.FindAll(".w3-tree-view-label").Single(label => label.TextContent.Contains("Components")).Click();

        Assert.Contains("API Reference", cut.Markup);

        var empty = context.Render<W3TreeView<TreeNode>>(parameters => parameters
            .Add(p => p.Items, Array.Empty<TreeNode>())
            .Add(p => p.EmptyText, "No tree nodes"));

        Assert.Contains("No tree nodes", empty.Find(".w3-tree-view-empty").TextContent);
    }

    [Fact]
    public void TreeViewRendersDuplicateSiblingLabelsWithoutKeyCollision()
    {
        using var context = new BunitContext();
        var nodes = new[]
        {
            new TreeNode("dup-1", "Folder", "Folder"),
            new TreeNode("dup-2", "Folder", "Folder"),
        };
        var cut = context.Render<W3TreeView<TreeNode>>(parameters => parameters
            .Add(p => p.Items, nodes)
            .Add(p => p.TextSelector, node => node.Name));

        Assert.Equal(2, cut.FindAll("li[role='treeitem']").Count);

        // Two siblings resolve to the same value (no ValueSelector); the position
        // based render key must keep their @keys distinct so re-rendering after a
        // state change does not throw a duplicate key exception.
        cut.FindAll(".w3-tree-view-label")[1].Click();

        Assert.Equal(2, cut.FindAll("li[role='treeitem']").Count);
    }

    private static readonly TreeNode[] Nodes =
    [
        new("components", "Components", "Guide", null, false, [
            new("api", "API Reference", "Page", "components/data-table"),
            new("archive", "Archive", "Page", null, true)
        ]),
        new("about", "About", "Page", "about")
    ];

    private sealed record TreeNode(
        string Id,
        string Name,
        string Kind,
        string? Href = null,
        bool Disabled = false,
        TreeNode[]? ChildNodes = null)
    {
        public IReadOnlyList<TreeNode> Children { get; } = ChildNodes ?? [];
    }
}
