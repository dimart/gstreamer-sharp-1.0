using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Bin : Element
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_bin_new(IntPtr name);
		[DllImport(Application.Dll)]
		static extern bool gst_bin_add(IntPtr bin, IntPtr element);
		[DllImport(Application.Dll)]
		static extern bool gst_bin_remove(IntPtr bin, IntPtr element);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_bin_get_by_name(IntPtr bin, IntPtr name);

		public Bin (IntPtr raw) : base(raw)
		{
		}

		public Bin (string name) : base(IntPtr.Zero)
		{
			Raw = gst_bin_new (Marshal.StringToHGlobalAuto (name));
		}

		public bool Add (Element e)
		{
			return gst_bin_add (Raw,e.Handle);
		}

		public bool Remove (Element e)
		{
			return gst_bin_remove (Raw,e.Handle);
		}

		public Element GetByName(string name) {
				return new Element(gst_bin_get_by_name (Raw,Marshal.StringToHGlobalAuto(name)));
		}
	}
}

