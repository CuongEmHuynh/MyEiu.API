using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.App.Notification
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? CreatBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public List<string> FilesUrl { get; set; }
        public NotificationViewModel()
        {
            FilesUrl = new List<string>();
        }
    }
}
