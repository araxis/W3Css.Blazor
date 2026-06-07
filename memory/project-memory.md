# W3Css.Blazor Project Memory

Last updated: 2026-06-05

## Snapshot

- Repo target: Blazor component suite over W3.CSS (small, CSS-first, practical defaults).
- Branch: `work/0.8.0-polish`.
- Current package baseline: preparing `0.8.0` polish over published `0.7.0`.
- .NET target: `net10.0` (`10.0.300` SDK).
- Current state:
  - 127 component `.razor` files.
  - 121 docs topic pages plus top-level Starter Kit, Gallery, and Patterns guides.
  - Starter sample app under `samples/W3Css.Blazor.StarterKit` with no source CSS; it links only the bundled package stylesheet.
  - 490 passing tests for the current polish slice.
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

1. 0.8.0 polish release preparation:
   - package metadata, release notes, starter version-label guard, adoption-path docs copy, browser sweep assertions, memory sync, local package smoke, docs sweep, and starter sweep are prepared locally.
2. Publish handoff:
   - after merge to `main`, tag `v0.8.0`, verify release workflow, and run public package smoke for `0.8.0`.

## Memory Files

- `memory/README.md`: handoff sequence and folder policy.
- `memory/current-state.md`: current branch, verification, and work snapshot.
- `memory/decisions.md`: architecture and strategy baseline.
- `memory/development-plan.md`: phase roadmap.
- `memory/feature-list.md`: canonical implementation matrix.
- `memory/findings.md`: W3.CSS source findings and mapping notes.
- `memory/w3css-gap-scan.md`: topic-to-library coverage status.
- `memory/progress-log.md`: dated execution log.
