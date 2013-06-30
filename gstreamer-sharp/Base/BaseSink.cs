using System;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class BaseSink : Element
	{
		public BaseSink (IntPtr raw) : base(raw)
		{
		}

		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_sync(IntPtr sink, bool sync);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_sink_get_sync(IntPtr sink);

		public bool Sync {
			get{return gst_base_sink_get_sync (Handle);}
			set{gst_base_sink_set_sync(Handle,value);}
		}
	}
}

