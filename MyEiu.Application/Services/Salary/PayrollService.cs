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
        Task<Payroll> GetMonthlyAsync(
        [NotNull] int year,
        [NotNull] int month,
        [NotNull] string staffId,
        [NotNull] string payrollFormId
    );
    }
    public class PayrollService : IPayrollService
    {
        public Task<Payroll> GetMonthlyAsync([NotNull] int year, [NotNull] int month, [NotNull] string staffId, [NotNull] string payrollFormId)
        {
            Employee employ = new Employee();
            string employId = employ.Id;

            EmployeeSalaryDetail employeeSalaryDetail = new EmployeeSalaryDetail();

            EmployeeInsuranceSalary employeeInsuranceSalary = new EmployeeInsuranceSalary();


            throw new NotImplementedException();
        }
    }
}
