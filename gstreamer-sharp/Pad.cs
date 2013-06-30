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

	public class PadTemplate : Gst.Object
	{

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

