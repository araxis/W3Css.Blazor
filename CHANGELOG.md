# Changelog

All notable changes to W3Css.Blazor will be documented in this file.

## 0.5.3 - 2026-06-02

### Toast Spacing Patch

- Fixed `W3Toast` close-button spacing so the close action participates in the toast grid layout instead of floating over panel padding.
- Regenerated the bundled package stylesheet so the single documented stylesheet includes the toast layout polish.
- Added release-quality coverage for the toast grid and padding rules in the package stylesheet bundle.

### Release Quality

- Updated package metadata, README adoption links, versions documentation, release notes, starter label, and memory files for 0.5.3.
- Kept the vendored W3.CSS source unchanged.

## 0.5.2 - 2026-06-02

### Navigation Patch

- Fixed `W3NavMenuItem` root links so `Href=""` renders an app-root anchor and remains selectable from nested routes.
- Added regression coverage for root-link active state and refreshed the nav menu docs note.

### Release Quality

- Updated package metadata, README adoption links, versions documentation, release notes, starter label, and memory files for 0.5.2.
- Kept the vendored W3.CSS source unchanged.

## 0.5.1 - 2026-06-02

### Starter Kit Polish

- Removed the starter sample's remaining source stylesheet dependency so it runs from the package stylesheet and W3 utility classes.
- Strengthened package-level app shell, app bar, icon, modal, data table, chart, and dark-theme styling used by the starter sample.
- Hardened package consumer smoke coverage so local package checks use an isolated package cache and verify bundled component styles.

### Release Quality

- Updated package metadata, README adoption links, versions documentation, release notes, and memory files for 0.5.1.
- Kept the vendored W3.CSS source unchanged.

## 0.5.0 - 2026-06-02

### Starter Kit Adoption

- Added a runnable `samples/W3Css.Blazor.StarterKit` Blazor WebAssembly app with dashboard, settings, customers, and workflow routes.
- Added starter-kit docs covering setup, expected files, sample routes, and related adoption guidance.
- Reframed the component index around a starter path instead of an empty readiness backlog.

### Release Quality

- Expanded package consumer smoke coverage to compile starter-kit primitives including forms, fields, modals, message boxes, and toast provider wiring.
- Updated package metadata, README adoption links, versions documentation, release notes, and memory files for 0.5.0.
- Kept the vendored W3.CSS source unchanged.

## 0.4.0 - 2026-06-01

### Quality Hardening

- Expanded IntelliSense-facing XML documentation coverage across exported public APIs and component parameters.
- Added broad XML documentation guard coverage for exported public API members.
- Added component docs/API parity guard coverage so documented component topic pages stay aligned with public parameters.
- Added a Playwright-backed docs browser sweep for top adoption and high-risk interactive pages, covering console errors and horizontal overflow.

### Documentation And Release Quality

- Updated package metadata, README adoption links, versions documentation, release notes, and memory files for 0.4.0.
- Kept the vendored W3.CSS source unchanged.

## 0.3.0 - 2026-06-01

### Adoption Polish

- Added end-to-end adoption patterns for app shell dashboards, validated settings forms, searchable data tables, and modal/message-box workflows.
- Improved README onboarding with a shorter install path, a first app screen sample, and direct links to key adoption docs.
- Added XML documentation summaries for selected high-use public APIs.
- Added a documentation guard test that fails when selected public API summaries are missing.

### Release Quality

- Updated package metadata, versions documentation, changelog, and release notes for 0.3.0.
- Kept the vendored W3.CSS source unchanged.

## 0.2.0 - 2026-06-01

### App Components

- Added and polished app-grade primitives for shell, navigation, data, feedback,
  action layout, empty states, drawers, menus, popovers, swipe areas, and page
  content navigation.
- Added consistent wrapping action rows for forms, tables, cards, dialogs, and
  compact command surfaces.
- Integrated reusable empty/error surfaces into data-table flows.

### Accessibility And UX

- Added required-state, validation, ARIA, and input-time behavior polish across
  form controls.
- Added keyboard support and focus handling across tabs, rating, stepper,
  pagination, bottom navigation, menus, dropdowns, popovers, sidebars, modals,
  message boxes, drawers, alerts, and focus traps.
- Added theme-controlled focus rings and semantic status tokens.

### Documentation And Release Quality

- Completed current component topic API tables and docs smoke coverage.
- Updated package metadata and release notes for the current release.
- Expanded the public package consumer smoke check to compile current app
  primitives in a clean consumer project.
- Hardened release automation so a release fails when version-specific release
  notes are missing.

## 0.1.0 - 2026-05-31

### Foundation

- Added bundled W3.CSS 5.01 static asset.
- Added CI package artifact creation for release validation.
- Added foundation, layout, form, navigation, overlay, content, and content-extra components.
- Added image, code, note, and quote component topics.
- Added Blazor WebAssembly documentation site.
- Added bUnit component tests and GitHub Actions workflows.

### Theming

- Added a design-token layer (`w3-theme.css`) — CSS variables plus token utility
  classes — since W3.CSS ships no variables of its own.
- Added a single-file bundle `w3css-blazor.css` (framework + token layer), so apps
  install with one `<link>`; the two source files remain individually linkable.
- Added `W3Theme` (record of `Primary`, `Secondary`, `Accent`, `Surface`,
  `Background`, `Border`, `Radius`, `FontFamily`, and a `Dark` variant) and
  `W3ThemeProvider` to brand a whole app or region from one place, including dark mode.
- Added themed `W3Color` tokens (`Primary`, `Secondary`, `Accent`, `Surface`) that
  resolve the theme variables.
- Themed-by-default: component default colors map to theme tokens so zero-config
  usage looks branded.
- `.w3-theme-root` is a themed page surface (`Background` / `OnBackground`), so
  wrapping an app themes the page — including dark mode — with no per-mode app CSS.

### Components

- Consolidated to one canonical, W3.CSS-loyal component per concept; folded
  common app-component features into existing components instead of shipping alias
  shims (removed `W3TextField`, `W3NumericField`, `W3DatePicker`, `W3TimePicker`,
  `W3ProgressLinear`, `W3Snackbar`, `W3Carousel`, `W3ExpansionPanels`, `W3SimpleTable`,
  `W3DataGrid`, `W3ColorPicker`, `W3DynamicTabs`, `W3Dialog`, and related types).
- Added components for capabilities absent from W3.CSS: `W3Spacer`, `W3Footer`,
  `W3AvatarGroup`, `W3MessageBox`, `W3ToggleGroup` / `W3ToggleItem`, `W3Mask`,
  `W3ScrollToTop`, `W3PageContentNavigation` / `W3PageSection`, and `W3Chart`.
