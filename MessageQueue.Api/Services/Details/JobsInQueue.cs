using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessageQueue.Api.Services.Details
{
	public class JobsInQueue
	{
		[JsonProperty("createdJobIds")]
		public List<string> CreatedJobIds { get; set; }
		[JsonProperty("completeJobIds")]
		public List<string> CompleteJobIds { get; set; }
		[JsonProperty("processingJobIds")]
		public List<string> ProcessingJobIds { get; set; }
		[JsonProperty("errorJobIds")]
		public List<string> ErrorJobIds { get; set; }
		[JsonProperty("successJobIds")]
		public List<string> SuccessJobIds { get; set; }

	}
}