using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEiu.Data.Entities.Web
{
    [Table("wp_yoast_indexable")]
    public class ThumbnailWebEiu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("wp_posts")]
        public int post_parent { get; set; }
        public string Twitter_Image { get; set; }
        public virtual Post Post { get; set; }

    }
}
