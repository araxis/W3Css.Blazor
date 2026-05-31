using Bunit;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using AppBarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.AppBarPage;
using AppShellPage = W3Css.Blazor.Docs.Pages.ComponentTopics.AppShellPage;
using AutocompletePage = W3Css.Blazor.Docs.Pages.ComponentTopics.AutocompletePage;
using AvatarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.AvatarPage;
using LinkPage = W3Css.Blazor.Docs.Pages.ComponentTopics.LinkPage;
using FormPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FormPage;
using BottomNavigationPage = W3Css.Blazor.Docs.Pages.ComponentTopics.BottomNavigationPage;
using BreadcrumbPage = W3Css.Blazor.Docs.Pages.ComponentTopics.BreadcrumbPage;
using ButtonGroupPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ButtonGroupPage;
using ChatPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ChatPage;
using CollapsePage = W3Css.Blazor.Docs.Pages.ComponentTopics.CollapsePage;
using ChipPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ChipPage;
using ChipSetPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ChipSetPage;
using SelectItemPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SelectItemPage;
using ColorInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ColorInputPage;
using ComponentsIndexPage = W3Css.Blazor.Docs.Pages.Components;
using DataTablePage = W3Css.Blazor.Docs.Pages.ComponentTopics.DataTablePage;
using DateInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DateInputPage;
using DarkModePage = W3Css.Blazor.Docs.Pages.ComponentTopics.DarkModePage;
using DividerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DividerPage;
using DrawerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DrawerPage;
using DropZonePage = W3Css.Blazor.Docs.Pages.ComponentTopics.DropZonePage;
using FabPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FabPage;
using ProgressCircularPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ProgressCircularPage;
using FileInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FileInputPage;
using FiltersPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FiltersPage;
using FocusTrapPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FocusTrapPage;
using HighlighterPage = W3Css.Blazor.Docs.Pages.ComponentTopics.HighlighterPage;
using IconButtonPage = W3Css.Blazor.Docs.Pages.ComponentTopics.IconButtonPage;
using ImageListPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ImageListPage;
using ListItemPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ListItemPage;
using MenuPage = W3Css.Blazor.Docs.Pages.ComponentTopics.MenuPage;
using ModalPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ModalPage;
using NavMenuPage = W3Css.Blazor.Docs.Pages.ComponentTopics.NavMenuPage;
using NavbarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.NavbarPage;
using RadioGroupPage = W3Css.Blazor.Docs.Pages.ComponentTopics.RadioGroupPage;
using NumberInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.NumberInputPage;
using DateRangePickerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DateRangePickerPage;
using PaperPage = W3Css.Blazor.Docs.Pages.ComponentTopics.PaperPage;
using ToggleIconButtonPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ToggleIconButtonPage;
using PopoverPage = W3Css.Blazor.Docs.Pages.ComponentTopics.PopoverPage;
using RatingPage = W3Css.Blazor.Docs.Pages.ComponentTopics.RatingPage;
using SliderPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SliderPage;
using SkeletonPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SkeletonPage;
using SpinnerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SpinnerPage;
using StackPage = W3Css.Blazor.Docs.Pages.ComponentTopics.StackPage;
using ScrollToTopPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ScrollToTopPage;
using PageContentNavigationPage = W3Css.Blazor.Docs.Pages.ComponentTopics.PageContentNavigationPage;
using ChartPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ChartPage;
using ThemingPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ThemingPage;
using SpacerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SpacerPage;
using FooterPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FooterPage;
using AvatarGroupPage = W3Css.Blazor.Docs.Pages.ComponentTopics.AvatarGroupPage;
using MessageBoxPage = W3Css.Blazor.Docs.Pages.ComponentTopics.MessageBoxPage;
using ToggleGroupPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ToggleGroupPage;
using MaskPage = W3Css.Blazor.Docs.Pages.ComponentTopics.MaskPage;
using StepperPage = W3Css.Blazor.Docs.Pages.ComponentTopics.StepperPage;
using SwitchPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SwitchPage;
using TabsPage = W3Css.Blazor.Docs.Pages.ComponentTopics.TabsPage;
using TablePage = W3Css.Blazor.Docs.Pages.ComponentTopics.TablePage;
using OverlayPage = W3Css.Blazor.Docs.Pages.ComponentTopics.OverlayPage;
using TimeInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.TimeInputPage;
using TimelinePage = W3Css.Blazor.Docs.Pages.ComponentTopics.TimelinePage;
using ToastPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ToastPage;
using ToolbarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ToolbarPage;
using TreeViewPage = W3Css.Blazor.Docs.Pages.ComponentTopics.TreeViewPage;
using VersionsPage = W3Css.Blazor.Docs.Pages.ComponentTopics.VersionsPage;

