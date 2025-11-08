using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Name { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
 
    }
}
