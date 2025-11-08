using Ecommerce.Models;

namespace Ecommerce.ViewModels
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Rate { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName{ get; set; }
    }
}
