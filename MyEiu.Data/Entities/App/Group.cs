using MyEiu.Data.Entities.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public int IsDeleted { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
