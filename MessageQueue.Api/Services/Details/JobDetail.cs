using System;
using System.Collections.Generic;
using System.Linq;
using MessageQueue.Api.Models;
using MessageQueue.Common.Enum;
using MessageQueue.Common.Model;

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

		public DateTime? RequestDateTime { get; set; }

		public DateTime? CompletedDateTime { get; set; }

		public ResultStatus ResultStatus { get; set; }

		public ProcessStatus ProcessStatus { get; set; }
	}
}