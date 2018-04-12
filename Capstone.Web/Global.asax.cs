﻿using Capstone.Web.NPSDatabase;
using GLWebApp;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capstone.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);


        }

        // Configure the dependency injection container.
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            string connectionString = ConfigurationManager.ConnectionStrings["NpsDB"].ConnectionString;

            kernel.Bind<INpsDAL>().To<NpsDALSql>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
    }

}