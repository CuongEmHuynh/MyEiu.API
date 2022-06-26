using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("PostUser")]
    public class PostUser
    {
        //chứa all user bao gồm cả trong group để xác định ???
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;      
        public string? Email { get; set; }

        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        [ForeignKey("Post")]
        public int? PostId { get; set; }       
        public virtual Post? Post { get; set; }
    }
}
