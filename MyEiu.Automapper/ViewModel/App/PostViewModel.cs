using MyEiu.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.App
{
    public class PostViewModel
    {      
        public int Id { get; set; }
        public int PostType { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }

        public PostPriority Priority { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? Date { get; set; }// get CreateDate or ModifyDate, select one which newer
    }
}
