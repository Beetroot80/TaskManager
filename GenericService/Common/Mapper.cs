using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ServiceEntities;

namespace GenericService
{
    
    class MapperProfile : Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<DomainCore.Comment, Comment>().MaxDepth(1);
            CreateMap<DomainCore.DomainTask, ServiceTask>().MaxDepth(1);
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
