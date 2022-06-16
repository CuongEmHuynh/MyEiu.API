using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEiu.Data.Entities.Web
{
    [Table("wp_icl_translations")]
    public class TranslationWebEiu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Translation_Id { get; set; }
        [ForeignKey("wp_posts")]
        public int Element_Id { get; set; }
        public string? Language_Code { get; set; }

        public virtual PostWebEiu PostWebEiu { get; set; }





    }
}
