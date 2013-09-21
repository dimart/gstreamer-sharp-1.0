using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class CapsFeatures : GLib.Opaque
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_caps_features_new_empty ();

		public CapsFeatures (IntPtr raw) : base(raw)
		{
		}
		public CapsFeatures() 
			: this(gst_caps_features_new_empty ())
		{
		}
	}
}

