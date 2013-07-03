using System;
using System.Runtime.InteropServices;

namespace GLib.Quark
{
	public static class QuarkUtility
	{
		[DllImport("glib-2.0")]
		static extern IntPtr g_quark_to_string(uint quark);
		[DllImport("glib-2.0")]
		static extern uint g_quark_from_string(IntPtr str);

		public static string ToQuarkString(this uint quark){
			return Marshal.PtrToStringAuto (g_quark_to_string (quark));
		}
		public static uint FromString(string str){
			return g_quark_from_string (Marshal.StringToHGlobalAuto (str));
		}
	}
}

