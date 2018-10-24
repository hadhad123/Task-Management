﻿using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagment.ViewModels;

namespace TaskManagment.Mappings
{
    public class AutoMapperConfiguration
    {
        public void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                ConfiguremodelToViewModel(cfg);
                ConfigureViewModelToModel(cfg);

            });
        }

        private void ConfiguremodelToViewModel(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, UserViewModel>()
                  .ForMember(x => x.UserName, m => m.MapFrom(a => a.UserName))
                  .ForMember(x => x.Email, m => m.MapFrom(a => a.Email))
                  .ForMember(x => x.Active, m => m.MapFrom(a => a.Active))
                  .ForMember(x => x.HashedPassword, m => m.MapFrom(a => a.HashedPassword));

        }
        private void ConfigureViewModelToModel(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserViewModel, User>()
                  .ForMember(x => x.UserName, m => m.MapFrom(a => a.UserName))
                  .ForMember(x => x.Email, m => m.MapFrom(a => a.Email))
                  .ForMember(x => x.Active, m => m.MapFrom(a => a.Active))
                  .ForMember(x => x.HashedPassword, m => m.MapFrom(a => a.HashedPassword))
                  .ForMember(x => x.Salt, opt => opt.Ignore());

        }

    }
}