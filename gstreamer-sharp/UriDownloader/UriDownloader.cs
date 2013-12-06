using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class UriDownloader : Object
	{
		[DllImport(Application.UriDownloaderDll)]
		static extern IntPtr gst_uri_downloader_new ();
		[DllImport(Application.UriDownloaderDll)]
		static extern IntPtr gst_uri_downloader_fetch_uri_with_range (IntPtr downloader, IntPtr uri, long start, long end);
		[DllImport(Application.UriDownloaderDll)]
		static extern IntPtr gst_uri_downloader_fetch_uri (IntPtr downloader, IntPtr uri);
		[DllImport(Application.UriDownloaderDll)]
		static extern void gst_uri_downloader_free (IntPtr downloader);
		[DllImport(Application.UriDownloaderDll)]
		static extern void gst_uri_downloader_reset (IntPtr downloader);
		[DllImport(Application.UriDownloaderDll)]
		static extern void gst_uri_downloader_cancel (IntPtr downloader);

		~UriDownloader ()
		{
			gst_uri_downloader_free (Handle);
		}

		public UriDownloader (IntPtr raw) : base (raw)
		{
		}

		public UriDownloader ()
		{
			Raw = gst_uri_downloader_new ();
		}

		public void Cancel ()
		{
			gst_uri_downloader_cancel (Handle);
		}

		public Fragment FetchUri (string uri)
		{
			return new Fragment (gst_uri_downloader_fetch_uri(Handle, Marshal.StringToHGlobalAuto(uri)));
		}

		public Fragment FetchUri (string uri, long start, long end)
		{
			return new Fragment (gst_uri_downloader_fetch_uri_with_range(
				Handle,
				Marshal.StringToHGlobalAuto(uri),
				start,
				end));
		}

		public void Reset ()
		{
			gst_uri_downloader_reset (Handle);
		}
	}
}

