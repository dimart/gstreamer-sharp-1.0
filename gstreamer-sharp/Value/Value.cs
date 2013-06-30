using System;
using System.Runtime.InteropServices;


namespace Gst.Value
{
	public static class Values
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_value_serialize(ref GLib.Value val);
		[DllImport(Application.Dll)]
		static extern bool gst_value_deserialize(out GLib.Value val,IntPtr src);
		[DllImport(Application.Dll)]
		static extern bool gst_value_is_fixed(ref GLib.Value val); 
		[DllImport(Application.Dll)]
		static extern void gst_value_array_append_value(IntPtr array, ref GLib.Value val);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_value_get_caps(ref GLib.Value val);
		[DllImport(Application.Dll)]
		static extern void gst_value_set_caps(ref GLib.Value val, IntPtr caps);
		[DllImport(Application.Dll)]
		static extern int gst_value_compare(ref GLib.Value val, ref GLib.Value val2);
		[DllImport(Application.Dll)]
		static extern bool gst_value_can_compare(ref GLib.Value val, ref GLib.Value val2);

		public static string Serialize(this GLib.Value val){
			return Marshal.PtrToStringAuto (gst_value_serialize(ref val));
		}
		public static bool Deserialize(out GLib.Value val, string src){
			return gst_value_deserialize (out val, Marshal.StringToHGlobalAuto (src));
		}

		public static bool IsFixed(this GLib.Value val)
		{
			return gst_value_is_fixed(ref val);
		}
		public static Caps GetCaps(this GLib.Value val){
			return new Caps(gst_value_get_caps (ref val));
		}
		public static void SetCaps(this GLib.Value val, Caps caps){
			gst_value_set_caps (ref val, caps.Handle);
		}
		public static int Compare(this GLib.Value val, GLib.Value other){
			return gst_value_compare (ref val, ref other);
		}
		public static bool CanCompare(this GLib.Value val, GLib.Value other){
			return gst_value_can_compare (ref val, ref other);
		}
	}  
}

