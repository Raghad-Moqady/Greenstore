//file reader && show selected image before submit

const editCategoryForm = document.forms["editCategoryForm"];
const preview_box = document.querySelector(".preview_box");
const preview = document.querySelector(".preview");

editCategoryForm.file.addEventListener("change", () => {

    const file = editCategoryForm.file.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.addEventListener("load", (e) => {
        preview_box.classList.remove("d-none");
        preview.setAttribute("src", e.target.result);
    });
});