using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class ElementFactory : GLib.Opaque
	{
		protected ElementFactory(IntPtr raw) : base(raw)
		{}
		protected ElementFactory() : base(IntPtr.Zero)
		{}

		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_get_type ();
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_find(IntPtr name);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_get_element_type(IntPtr factory);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make(IntPtr factory_name, IntPtr name);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_create(IntPtr factory, IntPtr name);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_get_metadata(IntPtr factory, IntPtr key);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_get_metadata_keys(IntPtr factory);

		public static GLib.GType GType {
			get{
				return new GLib.GType(gst_element_factory_get_type ());
			}
		}

		public static ElementFactory Find (string name)
		{
			return new ElementFactory(gst_element_factory_find (Marshal.StringToHGlobalAuto(name)));
		}
		public Element Create (string name)
		{
			return new Element(gst_element_factory_create (Handle,Marshal.StringToHGlobalAuto (name)));
		}
		public string GetMetadata (string key)
		{
			return Marshal.PtrToStringAuto (
				gst_element_factory_get_metadata(Handle,Marshal.StringToHGlobalAuto(key))
			);
		}
		public string[] MetadataKeys {
			get{
				return GLib.Marshaller.PtrToStringArrayGFree (
				gst_element_factory_get_metadata_keys (Handle)
				);
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

