using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagment.ViewModels;

namespace TaskManagment.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {

        //public override string ProfileName
        //{
        //    get { return "ViewModelToDomainMappings"; }
        //}

        //protected override void Configure()
        //{
        //    Mapper.CreateMap<UserViewModel, User>()
        //        .ForMember(u => u.UserName, map => map.MapFrom(vm => vm.UserName))
        //        .ForMember(u => u.Email, map => map.MapFrom(vm => vm.Email)
        //       );
        //}
    }
}