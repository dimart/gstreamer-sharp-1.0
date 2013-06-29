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
	}
}

