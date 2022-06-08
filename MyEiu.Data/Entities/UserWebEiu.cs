using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities
{
    [Table("wp_users")]
    public class UserWebEiu
    {
        public UserWebEiu()
        {
            this.Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public string? display_name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }    

    }
}
