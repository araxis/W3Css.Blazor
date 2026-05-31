using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace W3Css.Blazor;

/// <summary>
/// Dependency injection helpers for W3Css.Blazor services.
/// </summary>
public static class W3CssBlazorServiceCollectionExtensions
{
    /// <summary>
    /// Adds W3Css.Blazor services such as toast notifications.
    /// </summary>
    public static IServiceCollection AddW3CssBlazor(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddScoped<W3ToastService>();

        return services;
    }

    /// <summary>
    /// Adds only W3Css.Blazor toast notification services.
    /// </summary>
    public static IServiceCollection AddW3Toasts(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddScoped<W3ToastService>();

        return services;
    }
}

