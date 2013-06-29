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
			public int flags;
			public IntPtr data;
			public long size;
			public long maxsize;
			IntPtr user_data;
			IntPtr _gst_reserved;
		}
		[DllImport(Application.GlueDll)]
		static extern void gstsharp_memory_get_info_data(IntPtr memory, int flags, out IntPtr data, out long length);

		public GstMapInfo i;

		public MapInfo (GstMapInfo info)
		{
			i = info;
		}

		public Gst.Memory Memory {
			get{return new Gst.Memory(i.memory);}
		}

		public MapFlags Flags {
			get{
				return (MapFlags)i.flags;
			}
		}
		public byte[] Data {
			get{
				IntPtr o;
				long l;
				gstsharp_memory_get_info_data (i.memory,i.flags,out o, out l);
				byte[] data = new byte[l];
				Marshal.Copy (o,data,0,(int)l);
				return data;
			}
		}
		public long MaxSize {
			get{
				return i.maxsize;
			}
		}
	}
}

