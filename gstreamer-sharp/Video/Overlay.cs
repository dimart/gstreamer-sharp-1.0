using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gst.Video
{
	public interface Overlay : GLib.IWrapper
	{
		void Expose();
		void HandleEvents(bool handle_events);
		void SetRenderRectangle(int x, int y, int width, int height);
		UIntPtr WindowHandle{set;}
	}

	public class OverlayAdapter :  GLib.IWrapper, Overlay
	{
		[DllImport(Application.VideoDll)]
		static extern IntPtr gst_video_overlay_get_type();

		[DllImport(Application.VideoDll)]
		static extern bool gst_video_overlay_set_render_rectangle(IntPtr o,
		                                                         int x, int y, int width, int height );

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_set_window_handle(IntPtr o, UIntPtr handle);

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_expose(IntPtr o);

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_handle_events(IntPtr o, bool b);

		IntPtr ptr;

		public static GLib.GType GType {
			get{return new GLib.GType(gst_video_overlay_get_type ());}
		}

		public  IntPtr Handle {
			get{return ptr;}
		}

		public OverlayAdapter (IntPtr raw)
		{
			ptr = raw;
		}

		public void SetRenderRectangle(int x, int y, int width, int height){
			gst_video_overlay_set_render_rectangle(Handle,x,y,width,height);
		}

		public void Expose(){
			gst_video_overlay_expose (Handle);
		}
		public void HandleEvents(bool handle_events){
			gst_video_overlay_handle_events (Handle,handle_events);
		}


		public UIntPtr WindowHandle {
			set{
gst_video_overlay_set_window_handle(Handle,value);
			}
		}
	}
}

