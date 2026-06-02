param(
    [string]$BaseUrl = "http://localhost:5016",
    [switch]$StartServer,
    [int]$TimeoutSeconds = 90,
    [string[]]$Routes = @(
        "/",
        "/starter-kit",
        "/components",
        "/patterns",
        "/components/theming",
        "/components/app-shell",
        "/components/form",
        "/components/data-table",
        "/components/modal",
        "/components/empty-state",
        "/components/versions",
        "/components/tabs",
        "/components/menu",
        "/components/dropdown",
        "/components/popover",
        "/components/sidebar",
        "/components/message-box",
        "/components/drawer",
        "/components/autocomplete",
        "/components/drop-zone",
        "/components/bottom-navigation",
        "/components/rating",
        "/components/stepper",
        "/components/pagination"
    )
)

$ErrorActionPreference = "Stop"
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$serverProcess = $null
$tempDir = $null

function Test-DocsServer {
    param([string]$Url)

    try {
        $response = Invoke-WebRequest -Uri $Url -UseBasicParsing -TimeoutSec 5
        return $response.StatusCode -ge 200 -and $response.StatusCode -lt 500
    }
    catch {
        return $false
    }
}

try {
    if (-not (Test-DocsServer $BaseUrl)) {
        if (-not $StartServer) {
            throw "Docs server is not reachable at $BaseUrl. Start it first or pass -StartServer."
        }

        $serverProcess = Start-Process -FilePath "dotnet" `
            -ArgumentList @("run", "--project", "src/W3Css.Blazor.Docs", "--urls", $BaseUrl) `
            -WorkingDirectory $repoRoot `
            -WindowStyle Hidden `
            -PassThru

        $deadline = (Get-Date).AddSeconds($TimeoutSeconds)
        while ((Get-Date) -lt $deadline -and -not (Test-DocsServer $BaseUrl)) {
            Start-Sleep -Milliseconds 500
        }

        if (-not (Test-DocsServer $BaseUrl)) {
            throw "Docs server did not become reachable at $BaseUrl within $TimeoutSeconds seconds."
        }
    }

    $tempDir = Join-Path $repoRoot "artifacts/docs-browser-sweep"
    if (Test-Path $tempDir) {
        Remove-Item -LiteralPath $tempDir -Recurse -Force
    }
    New-Item -ItemType Directory -Path $tempDir | Out-Null
    $specPath = Join-Path $tempDir "docs-sweep.spec.js"
    $configPath = Join-Path $tempDir "playwright.config.js"
    $routesJson = ConvertTo-Json $Routes -Compress
    $baseUrlJson = ConvertTo-Json $BaseUrl -Compress

    $spec = @"
const { test, expect } = require('@playwright/test');

const baseUrl = $baseUrlJson;
const routes = $routesJson;
const viewports = [
  { name: 'desktop', width: 1366, height: 900 },
  { name: 'mobile', width: 390, height: 844 }
];

for (const viewport of viewports) {
  test('docs browser sweep ' + viewport.name, async ({ page }) => {
    const consoleErrors = [];
    page.on('console', message => {
      if (message.type() === 'error') {
        consoleErrors.push(message.text());
      }
    });
    page.on('pageerror', error => consoleErrors.push(error.message));
    await page.setViewportSize(viewport);

    const firstResponse = await page.goto(baseUrl, { waitUntil: 'domcontentloaded', timeout: 30000 });
    expect(firstResponse, 'base route returned a response').not.toBeNull();
    expect(firstResponse.ok(), 'base route response should be OK').toBeTruthy();
    try {
      await expect(page.locator('h1').first(), 'base route rendered a heading').toBeVisible({ timeout: 30000 });
    } catch (error) {
      const bodyText = await page.locator('body').innerText({ timeout: 1000 }).catch(() => '');
      throw new Error(error.message + '\nConsole errors:\n' + consoleErrors.join('\n') + '\nBody text:\n' + bodyText.slice(0, 1000));
    }

    for (const route of routes) {
      const directResponse = await page.request.get(new URL(route, baseUrl).toString());
      expect(directResponse.ok(), route + ' direct response should be OK').toBeTruthy();

      await page.evaluate(nextRoute => {
        history.pushState(null, '', nextRoute);
        window.dispatchEvent(new PopStateEvent('popstate'));
      }, route);

      try {
        await expect(page.locator('h1').first(), route + ' rendered a heading').toBeVisible({ timeout: 10000 });
      } catch (error) {
        const bodyText = await page.locator('body').innerText({ timeout: 1000 }).catch(() => '');
        throw new Error(error.message + '\nRoute: ' + route + '\nConsole errors:\n' + consoleErrors.join('\n') + '\nBody text:\n' + bodyText.slice(0, 1000));
      }

      const overflow = await page.evaluate(() => ({
        scrollWidth: document.documentElement.scrollWidth,
        clientWidth: document.documentElement.clientWidth
      }));

      expect(overflow.scrollWidth, route + ' should not overflow horizontally').toBeLessThanOrEqual(overflow.clientWidth + 1);
    }

    expect(consoleErrors, viewport.name + ' console errors').toEqual([]);
  });
}
"@

    Set-Content -Path $specPath -Value $spec -NoNewline
    $config = @"
module.exports = {
  testDir: __dirname,
  testMatch: /.*\.spec\.js/,
  timeout: 120000,
  fullyParallel: true,
  workers: 2,
  use: {
    browserName: 'chromium'
  }
};
"@
    Set-Content -Path $configPath -Value $config -NoNewline
    Push-Location $tempDir
    try {
        npm init -y | Out-Null
        if ($LASTEXITCODE -ne 0) {
            throw "Docs browser sweep package initialization failed with exit code $LASTEXITCODE."
        }

        npm install @playwright/test@1.60.0 --no-save | Out-Null
        if ($LASTEXITCODE -ne 0) {
            throw "Docs browser sweep dependency restore failed with exit code $LASTEXITCODE."
        }

        npx playwright test --config $configPath --reporter=line
        if ($LASTEXITCODE -ne 0) {
            throw "Docs browser sweep failed with exit code $LASTEXITCODE."
        }
    }
    finally {
        Pop-Location
    }
}
finally {
    if ($tempDir -and (Test-Path $tempDir)) {
        Remove-Item -LiteralPath $tempDir -Recurse -Force
    }

    if ($serverProcess -and -not $serverProcess.HasExited) {
        Stop-Process -Id $serverProcess.Id -Force
    }
}
