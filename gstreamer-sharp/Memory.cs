using System;
using System.Runtime.InteropServices;

namespace Gst
{
	[Flags]
	public enum MemoryFlags
	{
		ReadOnly = MiniObjectFlags.LockReadOnly,
		NoShare = MiniObjectFlags.Last << 0,
		ZeroPrefixed = MiniObjectFlags.Last << 1,
		ZeroPadded = MiniObjectFlags.Last << 2,
		PhysicallyContiguous = MiniObjectFlags.Last << 3,
		NotMappable = MiniObjectFlags.Last << 4,
		Last = MiniObjectFlags.Last << 16
	}

	public class Memory : MiniObject
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate IntPtr GstMemoryMapFunction(IntPtr memory, long maxsize, int flags);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate void GstMemoryUnmapFunction(IntPtr memory);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate IntPtr GstMemoryCopyFunction(IntPtr memory, long offset, long size);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate IntPtr GstMemoryShareFunction(IntPtr memory, long offset, long size);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate bool GstMemoryIsSpanFunction(IntPtr memory, IntPtr o, out  long offset);

		[DllImport(Application.Dll)]
		static extern bool gst_memory_map (IntPtr memory, out MapInfo.GstMapInfo info, MapFlags flags);

		[StructLayout(LayoutKind.Sequential)]
		struct GstAllocator
		{
			IntPtr  @object;

			IntPtr							 mem_type;

			public GstMemoryMapFunction      mem_map;
			public GstMemoryUnmapFunction    mem_unmap;

			public GstMemoryCopyFunction     mem_copy;
			public GstMemoryShareFunction    mem_share;
			public GstMemoryIsSpanFunction   mem_is_span;

			/*< private >*/
			IntPtr _gst_reserved;

			IntPtr priv;
		};

		[StructLayout(LayoutKind.Sequential)]
		struct GstMemory
		{
			public IntPtr mini_object;
			public IntPtr allocator;
			public IntPtr parent;
			public uint maxsize;
			public uint align;
			public uint offset;
			public uint size;
		}

		public Memory (IntPtr raw) : base(raw)
		{
		}

		public Gst.Allocator Allocator {
			get{
				GstMemory mem = (GstMemory)Marshal.PtrToStructure (Handle,typeof(GstMemory));
				return new Gst.Allocator(mem.allocator);
			}
		}
/*
		public Memory Parent {
			get{
				GstMemory mem = (GstMemory)Marshal.PtrToStructure (Handle,typeof(GstMemory));
				return new Memory(mem.parent);
			}
		}

		public uint MaxSize {
			get{
				return ((GstMemory)Marshal.PtrToStructure (Handle,typeof(GstMemory))).maxsize;
			}
		}

		public uint Align {
			get{
				return ((GstMemory)Marshal.PtrToStructure (Handle,typeof(GstMemory))).align;
			}
		}

		public uint Offset {
			get{
				return ((GstMemory)Marshal.PtrToStructure (Handle,typeof(GstMemory))).offset;
			}
		}
*/
		public uint Size {
			get {
				return ((GstMemory)Marshal.PtrToStructure (Handle, typeof(GstMemory))).size;
			}
			set{
				GstMemory mem = (GstMemory)Marshal.PtrToStructure (Handle,typeof(GstMemory));
				mem.size = value;
				Marshal.StructureToPtr (mem,Handle,true);
			}
		}

		public bool Map(out MapInfo info, MapFlags flags){
			MapInfo.GstMapInfo ptr;
			bool b = gst_memory_map (Handle, out ptr, flags);
			info = new MapInfo (ptr);
			return b;
		}
	}
}

