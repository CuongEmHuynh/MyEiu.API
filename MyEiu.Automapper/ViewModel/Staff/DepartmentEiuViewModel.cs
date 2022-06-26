using MyEiu.Data.Entities.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.Staff
{
    public class DepartmentEiuViewModel
    {
        public int RecordID { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public int IsDeleted { get; set; }
        public virtual StaffEiu? StaffEiu { get; set; }
    }
}
