# Current State

Last updated: 2026-06-05

## Repository

- Canonical branch: `main`.
- Tags `v0.1.0`, `v0.2.0`, `v0.3.0`, `v0.4.0`, `v0.5.0`, `v0.5.1`, `v0.5.2`, `v0.5.3`, `v0.6.0`, and `v0.7.0` mark published package releases; `0.8.0` is prepared locally and awaits merge/tag/publish.
- Solution: `W3Css.Blazor.slnx`.
- SDK: `.NET 10.0.300` (`global.json`).

## Implementation Snapshot

- W3.CSS 5.01 is shipped in `src/W3Css.Blazor/wwwroot/w3css/5/w3.css`.
- Current components in `src/W3Css.Blazor/Components`:
  - 127 `.razor` component files present.
- Current docs routes in `src/W3Css.Blazor.Docs/Pages/ComponentTopics`: 121 pages, plus top-level Starter Kit, Gallery, and Patterns adoption pages.
- Starter sample: `samples/W3Css.Blazor.StarterKit` is included in the solution and builds against the local library project.
- Tests in project source: 71 `.cs` test files.
- Test suite status:
  - `dotnet test W3Css.Blazor.slnx --configuration Release --no-build /nr:false` -> **490 passing tests**.
- Build status:
  - `dotnet build W3Css.Blazor.slnx --configuration Release` → **0 warnings, 0 errors**.

## Verified Features

- Implemented library includes:
  - Layout/surface: `W3Container`, `W3Panel`, `W3Card`, `W3Paper`, `W3Display`, `W3DisplayContainer`, `W3Flex`, `W3Grid`, `W3Row`, `W3Column`, `W3Cell`, `W3CellRow`, `W3Spacing`, `W3Text`, `W3Effect`, `W3Animate`, `W3HoverColor`, `W3Direction`, `W3Border`.
  - Inputs and forms: `W3Input`, `W3Field`, `W3Form`, `W3Select`, `W3SelectItem`, `W3Checkbox`, `W3RadioGroup`, `W3Radio`, `W3TextArea`, `W3Mask`, `W3NumberInput`, `W3DateInput`, `W3TimeInput`, `W3Slider`, `W3Switch`, `W3ColorInput`, `W3FileInput`, `W3Autocomplete`, `W3DropZone`.
  - Navigation and page structure: `W3Bar`, `W3BarItem`, `W3AppShell`, `W3Navbar`, `W3NavbarItem`, `W3NavMenu`, `W3NavMenuGroup`, `W3NavMenuItem`, `W3Breadcrumb`, `W3BreadcrumbItem`, `W3Dropdown`, `W3Tabs`, `W3TabPanel`, `W3Sidebar`, `W3Pagination`, `W3Stepper`, `W3Step`, `W3BottomNavigation`, `W3BottomNavigationItem`, `W3Menu`, `W3MenuItem`, `W3MenuDivider`, `W3Tooltip`, `W3Popover`, `W3Drawer`, `W3ScrollToTop`, `W3SwipeArea`, `W3AppBar`, `W3Toolbar`, `W3Link`, `W3PageContentNavigation`, `W3PageSection`.
  - Data display: `W3Table`, `W3DataTable`, `W3DataColumn`, `W3List`, `W3ListItem`, `W3Timeline`, `W3TimelineItem`, `W3TreeView`, `W3Chart`.
  - Feedback and overlays: `W3Alert`, `W3Spinner`, `W3EmptyState`, `W3MessageBox`, `W3Modal`, `W3Toast`, `W3ToastProvider`, `W3ToastService`, `W3ToastMessage`, `W3ToastOptions`, `W3ToastPosition`, `W3Overlay`, `W3FocusTrap`, `W3Skeleton`.
  - Content/media/utilities: `W3Image`, `W3ImageList`, `W3ImageListItem`, `W3Slideshow`, `W3Slide`, `W3Chat`, `W3ChatMessage`, `W3Code`, `W3Note`, `W3Quote`, `W3Icon`, `W3Highlighter`, `W3Button`, `W3IconButton`, `W3ToggleIconButton`, `W3Fab`, `W3ActionRow`, `W3Tag`, `W3Chip`, `W3ChipSet`, `W3Badge`, `W3Avatar`, `W3AvatarGroup`, `W3Footer`, `W3Spacer`, `W3Progress`, `W3ProgressCircular`, `W3Stack`, `W3Divider`, `W3Skeleton`.
  - Theme support: `W3Theme` and `W3ThemeProvider` with bundled `w3-theme.css`, brand tokens, surface tokens, status tokens, dark mode, focus tokens, and radius/font controls.

