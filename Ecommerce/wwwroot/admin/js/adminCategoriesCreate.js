//file reader && show selected image before submit

const createCategoryForm = document.forms["createCategoryForm"];
const preview_box = document.querySelector(".preview_box");
const preview = document.querySelector(".preview");

createCategoryForm.file.addEventListener("change", () => {
     
    const file = createCategoryForm.file.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.addEventListener("load", (e) => {
        preview_box.classList.remove("d-none");
        preview.setAttribute("src", e.target.result);
    });
});