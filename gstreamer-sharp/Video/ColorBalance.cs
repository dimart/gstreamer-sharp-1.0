using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gst.Video
{
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
	}

	public interface ColorBalance
	{
		List<ColorBalanceChannel> Channels{get;}
		int GetValue(ColorBalanceChannel channel);
		void SetValue(ColorBalanceChannel channel, int val);
	}
}

