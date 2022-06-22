using MyEiu.Automapper.ViewModel.Salary.Employees;
using MyEiu.Automapper.ViewModel.Salary.Payrolls;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Application.Services.Salary
{
    public interface IPayrollService
    {
        Payroll GetMonthlyAsync(
        [NotNull] int year,
        [NotNull] int month,
        [NotNull] string staffId,
        [NotNull] string payrollFormId
    );

        string GetTemplateSalary();
    }
    public class PayrollService : IPayrollService
    {
        public Payroll GetMonthlyAsync([NotNull] int year, [NotNull] int month, [NotNull] string staffId, [NotNull] string payrollFormId)
        {
            Employee employ = new Employee();
            string employId = employ.Id;

            EmployeeSalaryDetail employeeSalaryDetail = new EmployeeSalaryDetail();

            EmployeeInsuranceSalary employeeInsuranceSalary = new EmployeeInsuranceSalary();

            var payroll = new Payroll()
            {
                EmployeeId= "1306",
                StaffId ="0900012",
                Department ="09",
                FullName="Huỳnh Cường Em",
                FixedSalary=5555555555,
                UnEmploymentInsuranceSalary= 444444444,
                SocialAndHealthInsuranceSalary= 3333333333,
                Position="1"
            };
            List<PayrollForm> payrollForms = new List<PayrollForm>() ;
            for (int i = 0; i < 3; i++)
            {
                PayrollDetail payrollDetail = new PayrollDetail();
                payrollDetail.PayrollItem = "k1"+i;
                payrollDetail.PayrollItemName = "A1" + i;
                payrollDetail.Value = 180000 + i;

                payroll.PayrollDetails.Add(payrollDetail);
            }

            

            return payroll;

            
        }

        public string GetTemplateSalary()
        {
            throw new NotImplementedException();
        }
    }
}
