using System.Text.RegularExpressions;

namespace W3Css.Blazor.Tests;

public sealed class W3ReleaseQualityTests
{
    private const string CurrentVersion = "0.5.0";

    [Fact]
    public void ReadmeDocumentsCurrentAppPrimitivesAndBranchFlow()
    {
        var readme = ReadRepositoryFile("README.md");

        Assert.Contains("W3ThemeProvider", readme);
        Assert.Contains("W3DataTable", readme);
        Assert.Contains("W3ActionRow", readme);
        Assert.Contains("work/<short-topic>", readme);
        Assert.DoesNotContain("feature/<short-topic>", readme);
        Assert.DoesNotContain("release/<version>", readme);
    }

    [Fact]
    public void ReleaseMemoryReflectsPostFirstReleaseState()
    {
        var currentState = ReadRepositoryFile("memory", "current-state.md");
        var developmentPlan = ReadRepositoryFile("memory", "development-plan.md");
        var projectMemory = ReadRepositoryFile("memory", "project-memory.md");

        Assert.Contains("metadata targets `0.5.0`", currentState);
        Assert.Contains("Package And Release Readiness (Complete)", developmentPlan);
        Assert.Contains("Current package baseline", projectMemory);
        Assert.DoesNotContain("points at current `HEAD`", currentState);
    }

    [Fact]
    public void PackageVersionAndReleaseNotesStayAligned()
    {
        var project = ReadRepositoryFile("src", "W3Css.Blazor", "W3Css.Blazor.csproj");
        var changelog = ReadRepositoryFile("CHANGELOG.md");
        var releaseNotesPath = PathFromRepositoryRoot("docs", "release-notes", $"{CurrentVersion}.md");

        Assert.Contains($"<Version>{CurrentVersion}</Version>", project);
        Assert.Contains($"docs/release-notes/{CurrentVersion}.md", project);
        Assert.Contains($"## {CurrentVersion} - 2026-06-02", changelog);
        Assert.True(File.Exists(releaseNotesPath), $"Release notes file is missing: {releaseNotesPath}");
    }

    [Fact]
    public void ConsumerSmokeCoversCurrentAppPrimitives()
    {
        var smoke = ReadRepositoryFile("tools", "package-consumer-smoke.ps1");

        Assert.Contains("W3ThemeProvider", smoke);
        Assert.Contains("W3Card", smoke);
        Assert.Contains("W3DataTable", smoke);
        Assert.Contains("W3ActionRow", smoke);
        Assert.Contains("W3EmptyState", smoke);
        Assert.Contains("W3Form", smoke);
        Assert.Contains("W3Field", smoke);
        Assert.Contains("W3Modal", smoke);
        Assert.Contains("W3MessageBox", smoke);
        Assert.Contains("W3ToastProvider", smoke);
        Assert.Contains("AddW3CssBlazor", smoke);
        Assert.Contains("PackageSource", smoke);
    }

    [Fact]
    public void StarterKitSampleIsIncludedAndUsesPackagePrimitives()
    {
        var solution = ReadRepositoryFile("W3Css.Blazor.slnx");
        var project = ReadRepositoryFile("samples", "W3Css.Blazor.StarterKit", "W3Css.Blazor.StarterKit.csproj");
        var layout = ReadRepositoryFile("samples", "W3Css.Blazor.StarterKit", "Layout", "MainLayout.razor");
        var dashboard = ReadRepositoryFile("samples", "W3Css.Blazor.StarterKit", "Pages", "Home.razor");
        var workflow = ReadRepositoryFile("samples", "W3Css.Blazor.StarterKit", "Pages", "Workflow.razor");
        var workspace = ReadRepositoryFile("samples", "W3Css.Blazor.StarterKit", "Services", "StarterWorkspace.cs");
        var styles = ReadRepositoryFile("samples", "W3Css.Blazor.StarterKit", "wwwroot", "css", "app.css");
        var index = ReadRepositoryFile("samples", "W3Css.Blazor.StarterKit", "wwwroot", "index.html");

        Assert.Contains("samples/W3Css.Blazor.StarterKit/W3Css.Blazor.StarterKit.csproj", solution);
        Assert.Contains(@"..\..\src\W3Css.Blazor\W3Css.Blazor.csproj", project);
        Assert.Contains("_content/W3Css.Blazor/w3css-blazor.css", index);
        Assert.Contains("W3ThemeProvider", layout);
        Assert.Contains("W3AppShell", layout);
        Assert.Contains("W3ToastProvider", layout);
        Assert.Contains("W3DataTable", dashboard);
        Assert.Contains("W3EmptyState", dashboard);
        Assert.Contains("W3Modal", workflow);
        Assert.Contains("W3MessageBox", workflow);
        Assert.Contains("\"\\u2600\"", workspace);
        Assert.Contains("\"\\u263E\"", workspace);
        Assert.Contains("\"\\u2699\"", workspace);
        Assert.DoesNotContain("starter-theme-toggle", layout);
        Assert.DoesNotContain("starter-theme-icon", workspace);
        Assert.DoesNotContain("starter-theme-icon", styles);
        Assert.DoesNotContain("box-shadow: none;", styles);
        Assert.DoesNotContain("border-color: rgba(255, 255, 255, 0.46) !important;", styles);
        Assert.DoesNotContain("padding-bottom: 0.7rem", styles);
        Assert.DoesNotContain("padding-top: 0.7rem", styles);
        Assert.Contains("padding-top: 18px;", styles);
        Assert.Contains("padding-bottom: 18px;", styles);
    }

    [Fact]
    public void BrowserSweepCoversAdoptionAndInteractiveRoutes()
    {
        var sweep = ReadRepositoryFile("tools", "docs-browser-sweep.ps1");

        foreach (var route in new[]
        {
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
            "/components/pagination",
        })
        {
            Assert.Contains($"\"{route}\"", sweep);
        }

        Assert.Contains("consoleErrors", sweep);
        Assert.Contains("scrollWidth", sweep);
    }

    [Fact]
    public void PublicDocsAvoidComparisonLibraryNaming()
    {
        var blockedExact = string.Concat("Mud", "Blazor");
        var blockedPrefix = new Regex(@"\bMud[A-Z][A-Za-z]+\b", RegexOptions.CultureInvariant);
        var files = new[]
            {
                "README.md",
                "CHANGELOG.md",
            }
            .Select(file => PathFromRepositoryRoot(file))
            .Concat(Directory.EnumerateFiles(PathFromRepositoryRoot("src", "W3Css.Blazor.Docs"), "*.razor", SearchOption.AllDirectories));

        foreach (var file in files)
        {
            var text = File.ReadAllText(file);

            Assert.DoesNotContain(blockedExact, text);
            Assert.DoesNotMatch(blockedPrefix, text);
        }
    }

    private static string ReadRepositoryFile(params string[] pathParts)
    {
        return File.ReadAllText(PathFromRepositoryRoot(pathParts));
    }

    private static string PathFromRepositoryRoot(params string[] pathParts)
    {
        return Path.Combine(GetRepositoryRoot(), Path.Combine(pathParts));
    }

    private static string GetRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "W3Css.Blazor.slnx")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Could not locate the repository root.");
    }
}
