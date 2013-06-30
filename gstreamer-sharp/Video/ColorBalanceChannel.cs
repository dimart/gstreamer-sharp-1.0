using System;
using System.Runtime.InteropServices;

namespace Gst.Video
{
	public delegate void ColorBalanceChannelHandler(object o,ColorBalanceChannelArgs args);

	public class ColorBalanceChannelArgs : GLib.SignalArgs
	{
		public int Value {
			get{return (int)base.Args[0];}
		}
	}

	public class ColorBalanceChannel : GLib.Object
	{
		[StructLayout(LayoutKind.Sequential)]
		struct cbc
		{
			public IntPtr parent;
			public IntPtr label;
			public int min_value;
			public int max_value;

			IntPtr _gst_reserved;
		}

		public ColorBalanceChannel (IntPtr raw) : base(raw)
		{
		}

		public string Label {
			get{
				cbc channel = (cbc)Marshal.PtrToStructure (Handle,typeof(cbc));
				return Marshal.PtrToStringAuto (channel.label);
			}
		}

		public int MinValue {
			get{
				cbc channel = (cbc)Marshal.PtrToStructure (Handle,typeof(cbc));
				return channel.min_value;
			}
		}

		public int MaxValue {
			get{
				cbc channel = (cbc)Marshal.PtrToStructure (Handle,typeof(cbc));
				return channel.max_value;
			}
		}

		[GLib.Signal("value-changed")]
		public event ColorBalanceChannelHandler ValueChanged {
			add{
				AddSignalHandler ("value-changed",value,typeof(ColorBalanceChannelArgs));
			}
			remove{
				RemoveSignalHandler ("value-changed",value);
			}
		}
	}


}

