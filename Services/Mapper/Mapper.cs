using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ServiceMapper
{
    class MapperProfile : Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<DomainCore.DomainTask, ServiceEntities.ServiceTask>().ForMember("CreatedBy", d => d.MapFrom(src => src.CreatedBy_Id)).MaxDepth(1);
        }
    }
    public class MapperConfig
    {
        public static IMapper Mapper;
        public static void ConfigureAutoMapper()
        {
            AutoMapper.Mapper.Initialize((x =>
            x.AddProfile(new MapperProfile())));
        }
    }
}
