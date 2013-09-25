using System;
using System.Runtime.InteropServices;
using Gst;

namespace GstSharp
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void TaskFuncNative(IntPtr data);

	internal class TaskFuncWrapper
	{
		internal TaskFuncNative native;
		TaskFunc func;

		public TaskFuncWrapper (TaskFunc function){
			func = function;
			if(func != null)
				native = new TaskFuncNative(NativeCallback);
		}

		public void NativeCallback (IntPtr data){
			func ();
		}
	}
}

