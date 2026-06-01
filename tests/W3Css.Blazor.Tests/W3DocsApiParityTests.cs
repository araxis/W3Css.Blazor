using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3DocsApiParityTests
{
    private static readonly IReadOnlyDictionary<string, string> TopicPageOverrides = new Dictionary<string, string>(StringComparer.Ordinal)
    {
        ["W3AccordionItem"] = "AccordionPage.razor",
        ["W3Animate"] = "AnimationsPage.razor",
        ["W3BarItem"] = "BarPage.razor",
        ["W3Border"] = "BordersPage.razor",
        ["W3BottomNavigationItem"] = "BottomNavigationPage.razor",
        ["W3BreadcrumbItem"] = "BreadcrumbPage.razor",
        ["W3Cell"] = "CellsPage.razor",
        ["W3CellRow"] = "CellsPage.razor",
        ["W3ChatMessage"] = "ChatPage.razor",
        ["W3DataColumn"] = "DataTablePage.razor",
        ["W3DisplayContainer"] = "DisplayPage.razor",
        ["W3Effect"] = "EffectsPage.razor",
        ["W3HoverColor"] = "HoverColorsPage.razor",
        ["W3Icon"] = "IconsPage.razor",
        ["W3ImageListItem"] = "ImageListPage.razor",
        ["W3MenuDivider"] = "MenuPage.razor",
        ["W3MenuItem"] = "MenuPage.razor",
        ["W3NavbarItem"] = "NavbarPage.razor",
        ["W3NavMenuGroup"] = "NavMenuPage.razor",
        ["W3NavMenuItem"] = "NavMenuPage.razor",
        ["W3Slide"] = "SlideshowPage.razor",
        ["W3Spacing"] = "SpacingPage.razor",
        ["W3Step"] = "StepperPage.razor",
        ["W3TabPanel"] = "TabsPage.razor",
        ["W3Text"] = "TextFontsPage.razor",
        ["W3ThemeProvider"] = "ThemingPage.razor",
        ["W3TimelineItem"] = "TimelinePage.razor",
        ["W3ToastProvider"] = "ToastPage.razor",
        ["W3ToggleItem"] = "ToggleGroupPage.razor",
    };

    private static readonly IReadOnlySet<string> IgnoredFrameworkParameters = new HashSet<string>(StringComparer.Ordinal)
    {
        "DisplayName",
    };

    [Fact]
    public void ComponentTopicPagesDocumentPublicParameters()
    {
        var missing = EnumerateComponentParameterDocumentationGaps()
            .OrderBy(gap => gap.Component, StringComparer.Ordinal)
            .ThenBy(gap => gap.Parameter, StringComparer.Ordinal)
            .Select(gap => $"{gap.Component}.{gap.Parameter} in {gap.TopicPage}")
            .ToArray();

        Assert.True(
            missing.Length == 0,
            "Component topic pages are missing public parameter rows:" + Environment.NewLine + string.Join(Environment.NewLine, missing));
    }

    private static IEnumerable<(string Component, string Parameter, string TopicPage)> EnumerateComponentParameterDocumentationGaps()
    {
        foreach (var componentType in typeof(W3Button).Assembly.GetExportedTypes()
            .Where(type => type.Namespace == "W3Css.Blazor.Components")
            .Where(type => typeof(IComponent).IsAssignableFrom(type))
            .OrderBy(type => type.Name, StringComparer.Ordinal))
        {
            var topicPage = GetTopicPageName(componentType);
            var topicPath = Path.Combine(GetRepositoryRoot(), "src", "W3Css.Blazor.Docs", "Pages", "ComponentTopics", topicPage);

            Assert.True(File.Exists(topicPath), $"Documentation topic is missing for {componentType.Name}: {topicPath}");

            var topicText = File.ReadAllText(topicPath);
            var parameterNames = componentType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetCustomAttribute<ParameterAttribute>(inherit: true) is not null)
                .Select(property => property.Name)
                .Distinct(StringComparer.Ordinal)
                .Where(parameterName => !IgnoredFrameworkParameters.Contains(parameterName))
                .OrderBy(name => name, StringComparer.Ordinal);

            foreach (var parameterName in parameterNames)
            {
                if (!ContainsParameterTableCell(topicText, parameterName))
                {
                    yield return (componentType.Name, parameterName, topicPage);
                }
            }
        }
    }

    private static string GetTopicPageName(Type componentType)
    {
        var componentName = componentType.Name.Split('`')[0];

        if (TopicPageOverrides.TryGetValue(componentName, out var pageName))
        {
            return pageName;
        }

        return componentName[2..] + "Page.razor";
    }

    private static bool ContainsParameterTableCell(string topicText, string parameterName)
    {
        var pattern = $@"<td>[^<]*\b{Regex.Escape(parameterName)}\b[^<]*</td>";
        return Regex.IsMatch(topicText, pattern, RegexOptions.CultureInvariant);
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
