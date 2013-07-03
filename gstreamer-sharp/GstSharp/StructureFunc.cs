using System;
using System.Runtime.InteropServices;
using Gst;

namespace GstSharp
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate bool StructureFuncNative(uint quark, ref GLib.Value val, IntPtr data);

	internal class StructureFuncWrapper
	{
		internal StructureFuncNative native;
		StructureFunc managed;

		public StructureFuncWrapper (StructureFunc func)
		{
			managed = func;
			if(managed != null)
				native = new StructureFuncNative(NativeCallback);
		}

		public bool NativeCallback(uint quark, ref GLib.Value val, IntPtr data)
		{
			return managed(quark,ref val);
		}
	}
}

