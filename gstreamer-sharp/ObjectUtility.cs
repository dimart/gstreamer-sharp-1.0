using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public static class ObjectUtility
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_message_new_error(IntPtr src, ref GLib.Error e,
		                                           [MarshalAs(UnmanagedType.LPStr)]string  debug);

		public static Message NewError (this Gst.Object src, ref GLib.Error e, string debug)
		{
			return new Message(gst_message_new_error (src.Handle, ref e, debug));
		}
	}
}

