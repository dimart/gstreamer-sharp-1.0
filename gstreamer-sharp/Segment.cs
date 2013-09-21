using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public enum SeekType
	{
		None = 0,
		Set  = 1,
		End  = 2
	}

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
		SnapNearest = SnapBefore | SnapAfter
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
		static extern IntPtr gst_segment_new ();
		[DllImport(Application.Dll)]
		static extern void gst_segment_free (IntPtr segment);
		[DllImport(Application.Dll)]
		static extern void gst_segment_init (IntPtr segment, Format format);

		struct GstSegment {
			/*< public >*/
			public SegmentFlags flags;

			double         rate;
			double         applied_rate;

			public Format       format;
			UInt64        @base;
			UInt64         offset;
			UInt64         start;
			UInt64         stop;
			UInt64         time;

			UInt64         position;
			UInt64         duration;

			/* < private > */
			IntPtr        _gst_reserved;
		};

		~Segment(){
			gst_segment_free (Handle);
		}

		public Segment (IntPtr raw) : base(raw)
		{

		}
		public Segment() : this(gst_segment_new ())
		{}

		public void Init(Format format){
			gst_segment_init (Handle,format);
		}

		public Gst.Format Format {
			get {
				GstSegment seg = (GstSegment)Marshal.PtrToStructure (Handle,typeof(GstSegment));
				return seg.format;
			}
			set {
				GstSegment seg = (GstSegment)Marshal.PtrToStructure (Handle,typeof(GstSegment));
				seg.format = value;
				Marshal.StructureToPtr (seg, Handle, true);
			}
		}

		public SegmentFlags Flags {
			get {
				return ((GstSegment)Marshal.PtrToStructure (Handle,typeof(GstSegment))).flags;
			}
			set {
				GstSegment seg = (GstSegment)Marshal.PtrToStructure (Handle,typeof(GstSegment));
				seg.flags = value;
				Marshal.StructureToPtr (seg, Handle, true);
			}
		}
	}
}

