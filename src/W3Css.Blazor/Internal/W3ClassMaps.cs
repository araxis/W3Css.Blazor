namespace W3Css.Blazor.Internal;

internal static class W3ClassMaps
{
    public static string? ToBackgroundClass(this W3Color color)
    {
        var name = color.ToClassName();
        return name is null ? null : $"w3-{name}";
    }

    public static string? ToTextClass(this W3Color color)
    {
        var name = color.ToTextClassName();
        return name is null ? null : $"w3-text-{name}";
    }

    public static string? ToBorderClass(this W3Color color)
    {
        var name = color.ToBorderClassName();
        return name is null ? null : $"w3-border-{name}";
    }

    public static string? ToHoverBackgroundClass(this W3Color color)
    {
        var name = color.ToClassName();
        return name is null ? null : $"w3-hover-{name}";
    }

    public static string? ToHoverTextClass(this W3Color color)
    {
        var name = color.ToTextClassName();
        return name is null ? null : $"w3-hover-text-{name}";
    }

    public static string? ToHoverBorderClass(this W3Color color)
    {
        var name = color.ToBorderClassName();
        return name is null ? null : $"w3-hover-border-{name}";
    }

    public static string? ToClass(this W3Size size)
    {
        return size switch
        {
            W3Size.Tiny => "w3-tiny",
            W3Size.Small => "w3-small",
            W3Size.Large => "w3-large",
            W3Size.XLarge => "w3-xlarge",
            W3Size.XXLarge => "w3-xxlarge",
            W3Size.XXXLarge => "w3-xxxlarge",
            W3Size.Jumbo => "w3-jumbo",
            _ => null
        };
    }

    public static string? ToClass(this W3Round round)
    {
        return round switch
        {
            W3Round.Small => "w3-round-small",
            W3Round.Medium => "w3-round",
            W3Round.Large => "w3-round-large",
            W3Round.XLarge => "w3-round-xlarge",
            W3Round.XXLarge => "w3-round-xxlarge",
            _ => null
        };
    }


    public static string ToClass(this W3CardDepth depth)
    {
        return depth switch
        {
            W3CardDepth.None => "w3-card",
            W3CardDepth.Two => "w3-card-2",
            W3CardDepth.Four => "w3-card-4",
            _ => "w3-card"
        };
    }

    public static string ToClass(this W3AlertKind kind)
    {
        return kind switch
        {
            W3AlertKind.Success => "w3-success",
            W3AlertKind.Warning => "w3-warning",
            W3AlertKind.Danger => "w3-danger",
            W3AlertKind.Note => "w3-note",
            _ => "w3-info"
        };
    }

    public static string ToClass(this W3ToastPosition position)
    {
        return position switch
        {
            W3ToastPosition.TopLeft => "w3-toast-top-left",
            W3ToastPosition.TopMiddle => "w3-toast-top-middle",
            W3ToastPosition.TopRight => "w3-toast-top-right",
            W3ToastPosition.BottomLeft => "w3-toast-bottom-left",
            W3ToastPosition.BottomMiddle => "w3-toast-bottom-middle",
            _ => "w3-toast-bottom-right"
        };
    }

    public static string? ToClass(this W3ImageEffect effect)
    {
        return effect switch
        {
            W3ImageEffect.Opacity => "w3-opacity",
            W3ImageEffect.OpacityMin => "w3-opacity-min",
            W3ImageEffect.OpacityMax => "w3-opacity-max",
            W3ImageEffect.Grayscale => "w3-grayscale",
            W3ImageEffect.GrayscaleMin => "w3-grayscale-min",
            W3ImageEffect.GrayscaleMax => "w3-grayscale-max",
            W3ImageEffect.Sepia => "w3-sepia",
            W3ImageEffect.SepiaMin => "w3-sepia-min",
            W3ImageEffect.SepiaMax => "w3-sepia-max",
            _ => null
        };
    }

