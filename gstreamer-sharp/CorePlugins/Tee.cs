using System;
using System.Runtime.InteropServices;

namespace Gst.CorePlugins
{
	public enum TeePullMode
	{
		Never,
		Single
	}

	public class Tee : Element
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make (IntPtr element, IntPtr name);

		public Tee (IntPtr raw) : base(raw)
		{
		}
		public Tee (string name)
			: base(gst_element_factory_make (
				Marshal.StringToHGlobalAuto ("tee"),
				Marshal.StringToHGlobalAuto (name)
				))
		{
		}
		public Tee() : this(null)
		{
		}

		public Pad AllocPad {
			get { return (Pad)this["alloc-pad"]; }
			set { this ["alloc-pad"] = value; }
		}
		public bool HasChain {
			get { return (bool)this ["has-chain"]; }
			set { this ["has-chain"] = value; }
		}
		public string LastMessage {
			get { return (string)this["last-message"]; }
		}
		public int NumSrcPads {
			get { return (int)this ["num-src-pads"]; }
		}
		public TeePullMode PullMode {
			get { return (TeePullMode)this["pull-mode"]; }
			set { this ["pull-mode"] = value; }
		}
		public bool Silent {
			get { return (bool)this ["silent"]; }
			set { this ["silent"] = value; }
		}
	}
}

