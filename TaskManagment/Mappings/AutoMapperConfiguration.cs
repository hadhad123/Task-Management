using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagment.ViewModels;

namespace TaskManagment.Mappings
{
    //public class AutoMapperConfiguration
    //{
    //    public static void Configure()
    //    {
    //        Mapper.Initialize(x =>
    //        {
    //            x.AddProfile<DomainToViewModelMappingProfile>();
    //            x.AddProfile<ViewModelToDomainMappingProfile>();
    //        });

    //    }
    //}
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                ConfiguremodelToViewModel(cfg);

            });



             void ConfiguremodelToViewModel(IMapperConfigurationExpression cfg)
            {
                cfg.CreateMap<User, UserViewModel>()
                      .ForMember(x => x.UserName, m => m.MapFrom(a => a.UserName))
                      .ForMember(x => x.Email, m => m.MapFrom(a => a.Email))
                      ;

            }
        }


    }
}