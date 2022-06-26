using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("FileData")]
    public class FileData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        public string? DisplayName { get; set; }//user input when uploading file
        public string? FileName { get; set; }
        [Required]
        public string? Path { get; set; }
        public virtual PostFileData? PostFileData { get; set; }
    }
}
