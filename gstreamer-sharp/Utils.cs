using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Utils
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_fraction_get_type();

		public static GLib.GType FractionType(){
			return new GLib.GType(gst_fraction_get_type());
		}
	}
}

