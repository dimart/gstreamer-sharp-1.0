using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public delegate void TaskFunc ();

	public enum TaskState
	{
		Started,
		Stopped,
		Paused
	}

	public class Task : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_task_new (GstSharp.TaskFuncNative native, IntPtr data, GLib.DestroyNotify notify);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_task_get_pool (IntPtr task);
		[DllImport(Application.Dll)]
		static extern void gst_task_set_pool (IntPtr task, IntPtr pool);
		[DllImport(Application.Dll)]
		static extern bool gst_task_set_state (IntPtr task, TaskState state);
		[DllImport(Application.Dll)]
		static extern TaskState gst_task_get_state (IntPtr task);
		[DllImport(Application.Dll)]
		static extern bool gst_task_start (IntPtr task);
		[DllImport(Application.Dll)]
		static extern bool gst_task_stop (IntPtr task);
		[DllImport(Application.Dll)]
		static extern bool gst_task_pause (IntPtr task);
		[DllImport(Application.Dll)]
		static extern bool gst_task_join (IntPtr task);


		public Task (IntPtr raw) : base(raw)
		{
		}
		public Task (TaskFunc func) : base(IntPtr.Zero)
		{
			GstSharp.TaskFuncWrapper wrapper = new GstSharp.TaskFuncWrapper(func);
			IntPtr data = (IntPtr)GCHandle.Alloc (wrapper);
			GLib.DestroyNotify notify = GLib.DestroyHelper.NotifyHandler;
			Raw = gst_task_new (wrapper.native,data,notify);
		}

		public bool Join(){
			return gst_task_join (Handle);
		}
		public bool Start(){
			return gst_task_start (Handle);
		}
		public bool Stop(){
			return gst_task_stop (Handle);
		}
		public bool Pause(){
			return gst_task_pause (Handle);
		}

		public TaskPool Pool {
			get {
				return new TaskPool(gst_task_get_pool (Handle));
			}
			set{
				gst_task_set_pool (Handle, value.Handle);
			}
		}
		public TaskState State {
			get {
				return gst_task_get_state (Handle);
			}
			set {
				gst_task_set_state (Handle, value);
			}
		}
	}
}

