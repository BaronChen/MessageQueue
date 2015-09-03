using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleInjector;

namespace MessageQueue.Api.Models
{
	public static class ModelDI
	{
		public static void Confiure(Container container)
		{
			container.RegisterWebApiRequest<MyDbContext>();
		}
	}
}