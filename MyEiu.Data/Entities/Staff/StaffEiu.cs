using MyEiu.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEiu.Data.Entities.Staff
{
    [Table("staff_tbl")]
    public class StaffEiu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? StaffID { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public GenderEiu? Gender { get; set; }
        public int? BirthDate { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthYear { get; set; }
        public string? SchoolEmail { get; set; }
        [ForeignKey("Department_tbl")]
        public int? DepartmentID { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public int? IsDeleted { get; set; }
        public string? ImagePath { get; set; }

        public virtual DepartmentEiu DepartmentEiu { get; set; }
    }
}
