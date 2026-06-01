using System.Xml.Linq;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3PublicApiDocumentationTests
{
    [Theory]
    [InlineData("T:W3Css.Blazor.W3Theme")]
    [InlineData("P:W3Css.Blazor.W3Theme.Primary")]
    [InlineData("P:W3Css.Blazor.Components.W3ThemeProvider.Theme")]
    [InlineData("P:W3Css.Blazor.Components.W3ThemeProvider.Dark")]
    [InlineData("P:W3Css.Blazor.Components.W3AppShell.Header")]
    [InlineData("P:W3Css.Blazor.Components.W3AppShell.SidebarOpen")]
    [InlineData("P:W3Css.Blazor.Components.W3AppShell.SidebarWidth")]
    [InlineData("P:W3Css.Blazor.Components.W3AppBar.Title")]
    [InlineData("P:W3Css.Blazor.Components.W3Form.Busy")]
    [InlineData("P:W3Css.Blazor.Components.W3Input.UpdateOnInput")]
    [InlineData("P:W3Css.Blazor.Components.W3DataTable`1.Items")]
    [InlineData("P:W3Css.Blazor.Components.W3DataTable`1.Searchable")]
    [InlineData("P:W3Css.Blazor.Components.W3DataTable`1.RowActions")]
    [InlineData("P:W3Css.Blazor.Components.W3DataTable`1.Error")]
    [InlineData("P:W3Css.Blazor.Components.W3Modal.Actions")]
    [InlineData("P:W3Css.Blazor.Components.W3ActionRow.Label")]
    [InlineData("P:W3Css.Blazor.Components.W3EmptyState.Kind")]
    [InlineData("P:W3Css.Blazor.Components.W3Menu.Open")]
    [InlineData("P:W3Css.Blazor.Components.W3Tabs.ActiveValue")]
    public void SelectedPublicApiMembersHaveXmlSummaries(string memberName)
    {
        var xml = XDocument.Load(GetDocumentationPath());
        var member = xml.Descendants("member")
            .SingleOrDefault(element => string.Equals((string?)element.Attribute("name"), memberName, StringComparison.Ordinal));

        Assert.NotNull(member);

        var summary = string.Join(' ', member!
            .Elements("summary")
            .Select(element => element.Value.Trim()));

        Assert.False(string.IsNullOrWhiteSpace(summary), $"Missing XML summary for {memberName}.");
    }

    private static string GetDocumentationPath()
    {
        var assemblyDirectory = Path.GetDirectoryName(typeof(W3Button).Assembly.Location)
            ?? throw new DirectoryNotFoundException("Could not locate the component assembly output directory.");
        var outputXml = Path.Combine(assemblyDirectory, "W3Css.Blazor.xml");

        if (File.Exists(outputXml))
        {
            return outputXml;
        }

        var repositoryXml = Path.Combine(
            GetRepositoryRoot(),
            "src",
            "W3Css.Blazor",
            "bin",
            "Release",
            "net10.0",
            "W3Css.Blazor.xml");

        Assert.True(File.Exists(repositoryXml), $"Documentation XML is missing: {repositoryXml}");
        return repositoryXml;
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
