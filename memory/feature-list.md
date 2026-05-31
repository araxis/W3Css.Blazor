# Feature List

Last updated: 2026-05-31

Status values:

- Planned: not started.
- In progress: started but incomplete.
- Implemented: available in the library.
- Later: intentionally deferred.

## Core Foundation

| Feature | Status | Notes |
| --- | --- | --- |
| W3.CSS static asset packaging | Implemented | W3.CSS 5.01 is bundled at `_content/W3Css.Blazor/w3css/5/w3.css`. |
| Class builder | Implemented | Internal class composition utility removes duplicate classes. |
| Common component base | Implemented | Supports `Class`, `Style`, and `AdditionalAttributes`. |
| Shared style enums | Implemented | Includes color, size, roundness, card depth, alert kind, toast position, image effect, display position, visibility, cell alignment, animation, text direction, border side, border bar, padding, top padding, margin, text alignment, text style, font family, effect style, and hover effect. |
| XML documentation | In progress | Public enums and core component APIs documented; continuing as new APIs land. |
| Package metadata | Implemented | Version is pinned to `0.1.0`; repository URL and package policy are populated in project and release metadata. |

## Component Category Model

The docs catalog is grouped for real app/site work:

- Foundation and theme guidance.
- Site composition.
- Layout and surfaces.
- Actions, content, and media.
- Data display.
- Inputs and forms.
- Navigation and disclosure.
- Feedback and overlays.
- Utilities.

This model is intentionally similar to mature Blazor libraries, while keeping implementations W3.CSS-first.

## Layout Components

| Component | Status | Notes |
| --- | --- | --- |
| `W3Container` | Implemented | Core wrapper with background/text/roundness/color options. |
| `W3Panel` | Implemented | General panel with content and style variants. |
| `W3Card` | Implemented | Cards with header/body/footer slot classes and rich color/rounding options. |
| `W3Paper` | Implemented | W3.CSS-first paper surface wrapper with elevation, square, and outlined modes. |
| `W3Row` | Implemented | W3.CSS row layout wrapper. |
| `W3Column` | Implemented | Responsive small/medium/large column spans. |
| `W3Grid` | Implemented | W3.CSS grid utility wrapper. |
| `W3Flex` | Implemented | W3.CSS flex utility wrapper. |
| `W3Bar` | Implemented | Horizontal or block bar composition. |
| `W3BarItem` | Implemented | Links/buttons with mobile and disabled behavior. |
| `W3DisplayContainer` | Implemented | Positioned display container for overlays/placement. |
| `W3Display` | Implemented | `w3-show*`, `w3-hide*`, and custom position composition. |
| `W3CellRow` | Implemented | Table-cell-like row utility. |
| `W3Cell` | Implemented | Table-cell utility with alignment and mobile behavior. |
| `W3Spacing` | Implemented | Typed padding/margin/section class composition. |
| `W3Text` | Implemented | Text alignment/style/family utility wrapper. |
| `W3Effect` | Implemented | Effects wrapper utilities. |
| `W3Animate` | Implemented | Animation utility wrapper. |
| `W3HoverColor` | Implemented | Hover background/text/border classes. |
| `W3Direction` | Implemented | Direction utility composition (`ltr`/`rtl`). |
| `W3Responsive` | Implemented | Overflow wrapper for wide content. |
| `W3AppShell` | Implemented | App shell with sidebar/main/footer/header orchestration. |
| `W3Sidebar` | Implemented | Sidebar helper for app/site layouts. |
| `W3AppBar` | Implemented | App header composition with title, menu, nav, actions, and placement options. |
| `W3Toolbar` | Implemented | Dense action rows for editors, tables, and dashboards. |
| `W3Divider` | Implemented | Divider utility and section breaks. |
| `W3Stack` | Implemented | Simple vertical/horizontal spacing container. |
| `W3Spacer` | Implemented | W3.CSS-first flex filler for bars/toolbars. |
| `W3Footer` | Implemented | W3.CSS-first footer region with color/border/fixed options. |

## Content Components

