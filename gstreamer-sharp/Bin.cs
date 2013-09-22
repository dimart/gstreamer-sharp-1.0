using System;
using System.Runtime.InteropServices;
using System.Collections;

namespace Gst
{
	[Flags]
	public enum BinFlags
	{
		NoResync = ElementFlags.Last << 0,
		Last = ElementFlags.Last << 5
	}

	public delegate void ElementHandler(object sender, ElementArgs args);

	public class ElementArgs : GLib.SignalArgs
	{
		public Element Child {
			get{ 
				return (Element)base.Args [0];
			}
		}
	}

	public class Bin : Element
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_bin_new(IntPtr name);
		[DllImport(Application.Dll)]
		static extern bool gst_bin_add(IntPtr bin, IntPtr element);
		[DllImport(Application.Dll)]
		static extern bool gst_bin_remove(IntPtr bin, IntPtr element);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_bin_get_by_name(IntPtr bin, IntPtr name);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_bin_get_by_interface(IntPtr bin, IntPtr iface_type);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_bin_iterate_elements (IntPtr bin);

		public Bin (IntPtr raw) : base(raw)
		{
		}

		public Bin (string name) : base(IntPtr.Zero)
		{
			Raw = gst_bin_new (Marshal.StringToHGlobalAuto (name));
		}

		public bool Add (Element e)
		{
			return gst_bin_add (Raw,e.Handle);
		}

		public bool Remove (Element e)
		{
			return gst_bin_remove (Raw,e.Handle);
		}

		public Element GetByName(string name){
				return new Element(gst_bin_get_by_name(Handle,
				                                       Marshal.StringToHGlobalAuto(name)));
		}

		public Element GetByInterface(GLib.GType type) {
				return new Element(gst_bin_get_by_interface(Handle,type.Val));
		}
		public Iterator IterateElements(){
			return new Iterator(gst_bin_iterate_elements(Handle));
		}

		[GLib.Signal("element-added")]
		public event ElementHandler ElementAdded
		{
			add{
				base.AddSignalHandler("element-added",value,typeof(ElementArgs));
			}
			remove{
				base.RemoveSignalHandler ("element-added", value);
			}
		}

		[GLib.Signal("element-removed")]
		public event ElementHandler ElementRemoved
		{
			add{
				base.AddSignalHandler("element-removed",value,typeof(ElementArgs));
			}
			remove{
				base.RemoveSignalHandler ("element-removed", value);
			}
		}

		[GLib.Signal("do-latency")]
		public event EventHandler DoLatency
		{
			add {
				base.AddSignalHandler ("do-latency",value);
			}
			remove {
				base.RemoveSignalHandler ("do-latency", value);
			}
		}
	}
}

