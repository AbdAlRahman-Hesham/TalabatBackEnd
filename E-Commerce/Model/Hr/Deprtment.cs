using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace E_Commerce.Model.Hr
{
    [Table("Departments", Schema = "Hr")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        internal ICollection<Employee> Employees { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }

        internal Employee Manager { get; set; }
    }
}
