using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MessageQueue.Api.Models;
using MessageQueue.Api.Services;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace MessageQueue.Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			var container = new Container();
		
			ModelDI.Confiure(container);

			ServucesDI.Confiure(container);

			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			container.Verify();

			GlobalConfiguration.Configuration.DependencyResolver =
				new SimpleInjectorWebApiDependencyResolver(container);
		}
	}
}
