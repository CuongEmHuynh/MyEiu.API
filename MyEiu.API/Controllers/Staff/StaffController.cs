using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Dtos;
using MyEiu.Automapper.ViewModel;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.Entities.Staff;
using MyEiu.Utilities;

namespace MyEiu.API.Controllers.Staff
{
    public class StaffController : APIBaseController
    {
        private readonly IMapper _mapper;
        private readonly StaffEiuDbContext _staffeiudbcontext;
        private readonly MapperConfiguration _configMapper;

        public StaffController(IMapper mapper, StaffEiuDbContext staffeiudbcontext, MapperConfiguration configMapper)
        {
            _mapper = mapper;
            _staffeiudbcontext = staffeiudbcontext;
            _configMapper = configMapper;
        }

        [HttpGet]

        public async Task<ActionResult> Departments()
        {
            List<DepartmentEiuViewModel> departmentViewModel = new();
            List<DepartmentEiu> result = new();

            result = await _staffeiudbcontext.Departments.Where(d => d.IsDeleted == 0)
                                                   .Include(d => d.Staffs.Where(s => s.IsDeleted == 0))
                                                  .OrderByDescending(d => d.Code)
                                                  .ToListAsync();
       
            departmentViewModel = _mapper.Map<List<DepartmentEiuViewModel>>(result);

            
            return Ok(departmentViewModel);
        }
        [HttpGet]

        public async Task<ActionResult> Staffs()
        {
            List<StaffEiuViewModel> staffViewModel = new();
            List<StaffEiu> result = new();

            result = await _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0 && s.SchoolEmail=="vu.ho@eiu.edu.vn")                                                
                                                  .OrderByDescending(s => s.StaffID)
                                                  .ToListAsync();

            staffViewModel = _mapper.Map<List<StaffEiuViewModel>>(result);


            return Ok(staffViewModel);
        }
        [HttpPost]

        public async Task<ActionResult> PagingStaffs(StaffPagingDto staff)
        {
            List<StaffEiuViewModel> staffViewModel = new();
            List<StaffEiu> result = new();

            if(staff.Search_Key is null)
            {
                result = await _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0)
                                                .OrderByDescending(s => s.StaffID)
                                                .ToListAsync();
            }
            else
            {
                result = await _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0 && s.FullName.Contains(staff.Search_Key))
                                                .OrderByDescending(s => s.StaffID)
                                                .ToListAsync();
            }
          

            staffViewModel = _mapper.Map<List<StaffEiuViewModel>>(result);

            var pagingResult = staffViewModel.ToPaginationAsync(staff.Current_Page, staff.Page_Size);

            return Ok(pagingResult);
        }
    }
}
