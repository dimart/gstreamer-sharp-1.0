using System;
using System.Runtime.InteropServices;
using GstSharp;
using GLib;

namespace Gst
{
	public enum BusFlags
	{
		Flushing = ObjectFlags.Last << 0,
		Last     = ObjectFlags.Last << 1
	}

	public enum BusSyncReply
	{
		Drop  = 0,
		Pass  = 1,
		Async = 2,
	}

	public delegate bool BusFunc(Bus bus, Message message);

	public class Bus : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_bus_get_type();

		[DllImport(Application.Dll)]
		static extern IntPtr gst_bus_new ();

		[DllImport(Application.Dll)]
		static extern uint gst_bus_add_watch (IntPtr bus, BusFuncNative native, IntPtr data);

		[DllImport(Application.Dll)]
		static extern uint gst_bus_add_watch_full (IntPtr bus, int priority, BusFuncNative native,
		                           IntPtr data, GLib.DestroyNotify notify);

		[DllImport(Application.Dll)]
		static extern void gst_bus_add_signal_watch (IntPtr bus);

		[DllImport(Application.Dll)]
		static extern void gst_bus_add_signal_watch_full (IntPtr bus, int priority);
		
		[DllImport(Application.Dll)]
		static extern void gst_bus_enable_sync_message_emission(IntPtr bus);
		
		[DllImport(Application.Dll)]
		static extern void gst_bus_disable_sync_message_emission(IntPtr bus);

		[DllImport(Application.Dll)]
		static extern bool gst_bus_post(IntPtr bus, IntPtr message);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_bus_pop(IntPtr bus);
		
		[DllImport(Application.Dll)]
		static extern IntPtr gst_bus_pop_filtered(IntPtr bus,MessageType message_type);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_bus_timed_pop(IntPtr bus, UInt64 timeout);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_bus_timed_pop_filtered(IntPtr bus, UInt64 timeout, MessageType message_type);

		public Bus()
		{
			Raw = gst_bus_new ();
		}

		public Bus (IntPtr raw) : base(raw)
		{
		}

		public Message Pop ()
		{
			return new Gst.Message(gst_bus_pop (Handle));
		}

		public Message Pop (MessageType type)
		{
			return new Gst.Message(gst_bus_pop_filtered (Handle,type));
		}
		public Message Pop (UInt64 timeout){
			return new Gst.Message (gst_bus_timed_pop (Handle,timeout));
		}
		public Message Pop (UInt64 timeout, MessageType type){
			return new Gst.Message (gst_bus_timed_pop_filtered (Handle,timeout,type));
		}

		public bool Post (Message message)
		{
			return gst_bus_post (Handle,message.Handle);
		}

		public uint AddWatch (BusFunc func)
		{
			BusFuncWrapper wrapper = new BusFuncWrapper(func);
			IntPtr data = (IntPtr)GCHandle.Alloc (wrapper);
			return gst_bus_add_watch (Handle,wrapper.native,data);
		}
		public uint AddWatch (int priority, BusFunc func){
			BusFuncWrapper wrapper = new BusFuncWrapper(func);
			IntPtr data = (IntPtr)GCHandle.Alloc (wrapper);
			DestroyNotify notify = DestroyHelper.NotifyHandler;
			return gst_bus_add_watch_full (Handle, priority, wrapper.native, data, notify);
		}

		public void AddSignalWatch(){
			gst_bus_add_signal_watch (Handle);
		}
		public void AddSignalWatch(int priority){
			gst_bus_add_signal_watch_full(Handle,priority);
		}
		
		public void EnableSyncMessageEmission(){
			gst_bus_enable_sync_message_emission (Handle);
		}
		
		public void DisableSyncMessageEmission(){
			gst_bus_disable_sync_message_emission (Handle);
		}

		public static new GLib.GType GType {
			get{
				return new GLib.GType(gst_bus_get_type ());
			}
		}

		[Signal ("message")]
		public event MessageHandler Message
		{
			add
			{
				base.AddSignalHandler("message",value,typeof(MessageArgs));
			}
			remove
			{
				base.RemoveSignalHandler ("message", value);
			}
		}
		
		[Signal ("sync-message")]
		public event MessageHandler SyncMessage
		{
			add
			{
				base.AddSignalHandler("sync-message",value,typeof(MessageArgs));
			}
			remove
			{
				base.RemoveSignalHandler ("sync-message", value);
			}
		}
	}
}

