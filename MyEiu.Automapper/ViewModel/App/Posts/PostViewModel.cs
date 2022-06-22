using MyEiu.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.App.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int PostTypeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }

        public PostPriority Priority { get; set; }

        public PostStatus Status { get; set; }
        public string? Author { get; set; }
        public DateTime? CreateDate { get; set; }// get CreateDate or ModifyDate, select one which newer
        public string? Editor { get; set; }
        public DateTime? ModifyDate { get; set; }// get CreateDate or ModifyDate, select one which newer
    }
}
