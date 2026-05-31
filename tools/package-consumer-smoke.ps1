[CmdletBinding()]
param(
    [string]$PackageId = 'W3Css.Blazor',
    [string]$PackageVersion,
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

New-Item -Path $workspace -ItemType Directory -Force | Out-Null

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

    Write-Step "Installing $PackageId $PackageVersion from the public package feed"
    Invoke-Checked 'dotnet' @(
        'add',
        $projectFile,
        'package',
        $PackageId,
        '--version',
        $PackageVersion,
        '--source',
        'https://api.nuget.org/v3/index.json'
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

    $pagesDir = Join-Path $appDir 'Pages'
    New-Item -Path $pagesDir -ItemType Directory -Force | Out-Null
    Set-Content -Path (Join-Path $pagesDir 'W3Smoke.razor') -Value @'
@page "/w3-smoke"

<PageTitle>W3Css.Blazor package smoke</PageTitle>

<W3Container Class="w3-padding">
    <W3Alert Kind="W3AlertKind.Success" Title="Package smoke">
        The package is installed and rendering components.
    </W3Alert>

    <W3Card Depth="W3CardDepth.Four" Round="W3Round.Medium" Class="w3-margin-top">
        <p>Card content from the package consumer app.</p>
        <W3Button Color="W3Color.Teal" Round="W3Round.Medium">Save</W3Button>
    </W3Card>
</W3Container>
'@

    Invoke-Checked 'dotnet' @('restore', $projectFile)
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
