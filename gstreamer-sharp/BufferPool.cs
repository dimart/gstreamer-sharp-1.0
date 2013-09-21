using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class BufferPool : Gst.Object
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_pool_new ();
		[DllImport(Application.Dll)]
		static extern bool gst_buffer_pool_set_active (IntPtr pool, bool active);
		[DllImport(Application.Dll)]
		static extern bool gst_buffer_pool_is_active (IntPtr pool);
		[DllImport(Application.Dll)]
		static extern bool gst_buffer_pool_set_config (IntPtr pool, IntPtr structure);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_pool_get_config (IntPtr pool);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_pool_get_options (IntPtr pool);
		[DllImport(Application.Dll)]
		static extern FlowReturn gst_buffer_pool_acquire_buffer (IntPtr pool, out IntPtr buffer, IntPtr prms);
		[DllImport(Application.Dll)]
		static extern void gst_buffer_pool_release_buffer (IntPtr pool, IntPtr buffer);

		public BufferPool (IntPtr raw) : base(raw)
		{
		}
		public BufferPool() : this(gst_buffer_pool_new ())
		{}

		public bool Active {
			get {
				return gst_buffer_pool_is_active (Handle);
			}
			set {
				gst_buffer_pool_set_active (Handle, value);
			}
		}
		public Structure Config {
			get {
				return new Structure (gst_buffer_pool_get_config (Handle));
			}
			set {
				gst_buffer_pool_set_config (Handle, value.Handle);
			}
		}

		public string[] Options{
			get {
				return GLib.Marshaller.PtrToStringArrayGFree (gst_buffer_pool_get_options (Handle));
			}
		}
		public bool HasOption(string name){
			foreach (string val in Options)
				if (val == name)
					return true;
			return false;
		}
		public FlowReturn AcquireBuffer(out Buffer buffer, AcquireParams prms){
			IntPtr p = IntPtr.Zero;
			Marshal.StructureToPtr (prms, p, true);
			IntPtr buf;
			var ret = gst_buffer_pool_acquire_buffer (Handle, out buf, p);
			buffer = new Buffer (buf);
			return ret;
		}
		public void ReleaseBuffer(Buffer buffer){
			gst_buffer_pool_release_buffer (Handle, buffer.Handle);
		}

		[Flags]
		public enum AcquireFlags{
			None     = 0,
			KeyUnit  = 1 << 0,
			DontWait = 1 << 1,
			Discont  = 1 << 2,
			Last     = 1 << 16
		}

		public struct AcquireParams {
			public Format format;
			public Int64 start;
			public Int64 stop;
			public AcquireFlags flags;

			IntPtr _gst_reserved;
		}
	}
}

