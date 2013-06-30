using System;
using System.Runtime.InteropServices;

namespace Gst
{
	[Flags]
	public enum SeekFlags
	{
		None        = 0,
		Flush       = 1 << 0,
		Accurate    = 1 << 1,
		KeyUnit     = 1 << 2,
		Segment     = 1 << 3,
		Skip        = 1 << 4,
		SnapBefore  = 1 << 5,
		SnapAfter   = 1 << 6,
		SnapNearest = SeekFlags.SnapBefore | SeekFlags.SnapAfter
	}

	[Flags]
	public enum SegmentFlags
	{
		None    = SeekFlags.None,
		Reset   = SeekFlags.Flush,
		Skip    = SeekFlags.Skip,
		Segment = SeekFlags.Segment
	}

	public class Segment : GLib.Opaque
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_segment_new();
		[DllImport(Application.Dll)]
		static extern void gst_segment_init(IntPtr segment, Format format);
		[DllImport(Application.Dll)]
		static extern void  gst_segment_free(IntPtr segment);
		[DllImport(Application.Dll)]
		static extern ulong gst_segment_to_stream_time(IntPtr segment, Format format, ulong position);
		[DllImport(Application.Dll)]
		static extern ulong gst_segment_to_running_time(IntPtr segment, Format format, ulong position);

		~Segment(){
			gst_segment_free (Handle);
		}
		public Segment (IntPtr raw) : base(raw)
		{
		}
		public Segment () : base(IntPtr.Zero)
		{
			Raw = gst_segment_new ();
		}

		public void Init(Format format){
			gst_segment_init (Handle,format);
		}
		public ulong ToStreamTime(Format format, ulong position){
			return gst_segment_to_stream_time (Handle,format,position);
		}
		public ulong ToRunningTime(Format format, ulong position){
			return gst_segment_to_running_time (Handle,format,position);
		}
	}
}

