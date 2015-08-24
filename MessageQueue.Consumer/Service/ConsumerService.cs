using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MessageQueue.MessageHelper.MessageProcessor;
using MessageQueue.MessageHelper.Model;
using MessageQueue.MessageHelper.Receiver;
using Microsoft.ServiceBus.Messaging;

namespace MessageQueue.Consumer.Service
{
	partial class ConsumerService : ServiceBase
	{
		private EventLog _consumerEventLog;
		private const string QueueName = "testqueue";
		private MessageProcessor MessageProcessor;

		public ConsumerService()
		{
			InitializeComponent();

			_consumerEventLog = new EventLog();
			if (!EventLog.SourceExists("ConsumerServiceSource"))
			{
				EventLog.CreateEventSource(
					"ConsumerServiceSource", "ConsumerServiceLog");
			}
			_consumerEventLog.Source = "ConsumerServiceSource";
			_consumerEventLog.Log = "ConsumerServiceLog";

			MessageProcessor = new MessageProcessor(QueueName);

		}

		protected override void OnStart(string[] args)
		{
			//Debugger.Launch();
			_consumerEventLog.WriteEntry("Consumer service started.");

			var worker = new Thread(DoWork);
			worker.Name = "CosumerWorker";
			worker.IsBackground = false;
			worker.Start();
		}

		protected override void OnStop()
		{
			_consumerEventLog.WriteEntry("Consumer service stopped.");
			
		}

		protected void DoWork()
		{
			_consumerEventLog.WriteEntry("Receiving message from Queue...");
			while (true)
			{
				ReceiveMessage();
			}
		}

		protected void ReceiveMessage()
		{
			var message = MessageProcessor.ReceiveMessage();

			if (message != null)
			{
				ProcessMessage(message);
			}
			
		}

		protected void ProcessMessage(CallBackMessage message)
		{
			try
			{
				var callBackMessageProcessor = new CallBackMessageProcessor(_consumerEventLog);

				callBackMessageProcessor.ProcessCallBackMessage(message);
			}
			catch (Exception e)
			{
				_consumerEventLog.WriteEntry("Error to process message (" + message.JobId+"): "+e.Message+".\n" + e.StackTrace);
			}
		}
	}
}
