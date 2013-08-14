using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public enum LockFlags
	{
		Read      = 1 << 0,
		Write     = 1 << 1,
		Exclusive = 1 << 2,
		Last      = 1 << 8
	}

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
			public int flags;
			public IntPtr data;
			public long size;
			public long maxsize;
			IntPtr user_data;
			IntPtr _gst_reserved;
		}

		IntPtr ptr;

		public MapInfo (IntPtr raw)
		{
			ptr = raw;
		}

		public MapFlags Flags {
			get{
				GstMapInfo info = (GstMapInfo)Marshal.PtrToStructure (ptr,typeof(GstMapInfo));
				return (MapFlags)info.flags;
			}
		}

		public byte[] Data {
			get{
				GstMapInfo info = (GstMapInfo)Marshal.PtrToStructure (ptr,typeof(GstMapInfo));
				Console.WriteLine (info.size);
//			Marshal.Copy (info.data,data,0,(int)info.size);
				return new byte[]{1};
			}
		}

		public long MaxSize {
			get{
				GstMapInfo info = (GstMapInfo)Marshal.PtrToStructure (ptr,typeof(GstMapInfo));
				return info.maxsize;
			}
		}
	}
}

