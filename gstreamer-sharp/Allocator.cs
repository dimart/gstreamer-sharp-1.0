using System;
using System.Runtime.InteropServices;

namespace Gst
{
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

