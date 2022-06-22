using Microsoft.AspNetCore.Mvc;
using MyEiu.Application.Services.Salary;
using System.Diagnostics.CodeAnalysis;

namespace MyEiu.API.Controllers.Salary
{
    public class SalaryController :APIBaseController
    {
        private readonly IPayrollService _service;

        public SalaryController(IPayrollService service)
        {
            _service = service;
        }

        [HttpGet]        
        
        public ActionResult GetMonthlyAsync([NotNull] int year, [NotNull] int month, [NotNull] string staffId, [NotNull] string payrollFormId)
        {
            return Ok(_service.GetMonthlyAsync(year,month,staffId,payrollFormId));
        }

    }
}
