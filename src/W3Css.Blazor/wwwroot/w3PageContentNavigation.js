export function observe(dotNetRef, ids, options = {}) {
    if (!dotNetRef || !Array.isArray(ids) || ids.length === 0) {
        return {
            dispose() {
            }
        };
    }

    const order = ids.slice();
    const visible = new Set();
    let active = null;

    const elements = ids
        .map(id => document.getElementById(id))
        .filter(Boolean);

    const pickActive = () => {
        for (const id of order) {
            if (visible.has(id)) {
                return id;
            }
        }
        return active;
    };

    const observer = new IntersectionObserver(entries => {
        for (const entry of entries) {
            if (entry.isIntersecting) {
                visible.add(entry.target.id);
            } else {
                visible.delete(entry.target.id);
            }
        }

        const next = pickActive();
        if (next && next !== active) {
            active = next;
            dotNetRef.invokeMethodAsync("SetActive", next);
        }
    }, {
        rootMargin: typeof options.rootMargin === "string" ? options.rootMargin : "0px 0px -65% 0px",
        threshold: [0, 1]
    });

    elements.forEach(element => observer.observe(element));

    return {
        dispose() {
            observer.disconnect();
        }
    };
}
