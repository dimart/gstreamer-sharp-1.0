using System;
using System.Runtime.InteropServices;

namespace Gst
{
	[Flags]
	public enum CapsFlags
	{
		Any = MiniObjectFlags.Last << 0
	}

	public enum CapsIntersectMode
	{
		ZigZag = 0,
		First  = 1
	}

	public class Caps : MiniObject
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_new_empty();
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_new_empty_simple (IntPtr mtype);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_from_string(IntPtr str);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_to_string (IntPtr caps);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_get_structure(IntPtr caps, uint index);
		[DllImport(Application.Dll)]
		static extern uint gst_caps_get_size(IntPtr caps);
		[DllImport(Application.Dll)]
		static extern void gst_caps_append (IntPtr caps1, IntPtr caps2);
		[DllImport(Application.Dll)]
		static extern void gst_caps_append_structure (IntPtr caps, IntPtr structure);
		[DllImport(Application.Dll)]
		static extern void gst_caps_append_structure_full (IntPtr caps, IntPtr structure, IntPtr cf);
		[DllImport(Application.Dll)]
		static extern void gst_caps_merge (IntPtr caps1, IntPtr caps2);
		[DllImport(Application.Dll)]
		static extern void gst_caps_merge_structure (IntPtr caps, IntPtr structure);
		[DllImport(Application.Dll)]
		static extern void gst_caps_merge_structure_full (IntPtr caps, IntPtr structure, IntPtr cf);
		[DllImport(Application.Dll)]
		static extern void gst_caps_remove_structure (IntPtr caps, uint index);
		[DllImport(Application.Dll)]
		static extern void gst_caps_set_features (IntPtr caps, uint index, IntPtr cf);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_get_features (IntPtr caps, uint index);
		[DllImport(Application.Dll)]
		static extern void gst_caps_set_value (IntPtr caps, IntPtr name, ref GLib.Value val);

		public Caps (IntPtr raw) : base(raw)
		{
		}

		public Caps() : base(gst_caps_new_empty ())
		{

		}
		public Caps(string mtype)
			: base(gst_caps_new_empty_simple (Marshal.StringToHGlobalAuto (mtype)))
		{

		}

		public static Caps FromString(string val){
			return new Caps (gst_caps_from_string (Marshal.StringToHGlobalAuto (val)));
		}

		public Structure this [uint index] {
			get{
				return new Structure(gst_caps_get_structure(Handle,index));
			}
		}

		public uint Size {
			get{
				return gst_caps_get_size (Handle);
			}
		}

		public void Append(Caps caps){
			gst_caps_append (Handle, caps.Handle);
		}
		public void Append(Structure structure){
			gst_caps_append_structure (Handle, structure.Handle);
		}
		public void Append (Structure structure, CapsFeatures features){
			gst_caps_append_structure_full (Handle, structure.Handle, features.Handle);
		}
		public void Merge(Caps caps){
			gst_caps_merge (Handle, caps.Handle);
		}
		public void Merge(Structure structure){
			gst_caps_merge_structure (Handle, structure.Handle);
		}
		public void Merge(Structure structure, CapsFeatures features){
			gst_caps_merge_structure_full (Handle, structure.Handle, features.Handle);
		}
		public void Remove (uint index){
			gst_caps_remove_structure (Handle, index);
		}
		public CapsFeatures GetFeatures(uint index){
			return new CapsFeatures (gst_caps_get_features (Handle, index));
		}
		public void SetFeatures(uint index, CapsFeatures features){
			gst_caps_set_features (Handle, index, features.Handle);
		}
		public void SetValue (string name, GLib.Value val){
			gst_caps_set_value (Handle, Marshal.StringToHGlobalAuto (name), ref val);
		}
		public void SetValue (string name, object o){
			GLib.Value val = new GLib.Value (o);
			gst_caps_set_value (Handle, Marshal.StringToHGlobalAuto (name), ref val);
		}

		public override string ToString(){
			return Marshal.PtrToStringAuto (gst_caps_to_string (Handle));
		}
	}
}