namespace W3Css.Blazor.Tests;

public sealed class W3DocsSmokeTests
{
    [Theory]
    [InlineData(typeof(ComponentsIndexPage), "/components")]
    [InlineData(typeof(NavbarPage), "/components/navbar")]
    [InlineData(typeof(FiltersPage), "/components/filters")]
    [InlineData(typeof(DarkModePage), "/components/dark-mode")]
    [InlineData(typeof(VersionsPage), "/components/versions")]
    [InlineData(typeof(AppShellPage), "/components/app-shell")]
    [InlineData(typeof(AppBarPage), "/components/app-bar")]
    [InlineData(typeof(ToolbarPage), "/components/toolbar")]
    [InlineData(typeof(StackPage), "/components/stack")]
    [InlineData(typeof(ScrollToTopPage), "/components/scroll-to-top")]
    [InlineData(typeof(PageContentNavigationPage), "/components/page-content-navigation")]
    [InlineData(typeof(ChartPage), "/components/chart")]
    [InlineData(typeof(ThemingPage), "/components/theming")]
    [InlineData(typeof(SpacerPage), "/components/spacer")]
    [InlineData(typeof(FooterPage), "/components/footer")]
    [InlineData(typeof(AvatarGroupPage), "/components/avatar-group")]
    [InlineData(typeof(DividerPage), "/components/divider")]
    [InlineData(typeof(IconButtonPage), "/components/icon-button")]
    [InlineData(typeof(CollapsePage), "/components/collapse")]
    [InlineData(typeof(ToggleIconButtonPage), "/components/toggle-icon-button")]
    [InlineData(typeof(FabPage), "/components/fab")]
    [InlineData(typeof(ButtonGroupPage), "/components/button-group")]
    [InlineData(typeof(ChipPage), "/components/chip")]
    [InlineData(typeof(RadioGroupPage), "/components/radio-group")]
    [InlineData(typeof(LinkPage), "/components/link")]
    [InlineData(typeof(ChipSetPage), "/components/chip-set")]
    [InlineData(typeof(AvatarPage), "/components/avatar")]
    [InlineData(typeof(ImageListPage), "/components/image-list")]
    [InlineData(typeof(HighlighterPage), "/components/highlighter")]
    [InlineData(typeof(SkeletonPage), "/components/skeleton")]
    [InlineData(typeof(DataTablePage), "/components/data-table")]
    [InlineData(typeof(FormPage), "/components/form")]
    [InlineData(typeof(NumberInputPage), "/components/number-input")]
        [InlineData(typeof(PaperPage), "/components/paper")]
        [InlineData(typeof(DateInputPage), "/components/date-input")]
        [InlineData(typeof(DateRangePickerPage), "/components/date-range-picker")]
        [InlineData(typeof(TimeInputPage), "/components/time-input")]
        [InlineData(typeof(ProgressCircularPage), "/components/progress-circular")]
    [InlineData(typeof(SliderPage), "/components/slider")]
    [InlineData(typeof(RatingPage), "/components/rating")]
    [InlineData(typeof(SwitchPage), "/components/switch")]
    [InlineData(typeof(ColorInputPage), "/components/color-input")]
    [InlineData(typeof(FileInputPage), "/components/file-input")]
    [InlineData(typeof(DropZonePage), "/components/drop-zone")]
    [InlineData(typeof(AutocompletePage), "/components/autocomplete")]
    [InlineData(typeof(SelectItemPage), "/components/select-item")]
    [InlineData(typeof(ListItemPage), "/components/list-item")]
    [InlineData(typeof(NavMenuPage), "/components/nav-menu")]
    [InlineData(typeof(BottomNavigationPage), "/components/bottom-navigation")]
    [InlineData(typeof(BreadcrumbPage), "/components/breadcrumb")]
    [InlineData(typeof(MenuPage), "/components/menu")]
    [InlineData(typeof(TreeViewPage), "/components/tree-view")]
    [InlineData(typeof(TimelinePage), "/components/timeline")]
    [InlineData(typeof(ChatPage), "/components/chat")]
    [InlineData(typeof(StepperPage), "/components/stepper")]
    [InlineData(typeof(TabsPage), "/components/tabs")]
    [InlineData(typeof(TablePage), "/components/table")]
    [InlineData(typeof(MessageBoxPage), "/components/message-box")]
    [InlineData(typeof(ToggleGroupPage), "/components/toggle-group")]
    [InlineData(typeof(MaskPage), "/components/mask")]
    [InlineData(typeof(SpinnerPage), "/components/spinner")]
    [InlineData(typeof(ToastPage), "/components/toast")]
    [InlineData(typeof(ModalPage), "/components/modal")]
    [InlineData(typeof(PopoverPage), "/components/popover")]
    [InlineData(typeof(FocusTrapPage), "/components/focus-trap")]
    [InlineData(typeof(OverlayPage), "/components/overlay")]
    [InlineData(typeof(DrawerPage), "/components/drawer")]
    public void ImportantDocsPagesExposeExpectedRoutes(Type pageType, string expectedRoute)
    {
        var routes = pageType
            .GetCustomAttributes(typeof(RouteAttribute), inherit: false)
            .Cast<RouteAttribute>()
            .Select(attribute => attribute.Template);

        Assert.Contains(expectedRoute, routes);
    }

