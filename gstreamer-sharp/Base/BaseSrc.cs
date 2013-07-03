using System;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class BaseSrc : Element
	{
		[DllImport(Application.BaseDll)]
		static extern void gst_base_src_set_live(IntPtr src, bool live);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_src_is_live(IntPtr src);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_src_set_async(IntPtr src, bool async);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_src_is_async(IntPtr src);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_src_set_blocksize(IntPtr src, uint size);
		[DllImport(Application.BaseDll)]
		static extern uint gst_base_src_get_blocksize(IntPtr src);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_src_set_do_timestamp(IntPtr src, bool timestamp);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_src_get_do_timestamp(IntPtr src);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_src_set_caps(IntPtr src, IntPtr caps);
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_base_src_get_buffer_pool(IntPtr src);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_src_get_allocator(IntPtr src, out IntPtr allocator, out AllocationParams.GstAllocationParams prms);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_src_set_format(IntPtr src, Format format);

		public BaseSrc (IntPtr raw) : base(raw)
		{
		}

		public bool IsLive {
			get{return gst_base_src_is_live (Handle);}
			set{gst_base_src_set_live (Handle,value);}
		}
		public bool IsAsync {
			get{return gst_base_src_is_async (Handle);}
			set{gst_base_src_set_async (Handle,value);}
		}
		public uint BlockSize {
			get{return gst_base_src_get_blocksize(Handle);}
			set{gst_base_src_set_blocksize(Handle,value);}
		}
		public bool DoTimeStamp {
			get{ return gst_base_src_get_do_timestamp (Handle);}
			set{gst_base_src_set_do_timestamp (Handle,value);}
		}
		public Gst.Caps Caps {
			set{gst_base_src_set_caps (Handle,value.Handle);}
		}
		public Gst.BufferPool BufferPool {
			get{return new Gst.BufferPool(gst_base_src_get_buffer_pool (Handle));}
		}
		public Gst.Format Format {
			set{gst_base_src_set_format (Handle,value);}
		}

		public void GetAllocator(out Allocator allocator, out AllocationParams prms)
		{
			IntPtr i;
			AllocationParams.GstAllocationParams p;
			gst_base_src_get_allocator (Handle,out i, out p);
			allocator = new Allocator(i);
			prms = new AllocationParams(p);
		}
	}
}

