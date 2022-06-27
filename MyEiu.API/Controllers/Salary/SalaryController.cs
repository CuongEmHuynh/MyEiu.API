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
        public async Task<ActionResult> GetMonthlyAsync([NotNull] int year , [NotNull] int month , [NotNull] string staffId )
        {
            return Ok(await _salaryService.GetSalary(year, month, staffId));
        }




    }
}
