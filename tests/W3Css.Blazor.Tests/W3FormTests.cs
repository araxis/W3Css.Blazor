using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3FormTests
{
    [Fact]
    public void W3FormRendersWithClassAndStyle()
    {
        using var context = new BunitContext();

        var cut = context.Render<W3Form>(parameters => parameters
            .Add(p => p.Model, new FormModel())
            .Add(p => p.Class, "w3-card")
            .Add(p => p.Style, "max-width: 12rem;")
            .AddChildContent("<button type=\"submit\">Submit</button>"));

        var form = cut.Find("form");

        Assert.Contains("w3-card", form.GetAttribute("class"));
        Assert.Equal("max-width: 12rem;", form.GetAttribute("style"));
        Assert.Contains("Submit", form.TextContent);
    }

    [Fact]
    public void W3FormForwardsSubmitContextWhenModelProvided()
    {
        using var context = new BunitContext();

        var model = new FormModel { Name = "Alex" };
        EditContext? callbackContext = null;

        var cut = context.Render<W3Form>(parameters => parameters
            .Add(p => p.Model, model)
            .Add(p => p.OnSubmit, EventCallback.Factory.Create<EditContext>(this, context => callbackContext = context))
            .AddChildContent("<button type=\"submit\">Submit</button>"));

        cut.Find("button[type='submit']").Click();

        Assert.NotNull(callbackContext);
        Assert.Same(model, callbackContext!.Model);
    }

    private sealed class FormModel
    {
        public string Name { get; set; } = string.Empty;
    }
}
