function confirmDelete(uniqueId, isDeleteButtonClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteButtonClicked) {
        $("#" + deleteSpan).hide();
        $("#" + confirmDeleteSpan).show();
    }
    else {
        $("#" + deleteSpan).show();
        $("#" + confirmDeleteSpan).hide();
    }
}