using System;
using AutoMapper;

namespace ServiceMapper
{
    public class MapperProfile : Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<DomainEntities.DomainTask, ServiceEntities.ServiceTask>()
                .ForMember(x => x.CreatedById, op => op.MapFrom(task => task.CreatedBy_Id))
                .ForMember(x=> x.AssignedToId, op => op.MapFrom(task => task.AssignedTo))
                .ForMember( x=> x.Client, op => op.MapFrom(user => user.Client))
                .MaxDepth(1).ReverseMap();
            CreateMap<DomainEntities.ClientProfile, ServiceEntities.ClientProfile>()
                .ForMember(x => x.ApplicationUser , op => op.MapFrom(profile => profile.ApplicationUser)).MaxDepth(1).ReverseMap();
            CreateMap<DomainEntities.ApplicationUser, ServiceEntities.ApplicationUser>()
                .ForMember(x => x.ClientProfile, op => op.MapFrom(user => user.ClientProfile))
                .ForMember(x => x.Tasks, op => op.MapFrom(user => user.DomainTasks)).MaxDepth(1).ReverseMap();
            CreateMap<DomainEntities.ApplicationRole, ServiceEntities.ApplicationRole>();
            CreateMap<DomainEntities.Comment, ServiceEntities.Comment>();
            CreateMap<DomainEntities.Priority, ServiceEntities.Priority>()
                /*.ForMember(x => x.Tasks , op => op.AllowNull()).MaxDepth(1)*/;
            CreateMap<DomainEntities.Status, ServiceEntities.Status>();
            CreateMap<DomainEntities.Project, ServiceEntities.Project>()
                .ForMember(x => x.Clients, op => op.MapFrom(client => client.Clients))
                .MaxDepth(1);
            CreateMap<ServiceEntities.Project, DomainEntities.Project>()
                .ForMember(x => x.Id, op => op.Ignore());
            CreateMap<ServiceEntities.ServiceTask, DomainEntities.DomainTask>()
                .ForMember(x => x.Id, op => op.Ignore())
                .ForMember(x => x.Project, op => op.Ignore())
                .ForMember(x => x.Status , op => op.Ignore())
                .ForMember(x => x.Priority , op => op.Ignore())
                .ForMember(x => x.CreatedBy_Id , op => op.MapFrom(y => y.CreatedById));
        }
    }
}
