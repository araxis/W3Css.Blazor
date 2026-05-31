using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AccordionPage = W3Css.Blazor.Docs.Pages.ComponentTopics.AccordionPage;
using AppBarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.AppBarPage;
using AppShellPage = W3Css.Blazor.Docs.Pages.ComponentTopics.AppShellPage;
using AutocompletePage = W3Css.Blazor.Docs.Pages.ComponentTopics.AutocompletePage;
using AvatarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.AvatarPage;
using BadgePage = W3Css.Blazor.Docs.Pages.ComponentTopics.BadgePage;
using CheckboxPage = W3Css.Blazor.Docs.Pages.ComponentTopics.CheckboxPage;
using LinkPage = W3Css.Blazor.Docs.Pages.ComponentTopics.LinkPage;
using FieldPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FieldPage;
using FormPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FormPage;
using BottomNavigationPage = W3Css.Blazor.Docs.Pages.ComponentTopics.BottomNavigationPage;
using BreadcrumbPage = W3Css.Blazor.Docs.Pages.ComponentTopics.BreadcrumbPage;
using ButtonPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ButtonPage;
using ButtonGroupPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ButtonGroupPage;
using ChatPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ChatPage;
using CollapsePage = W3Css.Blazor.Docs.Pages.ComponentTopics.CollapsePage;
using ChipPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ChipPage;
using ChipSetPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ChipSetPage;
using CodePage = W3Css.Blazor.Docs.Pages.ComponentTopics.CodePage;
using ColumnPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ColumnPage;
using ContainerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ContainerPage;
using SelectItemPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SelectItemPage;
using ColorInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ColorInputPage;
using ComponentsIndexPage = W3Css.Blazor.Docs.Pages.Components;
using DataTablePage = W3Css.Blazor.Docs.Pages.ComponentTopics.DataTablePage;
using DateInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DateInputPage;
using DarkModePage = W3Css.Blazor.Docs.Pages.ComponentTopics.DarkModePage;
using DividerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DividerPage;
using DrawerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DrawerPage;
using DropZonePage = W3Css.Blazor.Docs.Pages.ComponentTopics.DropZonePage;
using DropdownPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DropdownPage;
using FabPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FabPage;
using ProgressCircularPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ProgressCircularPage;
using FileInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FileInputPage;
using FiltersPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FiltersPage;
using FlexPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FlexPage;
using FocusTrapPage = W3Css.Blazor.Docs.Pages.ComponentTopics.FocusTrapPage;
using GridPage = W3Css.Blazor.Docs.Pages.ComponentTopics.GridPage;
using HighlighterPage = W3Css.Blazor.Docs.Pages.ComponentTopics.HighlighterPage;
using IconButtonPage = W3Css.Blazor.Docs.Pages.ComponentTopics.IconButtonPage;
using IconsPage = W3Css.Blazor.Docs.Pages.ComponentTopics.IconsPage;
using ImagePage = W3Css.Blazor.Docs.Pages.ComponentTopics.ImagePage;
using ImageListPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ImageListPage;
using InputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.InputPage;
using ListItemPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ListItemPage;
using MenuPage = W3Css.Blazor.Docs.Pages.ComponentTopics.MenuPage;
using ModalPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ModalPage;
using NavMenuPage = W3Css.Blazor.Docs.Pages.ComponentTopics.NavMenuPage;
using NavbarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.NavbarPage;
using RadioGroupPage = W3Css.Blazor.Docs.Pages.ComponentTopics.RadioGroupPage;
using RadioPage = W3Css.Blazor.Docs.Pages.ComponentTopics.RadioPage;
using NumberInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.NumberInputPage;
using DateRangePickerPage = W3Css.Blazor.Docs.Pages.ComponentTopics.DateRangePickerPage;
using PaginationPage = W3Css.Blazor.Docs.Pages.ComponentTopics.PaginationPage;
using PaperPage = W3Css.Blazor.Docs.Pages.ComponentTopics.PaperPage;
using PanelPage = W3Css.Blazor.Docs.Pages.ComponentTopics.PanelPage;
using ToggleIconButtonPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ToggleIconButtonPage;
using PopoverPage = W3Css.Blazor.Docs.Pages.ComponentTopics.PopoverPage;
using RatingPage = W3Css.Blazor.Docs.Pages.ComponentTopics.RatingPage;
using RowPage = W3Css.Blazor.Docs.Pages.ComponentTopics.RowPage;
using SliderPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SliderPage;
using SlideshowPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SlideshowPage;
using SidebarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SidebarPage;
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
using NotePage = W3Css.Blazor.Docs.Pages.ComponentTopics.NotePage;
using QuotePage = W3Css.Blazor.Docs.Pages.ComponentTopics.QuotePage;
using StepperPage = W3Css.Blazor.Docs.Pages.ComponentTopics.StepperPage;
using SwitchPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SwitchPage;
using TagPage = W3Css.Blazor.Docs.Pages.ComponentTopics.TagPage;
using TabsPage = W3Css.Blazor.Docs.Pages.ComponentTopics.TabsPage;
using TablePage = W3Css.Blazor.Docs.Pages.ComponentTopics.TablePage;
using OverlayPage = W3Css.Blazor.Docs.Pages.ComponentTopics.OverlayPage;
using TimeInputPage = W3Css.Blazor.Docs.Pages.ComponentTopics.TimeInputPage;
using SelectPage = W3Css.Blazor.Docs.Pages.ComponentTopics.SelectPage;
using TextAreaPage = W3Css.Blazor.Docs.Pages.ComponentTopics.TextAreaPage;
using TimelinePage = W3Css.Blazor.Docs.Pages.ComponentTopics.TimelinePage;
using ToastPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ToastPage;
using ToolbarPage = W3Css.Blazor.Docs.Pages.ComponentTopics.ToolbarPage;
using TooltipPage = W3Css.Blazor.Docs.Pages.ComponentTopics.TooltipPage;
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
    [InlineData(typeof(ContainerPage), "/components/container")]
    [InlineData(typeof(PanelPage), "/components/panel")]
    [InlineData(typeof(RowPage), "/components/row")]
    [InlineData(typeof(ColumnPage), "/components/column")]
    [InlineData(typeof(GridPage), "/components/grid")]
    [InlineData(typeof(FlexPage), "/components/flex")]
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
    [InlineData(typeof(FieldPage), "/components/field")]
    [InlineData(typeof(InputPage), "/components/input")]
    [InlineData(typeof(TextAreaPage), "/components/text-area")]
    [InlineData(typeof(SelectPage), "/components/select")]
    [InlineData(typeof(CheckboxPage), "/components/checkbox")]
    [InlineData(typeof(RadioPage), "/components/radio")]
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
    [InlineData(typeof(DropdownPage), "/components/dropdown")]
    [InlineData(typeof(MenuPage), "/components/menu")]
    [InlineData(typeof(PaginationPage), "/components/pagination")]
    [InlineData(typeof(TreeViewPage), "/components/tree-view")]
    [InlineData(typeof(TimelinePage), "/components/timeline")]
    [InlineData(typeof(ChatPage), "/components/chat")]
    [InlineData(typeof(StepperPage), "/components/stepper")]
    [InlineData(typeof(AccordionPage), "/components/accordion")]
    [InlineData(typeof(TabsPage), "/components/tabs")]
    [InlineData(typeof(TablePage), "/components/table")]
    [InlineData(typeof(MessageBoxPage), "/components/message-box")]
    [InlineData(typeof(ToggleGroupPage), "/components/toggle-group")]
    [InlineData(typeof(MaskPage), "/components/mask")]
    [InlineData(typeof(SpinnerPage), "/components/spinner")]
    [InlineData(typeof(ToastPage), "/components/toast")]
    [InlineData(typeof(ModalPage), "/components/modal")]
    [InlineData(typeof(TooltipPage), "/components/tooltip")]
    [InlineData(typeof(PopoverPage), "/components/popover")]
    [InlineData(typeof(FocusTrapPage), "/components/focus-trap")]
    [InlineData(typeof(OverlayPage), "/components/overlay")]
    [InlineData(typeof(DrawerPage), "/components/drawer")]
    [InlineData(typeof(SidebarPage), "/components/sidebar")]
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
    public void ChartAndLayoutApiPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();

        var chartPage = context.Render<ChartPage>();
        Assert.Contains("Root background color class", chartPage.Markup);
        Assert.Contains("AdditionalAttributes", chartPage.Markup);
        Assert.Contains("script-free SVG chart supporting", chartPage.Markup);

        var collapsePage = context.Render<CollapsePage>();
        Assert.Contains("Alternative State Binding", collapsePage.Markup);
        Assert.Contains("ChildContent", collapsePage.Markup);
        Assert.Contains("Alternative expanded state", collapsePage.Markup);
        Assert.Contains("Extra attributes on the wrapper", collapsePage.Markup);

        var paperPage = context.Render<PaperPage>();
        Assert.Contains("paper-like surfaces", paperPage.Markup);
        Assert.Contains("Surface", paperPage.Markup);
        Assert.Contains("Surface body content", paperPage.Markup);
        Assert.Contains("Extra attributes on the paper root", paperPage.Markup);

        var panelPage = context.Render<PanelPage>();
        Assert.Contains("Panel body content", panelPage.Markup);
        Assert.Contains("Extra attributes on the panel root", panelPage.Markup);

        var containerPage = context.Render<ContainerPage>();
        Assert.Contains("Container body content", containerPage.Markup);
        Assert.Contains("Extra attributes on the container root", containerPage.Markup);

        var rowPage = context.Render<RowPage>();
        Assert.Contains("Row column content", rowPage.Markup);
        Assert.Contains("Extra attributes on the row root", rowPage.Markup);

        var columnPage = context.Render<ColumnPage>();
        Assert.Contains("Column body content", columnPage.Markup);
        Assert.Contains("Extra attributes on the column root", columnPage.Markup);

        var gridPage = context.Render<GridPage>();
        Assert.Contains("Grid item content", gridPage.Markup);
        Assert.Contains("Extra attributes on the grid root", gridPage.Markup);

        var flexPage = context.Render<FlexPage>();
        Assert.Contains("Flex item content", flexPage.Markup);
        Assert.Contains("Extra attributes on the flex root", flexPage.Markup);
    }

    [Fact]
    public void NavigationApiPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();

        var appShellPage = context.Render<AppShellPage>();
        Assert.Contains("Extra attributes on the root shell", appShellPage.Markup);
        Assert.Contains("Slot-level inline styles", appShellPage.Markup);

        var appBarPage = context.Render<AppBarPage>();
        Assert.Contains("Surface", appBarPage.Markup);
        Assert.Contains("Extra attributes on the header root", appBarPage.Markup);

        var navMenuPage = context.Render<NavMenuPage>();
        Assert.Contains("Inherited active item background", navMenuPage.Markup);
        Assert.Contains("Extra attributes on the nav root", navMenuPage.Markup);
        Assert.Contains("Extra classes, style, and attributes on the rendered item", navMenuPage.Markup);

        var toolbarPage = context.Render<ToolbarPage>();
        Assert.Contains("Extra attributes on the toolbar root", toolbarPage.Markup);
        Assert.Contains("Control gap in pixels", toolbarPage.Markup);

        var bottomNavigationPage = context.Render<BottomNavigationPage>();
        Assert.Contains("Surface and Black", bottomNavigationPage.Markup);
        Assert.Contains("Primary and White", bottomNavigationPage.Markup);
        Assert.Contains("Extra attributes on the nav root", bottomNavigationPage.Markup);
    }

    [Fact]
    public void DisclosureNavigationApiPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var accordionPage = context.Render<AccordionPage>();
        Assert.Contains("Surface", accordionPage.Markup);
        Assert.Contains("Extra attributes on the accordion wrapper", accordionPage.Markup);
        Assert.Contains("Extra attributes on the accordion item wrapper", accordionPage.Markup);

        var dropdownPage = context.Render<DropdownPage>();
        Assert.Contains("Surface", dropdownPage.Markup);
        Assert.Contains("Extra attributes on the dropdown wrapper", dropdownPage.Markup);

        var paginationPage = context.Render<PaginationPage>();
        Assert.Contains("Primary", paginationPage.Markup);
        Assert.Contains("Extra attributes on the pagination nav", paginationPage.Markup);

        var sidebarPage = context.Render<SidebarPage>();
        Assert.Contains("Surface", sidebarPage.Markup);
        Assert.Contains("Extra attributes on the sidebar element", sidebarPage.Markup);

        var tooltipPage = context.Render<TooltipPage>();
        Assert.Contains("Extra attributes on the tooltip root", tooltipPage.Markup);

        var scrollToTopPage = context.Render<ScrollToTopPage>();
        Assert.Contains("Button border class", scrollToTopPage.Markup);
        Assert.Contains("Extra attributes on the scroll button", scrollToTopPage.Markup);

        var linkPage = context.Render<LinkPage>();
        Assert.Contains("Extra attributes on the rendered link, button, or span", linkPage.Markup);
        Assert.Contains("Primary", linkPage.Markup);

        var pageContentNavigationPage = context.Render<PageContentNavigationPage>();
        Assert.Contains("W3PageSection Properties", pageContentNavigationPage.Markup);
        Assert.Contains("Extra attributes on the content navigation nav", pageContentNavigationPage.Markup);
        Assert.Contains("Indent level for nested entries", pageContentNavigationPage.Markup);
    }

    [Fact]
    public void FeedbackAndOverlayApiPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddW3CssBlazor();

        var alertPage = context.Render<W3Css.Blazor.Docs.Pages.ComponentTopics.AlertPage>();
        Assert.Contains("Extra attributes on the alert panel", alertPage.Markup);
        Assert.Contains("ARIA role", alertPage.Markup);

        var toastPage = context.Render<ToastPage>();
        Assert.Contains("Extra attributes on the toast root", toastPage.Markup);
        Assert.Contains("Extra provider classes, style, and root attributes", toastPage.Markup);

        var overlayPage = context.Render<OverlayPage>();
        Assert.Contains("Extra attributes on the overlay element", overlayPage.Markup);
        Assert.Contains("Clicking overlay sets", overlayPage.Markup);

        var popoverPage = context.Render<PopoverPage>();
        Assert.Contains("Surface / Black", popoverPage.Markup);
        Assert.Contains("Extra attributes on the root wrapper", popoverPage.Markup);

        var focusTrapPage = context.Render<FocusTrapPage>();
        Assert.Contains("Extra attributes on the trap root", focusTrapPage.Markup);
        Assert.Contains("Fallback focus target on the trap root", focusTrapPage.Markup);

        var spinnerPage = context.Render<SpinnerPage>();
        Assert.Contains("Extra attributes on the status root", spinnerPage.Markup);
        Assert.Contains("W3.CSS text size class", spinnerPage.Markup);

        var skeletonPage = context.Render<SkeletonPage>();
        Assert.Contains("Extra attributes on the skeleton root", skeletonPage.Markup);
        Assert.Contains("Component-scoped pulse animation", skeletonPage.Markup);
    }

    [Fact]
    public void ContentAndMediaApiPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();

        var imagePage = context.Render<ImagePage>();
        Assert.Contains("Extra attributes on the image element", imagePage.Markup);

        var imageListPage = context.Render<ImageListPage>();
        Assert.Contains("Default item caption background color", imageListPage.Markup);
        Assert.Contains("Extra attributes on the image list item figure", imageListPage.Markup);

        var slideshowPage = context.Render<SlideshowPage>();
        Assert.Contains("Previous button aria label", slideshowPage.Markup);
        Assert.Contains("Extra attributes on the carousel root", slideshowPage.Markup);

        var chatPage = context.Render<ChatPage>();
        Assert.Contains("Avatar image alt text", chatPage.Markup);
        Assert.Contains("Extra attributes on the message article", chatPage.Markup);

        var codePage = context.Render<CodePage>();
        Assert.Contains("Extra attributes on the rendered code element", codePage.Markup);

        var notePage = context.Render<NotePage>();
        Assert.Contains("Extra attributes on the note panel", notePage.Markup);

        var quotePage = context.Render<QuotePage>();
        Assert.Contains("Extra attributes on the blockquote", quotePage.Markup);

        var iconsPage = context.Render<IconsPage>();
        Assert.Contains("Extra attributes on the icon element", iconsPage.Markup);

        var highlighterPage = context.Render<HighlighterPage>();
        Assert.Contains("Extra attributes on the root span", highlighterPage.Markup);
    }

    [Fact]
    public void ActionAndUtilityApiPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();

        var buttonPage = context.Render<ButtonPage>();
        Assert.Contains("Extra attributes on the button element", buttonPage.Markup);

        var buttonGroupPage = context.Render<ButtonGroupPage>();
        Assert.Contains("Extra attributes on the button group root", buttonGroupPage.Markup);

        var iconButtonPage = context.Render<IconButtonPage>();
        Assert.Contains("Button background color class", iconButtonPage.Markup);
        Assert.Contains("Pressed visual state when", iconButtonPage.Markup);
        Assert.Contains("Extra attributes on the icon button element", iconButtonPage.Markup);

        var toggleIconButtonPage = context.Render<ToggleIconButtonPage>();
        Assert.Contains("Button background color class.", toggleIconButtonPage.Markup);
        Assert.Contains("Extra attributes on the toggle icon button element.", toggleIconButtonPage.Markup);

        var fabPage = context.Render<FabPage>();
        Assert.Contains("Button background color class", fabPage.Markup);
        Assert.Contains("Extra attributes on the floating action button element", fabPage.Markup);

        var tagPage = context.Render<TagPage>();
        Assert.Contains("Extra attributes on the tag element", tagPage.Markup);

        var badgePage = context.Render<BadgePage>();
        Assert.Contains("Extra attributes on the badge element", badgePage.Markup);

        var chipPage = context.Render<ChipPage>();
        Assert.Contains("Per-chip active background color", chipPage.Markup);
        Assert.Contains("Extra attributes on the chip button", chipPage.Markup);

        var chipSetPage = context.Render<ChipSetPage>();
        Assert.Contains("Primary", chipSetPage.Markup);
        Assert.Contains("Extra attributes on the chip set root", chipSetPage.Markup);

        var avatarPage = context.Render<AvatarPage>();
        Assert.Contains("Extra attributes on the avatar root", avatarPage.Markup);

        var avatarGroupPage = context.Render<AvatarGroupPage>();
        Assert.Contains("Extra attributes on the avatar group root", avatarGroupPage.Markup);

        var footerPage = context.Render<FooterPage>();
        Assert.Contains("Extra attributes on the footer element", footerPage.Markup);

        var spacerPage = context.Render<SpacerPage>();
        Assert.Contains("Extra attributes on the spacer element", spacerPage.Markup);

        var stackPage = context.Render<StackPage>();
        Assert.Contains("Extra attributes on the stack root", stackPage.Markup);

        var dividerPage = context.Render<DividerPage>();
        Assert.Contains("Extra attributes on the divider element", dividerPage.Markup);
    }

    [Fact]
    public void FormsAndInputsApiPagesDocumentCurrentParameterRows()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var inputPage = context.Render<InputPage>();
        Assert.Contains("Validation expression for EditForm scenarios", inputPage.Markup);
        Assert.Contains("Extra attributes on the input element", inputPage.Markup);

        var textAreaPage = context.Render<TextAreaPage>();
        Assert.Contains("Extra attributes on the textarea element", textAreaPage.Markup);

        var formPage = context.Render<FormPage>();
        Assert.Contains("EditContext", formPage.Markup);
        Assert.Contains("Extra attributes on the EditForm element", formPage.Markup);

        var fieldPage = context.Render<FieldPage>();
        Assert.Contains("Extra attributes on the field root", fieldPage.Markup);

        var selectPage = context.Render<SelectPage>();
        Assert.Contains("Raised when the selected value changes", selectPage.Markup);
        Assert.Contains("Extra attributes on the select element", selectPage.Markup);

        var selectItemPage = context.Render<SelectItemPage>();
        Assert.Contains("Extra attributes on the option element", selectItemPage.Markup);

        var checkboxPage = context.Render<CheckboxPage>();
        Assert.Contains("Extra attributes on the checkbox input", checkboxPage.Markup);

        var radioGroupPage = context.Render<RadioGroupPage>();
        Assert.Contains("EventCallback&lt;TValue?&gt;", radioGroupPage.Markup);
        Assert.Contains("Extra attributes on the radio group wrapper", radioGroupPage.Markup);

        var radioPage = context.Render<RadioPage>();
        Assert.Contains("Extra attributes on the radio input", radioPage.Markup);

        var maskPage = context.Render<MaskPage>();
        Assert.Contains("Extra attributes on the masked input", maskPage.Markup);

        var numberInputPage = context.Render<NumberInputPage>();
        Assert.Contains("Extra attributes on the number input", numberInputPage.Markup);

        var dateInputPage = context.Render<DateInputPage>();
        Assert.Contains("Extra attributes on the date input", dateInputPage.Markup);

        var timeInputPage = context.Render<TimeInputPage>();
        Assert.Contains("Extra attributes on the time input", timeInputPage.Markup);

        var sliderPage = context.Render<SliderPage>();
        Assert.Contains("Extra attributes on the range input", sliderPage.Markup);

        var switchPage = context.Render<SwitchPage>();
        Assert.Contains("Primary", switchPage.Markup);
        Assert.Contains("Extra attributes on the checkbox input", switchPage.Markup);

        var colorInputPage = context.Render<ColorInputPage>();
        Assert.Contains("Extra attributes on the picker root or native color input", colorInputPage.Markup);

        var fileInputPage = context.Render<FileInputPage>();
        Assert.Contains("Extra attributes on the file input", fileInputPage.Markup);

        var autocompletePage = context.Render<AutocompletePage>();
        Assert.Contains("Surface", autocompletePage.Markup);
        Assert.Contains("Extra attributes on the autocomplete input", autocompletePage.Markup);

        var dropZonePage = context.Render<DropZonePage>();
        Assert.Contains("Drop-zone background color class", dropZonePage.Markup);
        Assert.Contains("Extra attributes on the drop-zone root", dropZonePage.Markup);
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
