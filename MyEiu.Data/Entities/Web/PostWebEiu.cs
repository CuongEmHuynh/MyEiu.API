using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEiu.Data.Entities.Web
{
    [Table("wp_posts")]
    public class PostWebEiu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("wp_users")]
        public int Post_Author { get; set; }

        public string? Post_Excerpt { get; set; }

        public string? Post_Title { get; set; }
        public string? Post_Name { get; set; }       
        public string? Guid { get; set; }
        public DateTime Post_Date { get; set; }

        public string? Post_Type { get; set; }
        public string? Post_Status { get; set; }
        public string? Ping_Status { get; set; }

        public virtual UserWebEiu? UserWebEiu { get; set; }
        public virtual TranslationWebEiu? TranslationWebEiu { get; set; }
        public virtual ThumbnailWebEiu ThumbnailWebEiu { get; set; }

    }
}
