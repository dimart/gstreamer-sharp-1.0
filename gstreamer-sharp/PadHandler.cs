using System;

namespace Gst
{
	public delegate void PadHandler(object o, PadHandlerArgs args);

	public class PadHandlerArgs : GLib.SignalArgs
	{
		public Pad Pad {
			get{return (Gst.Pad)base.Args[0];}
		}
	}
}

