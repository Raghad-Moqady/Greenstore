using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Name { get; set; }

        [ValidateNever]
        public string Image { get; set; }

        public List<Product> Products { get; set; }= new List<Product>(); 
    }
}
