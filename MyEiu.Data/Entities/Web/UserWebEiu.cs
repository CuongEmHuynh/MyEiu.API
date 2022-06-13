using System.ComponentModel.DataAnnotations.Schema;

namespace MyEiu.Data.Entities.Web
{
    [Table("wp_users")]
    public class UserWebEiu
    {
        public UserWebEiu()
        {
            Posts = new HashSet<PostWebEiu>();
        }
        public int Id { get; set; }
        public string? display_name { get; set; }
        public virtual ICollection<PostWebEiu> Posts { get; set; }

    }
}
