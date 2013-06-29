using System;

namespace Gst
{
	public delegate void ElementHandler(object o, ElementHandlerArgs args);

	public class ElementHandlerArgs : GLib.SignalArgs
	{
		public Gst.Element Element {
			get{
				return (Gst.Element)base.Args[0];
			}
		}
	}
}

