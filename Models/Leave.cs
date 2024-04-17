using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspOne.Models
{
    public class Leave
    {
        public enum LeaveStatus
        {
            pending,
            Approved,
            Denied
        }
        [Key]
        public int LeaveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.pending;
        [ForeignKey("Employee")]
        public int FkEmployee { get; set; }
        public Employee? Employee { get; set; }
    }
}
