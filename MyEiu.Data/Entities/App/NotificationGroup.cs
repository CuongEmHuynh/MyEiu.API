using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    public class NotificationGroup
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public virtual Notification Notification { get; set; }
        public virtual Group Group { get; set; }
    }
}
