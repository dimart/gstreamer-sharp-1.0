using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class ElementFactory
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_get_type ();

		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make(IntPtr factory_name, IntPtr name);

		public static GLib.GType GType {
			get{
				return new GLib.GType(gst_element_factory_get_type ());
			}
		}

		public static Element Make (string factory_name)
		{
			return Make (factory_name,null);
		}

		public static Element Make(string factory_name, string name){
			IntPtr s1 = GLib.Marshaller.StringToPtrGStrdup (factory_name);
			IntPtr s2 = GLib.Marshaller.StringToPtrGStrdup (name);
			IntPtr e = gst_element_factory_make (s1,s2);
			GLib.Marshaller.Free (s1);
			GLib.Marshaller.Free (s2);
			return new Element(e);
		}
	}
}

