using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageQueue.Api.Models;
using MessageQueue.Api.Services.Implementation;
using MessageQueue.Api.Services.Interface;
using SimpleInjector;

namespace MessageQueue.Api.Services
{
	public class ServucesDI
	{
		public static void Confiure(Container container)
		{
			container.RegisterSingle<IJobQueueService, JobQueueService>();
		}
	}
}