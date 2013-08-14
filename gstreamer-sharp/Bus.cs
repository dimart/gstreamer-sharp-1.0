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
		static extern IntPtr gst_bus_pop_filtered(IntPtr bus,int message_type);

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
			return new Gst.Message(gst_bus_pop_filtered (Handle,(int)type));
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
				var sig  = Signal.Lookup(this,"message");
				sig.AddDelegate(value);
			}
			remove
			{
				var sig  = Signal.Lookup(this,"message");
				sig.RemoveDelegate (value);
			}
		}
		
		[Signal ("sync-message")]
		public event SyncMessageHandler SyncMessage
		{
			add
			{
				var sig  = Signal.Lookup(this,"sync-message");
				sig.AddDelegate(value);
			}
			remove
			{
				var sig  = Signal.Lookup(this,"sync-message");
				sig.RemoveDelegate(value);
			}
		}
	}
}