    public static string? ToClass(this W3FabPosition position)
    {
        return position switch
        {
            W3FabPosition.TopLeft => "w3-fab-top-left",
            W3FabPosition.TopRight => "w3-fab-top-right",
            W3FabPosition.BottomLeft => "w3-fab-bottom-left",
            W3FabPosition.BottomRight => "w3-fab-bottom-right",
            _ => null
        };
    }

    public static string ToClass(this W3MenuPlacement placement)
    {
        return placement switch
        {
            W3MenuPlacement.BottomEnd => "w3-menu-bottom-end",
            W3MenuPlacement.TopStart => "w3-menu-top-start",
            W3MenuPlacement.TopEnd => "w3-menu-top-end",
            _ => "w3-menu-bottom-start"
        };
    }

    public static string? ToClass(this W3DisplayPosition position)
    {
        return position switch
        {
            W3DisplayPosition.TopLeft => "w3-display-topleft",
            W3DisplayPosition.TopMiddle => "w3-display-topmiddle",
            W3DisplayPosition.TopRight => "w3-display-topright",
            W3DisplayPosition.Left => "w3-display-left",
            W3DisplayPosition.Middle => "w3-display-middle",
            W3DisplayPosition.Right => "w3-display-right",
            W3DisplayPosition.BottomLeft => "w3-display-bottomleft",
            W3DisplayPosition.BottomMiddle => "w3-display-bottommiddle",
            W3DisplayPosition.BottomRight => "w3-display-bottomright",
            W3DisplayPosition.Custom => "w3-display-position",
            _ => null
        };
    }

    public static string? ToClass(this W3Visibility visibility)
    {
        return visibility switch
        {
            W3Visibility.Hide => "w3-hide",
            W3Visibility.Show => "w3-show",
            W3Visibility.ShowBlock => "w3-show-block",
            W3Visibility.ShowInlineBlock => "w3-show-inline-block",
            W3Visibility.HideSmall => "w3-hide-small",
            W3Visibility.HideMedium => "w3-hide-medium",
            W3Visibility.HideLarge => "w3-hide-large",
            _ => null
        };
    }

    public static string? ToClass(this W3CellAlignment alignment)
    {
        return alignment switch
        {
            W3CellAlignment.Top => "w3-cell-top",
            W3CellAlignment.Middle => "w3-cell-middle",
            W3CellAlignment.Bottom => "w3-cell-bottom",
            _ => null
        };
    }

    public static string? ToClass(this W3Animation animation)
    {
        return animation switch
        {
            W3Animation.Top => "w3-animate-top",
            W3Animation.Bottom => "w3-animate-bottom",
            W3Animation.Left => "w3-animate-left",
            W3Animation.Right => "w3-animate-right",
            W3Animation.Opacity => "w3-animate-opacity",
            W3Animation.Zoom => "w3-animate-zoom",
            W3Animation.Fading => "w3-animate-fading",
            W3Animation.Spin => "w3-spin",
            W3Animation.Input => "w3-animate-input",
            _ => null
        };
    }

    public static string? ToClass(this W3TextDirection direction)
    {
        return direction switch
        {
            W3TextDirection.LeftToRight => "w3-ltr",
            W3TextDirection.RightToLeft => "w3-rtl",
            _ => null
        };
    }

    public static string? ToClass(this W3BorderSide border)
    {
        if (border == W3BorderSide.None)
        {
            return null;
        }

        if (border.HasFlag(W3BorderSide.Remove))
        {
            return "w3-border-0";
        }

        return new W3ClassBuilder()
            .AddIf(border.HasFlag(W3BorderSide.All), "w3-border")
            .AddIf(border.HasFlag(W3BorderSide.Top), "w3-border-top")
            .AddIf(border.HasFlag(W3BorderSide.Right), "w3-border-right")
            .AddIf(border.HasFlag(W3BorderSide.Bottom), "w3-border-bottom")
            .AddIf(border.HasFlag(W3BorderSide.Left), "w3-border-left")
            .ToString();
    }

