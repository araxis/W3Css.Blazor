# W3Css.Blazor Project Memory

Last updated: 2026-06-01

## Snapshot

- Repo target: Blazor component suite over W3.CSS (small, CSS-first, practical defaults).
- Branch: `main`.
- Current package baseline: `0.3.0` release target.
- .NET target: `net10.0` (`10.0.300` SDK).
- Current state:
  - 127 component `.razor` files.
  - 121 docs topic pages plus the top-level Patterns guide.
  - 493 passing tests after the 0.3.0 adoption-polish slice.
  - Build: `dotnet build W3Css.Blazor.slnx --configuration Release` (0 warnings, 0 errors).

## Architecture

- W3.CSS-first implementation; no vendored stylesheet edits.
- Theme layer:
  - `W3Theme` + `W3ThemeProvider`
  - bundled `w3-theme.css` for token defaults and utility classes.
- JS is scoped only to behavior-only components and cleanly disposed.
- Component compatibility strategy:
  - fold features into canonical components when possible,
  - add new component only when required capability is missing,
  - avoid alias components that duplicate concepts.

## Canonical Decisions

- Avoid `w3.css` changes in package.
- Keep navigation, forms, overlays, and data patterns Blazor-native and accessible.
- Maintain categorized docs index with per-topic examples, API tables, previous/next links.
- Package metadata and repository pointers are set in project properties.

## Current Priority

1. 0.3.0 adoption polish:
   - complete verification for Patterns docs, README adoption path, selected XML summaries, package metadata, and release notes.
   - run build, tests, pack, local package smoke, browser checks, and hygiene scans before PR.
2. Release completion:
   - merge after CI, tag `v0.3.0`, verify the release workflow, and run public package smoke.

## Memory Files

- `memory/README.md`: handoff sequence and folder policy.
- `memory/current-state.md`: current branch, verification, and work snapshot.
- `memory/decisions.md`: architecture and strategy baseline.
- `memory/development-plan.md`: phase roadmap.
- `memory/feature-list.md`: canonical implementation matrix.
- `memory/findings.md`: W3.CSS source findings and mapping notes.
- `memory/w3css-gap-scan.md`: topic-to-library coverage status.
- `memory/progress-log.md`: dated execution log.
