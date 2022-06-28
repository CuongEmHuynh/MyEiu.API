using Microsoft.AspNetCore.Mvc;
using MyEiu.Application.Services.Salary;
using System.Diagnostics.CodeAnalysis;

namespace MyEiu.API.Controllers.Salary
{
    public class SalaryController :APIBaseController
    {
       private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetMonthlyAsync([NotNull] int year , [NotNull] int month , [NotNull] string email )
        {
            return Ok(await _salaryService.GetSalary(year, month, email));
        }

        //[HttpGet]
        //public async Task<ActionResult> GetMonthlyAsync2([NotNull] int year, [NotNull] int month, [NotNull] string staffId, [NotNull] string payrollFormId)
        //{
        //    return Ok(await _salaryService.GetMonthlyAsync(year, month, staffId,payrollFormId));
        //}

    }
}
