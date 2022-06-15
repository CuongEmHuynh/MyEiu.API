using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    public class NotificationUser
    {
        //chứa all user bao gồm cả trong group để xác định ???
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;      
        public string? Email { get; set; }
        public string? DepartmentCode { get; set; }
        public string? DepartmentName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        [ForeignKey("Notification")]
        public int NotificationId { get; set; }       
        public virtual Notification Notification { get; set; }
    }
}
