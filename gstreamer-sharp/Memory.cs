using System;
using System.Runtime.InteropServices;

namespace Gst
{
	[Flags]
	public enum MemoryFlags
	{
		ReadOnly     = MiniObjectFlags.LockReadOnly,
		NoShare      = MiniObjectFlags.Last << 0,
		ZeroPrefixed = MiniObjectFlags.Last << 1,
		ZeroPadded   = MiniObjectFlags.Last << 2,
		Last         = MiniObjectFlags.Last << 16
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
		static extern IntPtr gst_memory_make_mapped(IntPtr memory, out MapInfo.GstMapInfo info, int flags);
		[DllImport(Application.Dll)]
		static extern bool gst_memory_map (IntPtr memory, out MapInfo.GstMapInfo info, int flags);
		[DllImport(Application.GlueDll)]
		static extern void gstsharp_memory_unmap (IntPtr memory, MapInfo.GstMapInfo info);
		[DllImport(Application.Dll)]
		static extern bool gst_memory_is_span (IntPtr memory, IntPtr o, out long offset);
	
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

		GstMemory mem;

		public Memory (IntPtr raw) : base(raw)
		{
			mem = (GstMemory)Marshal.PtrToStructure (raw,typeof(GstMemory));
		}

		public Gst.Allocator Allocator {
			get{
				return new Gst.Allocator(mem.allocator);
			}
		}

		public Memory Parent {
			get{
				return new Memory(mem.parent);
			}
		}

		public uint MaxSize {
			get{
				return mem.maxsize;
			}
		}

		public uint Align {
			get{
				return mem.align;
			}
		}

		public uint Offset {
			get{
				return mem.offset;
			}
		}

		public uint Size {
			get {
				return mem.size;
			}
			set{
				GstMemory mem = (GstMemory)Marshal.PtrToStructure (Handle,typeof(GstMemory));
				mem.size = value;
				Marshal.StructureToPtr (mem,Handle,true);
			}
		}

		public Memory MakeMapped (out MapInfo info, MapFlags flags)
		{
			MapInfo.GstMapInfo i;
			var memory = gst_memory_make_mapped (Handle,out i,(int)flags);
			info = new MapInfo(i);
			return new Memory(memory);
		}
		public MapInfo Map(MapFlags flags){
			MapInfo.GstMapInfo i;
			gst_memory_map (Handle,out i,(int)flags);
			return new MapInfo(i);
		}
		public void Unmap (MapInfo info)
		{
			gstsharp_memory_unmap (Handle, info.i);
		}
		public bool IsSpan(Memory memory, out long offset){
			return gst_memory_is_span (Handle,memory.Handle,out offset);
		}
	}
}

