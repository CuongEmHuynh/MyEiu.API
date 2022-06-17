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
        public int object_id { get; set; }
        public string object_type { get; set; }
        public string object_sub_type { get; set; }
        public string post_status { get; set; }
        public int post_parent { get; set; }
        public string open_graph_image { get; set; }
        public string Twitter_Image { get; set; }
        public virtual PostWebEiu PostWebEiu { get; set; }

    }
}
