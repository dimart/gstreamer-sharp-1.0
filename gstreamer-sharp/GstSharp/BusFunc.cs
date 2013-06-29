using System;
using System.Runtime.InteropServices;
using Gst;

namespace GstSharp
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate bool BusFuncNative (IntPtr bus, IntPtr message, IntPtr data);

	internal class BusFuncWrapper
	{
		internal BusFuncNative native;
		BusFunc managed;

		public BusFuncWrapper(BusFunc func){
			managed = func;
			if(managed != null)
				native = new BusFuncNative(NativeCallback);
		}

		public bool NativeCallback(IntPtr bus, IntPtr message, IntPtr data){
			Bus b = GLib.Object.GetObject (bus) as Bus;
			Message m = new Message(message);
			bool flag = managed(b, m);
			return flag;
		}
	}
}

