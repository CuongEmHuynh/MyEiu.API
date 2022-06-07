using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Automapper.Settings
{
    internal class DomainToViewModelMappingProfile : Profile
    {
       public DomainToViewModelMappingProfile()
        {
            //post -> postviewmodel
            //staff -> staffviewmodel
        }
    }
}
