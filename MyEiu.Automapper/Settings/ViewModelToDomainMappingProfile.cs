using AutoMapper;
using MyEiu.Automapper.ViewModel.App.FileDatas;
using MyEiu.Automapper.ViewModel.App.Posts;
using MyEiu.Automapper.ViewModel.Staff;
using MyEiu.Automapper.ViewModel.Web;
using MyEiu.Data.Entities.App;
using MyEiu.Data.Entities.Staff;
using MyEiu.Data.Entities.Web;

namespace MyEiu.Automapper.Settings
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //postviewmodel -> postdto
          
            CreateMap<PostViewModel, Post>().ForMember(des => des.CreateBy, options => options.MapFrom(src => src.AuthorId))
                    .ForMember(des => des.ModifyBy, options => options.MapFrom(src => src.EditorId))
                    ;
            CreateMap<FileDataViewModel, FileData>().ForMember(des => des.DisplayName, options => options.MapFrom(src => src.FileName));
        }
       
    }
}
