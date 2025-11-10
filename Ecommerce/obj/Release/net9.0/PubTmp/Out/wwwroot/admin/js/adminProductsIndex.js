const detailsModal = document.querySelector(".detailsModal");

const showDetails = (productVm) => {
    console.log(productVm);
    detailsModal.classList.add("show_modal");
    document.querySelector(".product_id").textContent = productVm.Id;
    document.querySelector(".product_name").textContent = productVm.Name;
    document.querySelector(".product_desc").textContent = productVm.Description;
    document.querySelector(".product_price").textContent = "$ " + productVm.Price;
    document.querySelector(".product_rate").textContent = productVm.Rate;
    document.querySelector(".product_quantity").textContent = productVm.Quantity;
    document.querySelector(".category_name").textContent = productVm.CategoryName;
    document.querySelector(".product_img").setAttribute("src", productVm.ImageUrl);

}


document.querySelector(".close_icon").addEventListener("click", () => {
    detailsModal.classList.remove("show_modal");
})  
