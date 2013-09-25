using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public static class Utils
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_fraction_get_type();
		[DllImport(Application.Dll)]
		static extern IntPtr gst_flow_get_name (FlowReturn ret);
		[DllImport(Application.Dll)]
		static extern uint gst_flow_to_quark (FlowReturn ret);
		[DllImport(Application.Dll)]
		static extern void gst_value_set_fraction (ref GLib.Value val, int num, int den);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_value_serialize (ref GLib.Value val);
		[DllImport(Application.Dll)]
		static extern void gst_util_set_value_from_string (ref GLib.Value val, IntPtr str);

		public static GLib.GType FractionType(){
			return new GLib.GType(gst_fraction_get_type());
		}

		public static string GetName(this FlowReturn ret){
			return Marshal.PtrToStringAuto (gst_flow_get_name (ret));
		}
		public static uint ToQuark(this FlowReturn ret){
			return gst_flow_to_quark (ret);
		}
		public static void SetFraction (this GLib.Value val, int num, int den){
			val = new GLib.Value (new GLib.GType(gst_fraction_get_type ()));
			gst_value_set_fraction (ref val, num, den);
		}
		public static string Serialize (this GLib.Value val){
			return Marshal.PtrToStringAuto (gst_value_serialize (ref val));
		}

		public static void FromString(this GLib.Value val, string str){
			gst_util_set_value_from_string (ref val, Marshal.StringToHGlobalAuto (str));
		}
	}
}

