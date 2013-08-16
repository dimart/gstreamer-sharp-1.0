using System;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class BaseSrc : Element
	{
		[DllImport(Application.GlueDll)]
		static extern IntPtr gstsharp_basesrc_get_caps (IntPtr src, IntPtr filter);
		[DllImport(Application.GlueDll)]
		static extern IntPtr gstsharp_basesrc_fixate (IntPtr src, IntPtr caps);

		public BaseSrc (IntPtr raw) : base(raw)
		{
		}

		protected Caps GetCaps(Caps filter) {
			return new Caps (gstsharp_basesrc_get_caps(Handle,filter.Handle));
		}
		protected Caps Fixate (Caps caps){
			return new Caps (gstsharp_basesrc_fixate(Handle,caps.Handle));
		}
	}
}

