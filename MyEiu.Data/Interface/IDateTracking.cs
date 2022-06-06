using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Interface
{
    internal interface IDateTracking
    {
        DateTime? CreateDate { get; set; }
        DateTime? ModifyDate { get; set; }
    }
}
