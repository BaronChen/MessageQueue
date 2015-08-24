using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageQueue.Common.Enum;

namespace MessageQueue.Common.Model
{
	public class UpdateJobResultStatusModel
	{
		public string Id { get; set; }

		public ResultStatus ResultStatus { get; set; }
	}
}
