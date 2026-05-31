# Current State

Last updated: 2026-05-31

## Repository

- Canonical branch: `main`.
- Tag `v0.1.0` points at current `HEAD` and has been pushed.
- Solution: `W3Css.Blazor.slnx`.
- SDK: `.NET 10.0.300` (`global.json`).

## Implementation Snapshot

- W3.CSS 5.01 is shipped in `src/W3Css.Blazor/wwwroot/w3css/5/w3.css`.
- Current components in `src/W3Css.Blazor/Components`:
  - 124 `.razor` component files present.
- Current docs routes in `src/W3Css.Blazor.Docs/Pages/ComponentTopics`: 118 pages.
- Tests in project source: 65 `.cs` test files.
- Test suite status:
  - `dotnet test tests/W3Css.Blazor.Tests/W3Css.Blazor.Tests.csproj --configuration Release` → **384 passing tests**.
- Build status:
  - `dotnet build W3Css.Blazor.slnx --configuration Release` → **0 warnings, 0 errors**.

## Verified Features

- Implemented library includes:
  - Layout/surface: `W3Container`, `W3Panel`, `W3Card`, `W3Paper`, `W3Display`, `W3DisplayContainer`, `W3Flex`, `W3Grid`, `W3Row`, `W3Column`, `W3Cell`, `W3CellRow`, `W3Spacing`, `W3Text`, `W3Effect`, `W3Animate`, `W3HoverColor`, `W3Direction`, `W3Border`.
  - Inputs and forms: `W3Input`, `W3Field`, `W3Form`, `W3Select`, `W3SelectItem`, `W3Checkbox`, `W3RadioGroup`, `W3Radio`, `W3TextArea`, `W3Mask`, `W3NumberInput`, `W3DateInput`, `W3TimeInput`, `W3Slider`, `W3Switch`, `W3ColorInput`, `W3FileInput`, `W3Autocomplete`, `W3DropZone`.
  - Navigation and page structure: `W3Bar`, `W3BarItem`, `W3AppShell`, `W3Navbar`, `W3NavbarItem`, `W3NavMenu`, `W3NavMenuGroup`, `W3NavMenuItem`, `W3Breadcrumb`, `W3BreadcrumbItem`, `W3Dropdown`, `W3Tabs`, `W3TabPanel`, `W3Sidebar`, `W3Pagination`, `W3Stepper`, `W3Step`, `W3BottomNavigation`, `W3BottomNavigationItem`, `W3Menu`, `W3MenuItem`, `W3MenuDivider`, `W3Tooltip`, `W3Popover`, `W3Drawer`, `W3ScrollToTop`, `W3AppBar`, `W3Toolbar`, `W3Link`, `W3PageContentNavigation`, `W3PageSection`.
  - Data display: `W3Table`, `W3DataTable`, `W3DataColumn`, `W3List`, `W3ListItem`, `W3Timeline`, `W3TimelineItem`, `W3TreeView`, `W3Chart`.
  - Feedback and overlays: `W3Alert`, `W3Spinner`, `W3MessageBox`, `W3Modal`, `W3Toast`, `W3ToastProvider`, `W3ToastService`, `W3ToastMessage`, `W3ToastOptions`, `W3ToastPosition`, `W3Overlay`, `W3FocusTrap`, `W3Skeleton`.
  - Content/media/utilities: `W3Image`, `W3ImageList`, `W3ImageListItem`, `W3Slideshow`, `W3Slide`, `W3Chat`, `W3ChatMessage`, `W3Code`, `W3Note`, `W3Quote`, `W3Icon`, `W3Highlighter`, `W3Button`, `W3IconButton`, `W3ToggleIconButton`, `W3Fab`, `W3Tag`, `W3Chip`, `W3ChipSet`, `W3Badge`, `W3Avatar`, `W3AvatarGroup`, `W3Footer`, `W3Spacer`, `W3Progress`, `W3ProgressCircular`, `W3Stack`, `W3Divider`, `W3Skeleton`.
  - Theme support: `W3Theme` and `W3ThemeProvider` with bundled `w3-theme.css`.

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
- Documentation consistency review is in progress; `W3Tabs` now documents the add/close tab API and current defaults, and `W3ToggleGroup` documents bind callbacks plus current selected-color defaults.
- The small docs/API gap pass now covers `W3Table`, `W3Drawer`, `W3Menu`, and `W3ProgressCircular` parameter tables with focused docs smoke coverage.
- The second docs/API gap pass now covers `W3DataTable`, `W3DateRangePicker`, `W3MessageBox`, `W3Modal`, and `W3Navbar` parameter tables with focused docs smoke coverage.
- The third docs/API gap pass now covers `W3Chart`, `W3Collapse`, `W3Paper`, `W3Panel`, `W3Container`, `W3Row`, `W3Column`, `W3Grid`, and `W3Flex` parameter tables with focused docs smoke coverage.
- The fourth docs/API gap pass now covers `W3AppShell`, `W3AppBar`, `W3NavMenu`, `W3Toolbar`, and `W3BottomNavigation` parameter tables with focused docs smoke coverage.
- Neutral terminology cleanup is complete; source, docs, tests, and memory no longer use external comparison-library names for the compatibility work.
- Remaining planned work is optional: extra theme tokens, focus-ring/theming polish, and `W3SwipeArea`-style pointer gesture support.
