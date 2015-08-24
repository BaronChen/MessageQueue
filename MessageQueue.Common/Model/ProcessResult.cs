using System.Collections.Generic;
using System.Linq;

namespace MessageQueue.Common.Model
{
	public class ProcessResult
	{
		public ProcessResult()
		{
			_errors = new List<string>();
			_messages = new List<string>();
		}

		private List<string> _errors;
		private List<string> _messages;

		public List<string> Errors
		{
			get { return _errors; }
		}

		public List<string> Messages
		{
			get { return _messages; }
		}

		public bool HasError()
		{
			return Errors.Any();
		}
	}
}