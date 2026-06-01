# W3Css.Blazor

[![Package Version](https://img.shields.io/nuget/v/W3Css.Blazor?label=package)](https://www.nuget.org/packages/W3Css.Blazor)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Lightweight Blazor components built on W3.CSS.

## Goals

- Small install path.
- CSS-first styling through bundled W3.CSS 5.01.
- Razor components with predictable class composition.
- No runtime script dependency for ordinary component behavior.
- Usable documentation site and focused component tests.

## Install

```bash
dotnet add package W3Css.Blazor
```

Add imports:

```razor
@using W3Css.Blazor
@using W3Css.Blazor.Components
```

Add the stylesheet to the host page — one file is all you need:

```html
<link rel="stylesheet" href="_content/W3Css.Blazor/w3css-blazor.css" />
```

`w3css-blazor.css` bundles the base W3.CSS framework **and** the design-token layer
(CSS variables + token classes), so themed colors and dark mode resolve out of the
box. Brand the whole app from one place by wrapping it in `W3ThemeProvider` — see
the Theming docs.

<details>
<summary>Prefer the two files separately?</summary>

The bundle is just these two concatenated; you can link them individually instead:

```html
<link rel="stylesheet" href="_content/W3Css.Blazor/w3css/5/w3.css" />
<link rel="stylesheet" href="_content/W3Css.Blazor/w3-theme.css" />
```

`w3.css` is the pristine, unmodified framework; `w3-theme.css` is the token layer.
</details>

## First Components

```razor
<W3ThemeProvider Theme="W3Theme.Default">
    <W3Button Color="W3Color.Primary" Round="W3Round.Medium">Save</W3Button>

    <W3Alert Kind="W3AlertKind.Success" Title="Saved">
        Your changes were saved.
    </W3Alert>

    <W3Card Depth="W3CardDepth.Four" Round="W3Round.Medium">
        <p>Card content</p>

        <Actions>
            <W3Button Border="true">Cancel</W3Button>
            <W3Button Color="W3Color.Primary" TextColor="W3Color.White">Save</W3Button>
        </Actions>
    </W3Card>

    <W3ActionRow Label="Table actions" JustifyStart="true">
        <W3Button Border="true">Edit</W3Button>
        <W3Button Border="true">Archive</W3Button>
    </W3ActionRow>

    <W3Image Src="logo.svg" Alt="Application logo" Width="96" Height="96" Responsive="true" />
</W3ThemeProvider>
```

For app screens, start with the layout and data primitives that already handle the
repetitive spacing work: `W3AppShell`, `W3AppBar`, `W3Card`, `W3DataTable`,
`W3Form`, and `W3ActionRow`.

## Repository Layout

- `src/W3Css.Blazor`: Razor Class Library.
- `src/W3Css.Blazor.Docs`: Blazor WebAssembly documentation site.
- `tests/W3Css.Blazor.Tests`: xUnit and bUnit tests.
- `memory`: project decisions, progress, findings, and current state.

## Development

```bash
dotnet restore W3Css.Blazor.slnx
dotnet build W3Css.Blazor.slnx
dotnet test W3Css.Blazor.slnx
dotnet pack src/W3Css.Blazor/W3Css.Blazor.csproj --configuration Release
dotnet run --project src/W3Css.Blazor.Docs
```

Run the package consumer smoke check after publishing a version:

```bash
pwsh ./tools/package-consumer-smoke.ps1
```

Pass `-PackageVersion 0.1.0` to check a specific published version.

## Branches And Commits

Branch names:

- `work/<short-topic>`
- `fix/<short-topic>`
- `docs/<short-topic>`
- `chore/<short-topic>`

Commit examples:

- `feat: add button component`
- `fix: correct alert role`
- `docs: add installation guide`
- `test: cover card variants`
- `build: add pages workflow`
