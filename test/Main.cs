using System;
using System.Runtime.InteropServices;
using Gst;

namespace test
{
	class MainClass
	{
		[DllImport("gobject-2.0")]
		static extern IntPtr g_signal_emit_by_name(IntPtr o, IntPtr string_detail,IntPtr p, out IntPtr val);

		public static void Main (string[] args)
		{
			Application.Init ();
			var bin = new Bin ("mybin");
			bin.ElementAdded += delegate(object o, ElementHandlerArgs eargs) {
				Console.WriteLine (eargs.Element.Name);
		};
			var e = ElementFactory.Make ("playbin","plau");
			bin.Add (e);
		}
	}
}
