using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("PostType")]
    public class PostType
    {
        [Key]        
        public int Id { get; set; }      
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }
}
