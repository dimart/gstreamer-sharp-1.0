using System;
using GLib;

namespace Gst
{
	public delegate void MessageHandler (object o, MessageArgs args);
	public delegate void SyncMessageHandler (object o, SyncMessageArgs args);

	public class MessageArgs : SignalArgs
	{
		//
		// Properties
		//
		public Message Message
		{
			get
			{
				return (Message)base.Args [0];
			}
		}
	}
	
	public class SyncMessageArgs : SignalArgs
	{
		public Message Message{
			get{
				Console.WriteLine (base.Args[0].GetType ().ToString ());
				return (Message)base.Args[0];
			}
		}
	}
}

