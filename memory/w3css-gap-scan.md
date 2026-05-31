# W3.CSS Gap Scan

Last updated: 2026-05-31

Source scan:

- `https://www.w3schools.com/w3css/default.asp`
- `https://www.w3schools.com/w3css/w3css_references.asp`
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

## Scan Summary

The original W3.CSS sidebar has two kinds of topics:

- Component-like topics that map well to Blazor components.
- Utility/reference topics that should mostly map to typed utility enums, component parameters, docs pages, or pass-through `Class` examples.

The library now covers the main component-like basics, including slideshow, direction helpers, icon guidance, dark mode guidance, filter guidance, validation guidance, mobile guidance, visibility guidance, round guidance, font guidance, color guidance, trends guidance, case study guidance, material design guidance, versions guidance, and optional navbar composition.

## Covered Topics

- Containers
- Panels
- Cards
- Buttons
- Notes
- Quotes
- Alerts
- Tables
- Lists
- Images
- Slideshows
- Inputs
- Badges
- Tags
- Icons
- Grid
- Flexbox
- Cells
- Rows
- Display
- Borders
- Spacing
- Text and Fonts
- Effects
- Direction
- Responsive
- Bars
- Dropdowns
- Accordions
- Sidebar
- Tabs
- Pagination
- Progress Bars
- Modal
- Tooltips
- Code
- Dark Mode
- Filters
- Validation
- Mobile
- Visibility
- Round
- Fonts
- Colors
- Color schemes and generator
- Trends
- Case Study
- Material Design
- Versions
- Navigation Bars

## Partial Coverage

- Borders: border side, border bar, and color utilities are modeled through `W3Border`; broader color library docs still remain.
- Spacing: padding, top padding, margin side, and section utilities are modeled through `W3Spacing`; docs still use raw classes where utility pass-through is simpler.
- Text and fonts: alignment, wide text, bold, italic, and font-family utilities are modeled through `W3Text`; broader default and custom font guidance is documented on the Fonts page.
- Effects: opacity, grayscale, sepia, hover opacity, hover filters, hover shadow, and hover none utilities are modeled through `W3Effect`.
- Direction: left-to-right and right-to-left utilities are modeled through `W3Direction` and `W3TextDirection`.
- Icons: consumer-provided icon classes are modeled through `W3Icon`, and docs explain how to include the selected icon library.
- Display: display containers, positioned content, visibility helpers, and fixed top/bottom guidance are covered.
- Animations: top, bottom, left, right, opacity, zoom, fading, spin, and input animation classes are modeled through `W3Animate`, `W3Animation`, and `W3Input`.
- Images: image-specific effects are covered for `W3Image`.
- Navigation: low-level bars and tab/sidebar/dropdown pieces exist, optional navbar composition is implemented for active navigation and inherited mobile behavior, and dropdown outside-click dismissal is handled by the shared dropdown component.
- Validation: Blazor form validation is supported and has W3.CSS-style guidance; broader CSS validator notes are documented on the guidance page.
- Flex Items: `W3Flex` exists, but item-level growth/shrink/order utilities are not represented as a typed surface.

## Missing Component Candidates

- None currently identified from the W3.CSS topic scan.

Closed in `feature/display-utility-components`:

- `W3CellRow` and `W3Cell`
- `W3DisplayContainer` and `W3Display`

Closed in `feature/slideshow-component`:

- `W3Slideshow` and `W3Slide`

Closed in `feature/border-utilities`:

- `W3Border`
- `W3BorderSide` and `W3BorderBar`

Closed in `feature/spacing-utilities`:

- `W3Spacing`
- `W3Padding`, `W3TopPadding`, and `W3Margin`

Closed in `feature/text-font-utilities`:

- `W3Text`
- `W3TextAlignment`, `W3TextStyle`, and `W3FontFamily`

Closed in `feature/effect-hover-utilities`:

- `W3Effect`
- `W3EffectStyle` and `W3HoverEffect`

Closed in `feature/animation-utilities`:

- `W3Animate`
- `W3Animation.Input`
- `W3Input` animation support

Closed in `feature/hover-color-utilities`:

- `W3HoverColor`
- `W3Color` hover background, text, and border class maps

Closed in `feature/direction-utilities`:

- `W3Direction`
- `W3TextDirection`

Closed in `feature/icon-guidance`:

- `W3Icon`
- W3.CSS-style Icons documentation page

Closed in `feature/dark-mode-filter-guidance`:

- Dark Mode guidance page using Blazor state, typed color parameters, and class pass-through.
- Filters guidance page using `W3Input`, `W3Table`, `W3List`, and C# filtering state.
- `W3Input.UpdateOnInput` for live filter input updates without script.

