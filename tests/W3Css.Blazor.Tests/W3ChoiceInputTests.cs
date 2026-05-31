using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ChoiceInputTests
{
    [Fact]
    public void CheckboxRendersRequiredLabelAndUpdatesValue()
    {
        using var context = new BunitContext();

        var value = false;
        var cut = context.Render<W3Checkbox>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<bool>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "terms")
            .Add(p => p.Label, "I accept")
            .Add(p => p.Required, true)
            .Add(p => p.LabelClass, "choice-label")
            .AddUnmatched("aria-describedby", "terms-help"));

        var label = cut.Find("label");
        var input = cut.Find("input");
        var text = cut.Find(".w3-choice-label-text");

        Assert.Contains("w3-choice-label", label.GetAttribute("class"));
        Assert.Contains("w3-checkbox-label", label.GetAttribute("class"));
        Assert.Contains("choice-label", label.GetAttribute("class"));
        Assert.Equal("checkbox", input.GetAttribute("type"));
        Assert.Equal("terms", input.GetAttribute("id"));
        Assert.True(input.HasAttribute("required"));
        Assert.Equal("true", input.GetAttribute("aria-required"));
        Assert.Equal("terms-help", input.GetAttribute("aria-describedby"));
        Assert.Equal("I accept", text.TextContent.Trim());

        input.Change(true);

        Assert.True(value);
    }

    [Fact]
    public void RadioGroupCascadesRequiredStateToChildRadios()
    {
        using var context = new BunitContext();

        string? value = null;
        var cut = context.Render<W3RadioGroup<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next))
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Label, "Contact method")
            .Add(p => p.Name, "contact")
            .Add(p => p.Required, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<W3Radio<string>>(0);
                builder.AddAttribute(1, "OptionValue", "email");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.AddContent(0, "Email");
                }));
                builder.CloseComponent();

                builder.OpenComponent<W3Radio<string>>(3);
                builder.AddAttribute(4, "OptionValue", "sms");
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.AddContent(0, "SMS");
                }));
                builder.CloseComponent();
            })));

        var group = cut.Find("[role='radiogroup']");
        var radios = cut.FindAll("input[type='radio']");

        Assert.Equal("Contact method", group.GetAttribute("aria-label"));
        Assert.Equal("true", group.GetAttribute("aria-required"));
        Assert.All(radios, radio =>
        {
            Assert.Equal("contact", radio.GetAttribute("name"));
            Assert.True(radio.HasAttribute("required"));
            Assert.Equal("true", radio.GetAttribute("aria-required"));
        });

        radios[1].Change(true);

        Assert.Equal("sms", value);
    }

    [Fact]
    public async Task ChoiceInputsReportValidationStateThroughFieldMessages()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3ChoiceValidationHost>();

        var isValid = await cut.InvokeAsync(() => cut.Instance.Validate());

        Assert.False(isValid);
        Assert.Contains("Accept terms to continue.", cut.Markup);
        Assert.Contains("Choose a contact method.", cut.Markup);
        Assert.Equal("true", cut.Find("[role='radiogroup']").GetAttribute("aria-invalid"));
        Assert.Contains("invalid", cut.Find("[role='radiogroup']").GetAttribute("class"));

        cut.Find("input[type='checkbox']").Change(true);
        cut.FindAll("input[type='radio']")[0].Change(true);

        isValid = await cut.InvokeAsync(() => cut.Instance.Validate());

        Assert.True(isValid);
        Assert.DoesNotContain("Accept terms to continue.", cut.Markup);
        Assert.DoesNotContain("Choose a contact method.", cut.Markup);
    }
}
