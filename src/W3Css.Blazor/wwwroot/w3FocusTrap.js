const focusableSelector = [
    "a[href]",
    "area[href]",
    "button:not([disabled])",
    "input:not([disabled]):not([type='hidden'])",
    "select:not([disabled])",
    "textarea:not([disabled])",
    "iframe",
    "object",
    "embed",
    "[contenteditable='true']",
    "[tabindex]:not([tabindex='-1'])"
].join(",");

export function connect(root, options = {}) {
    if (!root) {
        return {
            update() {
            },
            dispose() {
            }
        };
    }

    let active = false;
    let previousActiveElement = null;
    let currentOptions = normalizeOptions(options);

    const getFocusableElements = () => Array
        .from(root.querySelectorAll(focusableSelector))
        .filter(isFocusable);

    const queryRoot = selector => {
        if (!selector) {
            return null;
        }

        try {
            return root.querySelector(selector);
        } catch {
            return null;
        }
    };

    const focusElement = element => {
        if (!element || typeof element.focus !== "function") {
            return false;
        }

        element.focus({ preventScroll: true });
        return document.activeElement === element;
    };

    const focusInitialElement = () => {
        const initialElement = queryRoot(currentOptions.initialFocusSelector);

        if (focusElement(initialElement)) {
            return;
        }

        const autoFocusElement = root.querySelector("[autofocus]");

        if (focusElement(autoFocusElement)) {
            return;
        }

        const focusableElements = getFocusableElements();

        if (focusElement(focusableElements[0])) {
            return;
        }

        focusElement(root);
    };

    const handleKeyDown = event => {
        if (!active || event.key !== "Tab") {
            return;
        }

        const focusableElements = getFocusableElements();

        if (!focusableElements.length) {
            event.preventDefault();
            focusElement(root);
            return;
        }

        const firstElement = focusableElements[0];
        const lastElement = focusableElements[focusableElements.length - 1];
        const currentElement = document.activeElement;

        if (event.shiftKey && (currentElement === firstElement || !root.contains(currentElement))) {
            event.preventDefault();
            focusElement(lastElement);
            return;
        }

        if (!event.shiftKey && currentElement === lastElement) {
            event.preventDefault();
            focusElement(firstElement);
        }
    };

    const handleFocusIn = event => {
        if (!active || root.contains(event.target)) {
            return;
        }

        queueMicrotask(focusInitialElement);
    };

    const enable = () => {
        if (active) {
            return;
        }

        active = true;
        previousActiveElement = root.contains(document.activeElement) ? null : document.activeElement;
        root.addEventListener("keydown", handleKeyDown);
        document.addEventListener("focusin", handleFocusIn, true);

        if (currentOptions.autoFocus) {
            queueMicrotask(focusInitialElement);
        }
    };

    const disable = () => {
        if (!active) {
            return;
        }

        active = false;
        root.removeEventListener("keydown", handleKeyDown);
        document.removeEventListener("focusin", handleFocusIn, true);

        if (currentOptions.restoreFocus &&
            previousActiveElement &&
            typeof previousActiveElement.focus === "function" &&
            document.contains(previousActiveElement)) {
            previousActiveElement.focus({ preventScroll: true });
        }

        previousActiveElement = null;
    };

    const update = nextOptions => {
        currentOptions = normalizeOptions({ ...currentOptions, ...nextOptions });

        if (currentOptions.active) {
            enable();
            return;
        }

        disable();
    };

    update(options);

    return {
        update,
        dispose() {
            disable();
        }
    };
}

function normalizeOptions(options) {
    return {
        active: options?.active !== false,
        autoFocus: options?.autoFocus !== false,
        restoreFocus: options?.restoreFocus !== false,
        initialFocusSelector: typeof options?.initialFocusSelector === "string"
            ? options.initialFocusSelector
            : null
    };
}

function isFocusable(element) {
    if (!element || element.disabled || element.getAttribute("aria-hidden") === "true") {
        return false;
    }

    const style = window.getComputedStyle(element);

    if (style.display === "none" || style.visibility === "hidden") {
        return false;
    }

    return Boolean(element.offsetWidth || element.offsetHeight || element.getClientRects().length);
}
