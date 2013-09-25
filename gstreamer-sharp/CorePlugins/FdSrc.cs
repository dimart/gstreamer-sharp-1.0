using System;
using System.Runtime.InteropServices;

namespace Gst.CorePlugins
{
	public class FdSrc : Gst.Base.PushSrc
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make (IntPtr element, IntPtr name);

		public FdSrc (IntPtr raw) : base(raw)
		{
		}
		public FdSrc (string name)
			: base(gst_element_factory_make (
				Marshal.StringToHGlobalAuto ("fdsrc"),
				Marshal.StringToHGlobalAuto (name)
				))
		{

		}
		public FdSrc() : this(null)
		{
		}

		public int FD {
			get { return (int)this ["fd"]; }
			set { this ["fd"] = value; }
		}
		public ulong Timeout {
			get { return (ulong)this ["timeout"]; }
			set { this ["timeout"] = value; }
		}
	}
}

