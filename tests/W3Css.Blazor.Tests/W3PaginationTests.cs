using Bunit;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3PaginationTests
{
    [Fact]
    public void PaginationWindowsLargePageCountWithEllipsis()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Pagination>(parameters => parameters
            .Add(p => p.PageCount, 20)
            .Add(p => p.CurrentPage, 10));

        var buttonTexts = cut.FindAll("nav button").Select(button => button.TextContent.Trim()).ToList();

        Assert.True(buttonTexts.Count < 12, $"expected a windowed pager, got {buttonTexts.Count} buttons");
        Assert.NotEmpty(cut.FindAll(".w3-pagination-ellipsis"));
        Assert.Contains("1", buttonTexts);
        Assert.Contains("10", buttonTexts);
        Assert.Contains("20", buttonTexts);
        Assert.DoesNotContain("5", buttonTexts);
    }

    [Fact]
    public void PaginationListsEveryPageWhenCountFitsWindow()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Pagination>(parameters => parameters
            .Add(p => p.PageCount, 5)
            .Add(p => p.CurrentPage, 1));

        var buttonTexts = cut.FindAll("nav button").Select(button => button.TextContent.Trim()).ToList();

        Assert.Empty(cut.FindAll(".w3-pagination-ellipsis"));

        foreach (var page in new[] { "1", "2", "3", "4", "5" })
        {
            Assert.Contains(page, buttonTexts);
        }
    }
}
