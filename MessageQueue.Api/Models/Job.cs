using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageQueue.Api.Models
{
	public class Job
	{
		[Key]
		public Guid Id { get; set; }

		public DateTime RequestDateTime { get; set; }

		public DateTime CompletedDateTime { get; set; }




	}
}