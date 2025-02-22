using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Model.Inventory
{
    [Table("Products", Schema = "Inventory")]
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? UrlImg { get; set; } =
            "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png";

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        [ForeignKey("Category")]
        public int categoryId { get; set; }

        internal Category Category { get; set; }



    }
}
