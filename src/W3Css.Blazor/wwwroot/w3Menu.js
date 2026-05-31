export function connect(root, trigger, panel, options = {}) {
    if (!root || !trigger || !panel) {
        return {
            update() {
            },
            dispose() {
            }
        };
    }

    let currentOptions = normalizeOptions(options);
    let frame = 0;

    const schedulePosition = () => {
        if (!currentOptions.open || !document.contains(root)) {
            clearPosition();
            return;
        }

        if (frame) {
            cancelAnimationFrame(frame);
        }

        frame = requestAnimationFrame(() => {
            frame = 0;
            positionPanel();
        });
    };

    const positionPanel = () => {
        if (!currentOptions.open || !document.contains(root)) {
            clearPosition();
            return;
        }

        const triggerRect = trigger.getBoundingClientRect();
        const panelRect = panel.getBoundingClientRect();
        const panelWidth = panelRect.width || panel.offsetWidth || 192;
        const panelHeight = panelRect.height || panel.offsetHeight || 0;
        const viewportWidth = document.documentElement.clientWidth || window.innerWidth;
        const viewportHeight = document.documentElement.clientHeight || window.innerHeight;
        const padding = 8;
        const offset = 4;
        const placement = currentOptions.placement.toLowerCase();
        const alignEnd = placement.includes("end");
        const preferTop = placement.includes("top");

        let left = alignEnd ? triggerRect.right - panelWidth : triggerRect.left;
        let top = preferTop ? triggerRect.top - panelHeight - offset : triggerRect.bottom + offset;

        if (!preferTop && top + panelHeight > viewportHeight - padding && triggerRect.top - panelHeight - offset >= padding) {
            top = triggerRect.top - panelHeight - offset;
        }

        if (preferTop && top < padding && triggerRect.bottom + panelHeight + offset <= viewportHeight - padding) {
            top = triggerRect.bottom + offset;
        }

        left = clamp(left, padding, Math.max(padding, viewportWidth - panelWidth - padding));
        top = clamp(top, padding, Math.max(padding, viewportHeight - panelHeight - padding));

        panel.style.left = `${Math.round(left)}px`;
        panel.style.top = `${Math.round(top)}px`;
        panel.style.maxHeight = `calc(100vh - ${padding * 2}px)`;
    };

    const clearPosition = () => {
        if (frame) {
            cancelAnimationFrame(frame);
            frame = 0;
        }

        panel.style.removeProperty("left");
        panel.style.removeProperty("top");
        panel.style.removeProperty("max-height");
    };

    const update = nextOptions => {
        currentOptions = normalizeOptions({ ...currentOptions, ...nextOptions });

        if (currentOptions.open) {
            schedulePosition();
            return;
        }

        clearPosition();
    };

    window.addEventListener("resize", schedulePosition);
    window.addEventListener("scroll", schedulePosition, true);

    update(options);

    return {
        update,
        dispose() {
            window.removeEventListener("resize", schedulePosition);
            window.removeEventListener("scroll", schedulePosition, true);
            clearPosition();
        }
    };
}

function normalizeOptions(options) {
    return {
        open: options?.open === true,
        placement: typeof options?.placement === "string"
            ? options.placement
            : "BottomStart"
    };
}

function clamp(value, min, max) {
    return Math.min(Math.max(value, min), max);
}
