# Step-by-Step Development Plan

Last updated: 2026-06-02

## Phase 1: Foundation (Complete)

- Repo bootstrap, SDK pinning, project structure, global build config, CI basics, and docs site scaffolding.

## Phase 2: Core Infrastructure (Complete)

- W3.CSS asset packaging, base styles, shared primitives, shared enums, and primary tests.

## Phase 3: Component Expansion (Complete)

- Core components and utility coverage for layout, forms, display, navigation, data display, feedback, content, and overlays.

## Phase 4: Docs Maturity (Complete)

- Categorized component catalog, per-topic documentation pages, API tables, navigation and pager chains, and source links.
- Doc smoke checks and route coverage for changed routes.

## Phase 5: Package And Release Readiness (Complete)

- Package metadata, release automation, first public package release, and consumer smoke tooling are in place.
- Future releases should be planned as normal version-bump, release-note, tag, and consumer-smoke slices.

## Phase 6: W3.CSS-Loyal Consolidation (Complete)

- Component compatibility work is implemented as:
  - New capabilities where absent (`W3Spacer`, `W3Footer`, `W3AvatarGroup`, `W3MessageBox`, `W3Mask`, `W3ToggleGroup`, `W3ToggleItem`, `W3DateRangePicker`, etc.).
  - Feature folds into canonical components (`W3Table.Dense`, `W3ColorInput.ShowPalette`, `W3Tabs` close/add, `W3Modal.Actions`).
  - Alias shims that duplicate canonical concepts were intentionally removed.

## Phase 7: Theming, Defaults, And Ease Of Use (Complete)

- Theme-token MVP, semantic status tokens, themed-by-default polish, and focus-ring/theming polish are shipped.

## Phase 8: Adoption Polish (Complete)

- 0.3.0 stays non-breaking and avoids broad new component surface.
- Added first-screen adoption patterns, README onboarding improvements, selected public XML documentation guard coverage, package/release metadata, local/public package smoke verification, and browser verification for top adoption pages.

## Phase 9: Quality Hardening (Complete)

- 0.4.0 stays non-breaking and avoids broad new component surface.
- Expand XML documentation coverage and guards across exported public APIs.
- Add docs/API parity coverage for public component parameters.
- Add automated browser quality sweep tooling for adoption and high-risk interactive docs routes.
- Published `v0.4.0` after main CI, release workflow, and public package smoke passed.

## Phase 10: Starter Kit Adoption (Complete)

- 0.5.0 stays non-breaking and avoids broad new component surface.
- Added a runnable Blazor WebAssembly starter kit sample under `samples/W3Css.Blazor.StarterKit`.
- Added `/starter-kit` docs and reframed the component index around a starter path instead of an empty backlog.
- Expanded clean consumer package smoke coverage to compile starter-kit primitives.
- Starter readiness now requires the sample to run without custom source CSS; reusable theme, table, form, chart, and app-bar fixes belong in the package.
- Browser-verified the starter sample routes, customer search, settings save, modal edit, message-box archive workflow, and mobile overflow.
- Published `v0.5.0` after PR CI, main CI, Pages, release workflow, and public package smoke passed.
- Published `v0.5.1` after PR CI, main CI, Pages, release workflow, and public package smoke passed.

## Current Status Check

- Verification now passes on the current HEAD:
  - `dotnet build W3Css.Blazor.slnx --configuration Release`
  - `dotnet test W3Css.Blazor.slnx --configuration Release --no-build /nr:false`
  - `dotnet pack src/W3Css.Blazor/W3Css.Blazor.csproj --configuration Release --no-build --output artifacts/packages /nr:false`
  - `pwsh ./tools/package-consumer-smoke.ps1 -PackageVersion 0.5.1 -PackageSource artifacts/packages`
  - `pwsh ./tools/docs-browser-sweep.ps1 -BaseUrl http://localhost:5022 -StartServer`

- Components implemented: 127 component files plus docs topic coverage.
- Test coverage: 485 passing tests for the current starter-kit readiness slice.
