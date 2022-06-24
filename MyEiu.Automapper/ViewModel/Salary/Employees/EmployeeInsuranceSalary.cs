using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.Salary.Employees
{
    public class EmployeeInsuranceSalary
    {
        public string EmployeeInsuranceSalaryId { get; set; }
        public decimal? CoefficientSalary { get; set; }
        public decimal InsuranceSalary { get; set; }
        public decimal Difference { get; set; }
        public string Description { get; set; }
    }
}
