using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageQueue.Common.Enum;

namespace MessageQueue.Common.Model
{
	public class UpdateJobProcessStatusModel
	{
		public string Id { get; set; }

		public ProcessStatus ProcessStatus { get; set; }
	}
}
