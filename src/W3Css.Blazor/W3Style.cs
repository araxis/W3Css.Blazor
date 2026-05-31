namespace W3Css.Blazor;

/// <summary>
/// W3.CSS color utility values.
/// </summary>
public enum W3Color
{
    /// <summary>No color class.</summary>
    None,
    /// <summary>White.</summary>
    White,
    /// <summary>Black.</summary>
    Black,
    /// <summary>Red.</summary>
    Red,
    /// <summary>Pink.</summary>
    Pink,
    /// <summary>Purple.</summary>
    Purple,
    /// <summary>Deep purple.</summary>
    DeepPurple,
    /// <summary>Indigo.</summary>
    Indigo,
    /// <summary>Blue.</summary>
    Blue,
    /// <summary>Light blue.</summary>
    LightBlue,
    /// <summary>Cyan.</summary>
    Cyan,
    /// <summary>Aqua.</summary>
    Aqua,
    /// <summary>Teal.</summary>
    Teal,
    /// <summary>Green.</summary>
    Green,
    /// <summary>Light green.</summary>
    LightGreen,
    /// <summary>Lime.</summary>
    Lime,
    /// <summary>Sand.</summary>
    Sand,
    /// <summary>Khaki.</summary>
    Khaki,
    /// <summary>Yellow.</summary>
    Yellow,
    /// <summary>Amber.</summary>
    Amber,
    /// <summary>Orange.</summary>
    Orange,
    /// <summary>Deep orange.</summary>
    DeepOrange,
    /// <summary>Blue grey.</summary>
    BlueGrey,
    /// <summary>Brown.</summary>
    Brown,
    /// <summary>Light grey.</summary>
    LightGrey,
    /// <summary>Grey.</summary>
    Grey,
    /// <summary>Dark grey.</summary>
    DarkGrey,
    /// <summary>Pale red.</summary>
    PaleRed,
    /// <summary>Pale yellow.</summary>
    PaleYellow,
    /// <summary>Pale green.</summary>
    PaleGreen,
    /// <summary>Pale blue.</summary>
    PaleBlue,
    /// <summary>Paper.</summary>
    Paper,
    /// <summary>Asphalt.</summary>
    Asphalt,
    /// <summary>Crimson.</summary>
    Crimson,
    /// <summary>Cobalt.</summary>
    Cobalt,
    /// <summary>Emerald.</summary>
    Emerald,
    /// <summary>Olive.</summary>
    Olive,
    /// <summary>Taupe.</summary>
    Taupe,
    /// <summary>Sienna.</summary>
    Sienna,
    /// <summary>Theme primary token (requires a <c>W3ThemeProvider</c>).</summary>
    Primary,
    /// <summary>Theme secondary token (requires a <c>W3ThemeProvider</c>).</summary>
    Secondary,
    /// <summary>Theme accent token (requires a <c>W3ThemeProvider</c>).</summary>
    Accent,
    /// <summary>Theme surface token (requires a <c>W3ThemeProvider</c>).</summary>
    Surface
}

/// <summary>
/// W3.CSS text and control size utility values.
/// </summary>
public enum W3Size
{
    /// <summary>Default size.</summary>
    Default,
    /// <summary>Tiny size.</summary>
    Tiny,
    /// <summary>Small size.</summary>
    Small,
    /// <summary>Large size.</summary>
    Large,
    /// <summary>Extra large size.</summary>
    XLarge,
    /// <summary>Double extra large size.</summary>
    XXLarge,
    /// <summary>Triple extra large size.</summary>
    XXXLarge,
    /// <summary>Jumbo size.</summary>
    Jumbo
}

/// <summary>
/// W3.CSS rounded corner utility values.
/// </summary>
public enum W3Round
{
    /// <summary>No rounded corner class.</summary>
    None,
    /// <summary>Small rounded corners.</summary>
    Small,
    /// <summary>Default rounded corners.</summary>
    Medium,
    /// <summary>Large rounded corners.</summary>
    Large,
    /// <summary>Extra large rounded corners.</summary>
    XLarge,
    /// <summary>Double extra large rounded corners.</summary>
    XXLarge
}

/// <summary>
/// W3.CSS card shadow depth values.
/// </summary>
public enum W3CardDepth
{
    /// <summary>Base card class.</summary>
    None,
    /// <summary>Default card class.</summary>
    Default,
    /// <summary>Two-level shadow.</summary>
    Two,
    /// <summary>Four-level shadow.</summary>
    Four
}

