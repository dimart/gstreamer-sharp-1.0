using System;
using System.Runtime.InteropServices;

namespace Gst.CorePlugins
{
	public class FdSink : Gst.Base.Sink
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make (IntPtr element, IntPtr name);

		public FdSink (IntPtr raw) : base(raw)
		{
		}
		public FdSink(string name)
			: base(gst_element_factory_make (
				Marshal.StringToHGlobalAuto ("fdsink"),
				Marshal.StringToHGlobalAuto (name)
				))
		{
		}
		public FdSink() : this(null)
		{
		}

		public int FD {
			get { return (int)this ["fd"]; }
			set { this ["fd"] = value; }
		}
	}
}

