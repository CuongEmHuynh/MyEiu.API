using Becamex.Salary.Payrolls;
using Microsoft.EntityFrameworkCore;
using MyEiu.Automapper.ViewModel.Salary;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Utilities;
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
            var dataHRL002A = await PayrollProvider.GetMonthlyAsync(year, month, staffId, "HRL002A");
            var dataHRL002 = await PayrollProvider.GetMonthlyAsync(year, month, staffId, "HRL002");

            //Get Template  
            string pathFileTemplate = "wwwroot/Template/salary_template.html";
            string pathFileTemplateEmpty = "wwwroot/Template/salary-template-empty.html";
            string html = "";

            //if (data != null )
            //{
            //    
            html = File.ReadAllText(pathFileTemplate);

            //GetData
            string nameEml = dataHRL002A.FullName;

            var staffData = await _staffEiuDbContext.StaffEius.Include(s => s.DepartmentEiu)
                                      .FirstOrDefaultAsync(s => s.IsDeleted == 0 && s.Type != 4 && s.SchoolEmail.Trim() == staffId);

            var nameDept = staffData!.DepartmentEiu!.FullName;
            string staff = staffData.StaffID!;
            string staffType = staffData.Type == 0 ? "Giảng viên" : staffData.Type == 1 ? "Chuyên viên" : "Khách";

            //Mức lương bảo hiểm xã hội
            var tienBHXH = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "L12")!.Value ?? 0;
            var tienComTrua = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T20")!.Value ?? 0;
            var tongluongTra = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T28")!.Value ?? 0;
            var tongGiamTru = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T23")!.Value ?? 0;
            var thueThuNhapCN = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T26")!.Value ?? 0;
            var tongThuNhapChiuThue = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "B04")!.Value ?? 0;
            var baoHiemXH = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "B06")!.Value ?? 0;
            var baoHiemYT = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "B07")!.Value ?? 0;
            var baoHiemTN = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "B08")!.Value ?? 0;
            var soNguoiPhuThuoc = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T55")!.Value ?? 0;
            var congDoan = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T30")!.Value ?? 0;
            var doanPhi = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T31")!.Value ?? 0;
            var dangPhi = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "DP")!.Value ?? 0;
            var luongNangSuat = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "P00")!.Value ?? 0;
            var giamTruKhac = dataHRL002.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "P60")!.Value ?? 0;

            var tongTru = baoHiemXH + baoHiemYT + baoHiemTN + thueThuNhapCN + giamTruKhac + congDoan;
            var tongNhan = luongNangSuat + tienComTrua;
            var tienThucNhan = dataHRL002A.PayrollDetails.FirstOrDefault(x => x.PayrollItem.Trim() == "T34")!.Value;

            //var tienBHXH = 1000000;
            //var tienThucNhan = 800000;

            ////Replace data into template
            //Thông tin NV
            html = html.Replace("%monthly", month.ToString());
            html = html.Replace("%ten_bp", nameDept);
            html = html.Replace("%ten_nv", nameEml);
            html = html.Replace("%ma_nv", staff);
            html = html.Replace("%ten_vtr", staffType);

            //Thông tin chung
            html = html.Replace("%tien_bh", Decimal.ToInt32((decimal)tienBHXH).ToString("#,###"));
            html = html.Replace("%tien_tn", tienBHXH.ToDecimal().ToStringCheckZero("#,###"));

            //Các Khoản thu nhập
            html = html.Replace("%Luong_nang_suat", Decimal.ToInt32((decimal)luongNangSuat).ToString("#,###"));
            html = html.Replace("%L25", tienComTrua.ToDecimal().ToStringCheckZero("#,###"));
            html = html.Replace("%PC1", "0");
            html = html.Replace("%P00", "0");
            html = html.Replace("%P10 ", "0");
            html = html.Replace("%P20", "0");
            html = html.Replace("%P51", "0");
            html = html.Replace("%L73", "0");


            //Các khoả trừ
            html = html.Replace("%B01", Decimal.ToInt32((decimal)baoHiemXH).ToString("#,###"));
            html = html.Replace("%B02", Decimal.ToInt32((decimal)baoHiemYT).ToString("#,###"));
            html = html.Replace("%B03", Decimal.ToInt32((decimal)baoHiemTN).ToString("#,###"));
            html = html.Replace("%T20", thueThuNhapCN.ToDecimal().ToStringCheckZero("#,###"));
            html = html.Replace("%G_doan_phi", doanPhi.ToDecimal().ToStringCheckZero("#,###"));
            html = html.Replace("%U02", "0");
            html = html.Replace("%U01", congDoan.ToDecimal().ToStringCheckZero("#,###"));
            html = html.Replace("%P61 ", giamTruKhac.ToDecimal().ToStringCheckZero("#,###"));

            //Tổng

            //html = html.Replace("%P61", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));
            html = html.Replace("%G_tong_nhap", tongNhan.ToDecimal().ToStringCheckZero("#,###"));
            html = html.Replace("%G_tong_tru", tongTru.ToDecimal().ToStringCheckZero("#,###"));
            html = html.Replace("%Z20", Decimal.ToInt32((decimal)tienThucNhan).ToString("#,###"));

            //}
            //else
            //{
            //    html = File.ReadAllText(pathFileTemplateEmpty);
            //}

            object result = new
            {
                result = html
            };


            return html;

        }
    }
}

