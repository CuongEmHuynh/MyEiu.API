using Becamex.Salary.Payrolls;
using Microsoft.EntityFrameworkCore;
using MyEiu.Automapper.ViewModel.Salary;
using MyEiu.Data.EF.DbContexts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Application.Services.Salary
{
    public interface ISalaryService
    {
        Task<Payroll> GetMonthlyAsync(
       [NotNull] int year,
       [NotNull] int month,
       [NotNull] string staffId,
       [NotNull] string payrollFormId);

        Task<object> GetSalary([NotNull] int year,
       [NotNull] int month,
       [NotNull] string staffId
      );
    }
    public class SalaryService : ISalaryService
    {
        private readonly IPayrollProvider PayrollProvider;
        private readonly StaffEiuDbContext _staffEiuDbContext;

        public SalaryService(IPayrollProvider payrollProvider, StaffEiuDbContext staffEiuDbContext)
        {
            PayrollProvider = payrollProvider;
            _staffEiuDbContext = staffEiuDbContext;
        }

        public async Task<Payroll> GetMonthlyAsync([NotNull] int year, [NotNull] int month, [NotNull] string staffId, [NotNull] string payrollFormId)
        {
            return await PayrollProvider.GetMonthlyAsync(year, month, staffId, payrollFormId);
        }

        public async Task<object> GetSalary([NotNull] int year, [NotNull] int month, [NotNull] string staffId)
        {
            //Get info Salary
            //var data = await PayrollProvider.GetMonthlyAsync(year, month, staffId, "HRL002A");

            //Get Template  
            string pathFileTemplate = "wwwroot/Template/salary-template.html";
            string pathFileTemplateEmpty = "wwwroot/Template/salary-template-empty.html";
            string html = "";

            //if (data != null )
            //{
            //    var tienThucNhan = data.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T34")!.Value;
            html = File.ReadAllText(pathFileTemplate);

            //    //GetData
            //    string nameEml = data.FullName;
            //    var nameDept = (await _staffEiuDbContext.StaffEius.Include(s => s.DepartmentEiu)
            //                              .FirstOrDefaultAsync(s => s.IsDeleted == 0 && s.Type != 4 && s.SchoolEmail.Trim() == staffId))!.DepartmentEiu!.FullName;
            //    string staff = data.StaffId;

            //    var tienBHXH = data.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "L12")!.Value ?? 0;

            var tienBHXH = 1000000;
           var tienThucNhan = 800000;

            //Replace data into template
            html = html.Replace("%monthy", month.ToString());
            html = html.Replace("%ten_bp", "IT");
            html = html.Replace("%ten_nv", "ABC");
            html = html.Replace("%ma_nv", "033914515");
            html = html.Replace("%tien_bh", Decimal.ToInt32((decimal)tienBHXH).ToString("#,###"));
            html = html.Replace("%tien_tn", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));
            html = html.Replace("%luong_nang_suat", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));
            html = html.Replace("%B01", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));
            html = html.Replace("%G_tong_nhap", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));
            html = html.Replace("%G_tong_trư", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));
            html = html.Replace("%G_tong_trư", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));
            html = html.Replace("%z20", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));
            //}
            //else
            //{
            //    html = File.ReadAllText(pathFileTemplateEmpty);
            //}

            object result = new
            {
                result = html
            };


            return result;

        }
    }
}

