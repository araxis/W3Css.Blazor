[CmdletBinding()]
param(
    [string]$PackageId = 'W3Css.Blazor',
    [string]$PackageVersion,
    [string]$PackageSource = 'https://api.nuget.org/v3/index.json',
    [string]$Configuration = 'Release',
    [string]$WorkRoot = [System.IO.Path]::Combine([System.IO.Path]::GetTempPath(), 'w3css-blazor-package-smoke'),
    [switch]$KeepWorkspace
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Write-Step {
    param([string]$Message)

    Write-Host "==> $Message"
}

function Invoke-Checked {
    param(
        [string]$FilePath,
        [string[]]$Arguments
    )

    Write-Step "$FilePath $($Arguments -join ' ')"
    & $FilePath @Arguments
    if ($LASTEXITCODE -ne 0) {
        throw "Command failed with exit code $LASTEXITCODE`: $FilePath $($Arguments -join ' ')"
    }
}

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot '..')

if ([string]::IsNullOrWhiteSpace($PackageVersion)) {
    $projectPath = Join-Path $repoRoot 'src/W3Css.Blazor/W3Css.Blazor.csproj'
    [xml]$project = Get-Content -Path $projectPath
    $PackageVersion = [string]($project.Project.PropertyGroup.Version | Select-Object -First 1)
}

if ([string]::IsNullOrWhiteSpace($PackageVersion)) {
    throw 'PackageVersion was not provided and could not be read from the project file.'
}

$workspaceId = [guid]::NewGuid().ToString('N').Substring(0, 8)
$workspace = Join-Path $WorkRoot "consumer-$PackageVersion-$(Get-Date -Format 'yyyyMMddHHmmss')-$workspaceId"
$appDir = Join-Path $workspace 'PackageConsumerSmoke'
$projectFile = Join-Path $appDir 'PackageConsumerSmoke.csproj'
$publishDir = Join-Path $workspace 'publish'
$packagesDir = Join-Path $workspace 'packages'
$nugetConfigPath = Join-Path $workspace 'NuGet.Config'
$resolvedPackageSource = $PackageSource
if ($PackageSource -notmatch '^[a-zA-Z][a-zA-Z0-9+.-]*://' -and -not [System.IO.Path]::IsPathRooted($PackageSource)) {
    $candidateSource = Join-Path $repoRoot $PackageSource
    if (Test-Path $candidateSource) {
        $resolvedPackageSource = (Resolve-Path $candidateSource).Path
    }
}

New-Item -Path $workspace -ItemType Directory -Force | Out-Null
$escapedPackageSource = [System.Security.SecurityElement]::Escape($resolvedPackageSource)
Set-Content -Path $nugetConfigPath -NoNewline -Value @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="package-under-test" value="$escapedPackageSource" />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
</configuration>
"@

