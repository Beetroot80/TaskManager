using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ServiceMapper
{
    public class MapperProfile : Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<DomainCore.DomainTask, ServiceEntities.ServiceTask>()
                .ForMember(x => x.CreatedById, op => op.MapFrom(task => task.CreatedBy_Id))
                .ForMember(x=> x.AssignedToId, op => op.MapFrom(task => task.AssignedTo))
                .ForMember( x=> x.Client, op => op.MapFrom(user => user.Client))
                .MaxDepth(1).ReverseMap();
            CreateMap<DomainCore.ClientProfile, ServiceEntities.ClientProfile>()
                .ForMember(x => x.ApplicationUser , op => op.MapFrom(profile => profile.ApplicationUser)).MaxDepth(1).ReverseMap();
            CreateMap<DomainCore.ApplicationUser, ServiceEntities.ApplicationUser>()
                .ForMember(x => x.ClientProfile, op => op.MapFrom(user => user.ClientProfile))
                .ForMember(x => x.Tasks, op => op.MapFrom(user => user.DomainTasks)).MaxDepth(1).ReverseMap();
            CreateMap<DomainCore.ApplicationRole, ServiceEntities.ApplicationRole>();
            CreateMap<DomainCore.Comment, ServiceEntities.Comment>();
            CreateMap<DomainCore.Priority, ServiceEntities.Priority>();
            CreateMap<DomainCore.Status, ServiceEntities.Status>();
            CreateMap<DomainCore.Project, ServiceEntities.Project>()
                .MaxDepth(1);
        }
    }
}