    public static string? ToClass(this W3BorderBar borderBar)
    {
        if (borderBar == W3BorderBar.None)
        {
            return null;
        }

        return new W3ClassBuilder()
            .AddIf(borderBar.HasFlag(W3BorderBar.Top), "w3-topbar")
            .AddIf(borderBar.HasFlag(W3BorderBar.Right), "w3-rightbar")
            .AddIf(borderBar.HasFlag(W3BorderBar.Bottom), "w3-bottombar")
            .AddIf(borderBar.HasFlag(W3BorderBar.Left), "w3-leftbar")
            .ToString();
    }

    public static string? ToClass(this W3Padding padding)
    {
        return padding switch
        {
            W3Padding.Small => "w3-padding-small",
            W3Padding.Medium => "w3-padding",
            W3Padding.Large => "w3-padding-large",
            W3Padding.Vertical16 => "w3-padding-16",
            W3Padding.Vertical24 => "w3-padding-24",
            W3Padding.Vertical32 => "w3-padding-32",
            W3Padding.Vertical48 => "w3-padding-48",
            W3Padding.Vertical64 => "w3-padding-64",
            _ => null
        };
    }

    public static string? ToClass(this W3TopPadding topPadding)
    {
        return topPadding switch
        {
            W3TopPadding.Top24 => "w3-padding-top-24",
            W3TopPadding.Top32 => "w3-padding-top-32",
            W3TopPadding.Top48 => "w3-padding-top-48",
            W3TopPadding.Top64 => "w3-padding-top-64",
            _ => null
        };
    }

    public static string? ToClass(this W3Margin margin)
    {
        if (margin == W3Margin.None)
        {
            return null;
        }

        return new W3ClassBuilder()
            .AddIf(margin.HasFlag(W3Margin.All), "w3-margin")
            .AddIf(margin.HasFlag(W3Margin.Top), "w3-margin-top")
            .AddIf(margin.HasFlag(W3Margin.Right), "w3-margin-right")
            .AddIf(margin.HasFlag(W3Margin.Bottom), "w3-margin-bottom")
            .AddIf(margin.HasFlag(W3Margin.Left), "w3-margin-left")
            .ToString();
    }

    public static string? ToClass(this W3TextAlignment alignment)
    {
        return alignment switch
        {
            W3TextAlignment.Left => "w3-left-align",
            W3TextAlignment.Right => "w3-right-align",
            W3TextAlignment.Center => "w3-center",
            W3TextAlignment.Justify => "w3-justify",
            _ => null
        };
    }

    public static string? ToClass(this W3TextStyle textStyle)
    {
        if (textStyle == W3TextStyle.None)
        {
            return null;
        }

        return new W3ClassBuilder()
            .AddIf(textStyle.HasFlag(W3TextStyle.Wide), "w3-wide")
            .AddIf(textStyle.HasFlag(W3TextStyle.Bold), "w3-bold")
            .AddIf(textStyle.HasFlag(W3TextStyle.Italic), "w3-italic")
            .ToString();
    }

    public static string? ToClass(this W3FontFamily font)
    {
        return font switch
        {
            W3FontFamily.Serif => "w3-serif",
            W3FontFamily.SansSerif => "w3-sans-serif",
            W3FontFamily.Cursive => "w3-cursive",
            W3FontFamily.Monospace => "w3-monospace",
            _ => null
        };
    }

    public static string? ToClass(this W3EffectStyle effect)
    {
        return effect switch
        {
            W3EffectStyle.Opacity => "w3-opacity",
            W3EffectStyle.OpacityMin => "w3-opacity-min",
            W3EffectStyle.OpacityMax => "w3-opacity-max",
            W3EffectStyle.OpacityOff => "w3-opacity-off",
            W3EffectStyle.Grayscale => "w3-grayscale",
            W3EffectStyle.GrayscaleMin => "w3-grayscale-min",
            W3EffectStyle.GrayscaleMax => "w3-grayscale-max",
            W3EffectStyle.Sepia => "w3-sepia",
            W3EffectStyle.SepiaMin => "w3-sepia-min",
            W3EffectStyle.SepiaMax => "w3-sepia-max",
            _ => null
        };
    }

