# Findings

Last updated: 2026-05-31

## W3.CSS

- W3.CSS is documented as a CSS framework for websites and web apps.
- W3.CSS is CSS-only and fits the goal of a lightweight Blazor component library.
- W3.CSS 5.01 is available and documented through the W3.CSS 5 asset URL.
- W3.CSS 5.01 adds utility coverage for grid, flex, text helpers, and semantic colors.
- The bundled W3.CSS stylesheet header is `W3.CSS 5.01 March 11 2026`.
- The original W3.CSS site uses topic navigation, separate topic pages, previous/next links, examples, and compact class/reference sections.
- Local W3.CSS includes content utilities used by content extras: `w3-image`, `w3-code`, `w3-codespan`, `w3-note`, image opacity, grayscale, sepia, hover opacity, border color, leftbar, and rightbar classes.
- The 2026-05-23 gap scan found that the library covers the main component-like W3.CSS topics and has progressively closed utility coverage for borders, spacing, text, fonts, hover colors, animation docs, effects, direction, dark mode, icons, and navigation composition.
- Local W3.CSS display and cell classes now have component coverage through `W3DisplayContainer`, `W3Display`, `W3CellRow`, and `W3Cell`.
- The W3.CSS slideshow pattern now has component coverage through `W3Slideshow` and `W3Slide`, using Blazor-managed state for previous/next controls, indicators, captions, keyboard navigation, and optional timed advance.
- Local W3.CSS border side and border bar classes now have typed component coverage through `W3Border`, `W3BorderSide`, and `W3BorderBar`.
- Local W3.CSS spacing classes now have typed component coverage through `W3Spacing`, `W3Padding`, `W3TopPadding`, and `W3Margin`.
- Local W3.CSS text and font classes now have typed component coverage through `W3Text`, `W3TextAlignment`, `W3TextStyle`, and `W3FontFamily`.
- Local W3.CSS opacity, filter, shadow hover, and hover-none classes now have typed component coverage through `W3Effect`, `W3EffectStyle`, and `W3HoverEffect`.
- Local W3.CSS animation classes now have typed component coverage through `W3Animate`, `W3Animation`, and the `W3Input` animation parameter.
- Local W3.CSS hover background, hover text, and hover border color classes now have typed component coverage through `W3HoverColor`.
- Local W3.CSS direction classes now have typed component coverage through `W3Direction` and `W3TextDirection`.
- W3.CSS icon guidance expects a consuming page to include an external icon library; the library should not bundle an icon font by default.
- Icon library pass-through now has component coverage through `W3Icon`; consumers still provide the icon library stylesheet.
- W3.CSS dark mode guidance is centered on toggling dark color classes such as `w3-black`; the current implementation uses `W3ThemeProvider` + theme tokens plus default component token-aware colors instead of JavaScript class toggling.
- W3.CSS filter examples combine styled inputs with table, list, or dropdown content; in Blazor the filtering belongs in C# state while W3.CSS remains responsible for the input and result styling.
- W3.CSS validation guidance is about checking CSS correctness and vendor extension warnings; W3Css.Blazor adapts the topic by documenting standard Blazor form validation styled with W3.CSS utilities.
- W3.CSS Responsive Classes documents `w3-mobile` as the utility for block, full-width behavior on narrow screens, plus responsive hide classes and small/medium/large grid spans.
- W3.CSS Mobile app guidance shows mobile page composition with containers, cell rows, side navigation, and fixed top or bottom areas; this maps to existing components plus class pass-through rather than a new required runtime layer.
- W3.CSS Display documents position helpers, show/hide helpers, and mobile display helpers in the same utility family; the Blazor docs split broader visibility guidance into its own topic so fixed and responsive utilities are easier to find.
- W3.CSS `w3-top` and `w3-bottom` are fixed-position utilities. In the documentation site they should be demonstrated in a contained preview frame to avoid covering the page shell.
- W3.CSS Round documents `w3-round-small`, `w3-round`, `w3-round-medium`, `w3-round-large`, `w3-round-xlarge`, `w3-round-xxlarge`, and `w3-circle`; the current library maps rounded corners through `W3Round` and image circles through `W3Image.Circle`, with other circle uses handled through class pass-through.
- W3.CSS color guidance spans core color classes, text colors, border colors, optional color libraries, and theme stylesheets. The Blazor mapping uses `W3Color` for bundled colors, `W3AlertKind` for semantic colors, `W3HoverColor` for hover color classes, and `Class` pass-through for optional library and theme classes.
- The bundled W3.CSS 5 stylesheet defines Paper, Asphalt, Crimson, Cobalt, Emerald, Olive, Taupe, and Sienna for background and hover background classes, but not for text, border, hover text, or hover border class families. Text and border maps should only emit classes present in the bundled stylesheet.
- W3.CSS Color Schemes documents palette intent, achromatic schemes, monochromatic schemes, and flat design color examples. In W3Css.Blazor this remains guidance over typed colors, `Class`, `Style`, and app CSS.
- W3.CSS Color Generator creates private theme CSS with `w3-theme`, `w3-theme-light`, `w3-theme-dark`, `w3-theme-l*`, `w3-theme-d*`, `w3-theme-action`, and `w3-text-theme` classes. Private theme CSS should stay in the consuming app and be used through `Class` pass-through.
- W3.CSS Defaults documents the base readable defaults: 15px Verdana on `html, body`, 1.5 line height, heading sizes from 36px down to 16px, font-size utility classes from `w3-tiny` to `w3-jumbo`, and normal CSS override patterns loaded after W3.CSS.
- The documentation site loads `w3-colors-highway.css` and `w3-theme-indigo.css` only so color library and theme examples render; the library package still only bundles the base W3.CSS stylesheet.
- W3.CSS Fonts documents Verdana as the default font, Segoe UI as the default heading font, and four built-in font classes: `w3-serif`, `w3-sans-serif`, `w3-monospace`, and `w3-cursive`.
- W3.CSS Google Fonts guidance shows that external fonts are loaded by adding a font stylesheet in the page head and then using normal CSS or a custom class. This remains consumer-owned CSS in W3Css.Blazor.
- The documentation site loads Google Sofia only so the Fonts page example renders; the library package does not bundle Google fonts or custom font files.
- W3.CSS Trends documents higher-level web design patterns including flat design, almost-flat shadows, material design, cards, minimalism, readable typography, full-screen inputs, mobile first layouts, hero images, and single-page scrolling. In W3Css.Blazor these map to existing components, utility classes, and class pass-through instead of a new required wrapper.
- W3.CSS Case Study builds a complete site from a basic page shell through containers, color and size utilities, article sections, responsive columns, top navigation, and side navigation. In W3Css.Blazor this maps to semantic HTML plus existing layout, content, image, button, bar, and sidebar components.
- W3.CSS Material Design describes material-style pages as paper-like surfaces with shadows, hover effects, color themes, navigation bars, and colorful cards. In W3Css.Blazor this maps to `W3Card`, `W3Effect`, `W3HoverColor`, `W3Bar`, `W3List`, `W3Icon`, `W3Button`, and class pass-through for app-owned theme classes.
- W3.CSS Versions documents release history, current download paths, and the `w3pro.css` option. In W3Css.Blazor this maps to installation and upgrade guidance because the package ships one pinned base stylesheet.
- W3.CSS Navigation Bars documents `w3-bar`, `w3-bar-block`, `w3-bar-item`, `w3-mobile`, dropdown navigation, active links, and fixed navigation. In W3Css.Blazor this maps to `W3Navbar`, `W3NavbarItem`, and optional composition with `W3Dropdown`.
- W3.CSS dropdown examples use page-level click handling for dismissal. In W3Css.Blazor, the reusable dropdown component handles outside-click dismissal with a Blazor-rendered close layer so the behavior is shared by navbar and standalone dropdown examples without JavaScript.
- Current download URLs include:
  - `https://www.w3schools.com/w3css/5/w3.css`
  - `https://www.w3schools.com/w3css/4/w3.css`
  - `https://www.w3schools.com/w3css/4/w3pro.css`

