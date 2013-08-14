using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class FakeObject : Gst.Object
	{
		[DllImport ("gobject-2.0")]
private static extern IntPtr g_type_name (IntPtr raw);
				[DllImport (Application.GlueDll)]
		private static extern IntPtr gstsharp_g_type_from_instance (IntPtr raw);

		public FakeObject (IntPtr raw) : base(raw)
		{
		}

		public string NameType {
			get{
				return Marshal.PtrToStringAuto (g_type_name (gstsharp_g_type_from_instance(Handle)));
			}
		}
	}
}

