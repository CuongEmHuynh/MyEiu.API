using MyEiu.Data.Entities.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.App.FileDatas
{
    public class FileDataViewModel
    {
        
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public string? FileName { get; set; }      
        public string? Path { get; set; }
        public virtual PostFileData? PostFileData { get; set; }
    }
}
