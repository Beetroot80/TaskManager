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
                .ForMember("CreatedBy", d => d.MapFrom(src => src.CreatedBy_Id))
                .MaxDepth(1);
            CreateMap<DomainCore.ClientProfile, ServiceEntities.ClientProfile>().MaxDepth(1);
            CreateMap<DomainCore.Project, ServiceEntities.Project>().MaxDepth(1);
        }
    }
}