try {
    Write-Step "Creating disposable consumer app in $workspace"
    Invoke-Checked 'dotnet' @(
        'new',
        'blazorwasm',
        '--name',
        'PackageConsumerSmoke',
        '--framework',
        'net10.0',
        '--no-restore',
        '--output',
        $appDir
    )

    Write-Step "Installing $PackageId $PackageVersion from $resolvedPackageSource"
    Invoke-Checked 'dotnet' @(
        'add',
        $projectFile,
        'package',
        $PackageId,
        '--version',
        $PackageVersion,
        '--source',
        $resolvedPackageSource,
        '--no-restore'
    )

    $importsPath = Join-Path $appDir '_Imports.razor'
    Add-Content -Path $importsPath -Value @(
        '@using W3Css.Blazor',
        '@using W3Css.Blazor.Components'
    )

    $indexPath = Join-Path $appDir 'wwwroot/index.html'
    $indexHtml = Get-Content -Path $indexPath -Raw
    if ($indexHtml -notmatch '_content/W3Css.Blazor/w3css-blazor.css') {
        $stylesheet = '    <link rel="stylesheet" href="_content/W3Css.Blazor/w3css-blazor.css" />'
        $indexHtml = $indexHtml -replace '(?i)</head>', "$stylesheet`n</head>"
        Set-Content -Path $indexPath -Value $indexHtml -NoNewline
    }

    $programPath = Join-Path $appDir 'Program.cs'
    $program = Get-Content -Path $programPath -Raw
    if ($program -notmatch 'using W3Css\.Blazor;') {
        $program = $program -replace 'using Microsoft.AspNetCore.Components.WebAssembly.Hosting;', "using Microsoft.AspNetCore.Components.WebAssembly.Hosting;`nusing W3Css.Blazor;"
    }
    if ($program -notmatch 'AddW3CssBlazor') {
        $program = $program -replace '(builder\.Services\.AddScoped\(sp => new HttpClient \{ BaseAddress = new Uri\(builder\.HostEnvironment\.BaseAddress\) \}\);)', "`$1`nbuilder.Services.AddW3CssBlazor();"
    }
    Set-Content -Path $programPath -Value $program -NoNewline

    $pagesDir = Join-Path $appDir 'Pages'
    New-Item -Path $pagesDir -ItemType Directory -Force | Out-Null
    Set-Content -Path (Join-Path $pagesDir 'W3Smoke.razor') -Value @'
@page "/w3-smoke"
@using System.ComponentModel.DataAnnotations
@using Microsoft.AspNetCore.Components.Forms
@inject W3ToastService Toasts

<PageTitle>W3Css.Blazor package smoke</PageTitle>

<W3ThemeProvider Theme="W3Theme.Default">
    <W3Container Class="w3-padding">
        <W3Alert Kind="W3AlertKind.Success" Title="Package smoke">
            The package is installed and rendering components.
        </W3Alert>

        <W3Card Depth="W3CardDepth.Four" Round="W3Round.Medium" Class="w3-margin-top">
            <ChildContent>
                <p>Card content from the package consumer app.</p>
            </ChildContent>
            <Actions>
                <W3Button Border="true">Cancel</W3Button>
                <W3Button Color="W3Color.Teal" Round="W3Round.Medium">Save</W3Button>
            </Actions>
        </W3Card>

        <W3DataTable TItem="ProjectRow"
                     Items="Projects"
                     Searchable="true"
                     SearchPlaceholder="Find projects"
                     AriaLabel="Package smoke projects"
                     Class="w3-margin-top">
            <ChildContent>
                <W3DataColumn TItem="ProjectRow" Title="Name" CellText="project => project.Name" />
                <W3DataColumn TItem="ProjectRow" Title="Status" CellText="project => project.Status" />
            </ChildContent>
            <RowActions Context="project">
                <W3ActionRow Label="@($"{project.Name} actions")" Gap="6">
                    <W3Button Border="true">Open</W3Button>
                </W3ActionRow>
            </RowActions>
        </W3DataTable>

        <W3EmptyState Title="Smoke check ready"
                      Description="Current app primitives compile in a clean consumer project."
                      Kind="W3EmptyStateKind.Success"
                      Class="w3-margin-top" />

        <W3Form Model="@settings" OnValidSubmit="SaveSettings" Class="w3-card w3-padding w3-white w3-margin-top">
            <DataAnnotationsValidator />
            <W3Field TValue="string" Label="Workspace name" ForId="smoke-workspace" For="@(() => settings.WorkspaceName)">
                <W3Input id="smoke-workspace" @bind-Value="settings.WorkspaceName" />
            </W3Field>
            <W3ActionRow Label="Smoke form actions" JustifyStart="true">
                <W3Button Type="submit" Color="W3Color.Primary" TextColor="W3Color.White">Save</W3Button>
                <W3Button Border="true" OnClick="() => modalOpen = true">Open modal</W3Button>
                <W3Button Border="true" OnClick="() => confirmOpen = true">Confirm</W3Button>
            </W3ActionRow>
        </W3Form>

        <W3Modal Title="Smoke modal" @bind-Open="modalOpen">
            <ChildContent>
                <p>Modal content from the package consumer app.</p>
            </ChildContent>
            <Actions>
                <W3Button Border="true" OnClick="() => modalOpen = false">Close</W3Button>
            </Actions>
        </W3Modal>

        <W3MessageBox @bind-Visible="confirmOpen"
                      Title="Confirm smoke action?"
                      Message="Message-box workflow compiles in the clean consumer app."
                      YesText="Confirm"
                      NoText="Skip"
                      CancelText="Cancel"
                      OnResult="HandleConfirmResult" />
    </W3Container>

    <W3ToastProvider />
</W3ThemeProvider>

@code {
    private readonly SmokeSettings settings = new() { WorkspaceName = "Smoke workspace" };
    private bool modalOpen;
    private bool confirmOpen;

    private static readonly ProjectRow[] Projects =
    [
        new("Dashboard", "Ready"),
        new("Backlog", "Review")
    ];

    private async Task SaveSettings(EditContext _)
    {
        Toasts.ShowSuccess("Settings form compiled and submitted.", "Package smoke");
        await Task.CompletedTask;
    }

    private void HandleConfirmResult(bool? result)
    {
        Toasts.ShowInfo(result?.ToString() ?? "Canceled", "Message box");
    }

    private sealed record ProjectRow(string Name, string Status);

    private sealed class SmokeSettings
    {
        [Required]
        public string WorkspaceName { get; set; } = string.Empty;
    }
}
'@

    Invoke-Checked 'dotnet' @('restore', $projectFile, '--packages', $packagesDir, '--configfile', $nugetConfigPath)
    Invoke-Checked 'dotnet' @('build', $projectFile, '--configuration', $Configuration, '--no-restore')
    Invoke-Checked 'dotnet' @('publish', $projectFile, '--configuration', $Configuration, '--no-restore', '--output', $publishDir)

    $bundlePath = Join-Path $publishDir 'wwwroot/_content/W3Css.Blazor/w3css-blazor.css'
    if (-not (Test-Path $bundlePath)) {
        throw "Expected static web asset was not published: $bundlePath"
    }

    $bundle = Get-Content -Path $bundlePath -Raw
    if (-not $bundle.Contains('w3-button') -or -not $bundle.Contains('--w3-')) {
        throw "Published stylesheet did not include the expected W3.CSS and theme token content: $bundlePath"
    }

    if (-not $bundle.Contains('component scoped CSS') -or -not $bundle.Contains('W3AppBar.razor.rz.scp.css') -or -not $bundle.Contains('W3NavMenuItem.razor.rz.scp.css') -or -not $bundle.Contains('w3-icon-svg')) {
        throw "Published stylesheet did not include expected package component styles: $bundlePath"
    }

    Write-Step "Package consumer smoke passed for $PackageId $PackageVersion"
}
finally {
    if ($KeepWorkspace) {
        Write-Step "Kept workspace at $workspace"
    }
    elseif (Test-Path $workspace) {
        Remove-Item -LiteralPath $workspace -Recurse -Force
    }
}
