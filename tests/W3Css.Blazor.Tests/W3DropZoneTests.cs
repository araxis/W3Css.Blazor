using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3DropZoneTests
{
    [Fact]
    public void DropZoneRendersNativeFileInputAndSurfaceClasses()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3DropZone>(parameters => parameters
            .Add(p => p.Id, "asset-drop")
            .Add(p => p.Name, "assets")
            .Add(p => p.Accept, ".png,.jpg")
            .Add(p => p.Multiple, true)
            .Add(p => p.Label, "Drop assets")
            .Add(p => p.Description, "or choose files")
            .Add(p => p.AcceptHint, "PNG or JPG")
            .Add(p => p.Color, W3Color.White)
            .Add(p => p.TextColor, W3Color.Black)
            .Add(p => p.BorderColor, W3Color.Teal)
            .Add(p => p.Round, W3Round.Medium)
            .Add(p => p.Class, "upload-zone")
            .Add(p => p.Style, "max-width: 32rem;"));

        var zone = cut.Find(".w3-drop-zone");
        var input = cut.Find("input");
        var browseButton = cut.Find(".w3-drop-zone-browse");

        Assert.Equal("false", zone.GetAttribute("aria-disabled"));
        Assert.Contains("w3-drop-zone", zone.GetAttribute("class"));
        Assert.Contains("w3-padding-large", zone.GetAttribute("class"));
        Assert.Contains("w3-border", zone.GetAttribute("class"));
        Assert.Contains("w3-white", zone.GetAttribute("class"));
        Assert.Contains("w3-text-black", zone.GetAttribute("class"));
        Assert.Contains("w3-border-teal", zone.GetAttribute("class"));
        Assert.Contains("w3-round", zone.GetAttribute("class"));
        Assert.Contains("upload-zone", zone.GetAttribute("class"));
        Assert.Equal("max-width: 32rem;", zone.GetAttribute("style"));

        Assert.Equal("file", input.GetAttribute("type"));
        Assert.Equal("asset-drop", input.GetAttribute("id"));
        Assert.Equal("assets", input.GetAttribute("name"));
        Assert.Equal(".png,.jpg", input.GetAttribute("accept"));
        Assert.True(input.HasAttribute("multiple"));
        Assert.Equal("Drop assets", input.GetAttribute("aria-label"));
        Assert.Contains("w3-drop-zone-input", input.GetAttribute("class"));
        Assert.Equal("asset-drop", browseButton.GetAttribute("for"));
        Assert.Contains("w3-button", browseButton.GetAttribute("class"));
        Assert.Contains("w3-teal", browseButton.GetAttribute("class"));
        Assert.Contains("w3-text-white", browseButton.GetAttribute("class"));
        Assert.Equal("Choose files", browseButton.TextContent);

        Assert.Contains("Drop assets", cut.Markup);
        Assert.Contains("or choose files", cut.Markup);
        Assert.Contains("PNG or JPG", cut.Markup);
    }

    [Fact]
    public void DropZoneAppliesDraggingStateAndRaisesDragCallbacks()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var dragEnterCount = 0;
        var dragLeaveCount = 0;
        var dropCount = 0;

        var cut = context.Render<W3DropZone>(parameters => parameters
            .Add(p => p.OnDragEnter, EventCallback.Factory.Create<DragEventArgs>(this, _ => dragEnterCount++))
            .Add(p => p.OnDragLeave, EventCallback.Factory.Create<DragEventArgs>(this, _ => dragLeaveCount++))
            .Add(p => p.OnDrop, EventCallback.Factory.Create<DragEventArgs>(this, _ => dropCount++)));

        var zone = cut.Find(".w3-drop-zone");

        zone.TriggerEvent("ondragenter", new DragEventArgs());

        Assert.Equal(1, dragEnterCount);
        Assert.Contains("w3-drop-zone-dragging", cut.Find(".w3-drop-zone").GetAttribute("class"));
        Assert.Contains("w3-pale-blue", cut.Find(".w3-drop-zone").GetAttribute("class"));

        cut.Find(".w3-drop-zone").TriggerEvent("ondragleave", new DragEventArgs());

        Assert.Equal(1, dragLeaveCount);
        Assert.DoesNotContain("w3-drop-zone-dragging", cut.Find(".w3-drop-zone").GetAttribute("class"));

        cut.Find(".w3-drop-zone").TriggerEvent("ondrop", new DragEventArgs());

        Assert.Equal(1, dropCount);
        Assert.DoesNotContain("w3-drop-zone-dragging", cut.Find(".w3-drop-zone").GetAttribute("class"));
    }

    [Fact]
    public void DropZoneSupportsCompactDisabledAndCustomContent()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = context.Render<W3DropZone>(parameters => parameters
            .Add(p => p.Id, "disabled-drop")
            .Add(p => p.Disabled, true)
            .Add(p => p.Compact, true)
            .Add(p => p.AriaLabel, "Choose import file")
            .Add(p => p.ShowSelectedFiles, false)
            .AddChildContent("<strong>Custom upload</strong>"));

        var zone = cut.Find(".w3-drop-zone");
        var input = cut.Find("input");
        var browseButton = cut.Find(".w3-drop-zone-browse");

        Assert.Equal("true", zone.GetAttribute("aria-disabled"));
        Assert.Contains("w3-disabled", zone.GetAttribute("class"));
        Assert.Contains("w3-drop-zone-disabled", zone.GetAttribute("class"));
        Assert.Contains("w3-drop-zone-compact", zone.GetAttribute("class"));
        Assert.True(input.HasAttribute("disabled"));
        Assert.Equal("Choose import file", input.GetAttribute("aria-label"));
        Assert.Contains("w3-disabled", browseButton.GetAttribute("class"));
        Assert.Contains("Custom upload", cut.Markup);
        Assert.DoesNotContain("Drop files here", cut.Markup);
    }

    [Fact]
    public void DropZoneRaisesSelectedFilesChangedWithMultipleFiles()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        IReadOnlyList<string> selectedFileNames = [];

        var cut = context.Render<W3DropZone>(parameters => parameters
            .Add(p => p.Multiple, true)
            .Add(p => p.SelectedFilesChanged, EventCallback.Factory.Create<IReadOnlyList<IBrowserFile>>(
                this,
                files => selectedFileNames = files.Select(file => file.Name).ToArray())));

        cut.FindComponent<InputFile>().UploadFiles(
            InputFileContent.CreateFromText("first", "first.png"),
            InputFileContent.CreateFromText("second", "second.svg"));

        Assert.Equal(["first.png", "second.svg"], selectedFileNames);
        Assert.Contains("first.png", cut.Markup);
        Assert.Contains("second.svg", cut.Markup);
    }

    [Fact]
    public void DropZoneAccumulatesFilesAcrossSeparateSelectionsByDefault()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        IReadOnlyList<string> selectedFileNames = [];

        var cut = context.Render<W3DropZone>(parameters => parameters
            .Add(p => p.Multiple, true)
            .Add(p => p.SelectedFilesChanged, EventCallback.Factory.Create<IReadOnlyList<IBrowserFile>>(
                this,
                files => selectedFileNames = files.Select(file => file.Name).ToArray())));

        var inputFile = cut.FindComponent<InputFile>();

        inputFile.UploadFiles(InputFileContent.CreateFromText("first", "first.png"));
        inputFile.UploadFiles(InputFileContent.CreateFromText("second", "second.svg"));

        Assert.Equal(["first.png", "second.svg"], selectedFileNames);
        Assert.Contains("first.png", cut.Markup);
        Assert.Contains("second.svg", cut.Markup);
    }

    [Fact]
    public void DropZoneCanReplaceFilesWhenAccumulationIsDisabled()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        IReadOnlyList<string> selectedFileNames = [];

        var cut = context.Render<W3DropZone>(parameters => parameters
            .Add(p => p.Multiple, true)
            .Add(p => p.AccumulateFiles, false)
            .Add(p => p.SelectedFilesChanged, EventCallback.Factory.Create<IReadOnlyList<IBrowserFile>>(
                this,
                files => selectedFileNames = files.Select(file => file.Name).ToArray())));

        var inputFile = cut.FindComponent<InputFile>();

        inputFile.UploadFiles(InputFileContent.CreateFromText("first", "first.png"));
        inputFile.UploadFiles(InputFileContent.CreateFromText("second", "second.svg"));

        Assert.Equal(["second.svg"], selectedFileNames);
        Assert.DoesNotContain("first.png", cut.Markup);
        Assert.Contains("second.svg", cut.Markup);
    }
}
