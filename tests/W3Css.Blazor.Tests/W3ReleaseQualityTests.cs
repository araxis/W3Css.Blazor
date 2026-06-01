using System.Text.RegularExpressions;

namespace W3Css.Blazor.Tests;

public sealed class W3ReleaseQualityTests
{
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

        Assert.Contains("post-0.1.0 changes", currentState);
        Assert.Contains("Package And Release Readiness (Complete)", developmentPlan);
        Assert.Contains("Future releases", projectMemory);
        Assert.DoesNotContain("points at current `HEAD`", currentState);
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
