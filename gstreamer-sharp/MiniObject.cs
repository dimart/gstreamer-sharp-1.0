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
	[Flags]
	public enum LockFlags
	{
		Read      = 1 << 0,
		Write     = 1 << 1,
		Exclusive = 1 << 2,
		Last      = 1 << 8
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
		 public int    refcount;
		  int    lockstate;
		 public uint   flags;
		
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
		[DllImport(Application.Dll)]
		public static extern bool gst_mini_object_lock(IntPtr obj, int flags);
		[DllImport(Application.Dll)]
		public static extern bool gst_mini_object_unlock(IntPtr obj, int flags);
		[DllImport(Application.Dll)]
		public static extern bool gst_mini_object_is_writable(IntPtr obj);

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
		public int RefCount {
			get{
				return ((GstMiniObject)Marshal.PtrToStructure (handle,typeof(GstMiniObject))).refcount;
			}
		}
		public MiniObjectFlags Flags {
			get{return (MiniObjectFlags)((GstMiniObject)Marshal.PtrToStructure (handle,typeof(GstMiniObject))).flags;}
		}

		public IntPtr Handle {
			get{ return handle;}
			set{handle = value;}
		}

		public bool Lock (LockFlags flags)
		{
			return gst_mini_object_lock (handle,(int)flags);
		}
		public bool Unlock (LockFlags flags)
		{
			return gst_mini_object_unlock (handle,(int)flags);
		}
		public bool IsWritable {
			get{
				return gst_mini_object_is_writable (handle);
			}
		}

		public void Dispose(){
			gst_mini_object_unref (Handle);
		}
	}
}

