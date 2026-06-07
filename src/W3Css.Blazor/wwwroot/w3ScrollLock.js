// Reference-counted body scroll lock shared by every overlay on the page, so
// stacked modals/drawers restore the original scroll state only when the last
// one closes. Compensates for the scrollbar width to avoid a layout shift.
let lockCount = 0;
let previousOverflow = "";
let previousPaddingRight = "";

export function lock() {
    const body = document.body;

    if (lockCount === 0) {
        previousOverflow = body.style.overflow;
        previousPaddingRight = body.style.paddingRight;

        const scrollbarWidth = window.innerWidth - document.documentElement.clientWidth;

        if (scrollbarWidth > 0) {
            const currentPadding = parseFloat(window.getComputedStyle(body).paddingRight) || 0;
            body.style.paddingRight = `${currentPadding + scrollbarWidth}px`;
        }

        body.style.overflow = "hidden";
    }

    lockCount += 1;
}

export function unlock() {
    if (lockCount === 0) {
        return;
    }

    lockCount -= 1;

    if (lockCount === 0) {
        const body = document.body;
        body.style.overflow = previousOverflow;
        body.style.paddingRight = previousPaddingRight;
        previousOverflow = "";
        previousPaddingRight = "";
    }
}
