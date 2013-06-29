using System;
using System.Runtime.InteropServices;
using GLib;

namespace Gst
{
	public class Object : GLib.InitiallyUnowned
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_object_get_type();

		[DllImport(Application.Dll)]
		static extern void gst_object_set_name(IntPtr obj, IntPtr name);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_object_get_name(IntPtr obj);

		[DllImport(Application.Dll)]
		static extern bool gst_object_set_parent(IntPtr obj, IntPtr parent);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_object_get_parent(IntPtr obj);

		[DllImport(Application.Dll)]
		static extern void gst_object_unparent(IntPtr obj);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_object_get_path_string(IntPtr obj);

		[DllImport(Application.Dll)]
		static extern bool gst_object_has_ancestor(IntPtr obj, IntPtr ancestor);

		public Object () : base(IntPtr.Zero)
		{
		}

		public Object (IntPtr raw) : base(raw)
		{}

		public bool HasAncestor (Object o)
		{
			return gst_object_has_ancestor (Raw,o.Handle);
		}

		public static new GLib.GType GType{
			get{return new GLib.GType(gst_object_get_type ());}
		}

		public string Name {
			get{return Marshaller.PtrToStringGFree (gst_object_get_name (Raw));}
			set{gst_object_set_name (Raw,Marshaller.StringToPtrGStrdup (value));}
		}

		public string PathString {
			get{return Marshaller.PtrToStringGFree (gst_object_get_path_string (Raw));}
		}

		public Gst.Object Parent {
			get{return new Object(gst_object_get_parent (Raw));}
			set{gst_object_set_parent (Raw,value.Handle);}
		}

		public void Unparent(){gst_object_unparent (Raw);}

		public object this [string name] {
			get {
				Value val = base.GetProperty (name);
				object o = val.Val;
				val.Dispose ();
				return o;
			}
			set{
				Value val = new Value(this,name);
				val.Val = value;

				base.SetProperty (name,val);
				val.Dispose ();
			}
		}
	}
}

