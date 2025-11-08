using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Product name is required! ")]
        [MinLength(3,ErrorMessage ="Product name must be at least 3 char long")]
        [MaxLength(50,ErrorMessage = "The product name cannot exceed 50 characters.")]
        public string Name { get; set; }




        [Required(ErrorMessage = "Product Description is required! ")]
        [MinLength(5, ErrorMessage = "Product Description must be at least 5 char long")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Product price is required! ")]
        [Range(0.01,10000,ErrorMessage ="price must be between 0.01 and 10000")]
        public decimal Price { get; set; }


        [Range(1,5)]
        public int Rate {  get; set; }


        [Required(ErrorMessage = "Product Quantity is required! ")]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set;}

        [ValidateNever]
        public string Image {  get; set; }

        public int CategoryId {  get; set; }
        [ValidateNever]
        public Category Category { get; set; }

    }
}
