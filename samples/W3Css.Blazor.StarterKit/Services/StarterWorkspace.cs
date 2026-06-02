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
        Radius = "6px",
        Dark = W3Theme.DefaultDark with
        {
            Primary = "#14b8a6",
            OnPrimary = "#062925",
            Secondary = "#8fb3ff",
            Accent = "#ffd166",
            Surface = "#172121",
            OnSurface = "#eef7f6",
            Background = "#0f1718",
            OnBackground = "#e5f0ef",
            Border = "#314546",
            Info = "#7cc7ff",
            Success = "#86d993",
            Warning = "#ffd166",
            Danger = "#ff9b9b",
            Note = "#273b3c"
        }
    };

    public StarterThemeMode ThemeMode { get; private set; } = StarterThemeMode.Auto;

    public bool SystemPrefersDark { get; private set; }

    public bool DarkThemeEnabled => ThemeMode switch
    {
        StarterThemeMode.Light => false,
        StarterThemeMode.Dark => true,
        _ => SystemPrefersDark
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

    public IReadOnlyList<W3ToggleIconButtonState> ThemeModeStates { get; } =
    [
        new(nameof(StarterThemeMode.Light), "Theme mode: Light", "\u2600", title: "Use light mode"),
        new(nameof(StarterThemeMode.Dark), "Theme mode: Dark", "\u263E", title: "Use dark mode"),
        new(nameof(StarterThemeMode.Auto), "Theme mode: Auto", "\u25D0", title: "Follow system theme")
    ];

    public string ThemeModeValue => ThemeMode.ToString();

    public IReadOnlyList<string> CustomerPlanLabels => ["Starter", "Team", "Business"];

    public IReadOnlyList<W3ChartSeries> CustomerPlanSeries =>
    [
        new("Accounts", CustomerPlanLabels.Select(label => (double)Customers.Count(customer => customer.Plan == label)).ToArray())
    ];

    public IReadOnlyList<string> CustomerHealthLabels => ["Healthy", "Watch", "Blocked"];

    public IReadOnlyList<W3ChartSeries> CustomerHealthSeries =>
    [
        new("Accounts", CustomerHealthLabels.Select(label => (double)Customers.Count(customer => customer.Health == label)).ToArray(), "#14b8a6")
    ];

    public async Task SaveSettingsAsync()
    {
        await Task.Delay(150);
    }

    public void ResetSettings()
    {
        Settings = StarterSettings.CreateDefault();
    }

    public void SetThemeMode(StarterThemeMode mode)
    {
        ThemeMode = mode;
    }

    public void SetThemeModeValue(string? value)
    {
        if (Enum.TryParse<StarterThemeMode>(value, ignoreCase: false, out var mode))
        {
            SetThemeMode(mode);
        }
    }

    public void SetSystemPrefersDark(bool prefersDark)
    {
        SystemPrefersDark = prefersDark;
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

public enum StarterThemeMode
{
    Light,
    Dark,
    Auto
}
