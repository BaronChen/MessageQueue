using System;
using MessageQueue.MessageHelper.Model;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace MessageQueue.MessageHelper.Receiver
{
	public class MessageProcessor
	{
		private string _queueName;

		private QueueClient QueueClient;

		public MessageProcessor(string queueName)
		{
			_queueName = queueName;
			QueueClient = QueueClient.Create(_queueName);
		}

		public CallBackMessage ReceiveMessage()
		{
			BrokeredMessage message = null;

			//receive messages from Queue
			message = QueueClient.Receive(TimeSpan.FromSeconds(1));
			if (message != null)
			{
				var str = message.GetBody<string>();

				var callBackMessage = JsonConvert.DeserializeObject<CallBackMessage>(str);

				message.Complete();

				return callBackMessage;

			}

			return null;
		}

	

		public void StopReceiver()
		{
			QueueClient.Close();
		}

	}
}
