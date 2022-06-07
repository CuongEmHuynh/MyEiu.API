using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities
{
    [Table("wp_yoast_indexable")]
    public class ThumbnailWebEiu
    {
        public int Id { get; set; }
        public string Twitter_Image { get; set; }
    }
}
