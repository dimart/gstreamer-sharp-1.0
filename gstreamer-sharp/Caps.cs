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
		static extern IntPtr gst_caps_from_string([MarshalAs(UnmanagedType.LPStr)]string str);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_to_string (IntPtr caps);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_get_structure(IntPtr caps, int index);
		[DllImport(Application.Dll)]
		static extern uint gst_caps_get_size(IntPtr caps);
		[DllImport(Application.Dll)]
		static extern void gst_caps_append (IntPtr caps1, IntPtr caps2);
		[DllImport(Application.Dll)]
		static extern void gst_caps_append_structure (IntPtr caps, IntPtr structure);
		[DllImport(Application.Dll)]
		static extern void gst_caps_merge (IntPtr caps1, IntPtr caps2);
		[DllImport(Application.Dll)]
		static extern void gst_caps_merge_structure (IntPtr caps, IntPtr structure);

		public Caps (IntPtr raw) : base(raw)
		{
		}

		public Caps(){
			Handle = gst_caps_new_empty ();
		}
		public Caps(string caps_string){
			Handle = gst_caps_from_string (caps_string);
		}

		public Structure this [int index] {
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
		public void Merge(Caps caps){
			gst_caps_merge (Handle, caps.Handle);
		}
		public void Merge(Structure structure){
			gst_caps_merge_structure (Handle, structure.Handle);
		}

		public override string ToString(){
			return Marshal.PtrToStringAuto (gst_caps_to_string (Handle));
		}
	}
}

