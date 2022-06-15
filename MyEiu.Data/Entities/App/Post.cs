using MyEiu.Data.Enum;
using MyEiu.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("Post")]
    public class Post: IUserTracking,IDateTracking
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("PostType")]
        public int PostTypeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
      
        public PostPriority Priority { get; set; }
        public bool Disable { get; set; }

        public virtual PostType PostType { get; set; }    
        public virtual User Author { get; set; }      
        public virtual User Editor { get; set; }
        public virtual  ICollection<PostFile>? PostFiles { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        [ForeignKey("User")]
        public int? CreateBy { get ; set ; }
        [ForeignKey("User")]
        public int? ModifyBy { get ; set ; }
        public DateTime? CreateDate { get ; set ; }
        public DateTime? ModifyDate { get ; set ; }
    }
}
