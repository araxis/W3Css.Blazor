# Current State

Last updated: 2026-06-01

## Repository

- Canonical branch: `main`.
- Tag `v0.1.0` points at current `HEAD` and has been pushed.
- Solution: `W3Css.Blazor.slnx`.
- SDK: `.NET 10.0.300` (`global.json`).

## Implementation Snapshot

- W3.CSS 5.01 is shipped in `src/W3Css.Blazor/wwwroot/w3css/5/w3.css`.
- Current components in `src/W3Css.Blazor/Components`:
  - 125 `.razor` component files present.
- Current docs routes in `src/W3Css.Blazor.Docs/Pages/ComponentTopics`: 119 pages.
- Tests in project source: 58 `.cs` test files.
- Test suite status:
  - `dotnet test tests/W3Css.Blazor.Tests/W3Css.Blazor.Tests.csproj --configuration Release` -> **459 passing tests**.
- Build status:
  - `dotnet build W3Css.Blazor.slnx --configuration Release` → **0 warnings, 0 errors**.

## Verified Features

- Implemented library includes:
  - Layout/surface: `W3Container`, `W3Panel`, `W3Card`, `W3Paper`, `W3Display`, `W3DisplayContainer`, `W3Flex`, `W3Grid`, `W3Row`, `W3Column`, `W3Cell`, `W3CellRow`, `W3Spacing`, `W3Text`, `W3Effect`, `W3Animate`, `W3HoverColor`, `W3Direction`, `W3Border`.
  - Inputs and forms: `W3Input`, `W3Field`, `W3Form`, `W3Select`, `W3SelectItem`, `W3Checkbox`, `W3RadioGroup`, `W3Radio`, `W3TextArea`, `W3Mask`, `W3NumberInput`, `W3DateInput`, `W3TimeInput`, `W3Slider`, `W3Switch`, `W3ColorInput`, `W3FileInput`, `W3Autocomplete`, `W3DropZone`.
  - Navigation and page structure: `W3Bar`, `W3BarItem`, `W3AppShell`, `W3Navbar`, `W3NavbarItem`, `W3NavMenu`, `W3NavMenuGroup`, `W3NavMenuItem`, `W3Breadcrumb`, `W3BreadcrumbItem`, `W3Dropdown`, `W3Tabs`, `W3TabPanel`, `W3Sidebar`, `W3Pagination`, `W3Stepper`, `W3Step`, `W3BottomNavigation`, `W3BottomNavigationItem`, `W3Menu`, `W3MenuItem`, `W3MenuDivider`, `W3Tooltip`, `W3Popover`, `W3Drawer`, `W3ScrollToTop`, `W3SwipeArea`, `W3AppBar`, `W3Toolbar`, `W3Link`, `W3PageContentNavigation`, `W3PageSection`.
  - Data display: `W3Table`, `W3DataTable`, `W3DataColumn`, `W3List`, `W3ListItem`, `W3Timeline`, `W3TimelineItem`, `W3TreeView`, `W3Chart`.
  - Feedback and overlays: `W3Alert`, `W3Spinner`, `W3MessageBox`, `W3Modal`, `W3Toast`, `W3ToastProvider`, `W3ToastService`, `W3ToastMessage`, `W3ToastOptions`, `W3ToastPosition`, `W3Overlay`, `W3FocusTrap`, `W3Skeleton`.
  - Content/media/utilities: `W3Image`, `W3ImageList`, `W3ImageListItem`, `W3Slideshow`, `W3Slide`, `W3Chat`, `W3ChatMessage`, `W3Code`, `W3Note`, `W3Quote`, `W3Icon`, `W3Highlighter`, `W3Button`, `W3IconButton`, `W3ToggleIconButton`, `W3Fab`, `W3Tag`, `W3Chip`, `W3ChipSet`, `W3Badge`, `W3Avatar`, `W3AvatarGroup`, `W3Footer`, `W3Spacer`, `W3Progress`, `W3ProgressCircular`, `W3Stack`, `W3Divider`, `W3Skeleton`.
  - Theme support: `W3Theme` and `W3ThemeProvider` with bundled `w3-theme.css`, brand tokens, surface tokens, status tokens, dark mode, focus tokens, and radius/font controls.

## Standards and Packaging

- W3.CSS source file is not modified.
- Package metadata is set to:
  - Version `0.1.0`.
  - `Directory.Build.props` repository URL + type (`https://github.com/araxis/W3Css.Blazor`).
  - `PackageProjectUrl`, `RepositoryUrl`, `RepositoryType`, and release notes in `src/W3Css.Blazor/W3Css.Blazor.csproj`.
