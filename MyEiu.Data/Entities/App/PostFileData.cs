using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("PostFileData")]
    public class PostFileData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        [ForeignKey("FileData")]
        public int? FileDataId { get; set; }
        public virtual Post? Post { get; set; }
        public virtual FileData? FileData { get; set; } 
    }
}
