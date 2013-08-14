using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public enum MessageType
	{
		Unknown = 0,
		Eos = 1 << 0,
		Error = 1 << 1,
		Warning = 1 << 2,
		Info = 1 << 3,
		Tag = 1 << 4,
		Buffering = 1 << 5,
		StateChanged = 1 << 6,
		StateDirty = 1 << 7,
		StepDone = 1 << 8,
		ClockProvide = 1 << 9,
		ClockLost = 1 << 10,
		NewClock = 1 << 11,
		StructureChange = 1 << 12,
		StreamStatus = 1 << 13,
		Application = 1 << 14,
		Element = 1 << 15,
		SegmentStart = 1 << 16,
		SegmentDone = 1 << 17,
		DurationChanged = 1 << 18,
		Latency = 1 << 19,
		AsyncStart = 1 << 20,
		AsyncDone = 1 << 21,
		RequestState = 1 << 22,
		StepStart = 1 << 23,
		Qos = 1 << 24,
		Progress = 1 << 25,
		Toc = 1 << 26,
		ResetTime = 1 << 27,
		StreamStart = 1 << 28,
		Any = ~0
	}

	public class Message : MiniObject
	{
		[DllImport(Application.Dll,CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_message_get_type();

		[DllImport(Application.Dll)]
		static extern IntPtr gst_message_get_structure(IntPtr message);

		[DllImport(Application.GlueDll)]
		static extern uint gstsharp_gst_message_get_src_offset ();

		
		public Message (IntPtr raw) : base(raw)
		{
			
		}

		public unsafe Gst.Object Src {
			get{
				uint src_offset = gstsharp_gst_message_get_src_offset();
				IntPtr* raw_ptr = (IntPtr*) ( ( (byte*) Handle) + src_offset);
      			return GLib.Object.GetObject ( (*raw_ptr)) as Gst.Object;
			}
		}

		public Gst.Structure Structure {
			get{
				return new Gst.Structure(gst_message_get_structure (Handle));
			}
		}

		public static new GLib.GType GType{
			get{return new GLib.GType(gst_message_get_type ());}
		}
	}
}

