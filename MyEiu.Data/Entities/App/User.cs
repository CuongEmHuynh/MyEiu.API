using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Username { get; set; }
        
        public string? Password { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public DateTime Birthday { get; set; }
        [Required]
        public string? Email { get; set; }
        [ForeignKey("UserRole")]
        [Required]
        public int RoleId { get; set; }
        //[ForeignKey("Group")]
        //public int? GroupID { get; set; }
        public string? Phone { get; set; }
        public int? IsDeleted { get; set; }
        public string? ImagePath { get; set; }

        public virtual UserRole UserRole { get; set; }
        //public virtual Group Group { get; set; }    
        public virtual ICollection<Post> PostAuthors { get; set; }
        public virtual ICollection<Post> PostEditors { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
       


    }
}
