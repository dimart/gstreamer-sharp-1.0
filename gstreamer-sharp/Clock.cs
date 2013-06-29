using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Clock : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern UInt64 gst_clock_set_resolution(IntPtr clock, UInt64 resolution);
		[DllImport(Application.Dll)]
		static extern UInt64 gst_clock_get_resolution(IntPtr clock);
		[DllImport(Application.Dll)]
		static extern UInt64 gst_clock_get_time(IntPtr clock);
		[DllImport(Application.Dll)]
		static extern bool gst_clock_set_master(IntPtr clock, IntPtr master);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_clock_get_master(IntPtr clock);
 
		public Clock (IntPtr raw) : base(raw)
		{
		}

		public Clock () : base(IntPtr.Zero)
		{
		}

		public UInt64 Resolution {
			get{return gst_clock_get_resolution (Handle);}
			set{gst_clock_set_resolution (Handle,value);}
		}

		public UInt64 Time {
			get{return gst_clock_get_time(Handle);}
		}

		public Clock Master {
			get{return new Clock(gst_clock_get_master(Handle));}
			set{gst_clock_set_master (Handle,value.Handle);}
		}
	}
}

