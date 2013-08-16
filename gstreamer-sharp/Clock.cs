using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class ClockID : GLib.Opaque, IComparable<ClockID>
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_clock_id_ref (IntPtr id);
		[DllImport(Application.Dll)]
		static extern void gst_clock_id_unref (IntPtr id);
		[DllImport(Application.Dll)]
		static extern int gst_clock_id_compare_func (IntPtr id1, IntPtr id2);
		[DllImport(Application.Dll)]
		static extern UInt64 gst_clock_id_get_time (IntPtr id);
		[DllImport(Application.Dll)]
		static extern void gst_clock_id_unschedule (IntPtr id);

		~ClockID(){
			gst_clock_id_unref (Handle);
		}

		public ClockID(IntPtr raw) : base(gst_clock_id_ref(raw))
		{
		}

		public int CompareTo(ClockID id){
			return gst_clock_id_compare_func (Handle, id.Handle);
		}

		public UInt64 Time {
			get{ return gst_clock_id_get_time (Handle); }
		}

		public void Unschedule(){
			gst_clock_id_unschedule (Handle);
		}
	}

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
		[DllImport(Application.Dll)]
		static extern void gst_clock_set_timeout (IntPtr clock, UInt64 timeout);
		[DllImport(Application.Dll)]
		static extern UInt64 gst_clock_get_timeout (IntPtr clock);
		[DllImport(Application.Dll)]
		static extern bool gst_clock_single_shot_id_reinit (IntPtr clock, IntPtr id, UInt64 time);
		[DllImport(Application.Dll)]
		static extern bool gst_clock_periodic_id_reinit (IntPtr clock, IntPtr id, UInt64 time, UInt64 interval);
 
		public Clock (IntPtr raw) : base(raw)
		{
		}

		public Clock () : base(IntPtr.Zero)
		{
		}

		public bool SingleShotIdReinit(ClockID id, UInt64 time)
		{
			return gst_clock_single_shot_id_reinit (Handle, id.Handle, time);
		}

		public bool PeriodicIdReinit(ClockID id, UInt64 time, UInt64 interval)
		{
			return gst_clock_periodic_id_reinit (Handle, id.Handle, time, interval);
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

		public UInt64 Timeout {
			get{ return gst_clock_get_timeout (Handle); }
			set{ gst_clock_set_timeout (Handle, value); }
		}
	}
}

