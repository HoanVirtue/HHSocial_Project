using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class tblDisable
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public bool Disable { get; set; }
        public DateTime LogTime { get; set; }
        public virtual User? User { get; set; }
    }
}