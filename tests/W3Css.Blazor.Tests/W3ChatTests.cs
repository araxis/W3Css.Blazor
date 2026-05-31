using Bunit;
using Microsoft.AspNetCore.Components;
using W3Css.Blazor;
using W3Css.Blazor.Components;

namespace W3Css.Blazor.Tests;

public sealed class W3ChatTests
{
    [Fact]
    public void ChatRendersMessageListAndDefaultBubbles()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Chat>(parameters => parameters
            .Add(p => p.Label, "Support thread")
            .Add(p => p.Dense, true)
            .Add(p => p.Gap, 8)
            .Add(p => p.MaxHeight, "24rem")
            .Add(p => p.AvatarSize, 42)
            .Add(p => p.MessageColor, W3Color.PaleBlue)
            .Add(p => p.OwnMessageColor, W3Color.Teal)
            .Add(p => p.OwnMessageTextColor, W3Color.White)
            .Add(p => p.Class, "chat-extra")
            .Add(p => p.Style, "max-width: 42rem;")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3ChatMessage>(0);
                builder.AddAttribute(1, nameof(W3ChatMessage.Author), "Mira");
                builder.AddAttribute(2, nameof(W3ChatMessage.Initials), "MI");
                builder.AddAttribute(3, nameof(W3ChatMessage.Time), "09:12");
                builder.AddAttribute(4, nameof(W3ChatMessage.DateTime), "2026-05-26T09:12:00");
                builder.AddAttribute(5, nameof(W3ChatMessage.Text), "Checking spacing.");
                builder.AddAttribute(6, nameof(W3ChatMessage.AvatarColor), W3Color.Indigo);
                builder.AddAttribute(7, nameof(W3ChatMessage.AvatarTextColor), W3Color.White);
                builder.CloseComponent();

                builder.OpenComponent<W3ChatMessage>(8);
                builder.AddAttribute(9, nameof(W3ChatMessage.Author), "You");
                builder.AddAttribute(10, nameof(W3ChatMessage.Initials), "YO");
                builder.AddAttribute(11, nameof(W3ChatMessage.Time), "09:14");
                builder.AddAttribute(12, nameof(W3ChatMessage.Text), "Keep going.");
                builder.AddAttribute(13, nameof(W3ChatMessage.Own), true);
                builder.AddAttribute(14, nameof(W3ChatMessage.Status), "Seen");
                builder.CloseComponent();
            }));

        var chat = cut.Find("[role='list']");
        var messages = cut.FindAll(".w3-chat-message");
        var firstBubble = messages[0].QuerySelector(".w3-chat-message-bubble")!;
        var ownBubble = messages[1].QuerySelector(".w3-chat-message-bubble")!;

        Assert.Equal("Support thread", chat.GetAttribute("aria-label"));
        Assert.Contains("w3-chat-dense", chat.GetAttribute("class"));
        Assert.Contains("chat-extra", chat.GetAttribute("class"));
        Assert.Equal("--w3-chat-gap:8px;--w3-chat-max-height:24rem;max-width: 42rem", chat.GetAttribute("style"));
        Assert.Equal(2, messages.Count);
        Assert.DoesNotContain("w3-chat-message-own", messages[0].GetAttribute("class"));
        Assert.Contains("w3-chat-message-own", messages[1].GetAttribute("class"));
        Assert.Contains("w3-pale-blue", firstBubble.GetAttribute("class"));
        Assert.Contains("w3-teal", ownBubble.GetAttribute("class"));
        Assert.Contains("w3-text-white", ownBubble.GetAttribute("class"));
        Assert.Equal("2026-05-26T09:12:00", messages[0].QuerySelector("time")!.GetAttribute("datetime"));
        Assert.Contains("Checking spacing.", messages[0].TextContent);
        Assert.Contains("Seen", messages[1].TextContent);
        Assert.Equal(2, cut.FindAll(".w3-avatar").Count);
        Assert.Contains("--w3-avatar-size:42px", cut.Find(".w3-avatar").GetAttribute("style"));
    }

    [Fact]
    public void ChatMessageSupportsCustomContentAndNoAvatar()
    {
        using var context = new BunitContext();
        var cut = context.Render<W3Chat>(parameters => parameters
            .Add(p => p.ShowAvatars, false)
            .Add(p => p.MessageColor, W3Color.LightGrey)
            .Add(p => p.MessageTextColor, W3Color.Black)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<W3ChatMessage>(0);
                builder.AddAttribute(1, nameof(W3ChatMessage.Author), "Build");
                builder.AddAttribute(2, nameof(W3ChatMessage.Compact), true);
                builder.AddAttribute(3, nameof(W3ChatMessage.Border), true);
                builder.AddAttribute(4, nameof(W3ChatMessage.Color), W3Color.White);
                builder.AddAttribute(5, nameof(W3ChatMessage.ChildContent), (RenderFragment)(content =>
                {
                    content.OpenElement(0, "strong");
                    content.AddContent(1, "Done");
                    content.CloseElement();
                    content.AddContent(2, " with tests.");
                }));
                builder.CloseComponent();
            }));

        var message = cut.Find(".w3-chat-message");
        var bubble = cut.Find(".w3-chat-message-bubble");

        Assert.Contains("w3-chat-message-compact", message.GetAttribute("class"));
        Assert.Contains("w3-border", bubble.GetAttribute("class"));
        Assert.Contains("w3-white", bubble.GetAttribute("class"));
        Assert.Contains("Done", bubble.TextContent);
        Assert.Contains("with tests.", bubble.TextContent);
        Assert.Empty(cut.FindAll(".w3-avatar"));
    }
}
