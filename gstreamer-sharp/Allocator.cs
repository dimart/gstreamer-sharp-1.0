using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class AllocationParams : GLib.Opaque
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct GstAllocationParams {
			public MemoryFlags   flags;
			public long          align;
			public long          prefix;
			public long          padding;
			/*< private >*/
			IntPtr _gst_reserved;
		}

		~AllocationParams()
		{
			gst_allocation_params_free (Handle);
		}
		public AllocationParams() : base(IntPtr.Zero)
		{

		}
		public AllocationParams (GstAllocationParams prms) : this()
		{
			IntPtr i = IntPtr.Zero;
			Marshal.StructureToPtr (prms,i,false);
			Raw = i;
		}
		public AllocationParams(IntPtr raw) : base(raw)
		{

		}

		[DllImport(Application.Dll)]
		static extern void gst_allocation_params_init(IntPtr ap);
		[DllImport(Application.Dll)]
		static extern void gst_allocation_params_free(IntPtr ap);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_allocation_params_copy(IntPtr ap);

		public MemoryFlags Flags {
			get {
				return ((GstAllocationParams)Marshal.PtrToStructure (Handle, typeof(GstAllocationParams))).flags;
			}
			set{
				var gap = (GstAllocationParams)Marshal.PtrToStructure (Handle, typeof(GstAllocationParams));
				gap.flags = value;
				Marshal.StructureToPtr (gap,Handle,false);
			}
		}
		public void Init(){
			gst_allocation_params_init (Handle);
		}
		public AllocationParams Copy(){
			return new AllocationParams(gst_allocation_params_copy (Handle));
		}
	}

	public class Allocator : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_allocator_find(IntPtr name);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_allocator_alloc(IntPtr al, long size, IntPtr prms);
		[DllImport(Application.Dll)]
		static extern void gst_allocator_free(IntPtr al, IntPtr memory);

		public Allocator (IntPtr raw) : base(raw)
		{
		}

		public Memory Alloc (long size, AllocationParams prms)
		{
			return new Memory(gst_allocator_alloc (Handle,size,prms.Handle));
		}
		public void Free (Memory memory)
		{
			gst_allocator_free(Handle,memory.Handle);
		}

		public static Allocator Find(string name){
			return new Allocator(gst_allocator_find (Marshal.StringToHGlobalAuto (name)));
		}
	}
}

