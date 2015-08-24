using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.MessageHelper.Model
{
	public class CallBackMessage
	{
		public string Url { get; set; }
		public string PlayLoad { get; set; }
		public string JobId { get; set; }

		public string UpdateJobStatusUrl { get; set; }
		public string CompleteJobUrl { get; set; }
	}
}