Closed in `feature/validation-guidance`:

- Validation guidance page using `EditForm`, `DataAnnotationsValidator`, `W3Field`, `W3Input`, `W3Select`, `W3TextArea`, `ValidationSummary`, and submit feedback.
- CSS validation notes linking the original W3.CSS validation topic to standard Blazor and W3.CSS markup.

Closed in `feature/mobile-guidance`:

- Mobile guidance page documenting `w3-mobile`, typed `Mobile` parameters on `W3BarItem`, `W3Dropdown`, and `W3Cell`, class pass-through, responsive wrappers, and responsive column spans.
- Source links to W3.CSS Responsive Classes and W3.CSS Mobile app guidance.

Closed in `feature/visibility-guidance`:

- Visibility guidance page documenting `W3Visibility`, responsive hide classes, `w3-top`, `w3-bottom`, `w3-display-hover`, and class pass-through.
- Source links to W3.CSS Display and W3.CSS Responsive Classes.

Closed in `feature/round-guidance`:

- Round guidance page documenting `W3Round`, `Round` parameters, `W3Image.Circle`, `w3-circle`, raw `w3-round-*` class pass-through, and the W3.CSS Round source link.

Closed in `feature/color-guidance`:

- Colors guidance page documenting `W3Color`, text colors, border colors, hover colors, semantic alert colors, optional color libraries, optional themes, and related W3.CSS source links.
- Docs-only optional color library and theme stylesheets so live examples render while the library package remains lightweight.

Closed in `feature/defaults-guidance`:

- Defaults guidance page documenting W3.CSS base typography, heading sizes, font-size classes, plain HTML usage, and local override patterns.
- Pager chain now runs Components -> Defaults -> Colors.

Closed in `feature/font-guidance`:

- Fonts guidance page documenting W3.CSS default fonts, built-in font helper classes, Google font loading, plain class pass-through, and `W3Text.Font`.
- Pager chain now runs Components -> Defaults -> Fonts -> Colors.

Closed in `feature/color-schemes-guidance`:

- Color Schemes guidance page documenting W3.CSS palette swatches, achromatic schemes, theme class usage, private generator CSS, and related source links.
- Pager chain now runs Components -> Defaults -> Fonts -> Colors -> Color Schemes -> Trends -> Container.

Closed in `feature/trends-guidance`:

- Trends guidance page documenting W3.CSS flat color blocks, almost-flat cards, material-style surfaces, full-screen input composition, mobile-first hero flow, and single-page patterns.
- Pager chain now runs Components -> Defaults -> Fonts -> Colors -> Color Schemes -> Trends -> Container.

Closed in `feature/case-examples-guidance`:

- Case Study guidance page documenting W3.CSS-style site shells, color and size classes, responsive columns, top navigation, side navigation, and single-page flow.
- Pager chain now runs Components -> Defaults -> Fonts -> Colors -> Color Schemes -> Trends -> Case Study -> Container.

Closed in `feature/material-design-guidance`:

- Material Design guidance page documenting the original W3.CSS topic shape: material basics, download panels, color-theme travel cards, separated navigation bars, and colorful image cards.
- Pager chain now runs Components -> Defaults -> Fonts -> Colors -> Color Schemes -> Trends -> Case Study -> Material Design -> Container.

Closed in `feature/versions-guidance`:

- Versions guidance page documenting the original W3.CSS versions topic shape: version timeline, packaged stylesheet path, W3.CSS 4 and Pro stylesheet options, color files, and upgrade checks.
- Pager chain now runs Components -> Defaults -> Fonts -> Colors -> Color Schemes -> Trends -> Case Study -> Material Design -> Versions -> Container.

Closed in `feature/navbar-composition`:

- `W3Navbar` and `W3NavbarItem` composition over W3.CSS navigation bar patterns.
- Navbar guidance page documenting basic, responsive, dropdown, and fixed-wrapper examples.
- Shared `W3Dropdown` outside-click dismissal through a Blazor close layer.
- Pager chain now runs Tabs -> Accordion -> Navbar -> Dropdown -> Pagination.

## Missing Utility Coverage

- Hover utilities:
  - Hover background, text, and border color helpers are covered through `W3HoverColor`.
  - Hover shadow, opacity, grayscale, sepia, and none are covered through `W3HoverEffect`.

## Lower-Priority Documentation Topics

- Template/gallery references

## Closed in Docs Polish

- Current state: no missing W3.CSS topic-level components are currently open from the main scan.
- Docs smoke coverage for major route/interaction paths is already in place.
