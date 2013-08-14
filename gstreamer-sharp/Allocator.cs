using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Allocator : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_allocator_find(IntPtr name);

		public Allocator (IntPtr raw) : base(raw)
		{
		}

		public static Allocator Find(string name){
			return new Allocator(gst_allocator_find (Marshal.StringToHGlobalAuto (name)));
		}
	}
}

