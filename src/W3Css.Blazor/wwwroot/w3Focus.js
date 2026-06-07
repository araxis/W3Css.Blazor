// Moves keyboard focus to an element by id. Used by roving-tabindex widgets
// (tabs, stepper, tree view) so focus follows the active item after keyboard
// navigation, which Blazor cannot express without an element reference per item.
export function focusId(id) {
    if (!id) {
        return;
    }

    document.getElementById(id)?.focus();
}
