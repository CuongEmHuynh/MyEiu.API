using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.Salary.Employees
{
    public class EmployeeNetSalary
    {
        public string EmployeeNetSalaryId { get; set; }
        public byte? EncludeTax { get; set; }
        public byte? IncludeInsurance { get; set; }
        public string Description { get; set; }
    }
}