| Component | Status | Notes |
| --- | --- | --- |
| `W3Button` | Implemented | Core button wrapper with W3.CSS semantics and callbacks. |
| `W3IconButton` | Implemented | Compact icon-only button action slot. |
| `W3ToggleIconButton` | Implemented | Stateful icon toggle with accessible pressed state and optional two-way binding. |
| `W3Fab` | Implemented | Floating action button with shape and color variants. |
| `W3Badge` | Implemented | Compact semantic status marker. |
| `W3Tag` | Implemented | Small labeled tag/chip style marker. |
| `W3Chip` | Implemented | Standalone chip with typography and color variants. |
| `W3ChipSet` | Implemented | Group of selectable/filter chips. |
| `W3Chat` | Implemented | Message stream container with timestamp-friendly message rendering. |
| `W3ChatMessage` | Implemented | Message row model/renderer for `W3Chat`. |
| `W3Image` | Implemented | Responsive image wrapper and effect utilities. |
| `W3ImageListItem` | Implemented | Image list item used inside `W3ImageList` for gallery and card-style collections. |
| `W3Slideshow` | Implemented | Script-free slideshow with controls, indicators, and optional auto-cycle. |
| `W3Slide` | Implemented | Single slide model used by slideshow/carousel components. |
| `W3ImageList` | Implemented | Image list/grid composition. |
| `W3Code` | Implemented | Inline and block code formatting support. |
| `W3Highlighter` | Implemented | Text highlight utility for matched phrases. |
| `W3Note` | Implemented | Semantic note panel component. |
| `W3Quote` | Implemented | Citation-style quote component. |
| `W3Avatar` | Implemented | Avatar with initials/image/fallback behavior. |
| `W3AvatarGroup` | Implemented | Overlapping avatar row for compact participant displays. |
| `W3Icon` | Implemented | Lightweight wrapper for icon classes. |

## Inputs And Forms

| Component | Status | Notes |
| --- | --- | --- |
| `W3Input` | Implemented | Validation-friendly text input with `w3-input`. |
| `W3Mask` | Implemented | Masked input with digit, letter, alphanumeric tokens, and literal delimiters. |
| `W3NumberInput<TValue>` | Implemented | Native numeric input with W3.CSS styling and parsing helpers. |
| `W3DateInput` | Implemented | Date picker input with range/date binding helpers. |
| `W3TimeInput` | Implemented | Native time input with range and formatting support. |
| `W3Slider<TValue>` | Implemented | Native range input wrapper with step/min/max. |
| `W3Switch` | Implemented | Accessible toggle-style checkbox. |
| `W3ToggleGroup<TValue>` / `W3ToggleItem<TValue>` | Implemented | Segmented single/multi selector with `aria-pressed` items. |
| `W3ColorInput` | Implemented | Native color input with compact/full-width modes, plus an opt-in `ShowPalette` picker (preview + native + hex field + preset palette) that absorbed the former W3ColorPicker. |
| `W3FileInput` | Implemented | `InputFile` wrapper with accept/multiple/listing. |
| `W3RadioGroup` | Implemented | Binds grouped `W3Radio` options through shared value and selection events. |
| `W3DropZone` | Implemented | Large-file input and drag/drop surface component. |
| `W3Autocomplete` | Implemented | Searchable listbox/combo with templates and keyboard interaction. |
| `W3Select` | Implemented | Generic select wrapper with validation patterns. |
| `W3SelectItem` | Implemented | Typed option helper used inside `W3Select` and W3.CSS-first composition. |
| `W3Checkbox` | Implemented | Checkbox with validation and label support. |
| `W3Radio` | Implemented | Typed radio button wrapper. |
| `W3RadioGroup` | Implemented | Coordinates grouped `W3Radio` selection through shared value binding. |
| `W3TextArea` | Implemented | Text area wrapper with validation support and optional input-time updates. |
| `W3Field` | Implemented | Label/help/validation wrapper for form fields, with stable spacing hooks and custom validation message classes. |
| `W3Form` | Implemented | W3.CSS-first form wrapper using EditForm-style callbacks and class/style passthrough. |
| `W3Rating` | Implemented | Icon-first rating input component. |
| `W3DateRangePicker` | Implemented | Date range selector composed from two `W3DateInput` controls (range capability a single date input lacks). |

## Navigation

