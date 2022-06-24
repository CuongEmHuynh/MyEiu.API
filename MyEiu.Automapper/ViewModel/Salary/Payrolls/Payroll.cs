using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.Salary.Payrolls
{
    public class Payroll
    {
        public Payroll()
        {
            PayrollDetails = new List<PayrollDetail>();
        }

        public string EmployeeId { get; set; }
        public string StaffId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public decimal? FixedSalary { get; set; }
        public decimal? SocialAndHealthInsuranceSalary { get; set; }
        public decimal? UnEmploymentInsuranceSalary { get; set; }
        public IList<PayrollDetail> PayrollDetails { get; set; }
    }
}
