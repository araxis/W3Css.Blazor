using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace W3Css.Blazor.Docs.Layout;

public partial class NavMenu : IAsyncDisposable
{
    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    private bool collapseNavMenu = true;
    private string filter = string.Empty;
    private ElementReference searchInput;
    private IJSObjectReference? shortcutModule;

    private string NavMenuCssClass => collapseNavMenu ? "sidebar collapsed" : "sidebar";

    private bool HasFilter => !string.IsNullOrWhiteSpace(filter);

    private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        // Absolute URL from the app base so the import resolves under any route and
        // any deployment subpath (GitHub Pages rewrites the base href).
        var moduleUrl = $"{Navigation.BaseUri}js/navSearch.js";
        shortcutModule = await JS.InvokeAsync<IJSObjectReference>("import", moduleUrl);
        await shortcutModule.InvokeVoidAsync("register", searchInput);
    }

    public async ValueTask DisposeAsync()
    {
        if (shortcutModule is null)
        {
            return;
        }

        try
        {
            await shortcutModule.InvokeVoidAsync("dispose");
            await shortcutModule.DisposeAsync();
        }
        catch (JSDisconnectedException)
        {
            // The circuit/runtime is already gone; nothing to clean up.
        }
    }

    private void OnFilterInput(ChangeEventArgs e) => filter = e.Value?.ToString() ?? string.Empty;

    private void OnFilterKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Escape")
        {
            filter = string.Empty;
            return;
        }

        if (e.Key == "Enter")
        {
            var first = FilteredGroups().SelectMany(g => g.Items).FirstOrDefault();
            if (first is not null)
            {
                collapseNavMenu = true;
                Navigation.NavigateTo(first.Href);
            }
        }
    }

    // Returns the nav groups filtered by the search term. Empty filter returns all
    // groups unchanged; a filter keeps only items whose label or keywords match and
    // drops groups that end up empty.
    private IReadOnlyList<NavGroup> FilteredGroups()
    {
        if (!HasFilter)
        {
            return Groups;
        }

        var term = filter.Trim();

        return Groups
            .Select(g => g with { Items = g.Items.Where(i => Matches(i, term)).ToArray() })
            .Where(g => g.Items.Length > 0)
            .ToArray();
    }

    private static bool Matches(NavItem item, string term) =>
        item.Text.Contains(term, StringComparison.OrdinalIgnoreCase)
        || item.Keywords.Contains(term, StringComparison.OrdinalIgnoreCase);

    private sealed record NavItem(string Href, string Text, NavLinkMatch Match = NavLinkMatch.Prefix, string Keywords = "");

    private sealed record NavGroup(string? Title, NavItem[] Items);

    private static readonly NavGroup[] Groups =
    [
        new(null,
        [
            new("", "Overview", NavLinkMatch.All, "home start"),
            new("installation", "Installation", Keywords: "setup install nuget stylesheet"),
            new("starter-kit", "Starter Kit", Keywords: "sample app first screen dashboard settings customers workflow"),
            new("patterns", "Patterns", Keywords: "recipes examples samples dashboard form table modal"),
            new("components", "Components", Keywords: "catalog index all"),
        ]),
        new("Customization",
        [
            new("components/theming", "Theming & Customization", Keywords: "theme customize brand tokens variables provider"),
            new("components/colors", "Colors", Keywords: "palette"),
            new("components/color-schemes", "Color Schemes", Keywords: "palette generator"),
            new("components/dark-mode", "Dark Mode", Keywords: "theme night"),
        ]),
        new("Layout",
        [
            new("components/defaults", "Defaults"),
            new("components/fonts", "Fonts", Keywords: "typography"),
            new("components/trends", "Trends"),
            new("components/case-study", "Case Study"),
            new("components/material-design", "Material Design"),
            new("components/versions", "Versions"),
            new("components/app-shell", "App Shell", Keywords: "layout scaffold"),
            new("components/app-bar", "App Bar", Keywords: "appbar header"),
            new("components/toolbar", "Toolbar"),
            new("components/stack", "Stack"),
            new("components/divider", "Divider"),
            new("components/spacer", "Spacer"),
            new("components/footer", "Footer"),
            new("components/container", "Container"),
            new("components/panel", "Panel"),
            new("components/card", "Card"),
            new("components/paper", "Paper", Keywords: "surface"),
            new("components/row", "Row"),
            new("components/column", "Column"),
            new("components/grid", "Grid"),
            new("components/flex", "Flex", Keywords: "flexbox"),
            new("components/cells", "Cells"),
            new("components/display", "Display"),
            new("components/visibility", "Visibility", Keywords: "hide show"),
            new("components/borders", "Borders"),
            new("components/round", "Round", Keywords: "radius corner"),
            new("components/spacing", "Spacing", Keywords: "margin padding"),
            new("components/text-fonts", "Text Fonts", Keywords: "typography"),
            new("components/effects", "Effects", Keywords: "shadow opacity"),
            new("components/animations", "Animations", Keywords: "transition motion"),
            new("components/hover-colors", "Hover Colors"),
            new("components/direction", "Direction", Keywords: "rtl ltr"),
            new("components/bar", "Bar"),
            new("components/responsive", "Responsive", Keywords: "breakpoint mobile"),
            new("components/mobile", "Mobile"),
        ]),
        new("Forms",
        [
            new("components/input", "Input", Keywords: "text field textfield"),
            new("components/mask", "Mask", Keywords: "format pattern"),
            new("components/number-input", "Number Input", Keywords: "numeric spinner"),
            new("components/date-range-picker", "Date Range Picker", Keywords: "calendar"),
            new("components/date-input", "Date Input", Keywords: "date picker calendar"),
            new("components/time-input", "Time Input", Keywords: "time picker clock"),
            new("components/slider", "Slider", Keywords: "range"),
            new("components/rating", "Rating", Keywords: "stars"),
            new("components/switch", "Switch", Keywords: "toggle"),
            new("components/toggle-group", "Toggle Group", Keywords: "segmented"),
            new("components/color-input", "Color Input", Keywords: "color picker"),
            new("components/file-input", "File Input", Keywords: "upload"),
            new("components/drop-zone", "Drop Zone", Keywords: "upload dropzone drag"),
            new("components/autocomplete", "Autocomplete", Keywords: "combobox typeahead"),
            new("components/select", "Select", Keywords: "dropdown combobox"),
            new("components/select-item", "Select Item"),
            new("components/checkbox", "Checkbox"),
            new("components/radio", "Radio"),
            new("components/radio-group", "Radio Group"),
            new("components/text-area", "Text Area", Keywords: "multiline"),
            new("components/field", "Field"),
            new("components/form", "Form", Keywords: "editform"),
            new("components/validation", "Validation"),
        ]),
        new("Navigation",
        [
            new("components/tabs", "Tabs", Keywords: "dynamic tabs"),
            new("components/accordion", "Accordion", Keywords: "expansion panel collapse"),
            new("components/collapse", "Collapse"),
            new("components/navbar", "Navbar"),
            new("components/nav-menu", "Nav Menu"),
            new("components/bottom-navigation", "Bottom Navigation"),
            new("components/breadcrumb", "Breadcrumb"),
            new("components/page-content-navigation", "Page Content Navigation", Keywords: "toc anchor on this page"),
            new("components/link", "Link", Keywords: "anchor"),
            new("components/dropdown", "Dropdown"),
            new("components/menu", "Menu"),
            new("components/pagination", "Pagination", Keywords: "pager paging"),
            new("components/progress", "Progress", Keywords: "linear bar progressbar"),
            new("components/progress-circular", "Progress Circular", Keywords: "spinner loading"),
            new("components/stepper", "Stepper", Keywords: "wizard steps"),
            new("components/scroll-to-top", "Scroll To Top", Keywords: "back to top"),
            new("components/swipe-area", "Swipe Area", Keywords: "gesture pointer touch mobile"),
            new("components/spinner", "Spinner", Keywords: "loading"),
            new("components/toast", "Toast", Keywords: "snackbar notification message"),
        ]),
        new("Overlays",
        [
            new("components/modal", "Modal", Keywords: "dialog popup"),
            new("components/message-box", "Message Box", Keywords: "confirm prompt dialog alert"),
            new("components/overlay", "Overlay", Keywords: "backdrop scrim"),
            new("components/tooltip", "Tooltip"),
            new("components/popover", "Popover"),
            new("components/focus-trap", "Focus Trap"),
            new("components/drawer", "Drawer"),
            new("components/sidebar", "Sidebar"),
        ]),
        new("Content",
        [
            new("components/image", "Image"),
            new("components/image-list", "Image List", Keywords: "gallery"),
            new("components/slideshow", "Slideshow", Keywords: "carousel"),
            new("components/code", "Code"),
            new("components/highlighter", "Highlighter", Keywords: "mark"),
            new("components/filters", "Filters"),
            new("components/note", "Note", Keywords: "callout"),
            new("components/quote", "Quote", Keywords: "blockquote"),
            new("components/button", "Button"),
            new("components/icon-button", "Icon Button"),
            new("components/toggle-icon-button", "Toggle Icon Button"),
            new("components/fab", "Floating Action Button", Keywords: "fab"),
            new("components/button-group", "Button Group"),
            new("components/action-row", "Action Row", Keywords: "actions buttons commands footer form"),
            new("components/alert", "Alert"),
            new("components/table", "Table", Keywords: "simple table"),
            new("components/data-table", "Data Table", Keywords: "datagrid data grid sortable"),
            new("components/tree-view", "Tree View", Keywords: "treeview hierarchy"),
            new("components/list", "List"),
            new("components/list-item", "List Item"),
            new("components/timeline", "Timeline"),
            new("components/chart", "Chart", Keywords: "graph bar line plot"),
            new("components/chat", "Chat", Keywords: "message bubble"),
            new("components/badge", "Badge"),
            new("components/tag", "Tag", Keywords: "label"),
            new("components/chip-set", "Chip Set"),
            new("components/avatar", "Avatar"),
            new("components/avatar-group", "Avatar Group"),
            new("components/icons", "Icons"),
            new("components/skeleton", "Skeleton", Keywords: "placeholder loading"),
            new("components/empty-state", "Empty State", Keywords: "empty zero error recovery state"),
            new("components/chip", "Chip"),
        ]),
        new(null,
        [
            new("roadmap", "Roadmap"),
        ]),
    ];
}