/// <summary>
/// Semantic W3.CSS alert color values.
/// </summary>
public enum W3AlertKind
{
    /// <summary>Information alert.</summary>
    Info,
    /// <summary>Success alert.</summary>
    Success,
    /// <summary>Warning alert.</summary>
    Warning,
    /// <summary>Danger alert.</summary>
    Danger,
    /// <summary>Note alert.</summary>
    Note
}

/// <summary>
/// Fixed placement options for toast providers.
/// </summary>
public enum W3ToastPosition
{
    /// <summary>Top left viewport corner.</summary>
    TopLeft,
    /// <summary>Top center of the viewport.</summary>
    TopMiddle,
    /// <summary>Top right viewport corner.</summary>
    TopRight,
    /// <summary>Bottom left viewport corner.</summary>
    BottomLeft,
    /// <summary>Bottom center of the viewport.</summary>
    BottomMiddle,
    /// <summary>Bottom right viewport corner.</summary>
    BottomRight
}

/// <summary>
/// Relative placement options for popover content.
/// </summary>
public enum W3PopoverPlacement
{
    /// <summary>Content appears below the trigger.</summary>
    Bottom,
    /// <summary>Content appears above the trigger.</summary>
    Top,
    /// <summary>Content appears to the left of the trigger.</summary>
    Left,
    /// <summary>Content appears to the right of the trigger.</summary>
    Right
}

/// <summary>
/// W3.CSS sidebar position values.
/// </summary>
public enum W3SidebarPosition
{
    /// <summary>Left side sidebar.</summary>
    Left,
    /// <summary>Right side sidebar.</summary>
    Right
}

/// <summary>
/// App drawer behavior variants.
/// </summary>
public enum W3DrawerVariant
{
    /// <summary>Temporary drawer that appears above content and can use an overlay.</summary>
    Temporary,
    /// <summary>Persistent drawer intended to sit alongside app content.</summary>
    Persistent
}

/// <summary>
/// App bar placement options.
/// </summary>
public enum W3AppBarPosition
{
    /// <summary>Normal document flow.</summary>
    Static,
    /// <summary>Fixed to the top of the viewport with W3.CSS <c>w3-top</c>.</summary>
    Top,
    /// <summary>Fixed to the bottom of the viewport with W3.CSS <c>w3-bottom</c>.</summary>
    Bottom,
    /// <summary>Sticky within the nearest scroll container.</summary>
    Sticky
}

/// <summary>
/// Floating action button placement options.
/// </summary>
public enum W3FabPosition
{
    /// <summary>Normal document flow.</summary>
    Inline,
    /// <summary>Top left corner of the viewport or positioned parent.</summary>
    TopLeft,
    /// <summary>Top right corner of the viewport or positioned parent.</summary>
    TopRight,
    /// <summary>Bottom left corner of the viewport or positioned parent.</summary>
    BottomLeft,
    /// <summary>Bottom right corner of the viewport or positioned parent.</summary>
    BottomRight
}

/// <summary>
/// Command menu panel placement options.
/// </summary>
public enum W3MenuPlacement
{
    /// <summary>Panel opens below the trigger and aligns to the start edge.</summary>
    BottomStart,
    /// <summary>Panel opens below the trigger and aligns to the end edge.</summary>
    BottomEnd,
    /// <summary>Panel opens above the trigger and aligns to the start edge.</summary>
    TopStart,
    /// <summary>Panel opens above the trigger and aligns to the end edge.</summary>
    TopEnd
}

/// <summary>
/// W3.CSS image effect utility values.
/// </summary>
public enum W3ImageEffect
{
    /// <summary>No image effect class.</summary>
    None,
    /// <summary>Standard opacity.</summary>
    Opacity,
    /// <summary>Minimum opacity effect.</summary>
    OpacityMin,
    /// <summary>Maximum opacity effect.</summary>
    OpacityMax,
    /// <summary>Standard grayscale filter.</summary>
    Grayscale,
    /// <summary>Minimum grayscale filter.</summary>
    GrayscaleMin,
    /// <summary>Maximum grayscale filter.</summary>
    GrayscaleMax,
    /// <summary>Standard sepia filter.</summary>
    Sepia,
    /// <summary>Minimum sepia filter.</summary>
    SepiaMin,
    /// <summary>Maximum sepia filter.</summary>
    SepiaMax
}

