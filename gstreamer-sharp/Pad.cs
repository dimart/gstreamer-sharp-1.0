using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public enum FlowReturn
	{
		Ok		     =  0,
		Linked       = -1,
		Flushing     = -2,
		Eos          = -3,
		Negotiated   = -4,
		Error	     = -5,
		NotSupported = -6
	}

	public enum PadDirection
	{
		Unknown, Src, Sink
	}

	public enum PadPresence
	{
		Always, Sometimes, Request
	}

	[Flags]
	public enum PadTemplateFlags
	{
		Last = ObjectFlags.Last << 4
	}

	public class PadTemplate : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_pad_template_new([MarshalAs(UnmanagedType.LPStr)] string name,
		                                          PadDirection direction, PadPresence presence,
		                                          IntPtr caps);

		public PadTemplate(IntPtr raw) : base(raw)
		{}
		public PadTemplate (string name, PadDirection direction, PadPresence presence, Caps caps) : base(IntPtr.Zero)
		{
			Raw = gst_pad_template_new (name,direction,presence,caps.Handle);
		}
	}

	public class Pad : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_pad_new(IntPtr name, int direction);

		public Pad (IntPtr raw) : base(raw)
		{
		}

		public Pad(string name, PadDirection direction){
			Raw = gst_pad_new (
				Marshal.StringToHGlobalAuto (name),
				(int)direction
				);
		}
	}
}

