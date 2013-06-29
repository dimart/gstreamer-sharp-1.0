using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Structure
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_get_type();

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_new_empty (IntPtr name);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_copy(IntPtr structure);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_get_name(IntPtr structure);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_get_value(IntPtr structure, IntPtr name);

		[DllImport(Application.Dll)]
		static extern bool gst_structure_get_int(IntPtr structure, IntPtr name, out int val);

		IntPtr handle;

		public Structure Copy ()
		{
			return new Structure(gst_structure_copy (handle));
		}

		public string Name {
			get{
				return GLib.Marshaller.FilenamePtrToString (gst_structure_get_name (handle));
			}
		}

		public Structure (IntPtr raw)
		{
			handle = raw;
		}

		public Structure (string name)
		{
			handle = gst_structure_new_empty (
				GLib.Marshaller.StringToPtrGStrdup (name)
				);

		}

		public IntPtr Handle {
			get{return handle;}
			set{handle = value;}
		}

		public static GLib.GType GType {
			get{
				return new GLib.GType(gst_structure_get_type ());
			}
		}

		public int GetInt (string fieldname)
		{
			IntPtr s = Marshal.StringToHGlobalAuto (fieldname);
			int i;
			gst_structure_get_int(handle,s,out i);
			return i;
		}

		public object GetValue(string fieldname){
			IntPtr s = Marshal.StringToHGlobalAuto (fieldname);
			IntPtr val = gst_structure_get_value(Handle,s);
			return new GLib.Value(val).Val;
		}
	}
}

