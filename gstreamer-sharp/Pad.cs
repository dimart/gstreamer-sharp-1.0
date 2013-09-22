using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public enum PadDirection
	{
		Unknown, Src, Sink
	}

	public enum PadPresence
	{
		Always, Sometimes, Request
	}

	public enum FlowReturn
	{
		Ok            = 0,
		NotLinked     = -1,
		Flushing      = -2,
		EOS           = -3,
		NotNegociated = -4,
		Error         = -5,
		NotSupported  = -6
	}

	public enum PadLinkCheck
	{
		Nothing      = 0,
		Hierarchy    = 1 << 0,
		TemplateCaps = 1 << 1,
		Caps         = 1 << 2,
		Default      = PadLinkCheck.Hierarchy | PadLinkCheck.Caps
	}

	public class PadTemplate : Gst.Object
	{

	}

	public class Pad : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_pad_new(IntPtr name, int direction);
		[DllImport(Application.Dll)]
		static extern void gst_pad_set_active (IntPtr pad, bool active);
		[DllImport(Application.Dll)]
		static extern bool gst_pad_is_active (IntPtr pad);
		
		public Pad (IntPtr raw) : base(raw)
		{
		}

		public Pad(string name, PadDirection direction)
			: this(gst_pad_new (
				Marshal.StringToHGlobalAuto (name),
				(int)direction
				))
		{

		}

		public bool Active {
			get { return gst_pad_is_active (Handle); }
			set { gst_pad_set_active (Handle, value); }
		}

		[GLib.Signal("linked")]
		public event PadHandler Linked {
			add{
				base.AddSignalHandler ("linked",value,typeof(PadHandlerArgs));
			}
			remove{
				base.RemoveSignalHandler ("linked", value);
			}
		}

		[GLib.Signal("unlinked")]
		public event PadHandler Unlinked {
			add{
				base.AddSignalHandler ("unlinked",value,typeof(PadHandlerArgs));
			}
			remove{
				base.RemoveSignalHandler ("unlinked", value);
			}
		}
	}
}

