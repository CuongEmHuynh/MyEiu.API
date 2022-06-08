using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities
{
    [Table("wp_yoast_indexable")]
    public class ThumbnailWebEiu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public int ID { get; set; }
        [Column(Order = 2)]
        //[ForeignKey("wp_posts")]
        public int Post_Parent { get; set; }
        public string Twitter_Image { get; set; }
        //public virtual Post Post { get; set; }  
    }
}
