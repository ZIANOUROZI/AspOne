using System.ComponentModel.DataAnnotations;

namespace AspOne.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DateOfBirth { get; set; }
    }
}
