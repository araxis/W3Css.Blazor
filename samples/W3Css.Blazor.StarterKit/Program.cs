using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using W3Css.Blazor;
using W3Css.Blazor.StarterKit;
using W3Css.Blazor.StarterKit.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddW3CssBlazor();
builder.Services.AddScoped<StarterWorkspace>();

await builder.Build().RunAsync();
