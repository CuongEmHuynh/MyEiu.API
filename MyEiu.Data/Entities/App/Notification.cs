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
    [Table("Notification_App")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Post_App")]
        public int PostId { get; set; }
        [ForeignKey("User_App")]
        public int ReceivedUserId { get; set; }
        public int ReceivedGroupId { get; set; }
        public PostStatus Status { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
