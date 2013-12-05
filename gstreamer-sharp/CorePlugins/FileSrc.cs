using System;
using System.Runtime.InteropServices;

namespace Gst.CorePlugins
{
	public class FileSrc : Gst.Base.Src
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make (IntPtr element, IntPtr name);

		public FileSrc (IntPtr raw) : base(raw)
		{

		}
		public FileSrc (string name)
			: base(gst_element_factory_make (
				Marshal.StringToHGlobalAuto ("filesrc"),
				Marshal.StringToHGlobalAuto (name)
				))
		{

		}
		public FileSrc() : this(null)
		{}

		public string Location {
			get { return (string)this ["location"]; }
			set { this ["location"] = value; }
		}
	}
}

