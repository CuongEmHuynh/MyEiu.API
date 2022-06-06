using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.EF.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChange();

        Task SaveChangeAsync();
    }
}
