using System;
using System.Runtime.InteropServices;

namespace Gst
{
	
	public class MiniObject : IDisposable, GLib.IWrapper
	{
		delegate IntPtr cf(IntPtr data);
		delegate bool df(IntPtr data);
		delegate void ff(IntPtr data);
		
		[StructLayout(LayoutKind.Sequential)]
		struct GstMiniObject {
		  public IntPtr   type;
			
		  /*< public >*/ /* with COW */
		  int    refcount;
		  int    lockstate;
		  uint   flags;
		
		  cf copy;
		  df dispose;
		  ff free;
		
		  /* < private > */
		  /* Used to keep track of weak ref notifies and qdata */
		  uint n_qdata;
		  IntPtr qdata;
		}
		
		[DllImport(Application.Dll)]
		static extern void gst_mini_object_unref(IntPtr obj);

		[DllImport(Application.GlueDll)]
		static extern IntPtr gst_mini_object_get_type (IntPtr obj);

		IntPtr handle;

		public MiniObject () : this(IntPtr.Zero)
		{
		}

		public MiniObject (IntPtr raw)
		{
			handle = raw;
		}

		public GLib.GType GType {
			get{
				IntPtr t = ((GstMiniObject)Marshal.PtrToStructure (handle,typeof(GstMiniObject))).type;
				return new GLib.GType(t);
			}
		}

		public IntPtr Handle {
			get{ return handle;}
			set{handle = value;}
		}

		public void Dispose(){
			gst_mini_object_unref (Handle);
		}
	}
}

