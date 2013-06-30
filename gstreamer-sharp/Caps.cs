using System;
using System.Runtime.InteropServices;

namespace Gst
{
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

		public override string ToString(){
			return Marshal.PtrToStringAuto (gst_caps_to_string (Handle));
		}
	}
}

