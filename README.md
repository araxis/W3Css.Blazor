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

`w3css-blazor.css` bundles the base W3.CSS framework, the design-token layer
(CSS variables + token classes), and the package component styles, so layout,
icons, themed colors, and dark mode resolve out of the box. Brand the whole app
from one place by wrapping it in `W3ThemeProvider`.

<details>
<summary>Prefer the two files separately?</summary>

The base framework and token layer are also available separately. Use this path
only when your host app already includes the package component styles through its
own generated scoped stylesheet:

```html
<link rel="stylesheet" href="_content/W3Css.Blazor/w3css/5/w3.css" />
<link rel="stylesheet" href="_content/W3Css.Blazor/w3-theme.css" />
```

`w3.css` is the pristine, unmodified framework; `w3-theme.css` is the token layer.
</details>

## First App Screen

```razor
<W3ThemeProvider Theme="W3Theme.Default">
    <W3AppShell HeaderColor="W3Color.Teal"
                HeaderTextColor="W3Color.White"
                SidebarColor="W3Color.White">
        <Header>
            <W3AppBar Title="Operations"
                      ShowMenuButton="true"
                      ActionsClass="w3-hide-small">
                <Actions>
                    <W3Button Border="true" TextColor="W3Color.White">Export</W3Button>
                </Actions>
            </W3AppBar>
        </Header>
        <Sidebar>
            <nav class="w3-bar-block" aria-label="Application sections">
                <a class="w3-bar-item w3-button w3-teal" href="">Overview</a>
                <a class="w3-bar-item w3-button" href="settings">Settings</a>
            </nav>
        </Sidebar>
        <ChildContent>
            <W3DataTable TItem="TaskRow"
                         Items="tasks"
                         SearchPlaceholder="Search tasks"
                         EmptyText="No tasks found">
                <ChildContent>
                    <W3DataColumn TItem="TaskRow" Title="Task" CellText="@((TaskRow task) => task.Name)" />
                    <W3DataColumn TItem="TaskRow" Title="Owner" CellText="@((TaskRow task) => task.Owner)" />
                    <W3DataColumn TItem="TaskRow" Title="State" CellText="@((TaskRow task) => task.State)" />
                </ChildContent>
                <RowActions Context="task">
                    <W3Button Size="W3Size.Small" Border="true">Open</W3Button>
                </RowActions>
            </W3DataTable>
        </ChildContent>
    </W3AppShell>
</W3ThemeProvider>

@code {
    private readonly TaskRow[] tasks =
    [
        new("Review settings form", "Nora", "Ready"),
        new("Audit data states", "Iris", "Queued")
    ];

    private sealed record TaskRow(string Name, string Owner, string State);
}
```

For app screens, start with the layout and data primitives that already handle the
repetitive spacing work: `W3AppShell`, `W3AppBar`, `W3Card`, `W3DataTable`,
`W3Form`, `W3EmptyState`, and `W3ActionRow`.

## Run The Starter Kit

The repository includes a small Blazor WebAssembly starter app that wires the package into a dashboard, settings form, customer table, and modal workflow:

```bash
dotnet run --project samples/W3Css.Blazor.StarterKit
```

The sample uses a local project reference during development and the same bundled stylesheet path that package consumers use.

## Adoption Links

- [Starter Kit](src/W3Css.Blazor.Docs/Pages/StarterKitPage.razor): runnable sample app with shell, forms, data, modal workflow, and toast feedback.
- [Patterns](src/W3Css.Blazor.Docs/Pages/Patterns.razor): complete dashboard, form, table, and modal workflows.
- [Theming](src/W3Css.Blazor.Docs/Pages/ComponentTopics/ThemingPage.razor): brand tokens, dark surfaces, and focus/status colors.
- [Versions](src/W3Css.Blazor.Docs/Pages/ComponentTopics/VersionsPage.razor): package version, bundled stylesheet paths, and upgrade checks.
- [0.5.2 release notes](docs/release-notes/0.5.2.md): navigation patch release details.
- [Package smoke script](tools/package-consumer-smoke.ps1): clean consumer-app package validation.

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

Pass `-PackageVersion 0.5.2` to check a specific published version.
Pass `-PackageSource artifacts/packages` to check a locally packed version before tagging.

Run the browser quality sweep against a running docs site:

```bash
pwsh ./tools/docs-browser-sweep.ps1
```

Pass `-StartServer` to let the sweep start the docs app locally before checking top adoption and interactive routes.

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
