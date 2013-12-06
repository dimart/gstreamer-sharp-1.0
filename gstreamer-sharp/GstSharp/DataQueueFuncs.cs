using System;
using System.Runtime.InteropServices;
using Gst;
using Gst.Base;

namespace GstSharp
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate bool CheckFullFunctionNative (IntPtr queue, uint visible, uint bytes, ulong time, IntPtr data);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void DataQueueCallbackNative (IntPtr queue, IntPtr data);

	internal class DataQueueCallbackWrapper
	{
		internal DataQueueCallbackNative native;
		DataQueueCallback managed;

		public DataQueueCallbackWrapper (DataQueueCallback callback)
		{
			managed = callback;
			if (managed != null)
				native = new DataQueueCallbackNative (NativeCallback);
		}

		public void NativeCallback (IntPtr queue, IntPtr data)
		{
			DataQueue dataqueue = new DataQueue (queue);
			if (managed != null)
				managed (dataqueue);
		}
	}

	internal class DataQueueFunctionWrapper
	{
		internal CheckFullFunctionNative native;
		CheckFullFunction managed;

		public DataQueueFunctionWrapper (CheckFullFunction function)
		{
			managed = function;
			if (managed != null)
				native = new CheckFullFunctionNative (NativeFunction);
		}

		public bool NativeFunction (IntPtr queue, uint visible, uint bytes, ulong time, IntPtr data)
		{
			DataQueue dataqueue = new DataQueue (queue);
			if (managed != null)
				return managed (dataqueue, visible, bytes, time);
			return false;
		}
	}
}

