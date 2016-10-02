using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using ServiceMapper;
using TaskManager.Models;
using ServiceEntities;

namespace TaskManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MVCMapperConfig.ConfigureAutoMapper();
        }
    }

    class MVCMapperProfile : Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<Project, ProjectModel>()
                .ForMember(x => x.TaskCount, op => op.MapFrom(project => project.Tasks.Count))
                .ForMember(x => x.ClientsCount, op => op.MapFrom(project => project.Clients.Count >= 1 ? project.Clients.Count : 1 ))
                .MaxDepth(2)
                .ReverseMap();
            CreateMap<ViewTasksModel, ServiceTask>()
                .ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserModel>()
                .ReverseMap();
            CreateMap<ApplicationUser, EditUserModel>()
                .ForMember(x => x.Name, op => op.MapFrom(user => user.ClientProfile.Name))
                .ForMember(x => x.Surname, x => x.MapFrom(user => user.ClientProfile.Surname))
                .ReverseMap();
            CreateMap<ApplicationUser, AddUserModel>()
                .ForMember(x => x.Role, op => op.MapFrom(y => y.UserRoles.First()))
                .ForMember(x => x.Name, op => op.MapFrom(y => y.UserName))
                .ReverseMap();
            CreateMap<ApplicationUser, RegisterModel>()
                .ReverseMap();
            CreateMap<ServiceTask, ViewTasksModel>()
                .ForMember(x => x.AssignedToEmail, op => op.MapFrom(user => user.Client.Email))
                .ForMember(x => x.CreatedByEmail, op => op.MapFrom(user => user.CreatedBy.Email))
                .ForMember(x => x.PriorityTitle, op => op.MapFrom(title => title.Priority.Title))
                .ForMember(x => x.StatusTitle, op => op.MapFrom(status => status.Status.Title))
                .ForMember(x => x.ProjectTitle,op => op.MapFrom(project => project.Project.Title))
                .MaxDepth(1)
                .ReverseMap();
            CreateMap<Comment, CommentModel>();
        }
    }
    public static class MVCMapperConfig
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile(new MVCMapperProfile());
                x.AddProfile(new MapperProfile());
            });
        }
    }
}
