using System;
using System.Runtime.InteropServices;

namespace Gst.Video
{

	public interface ColorBalance : GLib.IWrapper
	{
		GLib.List Channels{get;}
		int this[ColorBalanceChannel channel] {get;set;}
	}

	public class ColorBalanceAdapter : GLib.IWrapper, ColorBalance
	{
		IntPtr ptr;

		public ColorBalanceAdapter (IntPtr raw)
		{
			ptr = raw;
		}

		[DllImport(Application.VideoDll)]
		static extern IntPtr gst_color_balance_list_channels(IntPtr cb);
		[DllImport(Application.VideoDll)]
		static extern int gst_color_balance_get_value(IntPtr o, IntPtr channel);
		[DllImport(Application.VideoDll)]
		static extern void gst_color_balance_set_value(IntPtr o, IntPtr channel, int val);

		public IntPtr Handle {
			get{return ptr;}
		}

		public GLib.List Channels {
			get{
				return new GLib.List(gst_color_balance_list_channels(ptr));
			}
		}

		public int this [ColorBalanceChannel channel] {
			get{
			return gst_color_balance_get_value (Handle,channel.Handle);
			}
			set{gst_color_balance_set_value (Handle,channel.Handle,value);}
		}
	}
}