## References

- `https://www.w3schools.com/w3css/default.asp`
- `https://www.w3schools.com/w3css/w3css_downloads.asp`
- `https://www.w3schools.com/w3css/w3css_references.asp`
- `https://www.w3schools.com/w3css/w3css_buttons.asp`
- `https://www.w3schools.com/w3css/w3css_icons.asp`
- `https://www.w3schools.com/w3css/w3css_animate.asp`
- `https://www.w3schools.com/w3css/w3css_effects.asp`
- `https://www.w3schools.com/w3css/w3css_slideshow.asp`
- `https://www.w3schools.com/w3css/w3css_darkmode.asp`
- `https://www.w3schools.com/w3css/w3css_filters.asp`
- `https://www.w3schools.com/w3css/w3css_validation.asp`
- `https://www.w3schools.com/w3css/w3css_display.asp`
- `https://www.w3schools.com/w3css/w3css_responsive.asp`
- `https://www.w3schools.com/w3css/w3css_mobile.asp`
- `https://www.w3schools.com/w3css/w3css_round.asp`
- `https://www.w3schools.com/w3css/w3css_defaults.asp`
- `https://www.w3schools.com/w3css/w3css_fonts.asp`
- `https://www.w3schools.com/W3CSS/w3css_fonts_google.asp`
- `https://www.w3schools.com/w3css/w3css_colors.asp`
- `https://www.w3schools.com/w3css/w3css_color_classes.asp`
- `https://www.w3schools.com/w3css/w3css_color_libraries.asp`
- `https://www.w3schools.com/w3css/w3css_color_themes.asp`
- `https://www.w3schools.com/w3css/w3css_color_material.asp`
- `https://www.w3schools.com/w3css/w3css_color_flat.asp`
- `https://www.w3schools.com/w3css/w3css_color_metro.asp`
- `https://www.w3schools.com/w3css/w3css_color_ios.asp`
- `https://www.w3schools.com/w3css/w3css_color_fashion.asp`
- `https://www.w3schools.com/w3css/w3css_color_schemes.asp`
- `https://www.w3schools.com/w3css/w3css_color_generator.asp`
- `https://www.w3schools.com/w3css/w3css_trends.asp`
- `https://www.w3schools.com/w3css/w3css_case.asp`
- `https://www.w3schools.com/w3css/w3css_material.asp`
- `https://www.w3schools.com/w3css/w3css_versions.asp`
- `https://www.w3schools.com/w3css/w3css_navigation.asp`
- `https://www.w3schools.com/w3css/w3css_bars.asp`
- `https://www.w3schools.com/w3css/w3css_dropdowns.asp`
- `https://www.w3schools.com/w3css/4/w3pro.css`

## Local Environment

- Available stable SDK used by the repository: .NET SDK `10.0.300`.
- .NET 11 preview is installed, but the repository is pinned to .NET 10 for stable project work.
- `rg` is available for fast searching.
- Local command instruction: use normal shell commands directly; do not prefix commands with `rtk`.
