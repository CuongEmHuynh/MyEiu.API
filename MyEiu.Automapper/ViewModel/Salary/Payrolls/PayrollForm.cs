using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel.Salary.Payrolls
{
    public class PayrollForm
    {
        public string PayrollElements { get; set; }
        public string Description { get; set; }
        public string Kind { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string Departments { get; set; }
        public string Timekeepings { get; set; }
        public string Products { get; set; }
        public string Allowances { get; set; }
        public string Formula { get; set; }
        public string Awards { get; set; }
        public string Others { get; set; }
        public decimal? Ids { get; set; }
        public byte? Iscal { get; set; }
    }
}
