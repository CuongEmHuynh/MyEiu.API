using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("File_App")]
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string DisplayName { get; set; }//user input when uploading file
        public string FileName { get; set; }
        [Required]
        public string Path { get; set; }
        public virtual ICollection<PostFile> PostFiles { get; set; }
    }
}
