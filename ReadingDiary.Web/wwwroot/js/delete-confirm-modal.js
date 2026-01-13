
document.addEventListener("DOMContentLoaded", () => {

    const form = document.getElementById("deleteConfirmForm");
    if (!form) return;

    const idInput = document.getElementById("deleteItemId");
    const bookIdInput = document.getElementById("deleteBookId");
    const textEl = document.getElementById("deleteConfirmText");

    document.querySelectorAll("[data-delete-modal]").forEach(btn => {
        btn.addEventListener("click", () => {

            form.action = `/${btn.dataset.controller}/${btn.dataset.action}`;

            idInput.value = btn.dataset.id;

            if (bookIdInput && btn.dataset.bookId) {
                bookIdInput.value = btn.dataset.bookId;
            }

            if (btn.dataset.text) {
                textEl.textContent = btn.dataset.text;
            }
        });
    });
});
