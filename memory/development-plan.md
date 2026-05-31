# Step-by-Step Development Plan

Last updated: 2026-05-31

## Phase 1: Foundation (Complete)

- Repo bootstrap, SDK pinning, project structure, global build config, CI basics, and docs site scaffolding.

## Phase 2: Core Infrastructure (Complete)

- W3.CSS asset packaging, base styles, shared primitives, shared enums, and primary tests.

## Phase 3: Component Expansion (Complete)

- Core components and utility coverage for layout, forms, display, navigation, data display, feedback, content, and overlays.

## Phase 4: Docs Maturity (Complete)

- Categorized component catalog, per-topic documentation pages, API tables, navigation and pager chains, and source links.
- Doc smoke checks and route coverage for changed routes.

## Phase 5: Package And Release Readiness (In Progress)

- Package metadata and release automation are in place.
- Current release blocker items:
  - Push the current `main` commits.
  - Push tag `v0.1.0` only after confirming the NuGet publish secret/path is ready.
  - NuGet publish credential validation in actual release run.

## Phase 6: W3.CSS-Loyal Consolidation (Complete)

- Component compatibility work is implemented as:
  - New capabilities where absent (`W3Spacer`, `W3Footer`, `W3AvatarGroup`, `W3MessageBox`, `W3Mask`, `W3ToggleGroup`, `W3ToggleItem`, `W3DateRangePicker`, etc.).
  - Feature folds into canonical components (`W3Table.Dense`, `W3ColorInput.ShowPalette`, `W3Tabs` close/add, `W3Modal.Actions`).
  - Alias shims that duplicate canonical concepts were intentionally removed.

## Phase 7: Theming, Defaults, And Ease Of Use (In Progress)

- Theme-token MVP shipped and themed-by-default polish completed for most components.
- Remaining work is optional but valuable:
  - finish remaining semantic color tokens if desired,
  - optional focus-ring/theming polish,
  - `W3SwipeArea` decision and implementation only if gesture interactions are required.

## Current Status Check

- Verification now passes on the current HEAD:
  - `dotnet build W3Css.Blazor.slnx --configuration Release`
  - `dotnet test tests/W3Css.Blazor.Tests/W3Css.Blazor.Tests.csproj --configuration Release --no-build`

- Components implemented: 124 component files plus docs topic coverage.
- Test coverage: 387 passing tests.
