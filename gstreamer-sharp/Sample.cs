using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Sample : GLib.Opaque
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_sample_new(IntPtr buffer, IntPtr caps, 
		                                    IntPtr segment, IntPtr structure);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_sample_get_caps(IntPtr sample);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_sample_get_buffer(IntPtr sample);

		public Sample (IntPtr raw) : base(raw)
		{
		}

		public Gst.Caps Caps {
			get{
				return new Gst.Caps(gst_sample_get_caps(Handle));
			}
		}
		public Gst.Buffer Buffer {
			get{
				return new Gst.Buffer(gst_sample_get_buffer(Handle));
			}
		}
	}
}

