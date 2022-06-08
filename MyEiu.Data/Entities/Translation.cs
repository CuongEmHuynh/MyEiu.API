using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities
{
    [Table("wp_icl_translations")]
    public class Translation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Translation_Id { get; set; }
        [ForeignKey("wp_posts")]
        public int Element_Id { get; set; }
        public string? Language_Code { get; set; }

        public virtual Post Post { get; set; }





    }
}