/// <summary>
/// W3.CSS display position utility values.
/// </summary>
public enum W3DisplayPosition
{
    /// <summary>No display position class.</summary>
    None,
    /// <summary>Top left position.</summary>
    TopLeft,
    /// <summary>Top middle position.</summary>
    TopMiddle,
    /// <summary>Top right position.</summary>
    TopRight,
    /// <summary>Middle left position.</summary>
    Left,
    /// <summary>Centered middle position.</summary>
    Middle,
    /// <summary>Middle right position.</summary>
    Right,
    /// <summary>Bottom left position.</summary>
    BottomLeft,
    /// <summary>Bottom middle position.</summary>
    BottomMiddle,
    /// <summary>Bottom right position.</summary>
    BottomRight,
    /// <summary>Custom absolute position using inline style.</summary>
    Custom
}

/// <summary>
/// W3.CSS visibility utility values.
/// </summary>
public enum W3Visibility
{
    /// <summary>No visibility class.</summary>
    Default,
    /// <summary>Hide the element.</summary>
    Hide,
    /// <summary>Show the element as a block.</summary>
    Show,
    /// <summary>Show the element as a block.</summary>
    ShowBlock,
    /// <summary>Show the element as inline block.</summary>
    ShowInlineBlock,
    /// <summary>Hide the element on small screens.</summary>
    HideSmall,
    /// <summary>Hide the element on medium screens.</summary>
    HideMedium,
    /// <summary>Hide the element on large screens.</summary>
    HideLarge
}

/// <summary>
/// W3.CSS cell vertical alignment values.
/// </summary>
public enum W3CellAlignment
{
    /// <summary>No cell alignment class.</summary>
    Default,
    /// <summary>Align cell content to the top.</summary>
    Top,
    /// <summary>Align cell content to the middle.</summary>
    Middle,
    /// <summary>Align cell content to the bottom.</summary>
    Bottom
}

/// <summary>
/// W3.CSS animation utility values.
/// </summary>
public enum W3Animation
{
    /// <summary>No animation class.</summary>
    None,
    /// <summary>Animate from the top.</summary>
    Top,
    /// <summary>Animate from the bottom.</summary>
    Bottom,
    /// <summary>Animate from the left.</summary>
    Left,
    /// <summary>Animate from the right.</summary>
    Right,
    /// <summary>Animate opacity.</summary>
    Opacity,
    /// <summary>Animate zoom.</summary>
    Zoom,
    /// <summary>Fade in and out.</summary>
    Fading,
    /// <summary>Spin animation.</summary>
    Spin,
    /// <summary>Animate input width when focused.</summary>
    Input
}

/// <summary>
/// W3.CSS text direction utility values.
/// </summary>
public enum W3TextDirection
{
    /// <summary>No text direction class.</summary>
    None,
    /// <summary>Left-to-right direction class.</summary>
    LeftToRight,
    /// <summary>Right-to-left direction class.</summary>
    RightToLeft
}

/// <summary>
/// W3.CSS thin border side utility values.
/// </summary>
[Flags]
public enum W3BorderSide
{
    /// <summary>No border side class.</summary>
    None = 0,
    /// <summary>All sides border class.</summary>
    All = 1,
    /// <summary>Top border class.</summary>
    Top = 2,
    /// <summary>Right border class.</summary>
    Right = 4,
    /// <summary>Bottom border class.</summary>
    Bottom = 8,
    /// <summary>Left border class.</summary>
    Left = 16,
    /// <summary>Remove border class.</summary>
    Remove = 32
}

/// <summary>
/// W3.CSS thick border bar utility values.
/// </summary>
[Flags]
public enum W3BorderBar
{
    /// <summary>No border bar class.</summary>
    None = 0,
    /// <summary>Top border bar class.</summary>
    Top = 1,
    /// <summary>Right border bar class.</summary>
    Right = 2,
    /// <summary>Bottom border bar class.</summary>
    Bottom = 4,
    /// <summary>Left border bar class.</summary>
    Left = 8
}

