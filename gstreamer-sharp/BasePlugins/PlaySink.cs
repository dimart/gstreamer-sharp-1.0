using System;
using System.Runtime.InteropServices;
using GLib;
using Gst.Audio;

namespace Gst.BasePlugins
{
	public class PlaySink : Element, Gst.Video.Overlay, Gst.Audio.StreamVolume
	{
		[DllImport ("gobject-2.0")]
		private static extern IntPtr g_type_from_name  (IntPtr raw);
		[DllImport (Application.GlueDll)]
		private static extern IntPtr gstsharp_g_type_from_instance (IntPtr raw);

		[DllImport(Application.VideoDll)]
		static extern bool gst_video_overlay_set_render_rectangle(IntPtr o,
		                                                         int x, int y, int width, int height );

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_set_window_handle(IntPtr o, UIntPtr handle);

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_expose(IntPtr o);

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_handle_events(IntPtr o, bool b);

		[DllImport(Application.AudioDll)]
		static extern void gst_stream_volume_set_volume(IntPtr a, int format, double val);
		[DllImport(Application.AudioDll)]
		static extern double gst_stream_volume_get_volume(IntPtr a, int format);
		[DllImport(Application.AudioDll)]
		static extern void gst_stream_volume_set_mute (IntPtr a, bool mute);
		[DllImport(Application.AudioDll)]
		static extern bool gst_stream_volume_get_mute(IntPtr a); 

		public static new GLib.GType GType {
			get{
				Element e = ElementFactory.Make ("playsink");
				return new GLib.GType(gstsharp_g_type_from_instance (e.Handle));
			}
		}

		public PlaySink(IntPtr raw) : base(raw)
		{}

		public PlaySink () : this(null)
		{
		}

		public PlaySink(string name) : base(IntPtr.Zero)
		{
			Raw = ElementFactory.Make ("playsink",name).Handle;
		}

		public void Expose(){gst_video_overlay_expose (Raw);}
		public void HandleEvents (bool handle)
		{
			gst_video_overlay_handle_events (Raw, handle);
		}
		public void SetRenderRectangle(int x, int y, int width, int height){
			gst_video_overlay_set_render_rectangle(Raw,x,y,width,height);
		}
		public void SetVolume(StreamVolumeFormat format, double val){
			gst_stream_volume_set_volume (Raw,(int)format,val);
		}
		public double GetVolume(StreamVolumeFormat format){
			return gst_stream_volume_get_volume (Raw,(int)format);
		}
		public double Volume {
			get{return GetVolume (StreamVolumeFormat.Db);}
			set{SetVolume (StreamVolumeFormat.Db,value);}
		}
		public bool Mute {
			get{return gst_stream_volume_get_mute (Raw);}
			set{gst_stream_volume_set_mute(Raw,value);}
		}
		public UIntPtr WindowHandle {
			set{
				gst_video_overlay_set_window_handle(Raw,value);
			}
		}
	}
}

