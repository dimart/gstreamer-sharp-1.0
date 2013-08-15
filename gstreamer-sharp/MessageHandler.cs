using System;
using GLib;

namespace Gst
{
	public delegate void MessageHandler (object o, MessageArgs args);

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
}

