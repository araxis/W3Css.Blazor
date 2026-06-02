namespace W3Css.Blazor.Internal;

internal static class W3IconCatalog
{
    internal static string GetMarkup(W3IconName iconName)
    {
        return iconName switch
        {
            W3IconName.Add => Line("<path d=\"M12 5v14M5 12h14\"/>"),
            W3IconName.Archive => Line("<path d=\"M4 7h16M6 7v14h12V7M9 11h6M5 3h14v4H5z\"/>"),
            W3IconName.ArrowDown => Line("<path d=\"M12 5v14M6 13l6 6 6-6\"/>"),
            W3IconName.ArrowLeft => Line("<path d=\"M19 12H5M11 6l-6 6 6 6\"/>"),
            W3IconName.ArrowRight => Line("<path d=\"M5 12h14M13 6l6 6-6 6\"/>"),
            W3IconName.ArrowUp => Line("<path d=\"M12 19V5M6 11l6-6 6 6\"/>"),
            W3IconName.Bell => Line("<path d=\"M18 9a6 6 0 1 0-12 0c0 7-3 7-3 9h18c0-2-3-2-3-9\"/><path d=\"M10 21h4\"/>"),
            W3IconName.Bolt => Line("<path d=\"M13 2 4 14h7l-2 8 9-12h-7l2-8z\"/>"),
            W3IconName.Calendar => Line("<path d=\"M7 3v4M17 3v4M4 9h18M5 5h14a2 2 0 0 1 2 2v14H3V7a2 2 0 0 1 2-2z\"/>"),
            W3IconName.Check => Line("<path d=\"m5 13 4 4L19 7\"/>"),
            W3IconName.ChevronDown => Line("<path d=\"m6 9 6 6 6-6\"/>"),
            W3IconName.ChevronLeft => Line("<path d=\"m15 6-6 6 6 6\"/>"),
            W3IconName.ChevronRight => Line("<path d=\"m9 6 6 6-6 6\"/>"),
            W3IconName.ChevronUp => Line("<path d=\"m6 15 6-6 6 6\"/>"),
            W3IconName.Close => Line("<path d=\"M6 6l12 12M18 6 6 18\"/>"),
            W3IconName.Columns => Line("<path d=\"M4 5h16v14H4zM12 5v14\"/>"),
            W3IconName.Copy => Line("<path d=\"M8 8h11v11H8z\"/><path d=\"M5 15H4a1 1 0 0 1-1-1V4a1 1 0 0 1 1-1h10a1 1 0 0 1 1 1v1\"/>"),
            W3IconName.Dashboard => Line("<path d=\"M4 13a8 8 0 0 1 16 0v6H4z\"/><path d=\"m12 13 4-4\"/><path d=\"M12 4v2M6.5 6.5l1.5 1.5M20 13h-2M4 13h2\"/>"),
            W3IconName.Delete => Line("<path d=\"M4 7h16M10 11v6M14 11v6M6 7l1 14h10l1-14M9 7V4h6v3\"/>"),
            W3IconName.Download => Line("<path d=\"M12 3v12M7 10l5 5 5-5M5 20h14\"/>"),
            W3IconName.Edit => Line("<path d=\"M4 20h4l12-12-4-4L4 16v4z\"/><path d=\"m14 6 4 4\"/>"),
            W3IconName.Error => Line("<circle cx=\"12\" cy=\"12\" r=\"9\"/><path d=\"M12 7v6M12 17h.01\"/>"),
            W3IconName.Eye => Line("<path d=\"M2 12s4-7 10-7 10 7 10 7-4 7-10 7S2 12 2 12z\"/><circle cx=\"12\" cy=\"12\" r=\"3\"/>"),
            W3IconName.File => Line("<path d=\"M7 3h8l5 5v16H7z\"/><path d=\"M15 3v6h6\"/>"),
            W3IconName.Filter => Line("<path d=\"M4 5h16l-6 7v6l-4 2v-8z\"/>"),
            W3IconName.Heart => Filled("<path d=\"M12 21s-8-4.5-8-11a5 5 0 0 1 9-3 5 5 0 0 1 9 3c0 6.5-10 11-10 11z\"/>"),
            W3IconName.HeartOutline => Line("<path d=\"M12 21s-8-4.5-8-11a5 5 0 0 1 9-3 5 5 0 0 1 9 3c0 6.5-10 11-10 11z\"/>"),
            W3IconName.Home => Line("<path d=\"M3 11 12 4l9 7\"/><path d=\"M5 10v10h5v-6h4v6h5V10\"/>"),
            W3IconName.Image => Line("<path d=\"M4 5h16v14H4z\"/><circle cx=\"9\" cy=\"10\" r=\"2\"/><path d=\"m4 16 4-4 3 3 3-4 6 6\"/>"),
            W3IconName.Info => Line("<circle cx=\"12\" cy=\"12\" r=\"9\"/><path d=\"M12 11v6M12 7h.01\"/>"),
            W3IconName.Link => Line("<path d=\"M10 13a5 5 0 0 0 7 0l2-2a5 5 0 0 0-7-7l-1 1\"/><path d=\"M14 11a5 5 0 0 0-7 0l-2 2a5 5 0 0 0 7 7l1-1\"/>"),
            W3IconName.List => Line("<path d=\"M8 6h13M8 12h13M8 18h13M3 6h.01M3 12h.01M3 18h.01\"/>"),
            W3IconName.Lock => Line("<path d=\"M6 10h12v11H6z\"/><path d=\"M8 10V7a4 4 0 0 1 8 0v3\"/>"),
            W3IconName.Mail => Line("<path d=\"M4 6h16v12H4z\"/><path d=\"m4 8 8 6 8-6\"/>"),
            W3IconName.Menu => Line("<path d=\"M4 6h16M4 12h16M4 18h16\"/>"),
            W3IconName.Message => Line("<path d=\"M5 5h14v10H8l-3 3z\"/>"),
            W3IconName.Minus => Line("<path d=\"M5 12h14\"/>"),
            W3IconName.Monitor => Line("<path d=\"M4 5h16v12H4z\"/><path d=\"M9 21h6M12 17v4\"/>"),
            W3IconName.Moon => Filled("<path d=\"M21 14.5A8.5 8.5 0 0 1 9.5 3a7 7 0 1 0 11.5 11.5z\"/>"),
            W3IconName.MoreVertical => Filled("<circle cx=\"12\" cy=\"5\" r=\"1.5\"/><circle cx=\"12\" cy=\"12\" r=\"1.5\"/><circle cx=\"12\" cy=\"19\" r=\"1.5\"/>"),
            W3IconName.Play => Filled("<path d=\"M7 4v16l13-8z\"/>"),
            W3IconName.Refresh => Line("<path d=\"M20 7v6h-6\"/><path d=\"M4 17v-6h6\"/><path d=\"M19 13a7 7 0 0 1-12 4\"/><path d=\"M5 11a7 7 0 0 1 12-4\"/>"),
            W3IconName.Redo => Line("<path d=\"M20 7v6h-6\"/><path d=\"M20 13a8 8 0 0 0-13.5-5.5L4 10\"/>"),
            W3IconName.Save => Line("<path d=\"M5 3h14l2 2v16H3V5z\"/><path d=\"M8 3v7h8V3M8 21v-7h8v7\"/>"),
            W3IconName.Search => Line("<circle cx=\"11\" cy=\"11\" r=\"7\"/><path d=\"m16 16 5 5\"/>"),
            W3IconName.Settings => Line("<path d=\"M12 8a4 4 0 1 0 0 8 4 4 0 0 0 0-8z\"/><path d=\"M3 12h3M18 12h3M12 3v3M12 18v3M5.6 5.6l2.1 2.1M16.3 16.3l2.1 2.1M18.4 5.6l-2.1 2.1M7.7 16.3l-2.1 2.1\"/>"),
            W3IconName.Star => Filled("<path d=\"m12 2 3.1 6.3 6.9 1-5 4.9 1.2 6.8L12 17.8 5.8 21 7 14.2 2 9.3l6.9-1z\"/>"),
            W3IconName.StarOutline => Line("<path d=\"m12 2 3.1 6.3 6.9 1-5 4.9 1.2 6.8L12 17.8 5.8 21 7 14.2 2 9.3l6.9-1z\"/>"),
            W3IconName.Success => Line("<circle cx=\"12\" cy=\"12\" r=\"9\"/><path d=\"m8 12 3 3 6-6\"/>"),
            W3IconName.Sun => Line("<circle cx=\"12\" cy=\"12\" r=\"4\"/><path d=\"M12 2v2M12 20v2M4.9 4.9l1.4 1.4M17.7 17.7l1.4 1.4M2 12h2M20 12h2M4.9 19.1l1.4-1.4M17.7 6.3l1.4-1.4\"/>"),
            W3IconName.Table => Line("<path d=\"M4 5h16v16H4zM4 10h16M4 15h16M10 5v16\"/>"),
            W3IconName.Tasks => Line("<path d=\"m4 7 2 2 4-4M12 8h8M4 14h.01M8 14h12M4 20h.01M8 20h12\"/>"),
            W3IconName.Undo => Line("<path d=\"M4 7v6h6\"/><path d=\"M4 13a8 8 0 0 1 13.5-5.5L20 10\"/>"),
            W3IconName.Upload => Line("<path d=\"M12 21V9M7 14l5-5 5 5M5 4h14\"/>"),
            W3IconName.User => Line("<circle cx=\"12\" cy=\"8\" r=\"4\"/><path d=\"M4 21a8 8 0 0 1 16 0\"/>"),
            W3IconName.Users => Line("<circle cx=\"9\" cy=\"8\" r=\"3\"/><circle cx=\"17\" cy=\"10\" r=\"3\"/><path d=\"M3 21a7 7 0 0 1 12 0M13 21a6 6 0 0 1 8 0\"/>"),
            W3IconName.Warning => Line("<path d=\"M12 3 2 21h20z\"/><path d=\"M12 9v5M12 17h.01\"/>"),
            W3IconName.Workflow => Line("<circle cx=\"6\" cy=\"6\" r=\"3\"/><circle cx=\"18\" cy=\"6\" r=\"3\"/><circle cx=\"12\" cy=\"18\" r=\"3\"/><path d=\"M8.5 8.5 11 15M15.5 8.5 13 15M9 6h6\"/>"),
            _ => string.Empty
        };
    }

    private static string Line(string content)
    {
        return content;
    }

    private static string Filled(string content)
    {
        return $"<g fill=\"currentColor\" stroke=\"none\">{content}</g>";
    }
}