## Standards and Packaging

- W3.CSS source file is not modified.
- Package metadata is set to:
  - Version `0.8.0`.
  - `Directory.Build.props` repository URL + type (`https://github.com/araxis/W3Css.Blazor`).
  - `PackageProjectUrl`, `RepositoryUrl`, `RepositoryType`, and release notes in `src/W3Css.Blazor/W3Css.Blazor.csproj`.
- README includes package version and MIT license badges.
- Release workflow builds, tests, packs, creates release artifacts, and publishes packages on `v*` tag pushes when the publishing secret is configured.
- First package release is published as `W3Css.Blazor` version `0.1.0`.
- Current package release is published as `W3Css.Blazor` version `0.7.0`; package metadata targets `0.8.0`.
- Releases `v0.1.0`, `v0.2.0`, `v0.3.0`, `v0.4.0`, `v0.5.0`, `v0.5.1`, `v0.5.2`, `v0.5.3`, `v0.6.0`, and `v0.7.0` are published with package artifacts.
- Package consumer smoke tooling verifies install, component compilation, publish output, bundled static assets, component styles inside the bundled stylesheet, and starter-kit primitives; local smoke uses an isolated per-run package cache so repeated same-version artifact checks do not reuse stale packages.

## Current Progress

- Component compatibility strategy is W3.CSS loyal: missing concepts are added as new reusable components where needed and duplicate alias components are intentionally avoided.
- The "themed by default" pass is complete for surface and primary intent defaults in most components.
- Public API hardening is in progress; `W3Slide` now uses the shared component base and forwards unmatched attributes to the rendered active slide.
- README, changelog, release notes, and memory release-state cleanup now reflect the published 0.2.0 release.
- README, changelog, release notes, and memory release-state cleanup now reflect the published 0.3.0 release.
- The 0.3.0 adoption-polish slice is complete: Patterns docs, README adoption path, release notes, versions docs, selected public XML documentation guard coverage, local package smoke, public package smoke, and top-page browser sweep all passed without changing vendored W3.CSS.
- The 0.4.0 quality-hardening release is complete: broad public API XML documentation guards, docs/API parity guards, browser sweep tooling, release notes, versions docs, README links, local package smoke, public package smoke, browser sweep, and memory updates passed without changing vendored W3.CSS.
- The 0.5.0 starter-kit adoption release is complete: sample app, starter-kit docs, component-index starter path, README links, release notes, expanded package smoke coverage, docs browser sweep, starter app browser verification, tag workflow, and public package smoke all passed without changing vendored W3.CSS.
- The 0.5.1 starter-kit polish patch release is complete: package-level starter sample styling, single package stylesheet verification, release notes, tag workflow, release workflow, and public package smoke passed without changing vendored W3.CSS.
- The 0.5.2 navigation patch release is complete: reusable nav menu root-link fix, release notes, release workflow, and public package smoke passed without changing vendored W3.CSS.
- The 0.5.3 toast spacing patch release is complete: reusable toast close-spacing fix, release notes, release workflow, and public package smoke passed without changing vendored W3.CSS.
- The 0.6.0 quality automation release is complete: starter-kit browser sweep tooling, package smoke CSS assertions, release guardrails, docs, tag workflow, release workflow, and public package smoke passed without changing vendored W3.CSS.
- The 0.7.0 live gallery release is complete: `/gallery` adds live copyable app/site examples, docs adoption links point to Gallery, release workflow passed, and public package smoke passed after clearing a stale local NuGet HTTP cache.
- The 0.8.0 polish release preparation is locally verified: package metadata/release notes target `0.8.0`, starter release-label drift is guarded by a sample-local version constant and browser-sweep assertion, roadmap/component index copy now points to the current Starter Kit -> Gallery -> Patterns -> Theming -> Versions adoption path, and local build/test/pack/smoke/browser sweeps passed.
- The starter-kit dashboard now includes meaningful customer-derived chart examples and a single multi-state theme toggle for Light/Dark/Auto.
- Built-in icons are now first-class through `W3IconName` + `W3Icon`; icon-capable components can render bundled SVG icons without external icon assets while preserving `IconClass` compatibility.
- The starter-kit theme toggle uses built-in Sun, Moon, and Monitor icons through multi-state `W3ToggleIconButton`, and dashboard chart cards have explicit top/bottom body padding.
- Multi-state `W3ToggleIconButton` no longer receives the binary pressed-ring class by default; the starter theme toggle uses a subtle themed border instead of a white inset ring.
- Starter app-bar actions now keep the theme toggle and release button at the same rendered height using out-of-box `W3ToggleIconButton` classes and built-in `W3IconName` states, with no starter-specific icon CSS.
- Starter kit readiness now requires zero starter source CSS: the sample links only `_content/W3Css.Blazor/w3css-blazor.css`, uses package/W3 utility primitives for layout, and has a release-quality guard against source CSS or hidden generated stylesheet links returning.
- The bundled `_content/W3Css.Blazor/w3css-blazor.css` now includes W3.CSS, theme tokens, and generated package component scoped CSS so app bars, nav menus, icons, and other component layouts work from the single documented stylesheet.
- `W3AppShell` now marks sidebar layouts with `w3-app-shell-has-sidebar` and keeps the shell header sticky in that state, so the fixed W3.CSS sidebar stays visually attached to its header while the page scrolls.
- `W3NavMenuItem` now treats `Href=""` as an application-root link by rendering the app base URI, so Blazor-style dashboard/root links navigate correctly from nested routes.
- `W3Toast` now reserves layout space for the close button with grid-based package CSS and explicit panel padding, so notification title/body spacing stays consistent without app-specific CSS.
- `W3Chart` now derives grid and axis strokes from the current chart text color with stronger opacity, so chart structure remains readable in both light and dark theme modes without app-specific CSS.
- Reusable dark-theme surfaces, tables, form controls, data-table chrome, row hover contrast, app-bar action wrapping, and chart sizing/color behavior now live in the package stylesheet/component CSS instead of the starter app.
- Reusable dark-theme hover contrast now covers W3.CSS hoverable table/list rows and striped even rows inside `W3ThemeProvider`, so table hover readability is fixed at the library level.
- Reusable dark-theme modal footer contrast now covers default `W3Modal`/`W3MessageBox` action footers inside `W3ThemeProvider`, so light-grey W3.CSS footer defaults do not create light strips in dark dialogs.
- `W3Chart` axis labels now emit essential SVG text styling directly so labels inherit the themed text color even when rendered through the manual SVG text builder path.
- `W3DataTable` footer spacing now keeps horizontal padding, and `W3ToggleIconButton` supports optional multi-state cycling while preserving its existing binary toggle behavior.
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
- The reusable action-row polish added `W3ActionRow` for forms, generic tables, list suffixes, and dialog/card-style command areas that need consistent wrapping gaps.
- The empty-state pattern pass added `W3EmptyState`, a dedicated docs page, and `W3DataTable` zero-result/error integration for reusable app recovery states.
- Remaining planned work is optional: visual quality sweeps and future version slices as needed.
- The 0.9.0 "Robustness and Reach" slice is planned (development-plan Phase 14) and in progress on branch `work/0.9.0-robustness-reach`: multi-target the packable library to `net8.0;net9.0;net10.0`, add SourceLink/symbols/icon/trimming and a CI bundle-drift gate, wire focus-trap + body-scroll-lock into overlays, complete tree/tooltip keyboard accessibility, harden data-table/pagination at scale, and make toast/theme/JS-teardown robust.
