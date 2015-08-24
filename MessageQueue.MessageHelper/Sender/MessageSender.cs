using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageQueue.MessageHelper.Model;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace MessageQueue.MessageHelper.Sender
{
	public class MessageSender
	{
		private string _queueName;

		private QueueClient QueueClient;

		public MessageSender(string queueName)
		{
			_queueName = queueName;
			QueueClient = QueueClient.Create(_queueName);
		}

		public void SendMessage(CallBackMessage message, string messageId)
		{
			var messageBody = JsonConvert.SerializeObject(message);
			var brokerMessage = new BrokeredMessage(messageBody);
			brokerMessage.MessageId = messageId;

			QueueClient.Send(brokerMessage);
		}

		public void StopReceiver()
		{
			QueueClient.Close();
		}
	}
}
