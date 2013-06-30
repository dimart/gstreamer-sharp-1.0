using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Structure : GLib.Opaque
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_get_type();
		[DllImport(Application.Dll)]
		static extern void gst_structure_free(IntPtr structure);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_new_empty (IntPtr name);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_copy(IntPtr structure);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_get_name(IntPtr structure);
		[DllImport(Application.Dll)]
		static extern void gst_structure_set_name(IntPtr structure, 
		                                          [MarshalAs(UnmanagedType.LPStr)] string name);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_get_value(IntPtr structure,
		                                             [MarshalAs(UnmanagedType.LPStr)] string name);

		[DllImport(Application.Dll)]
		static extern void gst_structure_set_value(IntPtr structure, [MarshalAs(UnmanagedType.LPStr)] string name,
		                                           ref GLib.Value val);
		[DllImport(Application.Dll)]
		static extern void gst_structure_remove_field(IntPtr structure,
		                                              [MarshalAs(UnmanagedType.LPStr)] string name);
		[DllImport(Application.Dll)]
		static extern void gst_structure_remove_all_fields(IntPtr structure);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_to_string(IntPtr structure);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_from_string([MarshalAs(UnmanagedType.LPStr)] string str,
		                                               [MarshalAs(UnmanagedType.LPStr)] out string end);
		[DllImport(Application.Dll)]
		static extern int gst_structure_n_fields(IntPtr structure);

		public Structure (IntPtr raw) : base(raw)
		{

		}

		public Structure (string name) : base(IntPtr.Zero)
		{
			Raw = gst_structure_new_empty (
				GLib.Marshaller.StringToPtrGStrdup (name)
				);

		}
		public static Structure FromString(string str, out string end){
			return new Structure(gst_structure_from_string (str, out end));
		}

		public Structure Copy ()
		{
			return new Structure(gst_structure_copy (Handle));
		}
		public void RemoveField(string name){
			gst_structure_remove_field (Handle,name);
		}
		public void RemoveFields (params string[] names)
		{
			foreach(string s in names)
				RemoveField (s);
		}
		public void RemoveAllFields ()
		{
			gst_structure_remove_all_fields (Handle);
		}
		public override string ToString(){
			return Marshal.PtrToStringAuto (gst_structure_to_string (Handle));
		}

		public string Name {
			get {
				return GLib.Marshaller.FilenamePtrToString (gst_structure_get_name (Handle));
			}
			set{
				gst_structure_set_name (Handle,value);
			}
		}

		public static GLib.GType GType {
			get{
				return new GLib.GType(gst_structure_get_type ());
			}
		}

		public object this [string fieldname] {
			get{
				IntPtr val = gst_structure_get_value(Handle,fieldname);
				return new GLib.Value(val).Val;
			}
			set{
				var val = new GLib.Value(value);
				gst_structure_set_value (Handle,fieldname,ref val);
				val.Dispose ();
			}
		}
		public int FieldCount {
			get{return gst_structure_n_fields (Handle);}
		}
	}
}