    [Fact]
    public void ComponentsIndexLinksToKeyGuidancePages()
    {
        using var context = new BunitContext();
        var cut = context.Render<ComponentsIndexPage>();

        Assert.Contains("W3Css.Blazor Components", cut.Markup);
        Assert.Contains("Component Categories", cut.Markup);
        Assert.Contains("Product Readiness Backlog", cut.Markup);
        Assert.NotEmpty(cut.FindAll("a[href='components/navbar']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/versions']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/app-shell']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/app-bar']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/toolbar']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/stack']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/scroll-to-top']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/page-content-navigation']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/chart']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/theming']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/spacer']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/footer']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/avatar-group']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/divider']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/toggle-icon-button']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/collapse']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/icon-button']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/fab']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/button-group']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/chip']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/radio-group']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/chip-set']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/avatar']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/image-list']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/link']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/highlighter']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/skeleton']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/data-table']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/button']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/input']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/number-input']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/form']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/date-input']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/date-range-picker']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/time-input']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/slider']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/rating']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/switch']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/select-item']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/color-input']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/file-input']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/drop-zone']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/autocomplete']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/paper']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/progress']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/progress-circular']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/message-box']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/toggle-group']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/mask']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/nav-menu']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/bottom-navigation']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/breadcrumb']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/menu']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/tree-view']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/list-item']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/timeline']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/chat']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/stepper']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/badge']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/spinner']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/toast']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/popover']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/focus-trap']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/overlay']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/drawer']"));
        Assert.NotEmpty(cut.FindAll("a[href='components/filters']"));
        Assert.Contains("W3DataTable", cut.Markup);
        Assert.Contains("W3TreeView", cut.Markup);
        Assert.Contains("W3Stepper", cut.Markup);
        Assert.Contains("W3AppShell", cut.Markup);
        Assert.Contains("W3AppBar", cut.Markup);
        Assert.Contains("W3Toolbar", cut.Markup);
        Assert.Contains("W3IconButton", cut.Markup);
        Assert.Contains("W3ToggleIconButton", cut.Markup);
        Assert.Contains("W3Fab", cut.Markup);
        Assert.Contains("W3ButtonGroup", cut.Markup);
        Assert.Contains("Button Group", cut.Markup);
        Assert.Contains("Badge", cut.Markup);
        Assert.Contains("W3Chip", cut.Markup);
        Assert.Contains("Radio Group", cut.Markup);
        Assert.Contains("W3ChipSet", cut.Markup);
        Assert.Contains("Select Item", cut.Markup);
        Assert.Contains("W3Avatar", cut.Markup);
        Assert.Contains("W3Skeleton", cut.Markup);
        Assert.Contains("W3Timeline", cut.Markup);
        Assert.Contains("List Item", cut.Markup);
        Assert.Contains("W3Rating", cut.Markup);
        Assert.Contains("W3Chat", cut.Markup);
        Assert.Contains("W3ImageList", cut.Markup);
        Assert.Contains("W3Highlighter", cut.Markup);
        Assert.Contains("W3Popover", cut.Markup);
        Assert.Contains("W3DropZone", cut.Markup);
        Assert.Contains("W3FocusTrap", cut.Markup);
        Assert.Contains("Paper", cut.Markup);
        Assert.Contains("W3DateRangePicker", cut.Markup);
        Assert.Contains("Progress Circular", cut.Markup);
        Assert.Contains("W3Drawer", cut.Markup);
        Assert.Contains("W3BottomNavigation", cut.Markup);
        Assert.Contains("W3Menu", cut.Markup);
        Assert.Contains("Form", cut.Markup);
    }

