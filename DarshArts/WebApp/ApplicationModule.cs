using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Interfaces;
using WebApp.Data.Repository;
using WebApp.Services;
using WebApp.Services.Interfaces;

namespace WebApp
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register automapper profile
            builder.RegisterType<DataProfile>().As<Profile>();

            // Register Repository
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();


            // Register Services
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
        }
    }
}
