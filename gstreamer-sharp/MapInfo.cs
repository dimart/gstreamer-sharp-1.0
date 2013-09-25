using System;
using System.Runtime.InteropServices;

namespace Gst
{

	public enum MapFlags
	{
		Read = LockFlags.Read,
		Write = LockFlags.Write,
		Last = 1 << 16
	}

	public class MapInfo
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct GstMapInfo{
			public IntPtr memory;
			public MapFlags flags;
			public IntPtr data;
			public long size;
			public long maxsize;
			IntPtr user_data;
			IntPtr _gst_reserved;
		}

		GstMapInfo info;

		public MapInfo (GstMapInfo raw)
		{
			info = raw;
		}

		public MapFlags Flags {
			get{
				return info.flags;
			}
		}

		public byte[] Data {
			get{
				byte[] data = new byte[info.size];
				Marshal.Copy (info.data,data,0,(int)info.size);
				return data;
			}
		}

		public long MaxSize {
			get{
				return info.maxsize;
			}
		}
	}
}

