using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.Salary.Employees
{
    public class EmployeeSalaryDetail
    {
        public string EmployeeSalaryDetailId { get; set; }
        public decimal? CoefficientSalary { get; set; }
        public decimal? CoefficientAdjustment { get; set; }
        public decimal? FixedSalary { get; set; }
        public string Description { get; set; }
    }
}
