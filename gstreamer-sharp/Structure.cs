using System;
using System.Runtime.InteropServices;
using GstSharp;

namespace Gst
{
	public delegate bool StructureFunc(uint quark, ref GLib.Value val);

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
		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_nth_field_name(IntPtr structure, int index);
		[DllImport(Application.Dll)]
		static extern bool gst_structure_foreach(IntPtr structure, StructureFuncNative native, IntPtr data);
		[DllImport(Application.Dll)]
		static extern bool gst_structure_map_in_place(IntPtr structure, StructureFuncNative native, IntPtr data);

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
		public bool Foreach (StructureFunc func)
		{
			StructureFuncWrapper wrapper = new StructureFuncWrapper(func);
			IntPtr data = (IntPtr)GCHandle.Alloc (wrapper);
			return gst_structure_foreach (Handle,wrapper.native,data);
		}
		public bool MapInPlace (StructureFunc func)
		{
			StructureFuncWrapper wrapper = new StructureFuncWrapper(func);
			IntPtr data = (IntPtr)GCHandle.Alloc (wrapper);
			return gst_structure_map_in_place (Handle,wrapper.native,data);
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
				return GetValue (fieldname).Val;
			}
			set{
				SetValue (fieldname,new GLib.Value(value));
			}
		}
		public int FieldCount {
			get{return gst_structure_n_fields (Handle);}
		}
		public string FieldName(int index){
			return Marshal.PtrToStringAuto (gst_structure_nth_field_name (Handle,index));
		}
		public GLib.Value GetValue(string fieldname){
			return new GLib.Value(gst_structure_get_value (Handle,fieldname));
		}
		public void SetValue(string fieldname, GLib.Value val){
			gst_structure_set_value (Handle,fieldname,ref val);
		}
	}
}