/// <summary>
/// W3.CSS padding utility values.
/// </summary>
public enum W3Padding
{
    /// <summary>No padding class.</summary>
    None,
    /// <summary>Small padding class.</summary>
    Small,
    /// <summary>Default padding class.</summary>
    Medium,
    /// <summary>Large padding class.</summary>
    Large,
    /// <summary>16 px vertical padding class.</summary>
    Vertical16,
    /// <summary>24 px vertical padding class.</summary>
    Vertical24,
    /// <summary>32 px vertical padding class.</summary>
    Vertical32,
    /// <summary>48 px vertical padding class.</summary>
    Vertical48,
    /// <summary>64 px vertical padding class.</summary>
    Vertical64
}

/// <summary>
/// W3.CSS top padding utility values.
/// </summary>
public enum W3TopPadding
{
    /// <summary>No top padding class.</summary>
    None,
    /// <summary>24 px top padding class.</summary>
    Top24,
    /// <summary>32 px top padding class.</summary>
    Top32,
    /// <summary>48 px top padding class.</summary>
    Top48,
    /// <summary>64 px top padding class.</summary>
    Top64
}

/// <summary>
/// W3.CSS margin utility values.
/// </summary>
[Flags]
public enum W3Margin
{
    /// <summary>No margin class.</summary>
    None = 0,
    /// <summary>All sides margin class.</summary>
    All = 1,
    /// <summary>Top margin class.</summary>
    Top = 2,
    /// <summary>Right margin class.</summary>
    Right = 4,
    /// <summary>Bottom margin class.</summary>
    Bottom = 8,
    /// <summary>Left margin class.</summary>
    Left = 16
}

/// <summary>
/// W3.CSS text alignment utility values.
/// </summary>
public enum W3TextAlignment
{
    /// <summary>No text alignment class.</summary>
    None,
    /// <summary>Left text alignment class.</summary>
    Left,
    /// <summary>Right text alignment class.</summary>
    Right,
    /// <summary>Center text alignment class.</summary>
    Center,
    /// <summary>Justified text alignment class.</summary>
    Justify
}

/// <summary>
/// W3.CSS text style utility values.
/// </summary>
[Flags]
public enum W3TextStyle
{
    /// <summary>No text style class.</summary>
    None = 0,
    /// <summary>Wide letter spacing class.</summary>
    Wide = 1,
    /// <summary>Bold text class.</summary>
    Bold = 2,
    /// <summary>Italic text class.</summary>
    Italic = 4
}

/// <summary>
/// W3.CSS font family utility values.
/// </summary>
public enum W3FontFamily
{
    /// <summary>No font family class.</summary>
    None,
    /// <summary>Serif font family class.</summary>
    Serif,
    /// <summary>Sans-serif font family class.</summary>
    SansSerif,
    /// <summary>Cursive font family class.</summary>
    Cursive,
    /// <summary>Monospace font family class.</summary>
    Monospace
}

/// <summary>
/// W3.CSS visual effect utility values.
/// </summary>
public enum W3EffectStyle
{
    /// <summary>No effect class.</summary>
    None,
    /// <summary>Standard opacity class.</summary>
    Opacity,
    /// <summary>Minimum opacity effect.</summary>
    OpacityMin,
    /// <summary>Maximum opacity effect.</summary>
    OpacityMax,
    /// <summary>Full opacity class.</summary>
    OpacityOff,
    /// <summary>Standard grayscale filter.</summary>
    Grayscale,
    /// <summary>Minimum grayscale filter.</summary>
    GrayscaleMin,
    /// <summary>Maximum grayscale filter.</summary>
    GrayscaleMax,
    /// <summary>Standard sepia filter.</summary>
    Sepia,
    /// <summary>Minimum sepia filter.</summary>
    SepiaMin,
    /// <summary>Maximum sepia filter.</summary>
    SepiaMax
}

/// <summary>
/// W3.CSS hover effect utility values.
/// </summary>
public enum W3HoverEffect
{
    /// <summary>No hover effect class.</summary>
    None,
    /// <summary>Opacity on hover.</summary>
    Opacity,
    /// <summary>Full opacity on hover.</summary>
    OpacityOff,
    /// <summary>Grayscale filter on hover.</summary>
    Grayscale,
    /// <summary>Sepia filter on hover.</summary>
    Sepia,
    /// <summary>Shadow on hover.</summary>
    Shadow,
    /// <summary>Transparent background and no shadow on hover.</summary>
    NoEffect
}
