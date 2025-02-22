using E_Commerce.Model.Hr;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Model.Inventory
{
    [Table("Categories", Schema = "Inventory")]
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        internal ICollection<Product> products { get; set; }



    }
}
