using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using PMS.Repository;
using PMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PMS.Configuration
{
    public class DependencyInjection
    {

        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());

            var cfg = AutoMapperConfig.RegisterMappingModules();
            builder.RegisterInstance(cfg.CreateMapper()).As<IMapper>();

            // manual registration of types;
            builder.RegisterType<ResumeService>().As<IResumeService>();
            builder.RegisterType<ResumeRepository>().As<IResumeRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<ProjectService>().As<IProjectService>();
            builder.RegisterType<FilterRepository>().As<IFilterRepository>();
            builder.RegisterType<FilterService>().As<IFilterService>();
            builder.RegisterType<WorkOrderRepository>().As<IWorkOrderRepository>();
            builder.RegisterType<WorkOrderService>().As<IWorkOrderService>();
            builder.RegisterType<DrawingService>().As<IDrawingService>();
            builder.RegisterType<DrawingRepository>().As<IDrawingRepository>();
            builder.RegisterType<CorrespondenceService>().As<ICorrespondenceService>();
            builder.RegisterType<CorrespondenceRepository>().As<ICorrespondenceRepository>();
            builder.RegisterType<ReportsService>().As<IReportsService>();
            builder.RegisterType<ReportsRepository>().As<IReportsRepository>();
            builder.RegisterType<BGsService>().As<IBGsService>();
            builder.RegisterType<BGsRepository>().As<IBGsRepository>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            // For property injection using Autofac
            // build

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            //  DependencyResolver.SetResolver(container);
        }
    }
}