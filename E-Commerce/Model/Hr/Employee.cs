using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Model.Hr
{
    [Table("Employees", Schema = "Hr")]
    public class Employee
    {
        [Key]
        public  int Id { get; set; }

        [MaxLength(250)]
        public string FName { get; set; }

        [MaxLength(250)]
        public string LName { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        internal Department Department { get; set; }
    }
}
