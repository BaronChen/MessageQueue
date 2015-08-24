using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MessageQueue.MessageHelper.MessageProcessor
{
	public class PostRequestResult
	{
		public bool Success { get; set; }

		public string Result { get; set; }
	}

	public static class PostManager
	{
		public static PostRequestResult PostRequest(string url, string body)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "Post";
			request.ContentType = "application/json";
			using (Stream requestStream = request.GetRequestStream())
			{
				var bytes = Encoding.ASCII.GetBytes(body);
				requestStream.Write(bytes, 0, bytes.Length);
			}

			var result = new PostRequestResult();
			try
			{
				var response =  (HttpWebResponse)request.GetResponse();
				using (var stream = response.GetResponseStream())
				{
					var reader = new StreamReader(stream, Encoding.UTF8);
					result.Result = reader.ReadToEnd();	
				}
				result.Success = true;
			}
			catch (WebException wex)
			{
				var httpResponse = wex.Response as HttpWebResponse;
				result.Success = false;
				if (httpResponse != null)
				{
					using (var stream = httpResponse.GetResponseStream())
					{
						var reader = new StreamReader(stream, Encoding.UTF8);
						result.Result = reader.ReadToEnd();
					}
				}
				else
				{
					result.Result = "Empty response for unknown reason.";
				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return result;
		}

	}
}
