using MyEiu.Data.Enum;
using MyEiu.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Entities.App
{
    [Table("Post")]
    public class Post: IUserTracking,IDateTracking
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("PostType")]
        public int? PostTypeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
      
        public PostPriority? Priority { get; set; }
        public bool? Disable { get; set; }
        public PostStatus? Status { get; set; }
        [ForeignKey("UserApp")]
        public int? CreateBy { get; set; }
        [ForeignKey("UserApp")]
        public int? ModifyBy { get; set; }
        public DateTime? CreateDate { get; set; }
        //public DateTime? PushDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public virtual PostType? PostType { get; set; }    
        public virtual UserApp? Author { get; set; }      
        public virtual UserApp? Editor { get; set; }
        public virtual  ICollection<PostFileData>? PostFileDatas { get; set; }
        public virtual ICollection<PostGroup>? PostGroups { get; set; }
        public virtual ICollection<PostUser>? PostUsers { get; set; }
        
    }
}
