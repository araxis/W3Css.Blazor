using System.Reflection;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3PublicApiDocumentationTests
{
    [Fact]
    public void ExportedPublicApiMembersHaveXmlSummaries()
    {
        var summaries = XDocument.Load(GetDocumentationPath())
            .Descendants("member")
            .Select(element => new
            {
                Name = (string?)element.Attribute("name") ?? string.Empty,
                Summary = string.Join(' ', element.Elements("summary").Select(summary => summary.Value.Trim()))
            })
            .Where(member => !string.IsNullOrWhiteSpace(member.Name))
            .ToDictionary(member => member.Name, member => member.Summary, StringComparer.Ordinal);

        var missing = EnumerateDocumentedPublicMemberNames()
            .Where(memberName => !HasSummary(summaries, memberName))
            .Order(StringComparer.Ordinal)
            .ToArray();

        Assert.True(
            missing.Length == 0,
            "Missing XML summaries for public API members:" + Environment.NewLine + string.Join(Environment.NewLine, missing));
    }

    private static IEnumerable<string> EnumerateDocumentedPublicMemberNames()
    {
        var assembly = typeof(W3Button).Assembly;

        foreach (var type in assembly.GetExportedTypes()
            .Where(type => type.Namespace?.StartsWith("W3Css.Blazor", StringComparison.Ordinal) == true)
            .Where(type => type.Name != "_Imports")
            .OrderBy(type => type.FullName, StringComparer.Ordinal))
        {
            yield return "T:" + GetXmlTypeName(type);

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(field => field.IsLiteral)
                .OrderBy(field => field.Name, StringComparer.Ordinal))
            {
                yield return "F:" + GetXmlTypeName(type) + "." + field.Name;
            }

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(property => property.GetMethod is not null)
                .Where(property => property.GetCustomAttribute<CascadingParameterAttribute>(inherit: true) is null)
                .OrderBy(property => property.Name, StringComparer.Ordinal))
            {
                yield return "P:" + GetXmlTypeName(type) + "." + property.Name;
            }

            foreach (var @event in type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .OrderBy(@event => @event.Name, StringComparer.Ordinal))
            {
                yield return "E:" + GetXmlTypeName(type) + "." + @event.Name;
            }

            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(method => !method.IsSpecialName)
                .Where(method => !method.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), inherit: false))
                .Where(method => !IsInfrastructureMethod(method))
                .OrderBy(method => method.Name, StringComparer.Ordinal))
            {
                yield return "M:" + GetXmlTypeName(type) + "." + method.Name;
            }
        }
    }

    private static bool HasSummary(IReadOnlyDictionary<string, string> summaries, string memberName)
    {
        if (summaries.TryGetValue(memberName, out var exactSummary))
        {
            return !string.IsNullOrWhiteSpace(exactSummary);
        }

        if (!memberName.StartsWith("M:", StringComparison.Ordinal))
        {
            return false;
        }

        return summaries
            .Where(member => member.Key.StartsWith(memberName + "(", StringComparison.Ordinal))
            .Any(member => !string.IsNullOrWhiteSpace(member.Value));
    }

    private static string GetXmlTypeName(Type type)
    {
        return (type.FullName ?? type.Name).Replace('+', '.');
    }

    private static bool IsInfrastructureMethod(MethodInfo method)
    {
        return method.Name is "Dispose" or "DisposeAsync" or "Equals" or "GetHashCode" or "ToString" or "Deconstruct"
            || method.Name.StartsWith("<Clone>$", StringComparison.Ordinal);
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
