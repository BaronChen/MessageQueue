using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.Common.Enum
{
	public enum ResultStatus
	{
		Requested,
		Error,
		Success
	}

	public enum ProcessStatus
	{
		Processing,
		Completed
	}
}
