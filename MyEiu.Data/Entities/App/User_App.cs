using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("User_App")]
    public class User_App
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? Email { get; set; }
        [ForeignKey("UserRole_App")]
        [Required]
        public int RoleId { get; set; }
        public virtual UserRole_App UserRole_App { get; set; }


    }
}
