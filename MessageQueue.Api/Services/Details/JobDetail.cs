using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageQueue.Api.Services.Details
{
	public class JobDetail
	{

		public JobDetail()
		{
			ProcessResult = new ProcessResult();
		}

		public ProcessResult ProcessResult { get; set; }

		public Guid Id { get; set; }

		public DateTime RequestDateTime { get; set; }

		public DateTime CompletedDateTime { get; set; }
	}
}