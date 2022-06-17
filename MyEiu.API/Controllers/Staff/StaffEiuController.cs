using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public ActionResult Departments()
        {
            var result = _staffeiudbcontext.Departments.Where(d => d.IsDeleted == 0)
                                                   .Include(d => d.Staffs.Where(s => s.IsDeleted == 0 && s.Type != 4)//4: type of member not staff
                                                    .OrderBy(s => s.StaffID))
                                                  .OrderBy(d => d.RecordID);


            var departmentstaffViewModel = _mapper.Map<List<DepartmentStaffEiuViewModel>>(result);

            
            return Ok(departmentstaffViewModel);
        }
        [HttpGet]
        public ActionResult Staffs()
        {
            var result = _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0 && s.Type != 4)   //4: type of member not staff                                             
                                                  .Include(s => s.DepartmentEiu)
                                                    .OrderBy(s => s.StaffID);
                     

            var staffViewModel =  _mapper.Map<List<StaffEiuViewModel>>(result);


            return Ok(staffViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> Staff(string email)
        {
            StaffEiuViewModel staffViewModel = new();

            StaffEiu? result = await _staffeiudbcontext.StaffEius.Include(s=>s.DepartmentEiu)
                                            .FirstOrDefaultAsync(s => s.IsDeleted == 0 && s.Type != 4 && s.SchoolEmail == email);   //4: type of member not staff                                                                                               
                                                  

            staffViewModel = _mapper.Map<StaffEiuViewModel>(result);


            return Ok(staffViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> PagingStaffs(StaffPagingDto staffpagingdto)
        {                     
            //
            var result = _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0 && s.Type != 4);    
            if(staffpagingdto.Search_Key != null)
            {
                result = _staffeiudbcontext.StaffEius.Where(s => s.IsDeleted == 0 && s.Type != 4 && s.FullName!.Contains(staffpagingdto.Search_Key));
            }

            var pagingResult = await result.OrderBy(s => s.StaffID).ProjectTo<StaffEiuViewModel>(_configMapper)
                                        .ToPaginationAsync(staffpagingdto.Current_Page, staffpagingdto.Page_Size);

            return Ok(pagingResult);
        }

    }
}
