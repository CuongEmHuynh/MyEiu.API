using MyEiu.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
       
        public PostStatus Status { get; set; }      
        
        public virtual ICollection<NotificationGroup> NotificationGroups { get; set; }
        public virtual ICollection<NotificationUser> NotificationUsers { get; set; }    
    }
}
