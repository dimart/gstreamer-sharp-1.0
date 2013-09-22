using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Plugin : GLib.Opaque
	{
		public Plugin (IntPtr raw) : base(raw)
		{
		}
	}
}

