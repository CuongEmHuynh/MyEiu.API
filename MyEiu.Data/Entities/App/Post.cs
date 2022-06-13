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
    [Table("Post_App")]
    public class Post
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("PostType_App")]
        public int PostTypeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AuthorId { get; set; }
        public DateTime EditedDate { get; set; }
        public int EditorId { get; set; }
        public PostPriority Priority { get; set; }
        public bool Disable { get; set; }

        public virtual PostType PostType { get; set; }    
        public virtual User Author { get; set; }      
        public virtual User Editor { get; set; }
        public virtual  ICollection<PostFile>? PostFiles { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
