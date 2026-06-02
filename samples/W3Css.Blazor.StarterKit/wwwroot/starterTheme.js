let mediaQuery;
let dotNetReference;

export function initializeThemePreference(reference) {
    dotNetReference = reference;
    mediaQuery = window.matchMedia("(prefers-color-scheme: dark)");
    mediaQuery.addEventListener("change", handlePreferenceChange);
    return mediaQuery.matches;
}

export function disposeThemePreference() {
    if (mediaQuery) {
        mediaQuery.removeEventListener("change", handlePreferenceChange);
    }

    mediaQuery = undefined;
    dotNetReference = undefined;
}

function handlePreferenceChange(event) {
    if (dotNetReference) {
        dotNetReference.invokeMethodAsync("SetSystemPrefersDark", event.matches);
    }
}
