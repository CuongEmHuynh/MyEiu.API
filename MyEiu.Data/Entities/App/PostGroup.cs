using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("PostGroup")]
    public class PostGroup
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;
     
        public int GroupId { get; set; }
       
        public string? GroupName { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public virtual Post? Post { get; set; }
    }
}
