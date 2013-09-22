using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class CapsFeatures : GLib.Opaque, IEquatable<CapsFeatures>
	{
		[DllImport(Application.Dll)]
		static extern void gst_caps_features_free (IntPtr cf);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_features_new_empty ();
		[DllImport(Application.Dll)]
		static extern void gst_caps_features_add (IntPtr cf, IntPtr feature);
		[DllImport(Application.Dll)]
		static extern void gst_caps_features_add_id (IntPtr cf, uint feature_id);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_features_to_string (IntPtr cf);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_features_from_string (IntPtr str);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_features_get_nth (IntPtr cf, uint index);
		[DllImport(Application.Dll)]
		static extern uint gst_caps_features_get_size (IntPtr cf);
		[DllImport(Application.Dll)]
		static extern bool gst_caps_features_contains (IntPtr cf, IntPtr str);
		[DllImport(Application.Dll)]
		static extern bool gst_caps_features_contains_id (IntPtr cf, uint id);
		[DllImport(Application.Dll)]
		static extern void gst_caps_features_remove (IntPtr cf, IntPtr str);
		[DllImport(Application.Dll)]
		static extern void gst_caps_features_remove_id (IntPtr cf, uint id);
		[DllImport(Application.Dll)]
		static extern bool gst_caps_features_is_equal (IntPtr cf1, IntPtr cf2);

		~CapsFeatures(){
			gst_caps_features_free (Handle);
		}

		public CapsFeatures (IntPtr raw) : base(raw)
		{
		}
		public CapsFeatures() 
			: this(gst_caps_features_new_empty ())
		{
		}
		public CapsFeatures(params string[] features)
			: this()
		{
			foreach (string s in features)
				Add (s);
		}
		public CapsFeatures(params uint[] ids) : this()
		{
			foreach (uint id in ids)
				Add (id);
		}
		public static CapsFeatures FromString (string str){
			return new CapsFeatures (gst_caps_features_from_string (Marshal.StringToHGlobalAuto (str)));
		}

		public void Add(string feature){
			gst_caps_features_add (Handle, Marshal.StringToHGlobalAuto (feature));
		}
		public void Add(uint id){
			gst_caps_features_add_id (Handle, id);
		}
		
		public bool Contains (string id) {
			return gst_caps_features_contains (Handle, Marshal.StringToHGlobalAuto (id));
		}
		public bool Contains (uint id) {
			return gst_caps_features_contains_id (Handle, id);
		}
		public void Remove (string id) {
			gst_caps_features_remove (Handle, Marshal.StringToHGlobalAuto (id));
		}
		public void Remove (uint id) {
			gst_caps_features_remove_id (Handle, id);
		}

		public override string ToString ()
		{
			return Marshal.PtrToStringAuto (gst_caps_features_to_string (Handle));
		}

		public void Clear () {
			while (Size > 0)
				Remove (this[0]);
		}

		public bool Equals (CapsFeatures other){
			return gst_caps_features_is_equal (Handle, other.Handle);
		}

		public string this [uint index] {
			get {
				return Marshal.PtrToStringAuto (gst_caps_features_get_nth (Handle, index));
			}
		}

		public uint Size {
			get {
				return gst_caps_features_get_size (Handle);
			}
		}
	}
}

