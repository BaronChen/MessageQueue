using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;
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
			
			config.EnableCors(new MyCorsPolicyProvider());

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);



			var container = new Container();
		
			ModelDI.Confiure(container);

			ServicesDI.Confiure(container);

			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			container.Verify();

			GlobalConfiguration.Configuration.DependencyResolver =
				new SimpleInjectorWebApiDependencyResolver(container);
		}
	}

	public class MyCorsPolicyProvider : ICorsPolicyProvider
	{
		private CorsPolicy _policy;

		public MyCorsPolicyProvider()
		{
			_policy = new CorsPolicy
			{
				AllowAnyMethod = true,
				AllowAnyHeader = true
			};

			// Add allowed origins.
			_policy.Origins.Add("http://localhost:5268");
		}

		public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_policy);
		}
	}
}
