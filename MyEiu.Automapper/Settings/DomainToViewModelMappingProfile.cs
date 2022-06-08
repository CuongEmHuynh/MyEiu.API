using AutoMapper;
using MyEiu.Automapper.ViewModel;
using MyEiu.Data.Entities;
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
            CreateMap<Post, PostViewModel>().ForMember(des => des.Post_Description, options => options.MapFrom(src => src.Post_Excerpt))
                .ForMember(des => des.Post_Url,options =>options.MapFrom(src=>src.Guid))
                .ForMember(des=>des.Post_Url,options =>options.MapFrom(src=> "https://eiu.edu.vn/?p=" + src.Id))
                .ForMember(des =>des.Post_Author,options => options.MapFrom(src=>src.UserWebEiu.display_name))
                .ForMember(des => des.Post_Thumbnail, options => options.MapFrom(src => src.ThumbnailWebEius.FirstOrDefault().Twitter_Image))
                ;
            //staff -> staffviewmodel
        }

    }
}
