using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public delegate void StructureForeachFunc(string fieldname, object val);

	public class Structure : GLib.Opaque, ICloneable
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
		static extern void gst_structure_set_name (IntPtr structure, IntPtr name);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_get_value(IntPtr structure, IntPtr name);
		[DllImport(Application.Dll)]
		static extern void gst_structure_set_value (IntPtr structure, IntPtr name, ref GLib.Value val);

		[DllImport(Application.Dll)]
		static extern bool gst_structure_get_int(IntPtr structure, IntPtr name, out int val);
		[DllImport(Application.Dll)]
		static extern void gst_structure_remove_field (IntPtr structure, IntPtr name);
		[DllImport(Application.Dll)]
		static extern void gst_structure_remove_all_fields (IntPtr structure);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_nth_field_name (IntPtr structure, uint index);
		[DllImport(Application.Dll)]
		static extern uint gst_structure_n_fields (IntPtr structure);
		[DllImport(Application.Dll)]
		static extern bool gst_structure_has_field_typed (IntPtr structure, IntPtr name, IntPtr type);
		[DllImport(Application.Dll)]
		static extern bool gst_structure_has_field (IntPtr structure, IntPtr name);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_from_string (IntPtr str, out IntPtr end);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_structure_to_string (IntPtr structure);

		public Structure Copy ()
		{
			return new Structure(gst_structure_copy (Handle));
		}

		public object Clone(){
			return Copy ();
		}

		public Structure (IntPtr raw) : base(raw)
		{

		}

		public Structure (string name)
				: this(gst_structure_new_empty (
					GLib.Marshaller.StringToPtrGStrdup (name)
					))
		{
		}
		public static Structure FromString (string str, out string end){
			IntPtr e;
			IntPtr sptr = gst_structure_from_string (Marshal.StringToHGlobalAuto (str),
			                                         out e);
			end = Marshal.PtrToStringAuto (e);
			return new Structure (sptr);
		}

		public static GLib.GType GType {
			get{
				return new GLib.GType(gst_structure_get_type ());
			}
		}

		public string GetFieldName(uint index){
			return Marshal.PtrToStringAuto (gst_structure_nth_field_name (Handle, index));
		}

		public GLib.Value GetValue(string fieldname){
			IntPtr s = Marshal.StringToHGlobalAuto (fieldname);
			IntPtr val = gst_structure_get_value(Handle,s);
			return new GLib.Value(val);
		}
		public void SetValue(string fieldname, GLib.Value val){
			gst_structure_set_value (Handle, 
			                         Marshal.StringToHGlobalAuto (fieldname),
			                         ref val);
		}
		public void RemoveField(string fieldname){
			gst_structure_remove_field (Handle, Marshal.StringToHGlobalAuto (fieldname));
		}
		public void RemoveFields(params string[] fieldnames){
			foreach (string fieldname in fieldnames)
				RemoveField (fieldname);
		}
		public void RemoveFields(){
			gst_structure_remove_all_fields (Handle);
		}
		public bool HasField(string fieldname){
			return gst_structure_has_field (Handle, Marshal.StringToHGlobalAuto (fieldname));
		}
		public bool HasField(string fieldname, GLib.GType type){
			return gst_structure_has_field_typed (Handle, Marshal.StringToHGlobalAuto (fieldname),
			                                      type.Val);
		}

		public void Foreach(StructureForeachFunc func){
			for (uint i = 0; i < FieldCount; i++)
				func (GetFieldName (i), this [GetFieldName (i)]);
		}
		public override string ToString ()
		{
			return Marshal.PtrToStringAuto (gst_structure_to_string (Handle));
		}

		public object this [string fieldname] {
			set {
 				GLib.Value val = new GLib.Value (value);
				gst_structure_set_value (Handle, 
				                         Marshal.StringToHGlobalAuto (fieldname),
				                         ref val);
			}
			get {
				IntPtr s = Marshal.StringToHGlobalAuto (fieldname);
				IntPtr val = gst_structure_get_value(Handle,s);
				return new GLib.Value(val).Val;
			}
		}

		public string Name {
			get{
				return GLib.Marshaller.FilenamePtrToString (gst_structure_get_name (Handle));
			}
			set{
				gst_structure_set_name (Handle, Marshal.StringToHGlobalAuto (value));
			}
		}

		public uint FieldCount{
			get {
				return gst_structure_n_fields (Handle);
			}
		}
	}
}

