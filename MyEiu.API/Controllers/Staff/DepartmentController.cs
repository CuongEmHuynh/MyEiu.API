using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEiu.Automapper.ViewModel.Staff;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.Entities.Staff;

namespace MyEiu.API.Controllers.Staff
{
    public class DepartmentController : APIBaseController
    {
        private readonly IMapper _mapper;
        private readonly StaffEiuDbContext _staffEiuDbContext;

        public DepartmentController(IMapper mapper, StaffEiuDbContext staffEiuDbContext)
        {
            _mapper = mapper;
            _staffEiuDbContext = staffEiuDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Departments()
        {
            List<DepartmentEiuViewModel> departmentViewModel = new();
            List<DepartmentEiu> result = new();

            result = await _staffEiuDbContext.Departments.Where(d => d.IsDeleted == 0)                                                   
                                                  .OrderBy(d => d.RecordID)
                                                  .ToListAsync();

            departmentViewModel = _mapper.Map<List<DepartmentEiuViewModel>>(result);


            return Ok(departmentViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> Staffs(int departmentid)
        {
            List<StaffEiuViewModel> staffViewModel = new();
            List<StaffEiu> result = new();

            result = await _staffEiuDbContext.StaffEius.Where(d => d.IsDeleted == 0 && d.Type!=4 && d.DepartmentEiu.RecordID ==departmentid)                                                 
                                                  .OrderBy(d => d.FirstName)
                                                  .ToListAsync();

            staffViewModel = _mapper.Map<List<StaffEiuViewModel>>(result);

            return Ok(staffViewModel);
        }
    }
}
