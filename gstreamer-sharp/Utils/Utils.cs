using System;
using System.Runtime.InteropServices;

namespace Gst.Utils
{
	public static class Utility
	{
		[DllImport(Application.Dll)]
		static extern int gst_util_greatest_common_divisor(int a, int b);
		[DllImport(Application.Dll)]
		static extern double gst_util_guint64_to_gdouble(ulong val);
		[DllImport(Application.Dll)]
		static extern ulong gst_util_gdouble_to_guint64(double val);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_state_get_name(State state);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_state_change_return_get_name(StateChangeReturn scr);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_fraction_get_type();

		public static GLib.GType FractionType(){
			return new GLib.GType(gst_fraction_get_type());
		}

		public static int GCD(this int a, int b){
			return gst_util_greatest_common_divisor (a,b);
		}
		public static double ToDouble(this ulong u){
			return gst_util_guint64_to_gdouble (u);
		}
		public static ulong ToULong(this double d){
			return gst_util_gdouble_to_guint64 (d);
		}
		public static string GetName(this State state){
			return Marshal.PtrToStringAuto(gst_element_state_get_name (state));
		}
		public static string GetName(this StateChangeReturn scr){
			return Marshal.PtrToStringAuto (gst_element_state_change_return_get_name (scr));
		}
	}
}

