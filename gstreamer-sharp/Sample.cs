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
		[DllImport(Application.Dll)]
		static extern IntPtr gst_sample_get_segment(IntPtr sample);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_sample_get_info(IntPtr sample);

		public Sample (IntPtr raw) : base(raw)
		{
		}
		public Sample(Buffer buffer, Caps caps, Segment segment, Structure structure) : 
			base(gst_sample_new (buffer.Handle,caps.Handle,segment.Handle,structure.Handle))
		{}

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
		public Gst.Segment Segment {
			get{
				return new Gst.Segment (gst_sample_get_segment (Handle));
			}
		}
		public Structure Info {
			get{
				return new Structure (gst_sample_get_info (Handle));
			}
		}
	}
}

