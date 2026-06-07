param(
    [string]$BaseUrl = "http://localhost:5026",
    [switch]$StartServer,
    [int]$TimeoutSeconds = 90
)

$ErrorActionPreference = "Stop"
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$serverProcess = $null
$tempDir = $null

function Test-StarterServer {
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
    if (-not (Test-StarterServer $BaseUrl)) {
        if (-not $StartServer) {
            throw "Starter kit server is not reachable at $BaseUrl. Start it first or pass -StartServer."
        }

        $serverProcess = Start-Process -FilePath "dotnet" `
            -ArgumentList @("run", "--project", "samples/W3Css.Blazor.StarterKit", "--urls", $BaseUrl) `
            -WorkingDirectory $repoRoot `
            -WindowStyle Hidden `
            -PassThru

        $deadline = (Get-Date).AddSeconds($TimeoutSeconds)
        while ((Get-Date) -lt $deadline -and -not (Test-StarterServer $BaseUrl)) {
            Start-Sleep -Milliseconds 500
        }

        if (-not (Test-StarterServer $BaseUrl)) {
            throw "Starter kit server did not become reachable at $BaseUrl within $TimeoutSeconds seconds."
        }
    }

    $tempDir = Join-Path $repoRoot "artifacts/starter-browser-sweep"
    if (Test-Path $tempDir) {
        Remove-Item -LiteralPath $tempDir -Recurse -Force
    }

    New-Item -ItemType Directory -Path $tempDir | Out-Null
    $specPath = Join-Path $tempDir "starter-sweep.spec.js"
    $configPath = Join-Path $tempDir "playwright.config.js"
    $baseUrlJson = ConvertTo-Json $BaseUrl -Compress

    $spec = @"
const { test, expect } = require('@playwright/test');

const baseUrl = $baseUrlJson;
const routes = [
  { path: '/', heading: 'Dashboard' },
  { path: '/settings', heading: 'Settings' },
  { path: '/customers', heading: 'Customers' },
  { path: '/workflow', heading: 'Workflow' }
];
const viewports = [
  { name: 'desktop', width: 1366, height: 900 },
  { name: 'mobile', width: 390, height: 844 }
];

async function expectNoHorizontalOverflow(page, route) {
  const overflow = await page.evaluate(() => ({
    scrollWidth: document.documentElement.scrollWidth,
    clientWidth: document.documentElement.clientWidth
  }));
  expect(overflow.scrollWidth, route + ' should not overflow horizontally').toBeLessThanOrEqual(overflow.clientWidth + 1);
}

async function gotoRoute(page, route) {
  const response = await page.goto(new URL(route, baseUrl).toString(), { waitUntil: 'domcontentloaded', timeout: 30000 });
  expect(response, route + ' returned a response').not.toBeNull();
  expect(response.ok(), route + ' response should be OK').toBeTruthy();
}

async function closeMobileSidebar(page, viewport) {
  if (viewport.name !== 'mobile') {
    return;
  }

  const sidebar = page.locator('aside[aria-label="Sidebar"]');
  if (await sidebar.getAttribute('aria-hidden') === 'false') {
    await page.locator('button[aria-controls="starter-sidebar"]').first().click();
    await expect(sidebar).toHaveAttribute('aria-hidden', 'true');
  }
}

for (const viewport of viewports) {
  test('starter kit browser sweep ' + viewport.name, async ({ page }) => {
    const consoleErrors = [];
    page.on('console', message => {
      if (message.type() === 'error') {
        consoleErrors.push(message.text());
      }
    });
    page.on('pageerror', error => consoleErrors.push(error.message));
    await page.setViewportSize(viewport);

    for (const route of routes) {
      await gotoRoute(page, route.path);
      await closeMobileSidebar(page, viewport);
      try {
        await expect(page.getByRole('heading', { name: route.heading, level: 1 }), route.path + ' heading').toBeVisible({ timeout: 30000 });
      } catch (error) {
        const bodyText = await page.locator('body').innerText({ timeout: 1000 }).catch(() => '');
        throw new Error(error.message + '\nRoute: ' + route.path + '\nConsole errors:\n' + consoleErrors.join('\n') + '\nBody text:\n' + bodyText.slice(0, 1000));
      }
      await expectNoHorizontalOverflow(page, route.path);
    }

    await gotoRoute(page, '/');
    await closeMobileSidebar(page, viewport);

    const themeToggle = page.locator('button[aria-label^="Theme mode:"]').first();
    await expect(themeToggle, 'theme toggle is visible').toBeVisible();
    const initialThemeLabel = await themeToggle.getAttribute('aria-label');
    await themeToggle.click();
    await expect.poll(async () => await themeToggle.getAttribute('aria-label'), {
      message: 'theme toggle should cycle to another state'
    }).not.toBe(initialThemeLabel);

    await page.getByRole('button', { name: 'Refresh' }).click();
    const toast = page.locator('.w3-toast').filter({ hasText: 'Dashboard data refreshed.' }).first();
    await expect(toast, 'dashboard refresh toast').toBeVisible();
    await expect(toast, 'toast title stays readable').toContainText('Dashboard');
    const toastLayout = await toast.evaluate(element => {
      const style = getComputedStyle(element);
      const rect = element.getBoundingClientRect();
      const close = element.querySelector('.w3-toast-close');
      const closeRect = close ? close.getBoundingClientRect() : null;
      return {
        display: style.display,
        gridTemplateColumns: style.gridTemplateColumns,
        width: rect.width,
        closeTop: closeRect ? closeRect.top - rect.top : -1,
        closeRight: closeRect ? rect.right - closeRect.right : -1
      };
    });
    expect(toastLayout.display, 'toast should use grid layout').toBe('grid');
    expect(toastLayout.gridTemplateColumns, 'toast should reserve a close column').toContain('px');
    expect(toastLayout.closeTop, 'toast close button top inset').toBeGreaterThanOrEqual(8);
    expect(toastLayout.closeRight, 'toast close button right inset').toBeGreaterThanOrEqual(8);
    expect(toastLayout.width, 'toast should stay within viewport').toBeLessThanOrEqual(viewport.width);

    await page.getByRole('button', { name: '0.9.0 Ready' }).click();
    const releaseToast = page.locator('.w3-toast').filter({ hasText: 'Starter kit release path is wired.' }).first();
    await expect(releaseToast, 'starter release toast').toBeVisible();
    await expect(releaseToast, 'starter release toast title').toContainText('0.8.0');

    await gotoRoute(page, '/customers');
    await closeMobileSidebar(page, viewport);
    await page.getByPlaceholder('Search customers').fill('Northwind');
    await expect(page.getByText('Northwind Studio')).toBeVisible();
    await expect(page.getByText('Contoso Labs')).toHaveCount(0);
    await expect(page.getByText('1-1 of 1')).toBeVisible();
    await page.getByRole('button', { name: 'Clear search' }).click();
    await expect(page.getByText('Contoso Labs')).toBeVisible();
    await expectNoHorizontalOverflow(page, '/customers after search');

    await gotoRoute(page, '/workflow');
    await closeMobileSidebar(page, viewport);
    await page.getByRole('button', { name: 'Edit record' }).click();
    await expect(page.getByRole('dialog').filter({ hasText: 'Edit record' })).toBeVisible();
    await page.getByRole('button', { name: 'Cancel' }).click();
    await expect(page.getByRole('dialog').filter({ hasText: 'Edit record' })).toHaveCount(0);

    await page.getByRole('button', { name: 'Archive' }).click();
    await expect(page.getByRole('dialog').filter({ hasText: 'Archive record?' })).toBeVisible();
    await page.getByRole('button', { name: 'Cancel' }).click();
    await expect(page.getByRole('dialog').filter({ hasText: 'Archive record?' })).toHaveCount(0);
    await expectNoHorizontalOverflow(page, '/workflow after dialogs');

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
            throw "Starter browser sweep package initialization failed with exit code $LASTEXITCODE."
        }

        npm install @playwright/test@1.60.0 --no-save | Out-Null
        if ($LASTEXITCODE -ne 0) {
            throw "Starter browser sweep dependency restore failed with exit code $LASTEXITCODE."
        }

        npx playwright test --config $configPath --reporter=line
        if ($LASTEXITCODE -ne 0) {
            throw "Starter browser sweep failed with exit code $LASTEXITCODE."
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
