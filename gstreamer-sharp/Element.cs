using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Element : Gst.Object
	{
		struct GstElement
		{
			public IntPtr @object;
			public IntPtr state_lock;
			public IntPtr               state_cond;
			public uint             state_cookie;
			public State              target_state;
			public State              current_state;
			public State              next_state;
			public State              pending_state;
			public StateChangeReturn  last_return;

			public IntPtr               bus;

			/* allocated clock */
			public IntPtr	clock;
			public Int64      base_time; /* NULL/READY: 0 - PAUSED: current time - PLAYING: difference to clock */
			public UInt64          start_time;

			/* element pads, these lists can only be iterated while holding
			* the LOCK or checking the cookie after each LOCK. */
			public UInt16            numpads;
			public IntPtr              pads;
			public UInt16     numsrcpads;
			public IntPtr              srcpads;
			public UInt16     numsinkpads;
			public IntPtr               sinkpads;
			public UInt32       pads_cookie;

			/*< private >*/
			IntPtr _gst_reserved;
		};

		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_get_type();

		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_provide_clock (IntPtr element);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_get_clock (IntPtr element);

		[DllImport(Application.Dll)]
		static extern bool gst_element_set_clock (IntPtr element, IntPtr clock);

		[DllImport(Application.Dll)]
		static extern void gst_element_set_bus (IntPtr element, IntPtr bus);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_get_bus(IntPtr element); 

		[DllImport(Application.Dll)]
		static extern int gst_element_set_state(IntPtr element, int state);

		[DllImport(Application.Dll)]
		static extern StateChangeReturn gst_element_get_state (IntPtr element,out State state, out State pending, long timeout);

		[DllImport(Application.Dll)]
		static extern void   gst_element_set_base_time       (IntPtr element, UInt64 time);
		[DllImport(Application.Dll)]
		static extern UInt64 gst_element_get_base_time       (IntPtr element);
		[DllImport(Application.Dll)]
		static extern void   gst_element_set_start_time      (IntPtr element, UInt64 time);
		[DllImport(Application.Dll)]
		static extern UInt64 gst_element_get_start_time      (IntPtr element);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_query(IntPtr element);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_iterate_pads(IntPtr element);
		[DllImport(Application.Dll)]
		static extern bool gst_element_link(IntPtr element, IntPtr other);
		[DllImport(Application.Dll)]
		static extern bool gst_element_link_filtered(IntPtr element, IntPtr other, IntPtr caps);
		[DllImport(Application.Dll)]
		static extern void gst_element_unlink(IntPtr element, IntPtr other);
		[DllImport(Application.Dll)]
		static extern bool gst_element_seek_simple(IntPtr element, Format format, SeekFlags flags, long position);

		public Element (IntPtr raw) : base(raw)
		{
		}

		public static new GLib.GType GType {
			get{return new GLib.GType(gst_element_get_type ());}
		}

		public UInt64 BaseTime {
			get{return gst_element_get_base_time (Handle);}
			set{gst_element_set_base_time (Handle,value);}
		}

		public UInt64 StartTime {
			get{return gst_element_get_start_time (Handle);}
			set{gst_element_set_start_time (Handle,value);}
		}

		public Clock ProvideClock(){
			return new Gst.Clock(gst_element_provide_clock (Handle));
		}

		public Gst.Clock Clock {
			get{ return new Gst.Clock (gst_element_get_clock (Raw));}
			set{gst_element_set_clock (Raw,value.Handle);}
		}

		public Gst.Bus Bus {
			get{return new Gst.Bus(gst_element_get_bus (Raw));}
			set{gst_element_set_bus (Raw,value.Handle);}
		}

		public Gst.Query Query {
			get{return new Gst.Query(gst_element_query (Raw));}
		}

		public StateChangeReturn SetState(State state){
			return (StateChangeReturn)gst_element_set_state (Raw,(int)state);
		}

		public StateChangeReturn GetState(out State state){
			State pending;
			return gst_element_get_state (Raw,out state,out pending,0);
		}

		public State GetState(){
			State s;
			GetState (out s);
			return s;
		}

		public bool Link (Element element)
		{
			return gst_element_link (Handle,element.Handle);
		}
		public bool Link (Element element, Caps caps)
		{
			return gst_element_link_filtered (Handle,element.Handle,caps.Handle);
		}
		public static bool Link (params Element[] elements)
		{
			for(int i = 0; i < elements.Length - 1; i++)
				if(!elements[i].Link (elements[i+1]))return false;
			return true;
		}
		public void Unlink (Element element)
		{
			gst_element_unlink (Handle,element.Handle);
		}
		public static void Unlink (params Element[] elements)
		{
			for(int i = 0; i < elements.Length - 1; i++)
				elements[i].Unlink (elements[i+1]);
		}

		public Gst.State State {
			get{
				Gst.State s;
				GetState (out s);
				return s;
			}
			set{
				SetState (value);
			}
		}

		public Iterator Pads {
			get{
				return new Iterator(gst_element_iterate_pads (Handle));
			}
		}

		public bool Seek(Format format, SeekFlags flags, long position){
			return gst_element_seek_simple (Handle,format,flags,position);
		}
		public bool Seek(SeekFlags flags, long position){
			return Seek (Format.Time,flags,position);
		}
		public bool Seek(long position){
			return Seek (SeekFlags.Flush|SeekFlags.KeyUnit,position);
		}

		[GLib.Signal("pad-added")]
		public event PadHandler PadAdded {
			add{
				AddSignalHandler("pad-added",value,typeof(PadHandlerArgs));
			}
			remove{			
				RemoveSignalHandler("pad-added",value);
			}
		}

		[GLib.Signal("pad-removed")]
		public event PadHandler PadRemoved {
			add{
				AddSignalHandler("pad-added",value,typeof(PadHandlerArgs));
			}
			remove{			
				RemoveSignalHandler("pad-added",value);
			}
		}

		[GLib.Signal("no-more-pads")]
		public event EventHandler NoMorePads {
			add{
				AddSignalHandler("no-more-pads",value);
			}
			remove{
				RemoveSignalHandler ("no-more-pads",value);
			}
		}
	}
}

