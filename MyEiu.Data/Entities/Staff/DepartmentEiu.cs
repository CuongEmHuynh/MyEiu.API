using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEiu.Data.Entities.Staff
{
    [Table("department_tbl")]
    public class DepartmentEiu
    {
        public DepartmentEiu()
        {
            Staffs = new HashSet<StaffEiu>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecordID { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public int IsDeleted { get; set; }

        public virtual ICollection<StaffEiu> Staffs { get; set; }

    }
}
