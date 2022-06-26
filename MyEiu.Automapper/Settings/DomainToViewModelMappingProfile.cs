﻿using AutoMapper;
using MyEiu.Automapper.ViewModel.App.Notification;
using MyEiu.Automapper.ViewModel.App.Posts;
using MyEiu.Automapper.ViewModel.Staff;
using MyEiu.Automapper.ViewModel.Web;
using MyEiu.Data.Entities.App;
using MyEiu.Data.Entities.Staff;
using MyEiu.Data.Entities.Web;

namespace MyEiu.Automapper.Settings
{
    internal class DomainToViewModelMappingProfile : Profile
    {

       public DomainToViewModelMappingProfile()
        {
            //postWEB -> postviewmodelWEB
            CreateMap<PostWebEiu, PostWebViewModel>().ForMember(des => des.Post_Description, options => options.MapFrom(src => src.Post_Excerpt))
                .ForMember(des => des.Post_Url,options =>options.MapFrom(src=>src.Guid))
                .ForMember(des=>des.Post_Url,options =>options.MapFrom(src=> "https://eiu.edu.vn/?p=" + src.Id))
                .ForMember(des =>des.Post_Author,options => options.MapFrom(src=>src.UserWebEiu!.display_name))
                .ForMember(des => des.Post_Thumbnail, options => options.MapFrom(src => src.ThumbnailWebEiu.open_graph_image))
                ;           
            //staff -> staffviewmodel
            CreateMap<StaffEiu, StaffEiuViewModel>().ForMember(des => des.Id, options => options.MapFrom(src => src.StaffID))
                .ForMember(des => des.Name, options => options.MapFrom(src => src.LastName + " " + src.MiddleName + " " + src.FirstName))
                .ForMember(des => des.DepartmentName, options => options.MapFrom(src => src.DepartmentEiu!.FullName))
                .ForMember(des => des.Email, options => options.MapFrom(src => src.SchoolEmail))
                .ForMember(des => des.Avatar, options => options.MapFrom(src => "http://it.eiu.vn/pcntt/img/ImageStaff/" + src.ImagePath))
                ;
            //department -> departmentStaffviewmodel
            CreateMap<DepartmentEiu, DepartmentStaffEiuViewModel>().ForMember(des => des.Id, options => options.MapFrom(src => src.RecordID))
                .ForMember(des => des.Name, options => options.MapFrom(src => src.FullName))
                .ForMember(des => des.Data, options => options.MapFrom(src => src.Staffs))
                ;
            //department -> departmentViewModel
            CreateMap<DepartmentEiu, DepartmentEiuViewModel>();
            //postAPP -> postviewmodelAPP
            CreateMap<Post, PostViewModel>();              
                ;
            CreateMap<Post, NotificationViewModel>().ForMember(des => des.CreatBy, options => options.MapFrom(src =>src.Author!.LastName + " " + src.Author.FirstName))
                ;

        }

    }
}