| Component | Status | Notes |
| --- | --- | --- |
| `W3Tabs` | Implemented | Blazor-controlled tabs; supports optional closeable (`ShowCloseButtons`/`Closeable`, `OnCloseTab`) and addable (`ShowAddButton`, `OnAddTab`) modes that absorbed the former W3DynamicTabs. |
| `W3TabPanel` | Implemented | Paired tab panel with active state and per-panel `Closeable` flag. |
| `W3Accordion` / `W3AccordionItem` | Implemented | Disclosure primitives with accessible states. |
| `W3Pagination` | Implemented | Button pagination with current-page binding. |
| `W3Progress` | Implemented | Determinate progress bar. |
| `W3ProgressCircular` | Implemented | Circular progress component with determinate/indeterminate modes. |
| `W3Navbar` / `W3NavbarItem` | Implemented | App-style navbar with mobile behavior and dropdown support. |
| `W3NavMenu` / `W3NavMenuGroup` / `W3NavMenuItem` | Implemented | Grouped navigation with route-aware links, badges, and command actions. |
| `W3Breadcrumb` / `W3BreadcrumbItem` | Implemented | Ordered breadcrumb path component. |
| `W3PageContentNavigation` / `W3PageSection` | Implemented | In-page table-of-contents scrollspy; `IntersectionObserver` via `w3PageContentNavigation.js` highlights the section in view. |
| `W3Dropdown` | Implemented | Stateful dropdown with outside-click close behavior. |
| `W3Stepper` / `W3Step` | Implemented | Workflow step components. |
| `W3ScrollToTop` | Implemented | Back-to-top button that appears past a scroll threshold and smooth-scrolls up (small `w3ScrollToTop.js` for the scroll listener). |
| `W3Menu` / `W3MenuItem` / `W3MenuDivider` | Implemented | Command menus with disabled items, descriptions, and close-on-select. |
| `W3BottomNavigation` | Implemented | Mobile bottom navigation with icon/label/badge support. |
| `W3Link` | Implemented | W3.CSS-first link/button primitive with optional underline control and disabled non-interactive fallback. |

## Data Display

| Component | Status | Notes |
| --- | --- | --- |
| `W3Table` | Implemented | Standard table with striped/bordered/hoverable/responsive options and `Dense` (`w3-small`) row density. |
| `W3DataTable<TItem>` / `W3DataColumn<TItem>` | Implemented | App data table over `W3Table` with search, sort, paging, loading/empty states, selection, and row actions. |
| `W3TreeView<TItem>` | Implemented | Hierarchical tree with expansion/selection state and templated nodes. |
| `W3List` / `W3ListItem` | Implemented | Lists and clickable list rows. |
| `W3Timeline` / `W3TimelineItem` | Implemented | Activity/audit/release timelines. |
| `W3Chart` / `W3ChartSeries` | Implemented | Dependency-free, script-free SVG charts: Bar, Line, Pie, Donut, with legend, axis labels, and per-series colors. |

## Feedback And Overlays

| Component | Status | Notes |
| --- | --- | --- |
| `W3Modal` | Implemented | Blazor-controlled modal with backdrop, close handling, and an `Actions` footer convenience (the former W3Dialog folded in). |
| `W3MessageBox` | Implemented | Confirm/alert prompt with `@bind-Visible` and `bool?` Yes/No/Cancel result. |
| `W3Tooltip` | Implemented | CSS-first tooltip wrapper. |
| `W3Popover` | Implemented | Action/detail overlays with anchored popover behavior. |
| `W3Drawer` | Implemented | Side drawer for temporary/persistent overlays. |
| `W3FocusTrap` | Implemented | Focus containment helper for overlay interactions. |
| `W3Overlay` | Implemented | Standalone backdrop layer with optional containment and auto-close behavior. |
| `W3Toast` | Implemented | Toast surface with service + provider integration. |
| `W3ToastProvider` / `W3ToastService` | Implemented | Global toast rendering infrastructure. |
| `W3Spinner` | Implemented | Accessible status spinner with size/color variants. |
| `W3Skeleton` | Implemented | Placeholder loading component. |
| `W3Slideshow` / `W3Slide` | Implemented | Script-free slideshow with controls/indicators. |
| `W3Alert` | Implemented | W3.CSS alert surfaces for status and feedback. |

## Theming And Assets

| Feature | Status | Notes |
| --- | --- | --- |
| Theme token system | Implemented (MVP) | `W3Theme` + `W3ThemeProvider` emit CSS variables + token classes; `W3Color.Primary/Secondary/Accent/Surface` adopt the theme. One place → reskin everything, arbitrary hex, dark mode. Non-breaking/opt-in; "themed by default" is the next phase. See [[decisions]]. |
| W3.CSS static file bundling | Implemented | W3.CSS 5.01 bundle in package assets. |
| Docs-only icon/font/theme support | Implemented | Icons and fonts are loaded only in docs examples; not bundled by default. |
| Icon strategy | Implemented | Consumer-provided icon classes via `W3Icon`. |
| Project mark for docs | Implemented | SVG mark in docs assets. |
| Core guidance topics | Implemented | Defaults, Fonts, Colors, Color Schemes, Trends, Case Study, Material Design, Versions, Dark Mode, Mobile, Visibility, Borders, Round, Spacing, Text Fonts, Effects, Animations, Hover Colors, Direction, Filters, Validation. |

## Utility Coverage

