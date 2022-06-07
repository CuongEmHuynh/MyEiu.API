using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities
{
    [Table("wp_posts")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Post_Excerpt { get; set; }

        public string? Post_Title { get; set; }
        public string? Guid { get; set; }
        //public string Thumbnail_Url { get; set; }
        public DateTime Post_Date { get; set; }
        public int Post_Author { get; set; }

    }
}
