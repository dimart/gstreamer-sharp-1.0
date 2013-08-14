using System;

namespace Gst
{
	public delegate void CapsHandler(object o, CapsHandlerArgs args);

	public class CapsHandlerArgs : GLib.SignalArgs
	{
		public Gst.Caps Caps {
			get{
				return (Gst.Caps)base.Args[0];
			}
		}
	}
}

