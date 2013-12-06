using System;
using System.Runtime.InteropServices;
using GstSharp;

namespace Gst.Base
{
	public delegate void DataQueueCallback (DataQueue queue);
	public delegate bool CheckFullFunction (DataQueue queue, uint visible, uint bytes, ulong time);

	public class DataQueueItem
	{
		[StructLayout(LayoutKind.Sequential)]
		internal struct GstDataQueueItem
		{
			public IntPtr @object;
			public uint size;
			public ulong duration;
			public bool visible;
			public GLib.DestroyNotify destroy;

			IntPtr _gst_reserved;
		}

		GstDataQueueItem item;

		public DataQueueItem ()
		{
			item = new GstDataQueueItem ();
		}

		public DataQueueItem (IntPtr raw)
		{
			item = (GstDataQueueItem)Marshal.PtrToStructure (raw, typeof(GstDataQueueItem));
		}

		public IntPtr Raw {
			get { 
				IntPtr data = GLib.Marshaller.Malloc ((ulong)Marshal.SizeOf(typeof (GstDataQueueItem)));
				Marshal.StructureToPtr (item, data, false);
				return data;
			}
		}

		public Gst.MiniObject Object {
			get {
				return new Gst.MiniObject (item.@object);
			}
			set { 
				item.@object = value.Handle;
			}
		}

		public uint Size {
			get { 
				return item.size;
			}
			set { 
				item.size = value;
			}
		}

		public ulong Duration {
			get { 
				return item.duration;
			}
			set { 
				item.duration = value;
			}
		}

		public bool Visible {
			get { 
				return item.visible;
			}
			set { 
				item.visible = value;
			}
		}

		public GLib.DestroyNotify Destroy {
			get { 
				return item.destroy;
			}
			set { 
				item.destroy = value;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct DataQueueSize
	{
		public uint Visible;
		public uint Bytes;
		public ulong Time;
	}

	public class DataQueue : Gst.Object
	{
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_data_queue_new (
			CheckFullFunctionNative fnative,
			DataQueueCallbackNative fullnative,
			DataQueueCallbackNative emptynative,
			IntPtr data);

		[DllImport(Application.BaseDll)]
		static extern void gst_data_queue_flush (IntPtr queue);
		[DllImport(Application.BaseDll)]
		static extern bool gst_data_queue_push (IntPtr queue, IntPtr item);
		[DllImport(Application.BaseDll)]
		static extern bool gst_data_queue_push_force (IntPtr queue, IntPtr item);
		[DllImport(Application.BaseDll)]
		static extern bool gst_data_queue_pop (IntPtr queue, out IntPtr item);
		[DllImport(Application.BaseDll)]
		static extern bool gst_data_queue_peek (IntPtr queue, out IntPtr item);
		[DllImport(Application.BaseDll)]
		static extern void gst_data_queue_get_level (IntPtr queue, out DataQueueSize size);
		[DllImport(Application.BaseDll)]
		static extern void gst_data_queue_set_flushing (IntPtr queue, bool flushing);
		[DllImport(Application.BaseDll)]
		static extern bool gst_data_queue_drop_head (IntPtr queue, IntPtr type);
		[DllImport(Application.BaseDll)]
		static extern bool gst_data_queue_is_full (IntPtr queue);
		[DllImport(Application.BaseDll)]
		static extern bool gst_data_queue_is_empty (IntPtr queue);
		[DllImport(Application.BaseDll)]
		static extern void gst_data_queue_limits_changed (IntPtr queue);

		public DataQueue (IntPtr raw) : base(raw)
		{
		}

		public DataQueue (CheckFullFunction function, DataQueueCallback fullcallback, DataQueueCallback emptycallback)
		{
			var wrapper = new DataQueueFunctionWrapper (function);
			var fwrapper = new DataQueueCallbackWrapper (fullcallback);
			var ewrapper = new DataQueueCallbackWrapper (emptycallback);
			IntPtr data = (IntPtr)GCHandle.Alloc (wrapper);
			Raw = gst_data_queue_new (wrapper.native, fwrapper.native, ewrapper.native, data);
		}

		public bool DropHead (GLib.GType type)
		{
			return gst_data_queue_drop_head (Handle, type.Val);
		}

		public bool IsEmpty ()
		{
			return gst_data_queue_is_empty (Handle);
		}

		public bool IsFull ()
		{
			return gst_data_queue_is_full (Handle);
		}

		public void Flush ()
		{
			gst_data_queue_flush (Handle);
		}

		public void LimitsChanged ()
		{
			gst_data_queue_limits_changed (Handle);
		}

		public bool Peek (out DataQueueItem item)
		{
			IntPtr data;
			var res = gst_data_queue_peek (Handle, out data);
			item = new DataQueueItem (data);	
			return res;
		}

		public bool Pop (out DataQueueItem item)
		{
			IntPtr data;
			var res = gst_data_queue_pop (Handle, out data);
			item = new DataQueueItem (data);	
			return res;
		}

		public bool Push (DataQueueItem item)
		{
			return gst_data_queue_push (Handle, item.Raw);
		}

		public bool PushForce (DataQueueItem item)
		{
			return gst_data_queue_push_force (Handle, item.Raw);
		}

		public bool Flushing {
			set { 
				gst_data_queue_set_flushing (Handle, value);
			}
		}

		public DataQueueSize Level {
			get { 
				DataQueueSize size;
				gst_data_queue_get_level (Handle, out size);
				return size;
			}
		}

		[GLib.Signal("empty")]
		public event EventHandler Empty 
		{
			add {
				base.AddSignalHandler ("empty", value);
			}
			remove {
				base.RemoveSignalHandler ("empty", value);
			}
		}

		[GLib.Signal("full")]
		public event EventHandler Full
		{
			add {
				base.AddSignalHandler ("full", value);
			}
			remove {
				base.RemoveSignalHandler ("full", value);
			}
		}
	}
}

