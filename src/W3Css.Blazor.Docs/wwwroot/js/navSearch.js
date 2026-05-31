// Global "/" shortcut: focus the docs sidebar search from anywhere on the page.
// Registered by NavMenu via JS interop; the handler is removed on dispose.
let inputElement = null;
let keydownHandler = null;

function isTypingTarget(el) {
    if (!el) {
        return false;
    }

    const tag = el.tagName;
    return tag === "INPUT" || tag === "TEXTAREA" || tag === "SELECT" || el.isContentEditable === true;
}

export function register(element) {
    // Replace any prior registration (e.g. after a hot reload) so we never stack handlers.
    dispose();
    inputElement = element;

    keydownHandler = (event) => {
        if (event.key !== "/" || event.ctrlKey || event.metaKey || event.altKey) {
            return;
        }

        // Don't hijack "/" while the user is typing in a field.
        if (isTypingTarget(event.target) || isTypingTarget(document.activeElement)) {
            return;
        }

        if (inputElement) {
            event.preventDefault();
            inputElement.focus();
            inputElement.select?.();
        }
    };

    document.addEventListener("keydown", keydownHandler);
}

export function dispose() {
    if (keydownHandler) {
        document.removeEventListener("keydown", keydownHandler);
    }

    keydownHandler = null;
    inputElement = null;
}
