using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Pipeline : Bin
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_pipeline_new(IntPtr name);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_pipeline_get_bus(IntPtr pipeline);
		[DllImport(Application.Dll)]
		static extern bool gst_pipeline_set_clock (IntPtr pipeline, IntPtr clock);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_pipeline_get_clock (IntPtr pipeline);
		[DllImport(Application.Dll)]
		static extern UInt64 gst_pipeline_get_delay (IntPtr pipeline);
		[DllImport(Application.Dll)]
		static extern void gst_pipeline_set_delay (IntPtr pipeline, UInt64 delay);

		public Pipeline (string name) : base(IntPtr.Zero)
		{
			Raw = gst_pipeline_new (Marshal.StringToHGlobalAuto (name));
		}

		public Pipeline (IntPtr raw) : base(raw)
		{

		}

		public new Gst.Bus Bus {
			get{
				return new Gst.Bus(gst_pipeline_get_bus (Handle));
			}
		}

		public Gst.Clock Clock {
			get {
				return new Gst.Clock (gst_pipeline_get_clock (Handle));
			}
			set {
				gst_pipeline_set_clock (Handle, value.Handle);
			}
		}

		public UInt64 Delay {
			get {
				return gst_pipeline_get_delay (Handle);
			}
			set {
				gst_pipeline_set_delay (Handle, value);
			}
		}
	}
}

