using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("PostFile_App")]
    public class PostFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Post_App")]
        public int PostId { get; set; }
        [ForeignKey("File_App")]
        public int FileId { get; set; }
        public virtual Post Post { get; set; }
        public virtual File File { get; set; } 
    }
}