    [Fact]
    public void ComponentsIndexLinksToAllComponentTopicRoutes()
    {
        using var context = new BunitContext();
        var cut = context.Render<ComponentsIndexPage>();

        var componentTopicRoutes = typeof(AppBarPage).Assembly.GetTypes()
            .Where(type => type.IsClass
                && !type.IsAbstract
                && type.Namespace == "W3Css.Blazor.Docs.Pages.ComponentTopics")
            .SelectMany(type => type.GetCustomAttributes(typeof(RouteAttribute), inherit: false)
                .Cast<RouteAttribute>()
                .Select(attribute => attribute.Template))
            .Where(route => route.StartsWith("/components/", StringComparison.Ordinal))
            .Distinct()
            .Select(route => route.TrimStart('/'))
            .ToList();

        var indexLinks = cut.FindAll("a[href]")
            .Select(link => link.GetAttribute("href"))
            .OfType<string>()
            .ToHashSet(StringComparer.Ordinal);

        foreach (var route in componentTopicRoutes)
        {
            Assert.Contains(route, indexLinks);
        }
    }

    [Fact]
    public void AppBarPageShowsShellExamplesAndMenuInteraction()
    {
        using var context = new BunitContext();
        var cut = context.Render<AppBarPage>();

        Assert.Contains("Application Header", cut.Markup);
        Assert.Contains("Inside W3AppShell", cut.Markup);
        Assert.Contains("Sticky Mobile Bar", cut.Markup);
        Assert.Contains("Menu state: Closed", cut.Markup);

        var menuButton = cut.FindAll("button").First(button => button.TextContent.Contains("Menu", StringComparison.Ordinal));
        menuButton.Click();

        Assert.Contains("Menu state: Open", cut.Markup);
    }

    [Fact]
    public void NavMenuPageShowsGroupedNavigationAndCommandInteraction()
    {
        using var context = new BunitContext();
        var cut = context.Render<NavMenuPage>();

        Assert.Contains("Grouped App Navigation", cut.Markup);
        Assert.Contains("Inside W3Sidebar", cut.Markup);
        Assert.Contains("W3NavMenu Parameters", cut.Markup);
        Assert.Contains("w3-nav-menu-item-active", cut.Markup);

        var runButton = cut.FindAll("button").Single(button => button.TextContent.Contains("Run check", StringComparison.Ordinal));
        runButton.Click();

        Assert.Contains("Last action: Run check", cut.Markup);
    }

    [Fact]
    public void TabsPageShowsAddAndCloseTabInteraction()
    {
        using var context = new BunitContext();
        var cut = context.Render<TabsPage>();

        Assert.Contains("Add And Close Tabs", cut.Markup);
        Assert.Contains("ShowAddButton", cut.Markup);
        Assert.Contains("CloseButtonLabel", cut.Markup);

        cut.FindAll("button")
            .Single(button => button.GetAttribute("aria-label") == "Add workspace tab")
            .Click();

        Assert.Contains("Workspace 3", cut.Markup);

        cut.FindAll("button")
            .Single(button => button.GetAttribute("aria-label") == "Close tab Workspace 1")
            .Click();

        Assert.Contains("Last closed: workspace-1", cut.Markup);
    }

    [Fact]
    public void SmallApiGapPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var tablePage = context.Render<TablePage>();
        Assert.Contains("Dense", tablePage.Markup);
        Assert.Contains("w3-small", tablePage.Markup);
        Assert.Contains("AdditionalAttributes", tablePage.Markup);

        var drawerPage = context.Render<DrawerPage>();
        Assert.Contains("CloseButtonLabel", drawerPage.Markup);
        Assert.Contains("Close drawer", drawerPage.Markup);
        Assert.Contains("Surface and Black", drawerPage.Markup);

        var menuPage = context.Render<MenuPage>();
        Assert.Contains("PanelClass", menuPage.Markup);
        Assert.Contains("Trigger button type attribute", menuPage.Markup);
        Assert.Contains("Surface / Black", menuPage.Markup);

