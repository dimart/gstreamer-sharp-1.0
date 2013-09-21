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
		[DllImport(Application.BaseDll)]
		static extern void gst_base_src_set_live (IntPtr src, bool live);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_src_is_live (IntPtr src);

		public BaseSrc (IntPtr raw) : base(raw)
		{
		}

		protected Caps GetCaps(Caps filter) {
			return new Caps (gstsharp_basesrc_get_caps(Handle,filter.Handle));
		}
		protected Caps Fixate (Caps caps){
			return new Caps (gstsharp_basesrc_fixate(Handle,caps.Handle));
		}

		public bool Live {
			get{ return gst_base_src_is_live (Handle); }
			set{ gst_base_src_set_live (Handle, value); }
		}
	}
}

