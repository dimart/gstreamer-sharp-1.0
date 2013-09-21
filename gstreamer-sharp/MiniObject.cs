using System;
using System.Runtime.InteropServices;

namespace Gst
{
	[Flags]
	public enum MiniObjectFlags
	{
		Lockable     = 1 << 0,
		LockReadOnly = 1 << 1,
		Last         = 1 << 4
	}

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
		internal static extern void gst_mini_object_unref(IntPtr obj);
		[DllImport(Application.Dll)]
		internal static extern IntPtr gst_mini_object_ref (IntPtr obj);
		[DllImport(Application.Dll)]
		static extern void gst_mini_object_replace (ref IntPtr obj, IntPtr newobj);
		[DllImport(Application.Dll)]
		static extern void gst_mini_object_take (ref IntPtr obj, IntPtr newobj);
		[DllImport(Application.GlueDll)]
		static extern IntPtr gstsharp_g_type_from_instance (IntPtr instance);

		IntPtr handle;

		public static MiniObject Replace(MiniObject obj){
			IntPtr old = obj.handle;
			IntPtr ptr = IntPtr.Zero;
			gst_mini_object_replace (ref old, ptr);
			obj.Handle = old;
			return new MiniObject (ptr);
		}

		public static MiniObject Take(MiniObject obj){
			IntPtr old = obj.handle;
			IntPtr ptr = IntPtr.Zero;
			gst_mini_object_take (ref old, ptr);
			obj.Handle = old;
			return new MiniObject (ptr);
		}

		public MiniObject () : this(IntPtr.Zero)
		{
		}

		public MiniObject (IntPtr raw)
		{
			handle = raw;
		}

		public GLib.GType GType {
			get{
				return new GLib.GType (gstsharp_g_type_from_instance(handle));
			}
		}

		public IntPtr Handle {
			get{ return handle;}
			internal set{handle = value;}
		}

		public void Dispose(){
			gst_mini_object_unref (Handle);
		}
	}
}

