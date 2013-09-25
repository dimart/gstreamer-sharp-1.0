using System;
using System.Runtime.InteropServices;

namespace Gst
{
	[Flags]
	public enum EventTypeFlags
	{
		Upstream    = 1 << 0,
		Downstream  = 1 << 1,
		Serialized  = 1 << 2,
		Sticky      = 1 << 3,
		StickyMulti = 1 << 4,
		Both        = EventTypeFlags.Upstream | EventTypeFlags.Downstream
	}

	public enum EventType
	{
		Unknown     = (0 << 8)   | 0,
		FlushStart  = (10 << 8)  | EventTypeFlags.Both,
		FlushStop   = (20 << 8)  | (EventTypeFlags.Both | EventTypeFlags.Serialized),
		StreamStart = (40 << 8)  | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky),
		Caps        = (50 << 8)  | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky),
		Segment     = (70 << 8)  | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky),
		Tag         = (80 << 8)  | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky | EventTypeFlags.StickyMulti),
		BufferSize  = (90 << 8)  | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky),
		SinkMessage = (100 << 8) | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky | EventTypeFlags.StickyMulti),
		EOS         = (110 << 8) | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky),
		Toc         = (120 << 8) | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky | EventTypeFlags.StickyMulti),
		Context     = (130 << 8) | (EventTypeFlags.Downstream | EventTypeFlags.Serialized | EventTypeFlags.Sticky | EventTypeFlags.StickyMulti),
		SegmentDone = (150 << 8) | (EventTypeFlags.Downstream | EventTypeFlags.Serialized),
		Gap         = (160 << 8) | (EventTypeFlags.Downstream | EventTypeFlags.Serialized),
		QOS         = (190 << 8) | EventTypeFlags.Upstream,
		Seek        = (200 << 8) | EventTypeFlags.Upstream,
		Navigation  = (210 << 8) | EventTypeFlags.Upstream,
		Latency     = (220 << 8) | EventTypeFlags.Upstream,
		Step        = (230 << 8) | EventTypeFlags.Upstream,
		Reconfigure = (240 << 8) | EventTypeFlags.Upstream,
		TocSelect   = (250 << 8) | EventTypeFlags.Upstream
	}

	public static class EventUtility
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_event_type_get_name (EventType type);
		[DllImport(Application.Dll)]
		static extern EventTypeFlags gst_event_type_get_flags (EventType type);

		public static string GetName(this EventType type){
			return Marshal.PtrToStringAuto (gst_event_type_get_name (type));
		}
		public static EventTypeFlags GetFlags(this EventType type){
			return gst_event_type_get_flags (type);
		}
	}

	public enum QOSType
	{
		Overflow  = 0,
		Underflow = 1,
		Throttle  = 2
	}

	[Flags]
	public enum StreamFlags
	{
		None,
		Sparse   = 1 << 0,
		Select   = 1 << 1,
		Unselect = 1 << 2
	}


	public class Event : MiniObject
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_event_new_custom (EventType type, IntPtr structure);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_event_get_structure (IntPtr e);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_event_writable_structure (IntPtr e);
		[DllImport(Application.Dll)]
		static extern bool gst_event_has_name (IntPtr e, IntPtr name);
		[DllImport(Application.Dll)]
		static extern uint gst_event_get_seqnum (IntPtr e);
		[DllImport(Application.Dll)]
		static extern void gst_event_set_seqnum (IntPtr e, uint num);

		public Event (IntPtr raw) : base(raw)
		{
		}
		public Event (EventType type, Structure structure)
			: base(gst_event_new_custom (type, structure.Handle))
		{
		}

		public bool HasName(string name){
			return gst_event_has_name (Handle, Marshal.StringToHGlobalAuto (name));
		}

		public Gst.Structure Structure {
			get {
				return new Gst.Structure (gst_event_get_structure (Handle));
			}
		}
		public Gst.Structure WritableStructure {
			get {
				return new Gst.Structure(gst_event_writable_structure (Handle));
			}
		}
		public uint Seqnum {
			get {
				return gst_event_get_seqnum (Handle);
			}
			set {
				gst_event_set_seqnum (Handle, value);
			}
		}
	}
}