        var progressPage = context.Render<ProgressCircularPage>();
        Assert.Contains("AccessibilityLabel", progressPage.Markup);
        Assert.Contains("Progressbar accessible label", progressPage.Markup);
        Assert.Contains("circular progress indicator in two modes", progressPage.Markup);
    }

    [Fact]
    public void MoreApiGapPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();

        var dataTablePage = context.Render<DataTablePage>();
        Assert.Contains("SelectedItems", dataTablePage.Markup);
        Assert.Contains("Surface", dataTablePage.Markup);
        Assert.Contains("Primary", dataTablePage.Markup);

        var dateRangePage = context.Render<DateRangePickerPage>();
        Assert.Contains("ValueExpression", dateRangePage.Markup);
        Assert.Contains("Color / TextColor", dateRangePage.Markup);
        Assert.Contains("Blazor-owned range state", dateRangePage.Markup);

        var messageBoxPage = context.Render<MessageBoxPage>();
        Assert.Contains("VisibleChanged", messageBoxPage.Markup);
        Assert.Contains("ChildContent", messageBoxPage.Markup);
        Assert.Contains("confirm and alert prompt", messageBoxPage.Markup);

        var modalPage = context.Render<ModalPage>();
        Assert.Contains("Actions", modalPage.Markup);
        Assert.Contains("Surface and None", modalPage.Markup);
        Assert.Contains("AdditionalAttributes", modalPage.Markup);

        var navbarPage = context.Render<NavbarPage>();
        Assert.Contains("ActiveValueChanged", navbarPage.Markup);
        Assert.Contains("callback", navbarPage.Markup);
        Assert.Contains("Primary", navbarPage.Markup);
    }

    [Fact]
    public void NavbarPageRendersDropdownExampleAndKeepsItInteractive()
    {
        using var context = new BunitContext();
        var cut = context.Render<NavbarPage>();

        Assert.Contains("Navbar Dropdown", cut.Markup);
        Assert.Contains("@bind-Open=\"moreOpen\"", cut.Markup);
        Assert.Contains("w3-dropdown-open", cut.Find(".w3-dropdown-click").GetAttribute("class"));
        Assert.Contains("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));

        cut.Find(".w3-dropdown-close-layer").Click();

        Assert.DoesNotContain("w3-dropdown-open", cut.Find(".w3-dropdown-click").GetAttribute("class"));
        Assert.DoesNotContain("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));

        var moreButton = cut.FindAll("button").Single(button => button.TextContent.Trim() == "More");
        moreButton.Click();

        Assert.Contains("w3-dropdown-open", cut.Find(".w3-dropdown-click").GetAttribute("class"));
        Assert.Contains("w3-show", cut.Find(".w3-dropdown-content").GetAttribute("class"));
    }

    [Fact]
    public void DarkModePageTogglesPreviewSurface()
    {
        using var context = new BunitContext();
        var cut = context.Render<DarkModePage>();

        Assert.Contains("Current mode:", cut.Markup);
        Assert.Contains("Light", cut.Markup);

        var toggle = cut.FindAll("button").Single(button => button.TextContent.Trim() == "Use dark surface");
        toggle.Click();

        Assert.Contains("Use light surface", cut.Markup);
        Assert.Contains("Dark", cut.Markup);
        Assert.Contains("w3-black", cut.Markup);
    }

    [Fact]
    public void FiltersPageNarrowsTableOnInput()
    {
        using var context = new BunitContext();
        var cut = context.Render<FiltersPage>();

        var peopleTable = cut.FindAll("table")[0];
        Assert.Contains("Ada Lovelace", peopleTable.TextContent);
        Assert.Contains("Grace Hopper", peopleTable.TextContent);

        cut.Find("input[aria-label='Search people']").Input("Grace");

        peopleTable = cut.FindAll("table")[0];
        Assert.DoesNotContain("Ada Lovelace", peopleTable.TextContent);
        Assert.Contains("Grace Hopper", peopleTable.TextContent);
    }

    [Fact]
    public void ComponentTopicPagesHaveValidComponentRoutes()
    {
        var topicTypes = typeof(AppBarPage).Assembly.GetTypes()
            .Where(type => type.IsClass
                && !type.IsAbstract
                && type.Namespace == "W3Css.Blazor.Docs.Pages.ComponentTopics"
                && type.GetCustomAttribute<RouteAttribute>() is not null);

        var routeAttributes = topicTypes
            .Select(type => type.GetCustomAttribute<RouteAttribute>())
            .Select(attribute => attribute!.Template)
            .ToList();

        Assert.NotEmpty(routeAttributes);
        Assert.Equal(routeAttributes.Count, routeAttributes.Distinct().Count());
        Assert.All(routeAttributes, route => Assert.StartsWith("/components/", route));
    }
}
