export function connect(dotNetRef, options = {}) {
    if (!dotNetRef) {
        return {
            update() {
            },
            dispose() {
            }
        };
    }

    let threshold = normalizeThreshold(options);
    let visible = null;

    const evaluate = () => {
        const position = window.scrollY || document.documentElement.scrollTop || 0;
        const next = position > threshold;

        if (next !== visible) {
            visible = next;
            dotNetRef.invokeMethodAsync("SetVisible", next);
        }
    };

    const onScroll = () => evaluate();

    window.addEventListener("scroll", onScroll, { passive: true });
    evaluate();

    return {
        update(nextOptions) {
            threshold = normalizeThreshold(nextOptions);
            evaluate();
        },
        dispose() {
            window.removeEventListener("scroll", onScroll);
        }
    };
}

export function scrollToTop(offset = 0, smooth = true) {
    const top = Number.isFinite(Number(offset)) ? Number(offset) : 0;
    window.scrollTo({ top, behavior: smooth ? "smooth" : "auto" });
}

function normalizeThreshold(options) {
    const value = Number(options?.visibleHeight);
    return Number.isFinite(value) && value >= 0 ? value : 300;
}
