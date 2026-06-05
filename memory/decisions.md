# Decisions

Last updated: 2026-06-05

## Identity And Targets

- Package/namespace: `W3Css.Blazor`.
- Target framework: `net10.0`.
- SDK pin: `10.0.300`.
- Repository branch convention:
  - `work/<short-topic>`
  - `fix/<short-topic>`
  - `docs/<short-topic>`
  - `chore/<short-topic>`

## Component Architecture

- Components live under `src/W3Css.Blazor/Components` and reuse shared helpers from `src/W3Css.Blazor/Internal` and shared models/enums in `src/W3Css.Blazor`.
- Blazor-owned state is preferred; behavior is CSS-first where possible.
- Class composition is centralized through shared builders/maps; `Class`/`Style`/`AdditionalAttributes` are pass-through defaults.
- JS is only used for the few components that need it (menus, drop zone, focus trap, scroll-to-top, in-page navigation, etc.) and each follows safe disposal/disconnect handling.

## W3.CSS–Loyal Strategy

- The project is W3.CSS-first and avoids changing vendored `w3.css`.
- Do **not** ship aliases named after a reference library by default. If a reference library exposes a feature not in W3.CSS coverage, either:
  1) fold that feature into an existing canonical component, or
  2) add a new component only when the capability is genuinely missing.
- Removed/removed-by-design aliases include: `W3TextField`, `W3NumericField`, `W3DatePicker`, `W3TimePicker`, `W3ProgressLinear`, `W3Snackbar`, `W3Carousel`, `W3CarouselItem`, `W3ExpansionPanel`, `W3ExpansionPanels`, `W3SimpleTable`, `W3DataGrid`, `W3ColorPicker`, `W3DynamicTabs`, and `W3Dialog` wrappers.
- A duplicate hidden component is intentionally not added; responsive show/hide is covered by `W3Display`/`W3Responsive`.
- `W3SwipeArea` is implemented as the pointer gesture component; it uses Blazor pointer events rather than a script module.

## Theming And Customization

- The theme token layer is implemented:
  - `W3Theme` (records tokens)
  - `W3ThemeProvider` (scope provider)
  - Bundled `wwwroot/w3-theme.css` for baseline token variables and utility classes.
- Core token colors now include `Primary`, `Secondary`, `Accent`, `Surface`, `Border`, status tokens (`Info`, `Success`, `Warning`, `Danger`, `Note`), plus optional dark-mode variants.
- Theming is app-visible by defaults (`W3Color.Primary/Secondary/Accent/Surface/Info/Success/Warning/Danger/Note` tokens are used where relevant).
- `w3.css` itself is not modified.

## Docs And Testing

- Docs are runnable Blazor topic pages with categorized catalog, previous/next navigation, parameter tables, and examples.
- Testing stack remains:
  - bUnit + xUnit for component/API behavior.
  - Playwright-style browser checks when visual behavior in docs needs manual verification.

## Repository Standards

- Default branch target: `main`.
- Package version currently targets `0.8.0` for the polish release preparation slice.
- `PackageProjectUrl`/`RepositoryUrl`/`RepositoryType` and release metadata are set from repo properties.

## Architecture Defaults

- Accessibility is first-party (semantic markup, ARIA where needed, visible focus behavior).
- Minimal surprises in APIs: typed params + sane defaults + `Class`/`Style` pass-through.
