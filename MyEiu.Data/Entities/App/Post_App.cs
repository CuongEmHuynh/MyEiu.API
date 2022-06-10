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
    public class Post_App
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NotificationTypeId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        [ForeignKey("PostFile_App")]
        public int AttachedFileId { get; set; }
        public virtual PostType_App PostType_App { get; set; }
        public virtual  ICollection<PostFile_App>? PostFile_Apps { get; set; }
    }
}
