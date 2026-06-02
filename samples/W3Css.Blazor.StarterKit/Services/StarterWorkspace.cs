using System.ComponentModel.DataAnnotations;
using W3Css.Blazor;

namespace W3Css.Blazor.StarterKit.Services;

public sealed class StarterWorkspace
{
    public W3Theme Theme { get; } = W3Theme.Default with
    {
        Primary = "#009688",
        Secondary = "#34568b",
        Accent = "#ffb305",
        Background = "#f4f7f7",
        Border = "#d8e3e3",
        Radius = "6px"
    };

    public StarterSettings Settings { get; private set; } = StarterSettings.CreateDefault();

    public List<WorkItem> WorkItems { get; } =
    [
        new("Review settings form", "Nora", "Ready"),
        new("Audit data states", "Iris", "Queued"),
        new("Ship starter workflow", "Omar", "In review")
    ];

    public List<CustomerAccount> Customers { get; } =
    [
        new("Northwind Studio", "Team", "Healthy", "Nora"),
        new("Contoso Labs", "Business", "Watch", "Iris"),
        new("Fabrikam Works", "Starter", "Healthy", "Omar"),
        new("Adventure Supply", "Business", "Blocked", "Mina"),
        new("Blue Yonder", "Team", "Healthy", "Nora")
    ];

    public WorkflowRecord WorkflowRecord { get; private set; } = new("Quarterly review", "Active", "Omar");

    public string? CustomerSearch { get; set; }

    public bool CustomersFailed { get; set; }

    public string LastWorkflowResult { get; private set; } = "none yet";

    public IReadOnlyList<Metric> Metrics =>
    [
        new("Open tasks", WorkItems.Count.ToString(), "2 waiting for review"),
        new("Customers", Customers.Count.ToString(), "1 account needs attention"),
        new("Release health", "Good", "No blocked checks")
    ];

    public async Task SaveSettingsAsync()
    {
        await Task.Delay(150);
    }

    public void ResetSettings()
    {
        Settings = StarterSettings.CreateDefault();
    }

    public void UpdateWorkflowRecord(string? name, string? owner)
    {
        WorkflowRecord = WorkflowRecord with
        {
            Name = string.IsNullOrWhiteSpace(name) ? WorkflowRecord.Name : name.Trim(),
            Owner = string.IsNullOrWhiteSpace(owner) ? WorkflowRecord.Owner : owner.Trim(),
            Status = "Updated"
        };
        LastWorkflowResult = "record saved";
    }

    public void ApplyArchiveResult(bool? result)
    {
        LastWorkflowResult = result switch
        {
            true => "record archived",
            false => "record kept active",
            _ => "archive canceled"
        };

        if (result == true)
        {
            WorkflowRecord = WorkflowRecord with { Status = "Archived" };
        }
    }
}

public sealed class StarterSettings
{
    [Required]
    [StringLength(48, MinimumLength = 3)]
    public string WorkspaceName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string NotificationEmail { get; set; } = string.Empty;

    [Required]
    public string DefaultRegion { get; set; } = string.Empty;

    public static StarterSettings CreateDefault()
    {
        return new StarterSettings
        {
            WorkspaceName = "Operations hub",
            NotificationEmail = "team@example.com",
            DefaultRegion = "North"
        };
    }
}

public sealed record Metric(string Label, string Value, string Detail);

public sealed record WorkItem(string Name, string Owner, string State);

public sealed record CustomerAccount(string Name, string Plan, string Health, string Owner);

public sealed record WorkflowRecord(string Name, string Status, string Owner);
