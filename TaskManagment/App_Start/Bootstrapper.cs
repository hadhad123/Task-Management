using Autofac;
using Autofac.Integration.Mvc;
using Data;
using Data.Infrastructure;
using Data.Repositories;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TaskManagment.Mappings;

namespace TaskManagment.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            // Services
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            //// EF Context
            //builder.RegisterType<TaskManagmentContext>()
            //       .As<TaskManagmentContext>()
            //       .InstancePerRequest();

            //// Repositories
            //builder.RegisterType<UserRepository>()
            //    .As<IUserRepository>().InstancePerRequest();

            //// Services
            //builder.RegisterType<UserService>()
            //    .As<IUserService>()
            //    .InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}