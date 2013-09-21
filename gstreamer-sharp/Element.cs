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
		static extern IntPtr gst_element_iterate_pads(IntPtr element);
		[DllImport(Application.Dll)]
		static extern bool gst_element_link (IntPtr element, IntPtr other);
		[DllImport(Application.Dll)]
		static extern bool gst_element_seek (IntPtr element, double rate, Format format, SeekFlags flags,
		                                     SeekType stype, Int64 start, SeekType etype, Int64 end);
		[DllImport(Application.Dll)]
		static extern bool gst_element_seek_simple (IntPtr element, Format format, SeekFlags flags,
		                            Int64 position);
		[DllImport(Application.Dll)]
		static extern bool gst_element_query (IntPtr element, IntPtr query);

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
		public bool Link (Element element){
			return gst_element_link (Handle, element.Handle);
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

		public bool Seek(Format format, double rate, SeekFlags flags, SeekType start_type, Int64 start,
		                 SeekType end_type, Int64 end){
			return gst_element_seek (Handle, rate, format, flags, start_type, start, end_type, end);
		}
		public bool Seek(Format format, SeekFlags flags, Int64 position){
			return gst_element_seek_simple (Handle, format, flags, position);
		}
		public bool Query(Gst.Query query){
			return gst_element_query (Handle,query);
		}

		public Iterator Pads {
			get{
				return new Iterator(gst_element_iterate_pads (Handle));
			}
		}

		[GLib.Signal("pad-added")]
		public event PadHandler PadAdded {
			add{
				base.AddSignalHandler("pad-added",value,typeof(PadHandlerArgs));
			}
			remove{			
				base.RemoveSignalHandler ("pad-added", value);
			}
		}

		[GLib.Signal("pad-removed")]
		public event PadHandler PadRemoved {
			add{
				base.AddSignalHandler("pad-removed",value,typeof(PadHandlerArgs));
			}
			remove{			
				base.RemoveSignalHandler ("pad-removed", value);
			}
		}

		[GLib.Signal("no-more-pads")]
		public event EventHandler NoMorePads {
			add{
				base.AddSignalHandler("no-more-pads",value);
			}
			remove{
				base.RemoveSignalHandler ("no-more-pads", value);
			}
		}
	}
}

