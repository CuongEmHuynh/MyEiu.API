using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.Interface
{
    internal interface IUserTracking
    {
        int? CreateBy { get; set; }
        int? ModifyBy { get; set; }

    }
}