    public static string? ToClass(this W3HoverEffect effect)
    {
        return effect switch
        {
            W3HoverEffect.Opacity => "w3-hover-opacity",
            W3HoverEffect.OpacityOff => "w3-hover-opacity-off",
            W3HoverEffect.Grayscale => "w3-hover-grayscale",
            W3HoverEffect.Sepia => "w3-hover-sepia",
            W3HoverEffect.Shadow => "w3-hover-shadow",
            W3HoverEffect.NoEffect => "w3-hover-none",
            _ => null
        };
    }

    private static string? ToClassName(this W3Color color)
    {
        return color switch
        {
            W3Color.White => "white",
            W3Color.Black => "black",
            W3Color.Red => "red",
            W3Color.Pink => "pink",
            W3Color.Purple => "purple",
            W3Color.DeepPurple => "deep-purple",
            W3Color.Indigo => "indigo",
            W3Color.Blue => "blue",
            W3Color.LightBlue => "light-blue",
            W3Color.Cyan => "cyan",
            W3Color.Aqua => "aqua",
            W3Color.Teal => "teal",
            W3Color.Green => "green",
            W3Color.LightGreen => "light-green",
            W3Color.Lime => "lime",
            W3Color.Sand => "sand",
            W3Color.Khaki => "khaki",
            W3Color.Yellow => "yellow",
            W3Color.Amber => "amber",
            W3Color.Orange => "orange",
            W3Color.DeepOrange => "deep-orange",
            W3Color.BlueGrey => "blue-grey",
            W3Color.Brown => "brown",
            W3Color.LightGrey => "light-grey",
            W3Color.Grey => "grey",
            W3Color.DarkGrey => "dark-grey",
            W3Color.PaleRed => "pale-red",
            W3Color.PaleYellow => "pale-yellow",
            W3Color.PaleGreen => "pale-green",
            W3Color.PaleBlue => "pale-blue",
            W3Color.Paper => "paper",
            W3Color.Asphalt => "asphalt",
            W3Color.Crimson => "crimson",
            W3Color.Cobalt => "cobalt",
            W3Color.Emerald => "emerald",
            W3Color.Olive => "olive",
            W3Color.Taupe => "taupe",
            W3Color.Sienna => "sienna",
            W3Color.Primary => "primary",
            W3Color.Secondary => "secondary",
            W3Color.Accent => "accent",
            W3Color.Surface => "surface",
            W3Color.Info => "info",
            W3Color.Success => "success",
            W3Color.Warning => "warning",
            W3Color.Danger => "danger",
            W3Color.Note => "note",
            _ => null
        };
    }

    private static string? ToTextClassName(this W3Color color)
    {
        return color switch
        {
            W3Color.PaleRed or
            W3Color.PaleYellow or
            W3Color.PaleGreen or
            W3Color.PaleBlue or
            W3Color.Paper or
            W3Color.Asphalt or
            W3Color.Crimson or
            W3Color.Cobalt or
            W3Color.Emerald or
            W3Color.Olive or
            W3Color.Taupe or
            W3Color.Sienna => null,
            _ => color.ToClassName()
        };
    }

    private static string? ToBorderClassName(this W3Color color)
    {
        return color switch
        {
            W3Color.Paper or
            W3Color.Asphalt or
            W3Color.Crimson or
            W3Color.Cobalt or
            W3Color.Emerald or
            W3Color.Olive or
            W3Color.Taupe or
            W3Color.Sienna => null,
            _ => color.ToClassName()
        };
    }
}