- README includes package version and MIT license badges.
- Release workflow builds, tests, packs, creates release artifacts, and publishes packages on `v*` tag pushes when the publishing secret is configured.
- First package release is published as `W3Css.Blazor` version `0.1.0`.
- GitHub release `v0.1.0` is published with `W3Css.Blazor.0.1.0.nupkg`.
- Package consumer smoke tooling verifies install, component compilation, publish output, and bundled static assets from the public package feed.

## Current Progress

- Component compatibility strategy is W3.CSS loyal: missing concepts are added as new reusable components where needed and duplicate alias components are intentionally avoided.
- The "themed by default" pass is complete for surface and primary intent defaults in most components.
- Public API hardening is in progress; `W3Slide` now uses the shared component base and forwards unmatched attributes to the rendered active slide.
- Documentation consistency review is complete for current component topic parameter tables; each implemented rendering surface now has explicit inherited attribute rows where applicable.
- The small docs/API gap pass now covers `W3Table`, `W3Drawer`, `W3Menu`, and `W3ProgressCircular` parameter tables with focused docs smoke coverage.
- The second docs/API gap pass now covers `W3DataTable`, `W3DateRangePicker`, `W3MessageBox`, `W3Modal`, and `W3Navbar` parameter tables with focused docs smoke coverage.
- The third docs/API gap pass now covers `W3Chart`, `W3Collapse`, `W3Paper`, `W3Panel`, `W3Container`, `W3Row`, `W3Column`, `W3Grid`, and `W3Flex` parameter tables with focused docs smoke coverage.
- The fourth docs/API gap pass now covers `W3AppShell`, `W3AppBar`, `W3NavMenu`, `W3Toolbar`, and `W3BottomNavigation` parameter tables with focused docs smoke coverage.
- The fifth docs/API gap pass now covers `W3Alert`, `W3Toast`, `W3Overlay`, `W3Popover`, `W3FocusTrap`, `W3Spinner`, and `W3Skeleton` parameter tables with focused docs smoke coverage.
- The sixth docs/API gap pass now covers `W3Image`, `W3ImageList`, `W3ImageListItem`, `W3Slideshow`, `W3Slide`, `W3Chat`, `W3ChatMessage`, `W3Code`, `W3Note`, `W3Quote`, `W3Icon`, and `W3Highlighter` parameter tables with focused docs smoke coverage.
- The seventh docs/API gap pass now covers `W3Button`, `W3ButtonGroup`, `W3IconButton`, `W3ToggleIconButton`, `W3Fab`, `W3Tag`, `W3Badge`, `W3Chip`, `W3ChipSet`, `W3Avatar`, `W3AvatarGroup`, `W3Footer`, `W3Spacer`, `W3Stack`, and `W3Divider` parameter tables with focused docs smoke coverage.
- The eighth docs/API gap pass now covers `W3Input`, `W3TextArea`, `W3Form`, `W3Field`, `W3Select`, `W3SelectItem`, `W3Checkbox`, `W3RadioGroup`, `W3Radio`, `W3Mask`, `W3NumberInput`, `W3DateInput`, `W3TimeInput`, `W3Slider`, `W3Switch`, `W3ColorInput`, `W3FileInput`, `W3Autocomplete`, and `W3DropZone` parameter tables with focused docs smoke coverage.
- The ninth docs/API gap pass now covers `W3Accordion`, `W3AccordionItem`, `W3Dropdown`, `W3Pagination`, `W3Sidebar`, `W3Tooltip`, `W3ScrollToTop`, `W3Link`, `W3PageContentNavigation`, and `W3PageSection` parameter/property tables with focused docs smoke coverage.
- The tenth docs/API gap pass now covers `W3Bar`, `W3BarItem`, `W3Card`, `W3CellRow`, `W3Cell`, `W3DisplayContainer`, `W3Display`, `W3Responsive`, `W3Spacing`, `W3Text`, `W3Effect`, `W3Animate`, `W3HoverColor`, `W3Direction`, and `W3Border` parameter tables with focused docs smoke coverage.
- The eleventh docs/API gap pass now covers the remaining breadcrumb, list, progress, rating, stepper, timeline, toggle group, and tree view parameter tables with focused docs smoke coverage.
- The form-control UX audit added input-time updates to `W3TextArea`, refreshed its docs example/API table, and verified the rendered docs page.
- The field UX audit added stable spacing hooks and customizable validation message classes to `W3Field`, refreshed the docs example/API table, and verified the rendered field page.
- The form submit UX audit added `Busy`/`Disabled` child-control disablement to `W3Form`, exposed `aria-busy`, refreshed the docs example/API table, and verified the rendered form page.
- The select UX audit added first-class `Placeholder` and `PlaceholderDisabled` support to `W3Select`, refreshed the select and validation examples, and verified the rendered select page.
- The choice-input UX audit added required-state support and label spacing hooks to `W3Checkbox`/`W3Radio`, made `W3RadioGroup` validation-aware, refreshed checkbox/radio docs, and verified the rendered docs pages.
- The value-input UX audit added required-state and validation ARIA support to `W3NumberInput`, `W3DateInput`, and `W3TimeInput`, refreshed required validation examples, and verified the rendered docs pages.
- The remaining input-control UX audit added validation, required-state, and accessibility polish to `W3Slider`, `W3Switch`, `W3Rating`, `W3ColorInput`, `W3FileInput`, `W3DropZone`, and `W3Autocomplete`, refreshed docs/examples, and verified the rendered docs pages.
- The focus and keyboard polish pass added theme-controlled focus tokens (`FocusColor`, `FocusWidth`, `FocusOffset`) plus bundled `:focus-visible` styles for links, buttons, inputs, and interactive roles; docs and browser verification cover the theming page.
- The tabs keyboard polish pass added arrow-key, Home/End, and Delete handling to `W3Tabs`, refreshed tab docs, and verified the rendered tabs page.
- The rating keyboard polish pass added arrow-key, Home/End, and clear-key handling to `W3Rating`, refreshed rating docs, and verified the rendered rating page.
- The stepper keyboard polish pass added arrow-key and Home/End handling to `W3Stepper` while respecting disabled and linear workflow rules, refreshed stepper docs, and verified the rendered stepper page.
- The pagination keyboard polish pass added arrow-key and Home/End handling to `W3Pagination`, refreshed pagination docs, and verified the rendered pagination page.
- The bottom-navigation keyboard polish pass added arrow-key and Home/End handling to `W3BottomNavigation` while skipping disabled and value-less items, refreshed bottom-navigation docs, and verified the rendered bottom-navigation page.
- The menu keyboard polish pass added roving item focus, ArrowUp/ArrowDown, Home/End, and Escape handling to `W3Menu`, refreshed menu docs, and verified the rendered menu page.
- The dropdown keyboard polish pass added ArrowUp/ArrowDown open behavior, Escape close behavior, trigger focus return, and content-control linkage to `W3Dropdown`, refreshed dropdown docs, and verified the rendered dropdown page.
- The popover keyboard polish pass added Escape close behavior, trigger focus return, and content-control linkage to `W3Popover`, refreshed popover docs, and verified the rendered popover page.
- The sidebar keyboard polish pass added Escape close behavior, a focusable open surface, and an optional accessible label to `W3Sidebar`, refreshed sidebar docs, and verified the rendered sidebar page.
- The modal accessibility polish pass added explicit `AriaLabel`/`AriaLabelledBy` support, stopped emitting broken title references for custom-header/no-title dialogs, refreshed modal docs, and verified the rendered modal page.
- The message-box accessibility polish pass added explicit `AriaLabel`/`AriaLabelledBy` pass-through to `W3MessageBox`, refreshed message-box keyboard and labeling docs, and verified the rendered message-box page.
- The drawer accessibility polish pass added explicit `AriaLabelledBy` support to `W3Drawer`, refreshed drawer keyboard and labeling docs, and verified the rendered drawer page.
- The alert behavior polish pass added opt-in dismissible alerts with visible binding, dismissal callbacks, and configurable live-region announcements; refreshed alert docs and verified the rendered alert page.
- The dialog action-spacing polish pass replaced modal action bars with a wrapping right-aligned action row, removed manual message-box button margins, refreshed modal/message-box docs, and verified rendered modal pages.
- The app-bar layout polish pass added explicit slot-presence classes and title-fill behavior so navigation/actions align as trailing app-bar content without large auto-margin gaps.
- The swipe-area feature pass added `W3SwipeArea`, `W3SwipeDirection`, and `W3SwipeEventArgs` for pointer gesture surfaces with directional callbacks, docs, and bUnit coverage.
- Neutral terminology cleanup is complete; source, docs, tests, and memory no longer use external comparison-library names for the compatibility work.
- The semantic theme-token expansion added provider-controlled Info/Success/Warning/Danger/Note tokens that match existing W3.CSS status colors by default and can be reused through `W3Color` values, alert kinds, toasts, text, borders, and hover utilities.
- The stack alignment polish added center/end main-axis alignment to `W3Stack`, making wrapped action rows easier to build without one-off CSS.
- The card action-row polish added an optional wrapping right-aligned `W3Card.Actions` footer slot for app cards with reliable button spacing.
- The data-table action-row polish added a wrapping, right-aligned row-action group with gap and class hooks for compact command cells.
- Remaining planned work is optional: visual quality sweeps.
