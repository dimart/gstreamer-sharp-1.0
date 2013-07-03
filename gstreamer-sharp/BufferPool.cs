using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class BufferPool : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern void gst_buffer_pool_set_active(IntPtr pool, bool active);
		[DllImport(Application.Dll)]
		static extern bool gst_buffer_pool_is_active(IntPtr pool);
		[DllImport(Application.Dll)]
		static extern bool gst_buffer_pool_set_config(IntPtr pool, IntPtr structure);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_pool_get_config(IntPtr pool);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_pool_get_options(IntPtr pool);
		[DllImport(Application.Dll)]
		static extern bool gst_buffer_pool_has_option(IntPtr pool, IntPtr option);
		[DllImport(Application.Dll)]
		static extern void gst_buffer_pool_config_set_allocator(IntPtr pool, IntPtr alloctor, IntPtr prms);
		[DllImport(Application.Dll)]
		static extern bool gst_buffer_pool_config_get_allocator(IntPtr pool, out IntPtr alloctor, 
		                                                      out  AllocationParams.GstAllocationParams prms);

		public BufferPool (IntPtr raw) : base(raw)
		{
		}

		public bool IsActive {
			get{return gst_buffer_pool_is_active (Handle);}
			set{gst_buffer_pool_set_active(Handle,value);}
		}
		public Structure Config {
			get{return new Structure(gst_buffer_pool_get_config (Handle));}
			set{gst_buffer_pool_set_config (Handle,value.Handle);}
		}
		public string[] Options {
			get{
				return GLib.Marshaller.PtrToStringArrayGFree (
					gst_buffer_pool_get_options (Handle)
				);
			}
		}

		public bool HasOption(string option){
			return gst_buffer_pool_has_option (Handle,Marshal.StringToHGlobalAuto (option));
		}
		public void SetAllocator(Allocator allocator, AllocationParams prms){
			gst_buffer_pool_config_set_allocator (Handle,allocator.Handle,prms.Handle);
		}
		public bool GetAllocator(out Allocator allocator, out AllocationParams prms)
		{
			IntPtr a = IntPtr.Zero;
			AllocationParams.GstAllocationParams p;
			bool b = gst_buffer_pool_config_get_allocator (Handle,out a, out p);
			allocator = new Allocator(a);
			prms = new AllocationParams(p);
			return b;
		}
	}
}

