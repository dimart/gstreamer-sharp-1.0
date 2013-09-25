using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class TaskPool : Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_task_pool_new ();
		[DllImport(Application.Dll)]
		static extern void gst_task_pool_prepare (IntPtr pool, out IntPtr error);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_task_pool_push (IntPtr pool, GstSharp.TaskFuncNative native,
		                                         IntPtr data, out IntPtr error);
		[DllImport(Application.Dll)]
		static extern void gst_task_pool_join (IntPtr pool, IntPtr id);
		[DllImport(Application.Dll)]
		static extern void gst_task_pool_cleanup (IntPtr pool);

		public TaskPool (IntPtr raw) : base(raw)
		{
		}
		public TaskPool () : base(gst_task_pool_new ())
		{
		}

		public void Prepare(){
			IntPtr ptr;
			gst_task_pool_prepare (Handle, out ptr);
		}
		public IntPtr Push(TaskFunc func){
			GstSharp.TaskFuncWrapper wrapper = new GstSharp.TaskFuncWrapper (func);
			IntPtr data = (IntPtr)GCHandle.Alloc (wrapper);
			IntPtr error;
			return gst_task_pool_push (Handle, wrapper.native, data, out error);
		}
		public void Join (IntPtr id){
			gst_task_pool_join (Handle, id);
		}
		public void Cleanup (){
			gst_task_pool_cleanup (Handle);
		}
	}
}

