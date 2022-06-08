using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities
{
    [Table("staff_tbl")]
    public class StaffEiu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffID { get; set; }
        public string FullName { get; set; }
    }
}
