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
		Read = 1 << 0,
		Write = 1 << 1,
		Exclusive = 1 << 2,
		Last = 1 << 8,
		ReadWrite = LockFlags.Read | LockFlags.Write
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
		static extern void gst_mini_object_unref(IntPtr obj);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_mini_object_ref (IntPtr obj);
		[DllImport(Application.Dll)]
		static extern void gst_mini_object_replace (ref IntPtr obj, IntPtr newobj);
		[DllImport(Application.Dll)]
		static extern void gst_mini_object_take (ref IntPtr obj, IntPtr newobj);
		[DllImport(Application.GlueDll)]
		static extern IntPtr gstsharp_g_type_from_instance (IntPtr instance);
		[DllImport(Application.Dll)]
		static extern bool gst_mini_object_lock (IntPtr o, LockFlags flags);
		[DllImport(Application.Dll)]
		static extern void gst_mini_object_unlock (IntPtr o, LockFlags flags);
		[DllImport(Application.Dll)]
		static extern bool gst_mini_object_is_writable (IntPtr o);
		[DllImport(Application.Dll)]
		static extern void gst_mini_object_make_writable (IntPtr o, bool writable);

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

		~MiniObject(){
			gst_mini_object_unref (handle);
		}

		public MiniObject () : this(IntPtr.Zero)
		{
		}

		public MiniObject (IntPtr raw)
		{
			handle = gst_mini_object_ref (raw);
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

		public bool Writable {
			get { return gst_mini_object_is_writable (handle); }
			set { gst_mini_object_make_writable (handle, value); }
		}

		public bool Lock(LockFlags flags){
			return gst_mini_object_lock (handle, flags);
		}
		public void Unlock(LockFlags flags){
			gst_mini_object_unlock (handle, flags);
		}

		public void Dispose(){
			gst_mini_object_unref (Handle);
		}
	}
}

