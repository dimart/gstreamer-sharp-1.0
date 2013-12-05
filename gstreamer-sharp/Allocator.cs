using System;
using System.Runtime.InteropServices;

namespace Gst
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct GstAllocationParams
	{
		public MemoryFlags flags;
		public uint align;
		public uint prefix;
		public uint padding;
		IntPtr _gst_reserved;
	}

	public class AllocationParams
	{
		IntPtr prms;

		[DllImport(Application.Dll)]
		static extern void gst_allocation_params_init (IntPtr ap);
		[DllImport(Application.Dll)]
		static extern void gst_allocation_params_free (IntPtr ap);

		~AllocationParams()
		{
			gst_allocation_params_free (prms);
		}

		internal AllocationParams (IntPtr raw)
		{
			prms = raw;
		}

		internal AllocationParams (GstAllocationParams prms)
		{
			this.prms = GLib.Marshaller.StructureToPtrAlloc (prms);
		}

		public void Init ()
		{
			gst_allocation_params_init (prms);
		}
	}

	[Flags]
	public enum AllocatorFlags
	{
		CustomAlloc = ObjectFlags.Last << 0,
		Last = ObjectFlags.Last << 16
	}

	public class Allocator : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_allocator_find(IntPtr name);
		[DllImport(Application.Dll)]
		static extern void gst_allocator_register(IntPtr name, IntPtr allocator);
		[DllImport(Application.Dll)]
		static extern void gst_allocator_set_default (IntPtr allocator);

		public Allocator (IntPtr raw) : base(raw)
		{
		}

		public static Allocator Find (string name){
			return new Allocator(gst_allocator_find (Marshal.StringToHGlobalAuto (name)));
		}
		public static void Register (string name, Allocator allocator){
			gst_allocator_register (Marshal.StringToHGlobalAuto (name), allocator.Handle);
		}

		public void SetDefault(){
			gst_allocator_set_default (Handle);
		}
	}
}

