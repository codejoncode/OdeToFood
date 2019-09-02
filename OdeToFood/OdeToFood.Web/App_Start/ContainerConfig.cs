using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OdeToFood.Web
{
    public static class ContainerConfig
    {
        public static void RegisterContainer(HttpConfiguration  httpConfiguration)
        {
            //Using dependency injection. 
            var builder = new ContainerBuilder();
            //MvcApplication is the class in the code behind that represents this application see Global.asax.cs
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //for the api 
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            //tell the continaer builder about the specific services I have 
            builder.RegisterType<SqlRestaurantData>().As<IRestaurantData>()
                .InstancePerRequest();//create a component and allow it to be arround for a single request for a particular user
            //once the request is over   throw it away.  safest and easiest way to use db context 
            //register the type of the sql data needs to be injected 
            builder.RegisterType<OdeToFoodDbContext>().InstancePerRequest();

                // don't need a singleton for database. dbContext is not a thread safe class
                //.SingleInstance();//other fieleds like per http request etc. 
                                  //^ use this type when someone needs something that represents IRestaurantData. 
                                  //this change can be made  here if we make other changes

            //build the container now we have something to give to the mvc5  framework 
            //when ever you need to resolve dependencies go to this container
            var container = builder.Build();
            //register all the controlers  build the container then set the contaiiner as teh dependency resolver
            //any where mvc uses to inject dependencies it wil use this container. 
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}