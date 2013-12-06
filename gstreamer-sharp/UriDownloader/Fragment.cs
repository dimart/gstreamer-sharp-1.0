using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Fragment : Object
	{
		[DllImport(Application.UriDownloaderDll)]
		static extern IntPtr gst_fragment_new ();
		[DllImport(Application.UriDownloaderDll)]
		static extern IntPtr gst_fragment_get_buffer (IntPtr fragment);
		[DllImport(Application.UriDownloaderDll)]
		static extern IntPtr gst_fragment_get_caps (IntPtr fragment);
		[DllImport(Application.UriDownloaderDll)]
		static extern void gst_fragment_set_caps (IntPtr fragment, IntPtr caps);
		[DllImport(Application.UriDownloaderDll)]
		static extern bool gst_fragment_add_buffer (IntPtr fragment, IntPtr buffer);

		[StructLayout(LayoutKind.Sequential)]
		struct GstFragment
		{

		}

		public Fragment (IntPtr raw) : base (raw)
		{
		}

		public Fragment ()
		{
			Raw = gst_fragment_new ();
		}

		public bool AddBuffer (Gst.Buffer buffer)
		{
			return gst_fragment_add_buffer (Handle, buffer.Handle);
		}

		public Gst.Buffer Buffer 
		{
			get { 
				return new Gst.Buffer (gst_fragment_get_buffer (Handle));
			}
		}

		public Gst.Caps Caps
		{
			get { 
				return new Gst.Caps (gst_fragment_get_caps (Handle));
			}
			set { 
				gst_fragment_set_caps (Handle, value.Handle);
			}
		}
	}
}

