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

		public static GLib.GType FractionType(){
			return new GLib.GType(gst_fraction_get_type());
		}

		public static string GetName(this FlowReturn ret){
			return Marshal.PtrToStringAuto (gst_flow_get_name (ret));
		}
		public static uint ToQuark(this FlowReturn ret){
			return gst_flow_to_quark (ret);
		}
	}
}