| Area | Status | Notes |
| --- | --- | --- |
| Border utilities | Implemented | `w3-border*`, `w3-bar`, `w3-border-*` wrappers. |
| Spacing utilities | Implemented | `w3-padding*`, `w3-margin*`, `w3-section` family support. |
| Text and font utilities | Implemented | Alignment/style/font wrappers and docs. |
| Display/Visibility utilities | Implemented | Positioning, fixed wrappers, and visibility helpers. |
| Color utilities | Implemented | `W3Color` plus alert/border/hover color support. |
| Round utilities | Implemented | `w3-round-*` helpers and `W3Round` wrappers. |
| Animation utilities | Implemented | Effect and motion wrappers (`Animate` / `Effect`). |
| Hover utilities | Implemented | `W3HoverColor` wrappers. |
| Direction utilities | Implemented | `W3Direction` and text direction support. |
| Mobile utilities | Implemented | Mobile wrappers and responsive guidance. |

## Documentation

| Area | Status | Notes |
| --- | --- | --- |
| Overview and installation | Implemented | Core onboarding and package usage. |
| Components index | Implemented | Categorized topics, next/previous navigation, backlog sections. |
| Component topic pages | Implemented | Current topics documented with examples, code blocks, references, and related links. |
| API reference | Implemented | Per-component parameter tables for implemented surfaces. |
| Roadmap | Implemented | Phase-driven expansion plan and readiness markers. |
| GitHub Pages workflow | Implemented | Auto publishes docs from `main`. |
| Docs smoke tests | Implemented | Route metadata and key interaction coverage. |

## Testing

| Test Type | Status | Notes |
| --- | --- | --- |
| Component rendering | Implemented | Core rendering, classes, attributes, and semantics covered. |
| Events and interactions | Implemented | Interaction coverage for tabs, accordion, dropdowns, stepper, nav menu, and overlays. |
| Form behavior | Implemented | Validation, updates, and native input behavior coverage. |
| Navigation and page links | Implemented | Route checks and component index path coverage. |
| Overlay behavior | Implemented | Modal, tooltip, toast, sidebar, focus trap, menu, overlay interactions, and backdrop composition. |
| Content display and timeline | Implemented | Tree, chat, timeline, data table, rating, drop zone, and gallery coverage. |
| Feedback and loading | Implemented | Spinner, skeleton, toast, file, autocomplete, and alert coverage. |
| Package validation | Implemented | `dotnet pack` generates `W3Css.Blazor.0.1.0.nupkg`; CI uploads package artifact. |
| Test count | Implemented | `dotnet test` reports 420 passing tests. |

## Component Compatibility Backlog

**Design rule (2026-05-29):** the library is **W3.CSS-loyal with one canonical component per concept**. Do NOT add alias shims named after a reference library (`W3TextField`, `W3NumericField`, `W3DataGrid`, etc.). Instead, fold any missing reference-library *feature* into the existing W3.CSS component, and add a brand-new component ONLY when the capability is entirely absent from the W3.CSS set. See [[decisions]] consolidation entry.

Consolidation (2026-05-29) removed 15 alias shims and folded their unique features into canonicals: `W3SimpleTable`→`W3Table.Dense`; `W3DataGrid`→`W3DataTable` (already had selection); `W3ColorPicker`→`W3ColorInput.ShowPalette`; `W3DynamicTabs`→`W3Tabs` closeable/addable; `W3Dialog`→`W3Modal.Actions` (W3MessageBox rebuilt on W3Modal); pure aliases removed outright (`W3TextField`, `W3NumericField`, `W3DatePicker`, `W3TimePicker`, `W3ProgressLinear`, `W3Snackbar`, `W3Carousel`, `W3CarouselItem`, `W3ExpansionPanels`, `W3ExpansionPanel`).

New-capability components kept (not duplicates): `W3Spacer`, `W3Footer`, `W3AvatarGroup`, `W3MessageBox`, `W3ToggleGroup`/`W3ToggleItem`, `W3Mask`, `W3DateRangePicker`, `W3Paper`.

The component compatibility backlog is effectively complete. One niche candidate remains:

| Candidate | Category | Priority | W3.CSS-first approach |
| --- | --- | --- | --- |
| `W3SwipeArea` | Utilities | Later | Needs pointer/JS interop; deferred to keep the CSS-first principle — implement only if a real need arises. |

Notes:

- A duplicate hidden component is NOT added — responsive show/hide is already `W3Display`/`W3Responsive`'s job.
- Reference-library theming infrastructure stays **guidance-first**, consistent with the project's dark-mode/color decisions; not planned as a component.
- Reference card sub-components are already covered by `W3Card` header/body/footer slots — no separate components.
