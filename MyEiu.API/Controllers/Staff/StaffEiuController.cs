using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Dtos;
using MyEiu.Automapper.ViewModel.Staff;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.Entities.Staff;
using MyEiu.Utilities;

namespace MyEiu.API.Controllers.Staff
{
    public class StaffEiuController : APIBaseController
    {
        private readonly IMapper _mapper;
        private readonly StaffEiuDbContext _staffeiudbcontext;
        private readonly MapperConfiguration _configMapper;

        public StaffEiuController(IMapper mapper, StaffEiuDbContext staffeiudbcontext, MapperConfiguration configMapper)
        {
            _mapper = mapper;
            _staffeiudbcontext = staffeiudbcontext;
            _configMapper = configMapper;
        }

        [HttpGet]

        public async Task<ActionResult> Departments()
        {
            List<DepartmentStaffEiuViewModel> departmentstaffViewModel = new();
            List<DepartmentEiu> result = new();

            result = await _staffeiudbcontext.Departments.Where(d => d.IsDeleted == 0)
                                                   .Include(d => d.Staffs.Where(s => s.IsDeleted == 0 && s.Type!=4)//4: type of member not staff
                                                    .OrderBy(s=>s.StaffID))
                                                  .OrderBy(d => d.RecordID)
                                                  .ToListAsync();
       
            departmentstaffViewModel = _mapper.Map<List<DepartmentStaffEiuViewModel>>(result);

            
            return Ok(departmentstaffViewModel);
        }
        [HttpGet]

        public async Task<ActionResult> Staffs()
        {
            List<StaffEiuViewModel> staffViewModel = new();
            List<StaffEiu> result = new();

            result = await _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0 && s.Type!=4)   //4: type of member not staff                                             
                                                  .OrderBy(s => s.StaffID)
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
                result = await _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0 && s.Type != 4)
                                                .OrderBy(s => s.StaffID)
                                                .ToListAsync();
            }
            else
            {
                result = await _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0 && s.FullName.Contains(staff.Search_Key) && s.Type != 4)
                                                .OrderBy(s => s.StaffID)
                                                .ToListAsync();
            }
          

            staffViewModel = _mapper.Map<List<StaffEiuViewModel>>(result);

            //var pagingResult = staffViewModel.ToPaginationAsync(staff.Current_Page, staff.Page_Size);

            return Ok(staffViewModel);
        }

    }
}
