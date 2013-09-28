using System;
using System.Runtime.InteropServices;

using Gst;
using Gst.Audio;

namespace GstSharp
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void PackFuncNative(IntPtr info, PackFlags flags, IntPtr data1, IntPtr data2, int length);

	internal class PackFuncWrapper
	{
		PackFunc managed;
		public PackFuncNative native;

		public PackFuncWrapper(PackFunc func){
			managed = func;
			if(managed != null)
				native = new PackFuncNative(PackFuncCallback);
		}

		public void PackFuncCallback(IntPtr info, PackFlags flags, IntPtr data1, IntPtr data2, int length){
			byte[] data3 = new byte[length];
			byte[] data4 = new byte[length];
			Marshal.Copy (data1,data3,0,length);
			Marshal.Copy (data2,data4,0,length);
			managed(new FormatInfo(info), flags, data3, data4, length);
		}
	}

	internal class PackFuncUnwrapper
	{
		public PackFunc managed;
		PackFuncNative native;

		public PackFuncUnwrapper(PackFuncNative func){
			native = func;
			if(native != null)
				managed = new PackFunc(Callback);
		}

		public void Callback(FormatInfo info, PackFlags flags, byte[] data1, byte[] data2, int length){
			IntPtr data3 = IntPtr.Zero;
			IntPtr data4 = IntPtr.Zero;
			Marshal.Copy (data1,0,data3,data1.Length);
			Marshal.Copy (data2,0,data4,data2.Length);
			native(info.Handle, flags, data3, data4, length);
		}
	}
}

