using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Gst.Audio;
using Gst.Video;

namespace Gst.BasePlugins
{
	public class PlayBin : Pipeline, ChildProxy, StreamVolume, Overlay, ColorBalance
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make (IntPtr element, IntPtr name);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_child_proxy_get_child_by_name(IntPtr parent, IntPtr name);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_child_proxy_get_child_by_index(IntPtr parent, uint index);
		[DllImport(Application.Dll)]
		static extern uint gst_child_proxy_get_children_count(IntPtr parent);
		[DllImport(Application.AudioDll)]
		static extern void gst_stream_volume_set_volume(IntPtr a, int format, double val);
		[DllImport(Application.AudioDll)]
		static extern double gst_stream_volume_get_volume(IntPtr a, int format);
		[DllImport(Application.VideoDll)]
		static extern bool gst_video_overlay_set_render_rectangle(IntPtr o,
		                                                         int x, int y, int width, int height );

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_set_window_handle(IntPtr o, UIntPtr handle);

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_expose(IntPtr o);

		[DllImport(Application.VideoDll)]
		static extern void gst_video_overlay_handle_events(IntPtr o, bool b);
		[DllImport(Application.VideoDll)]
		static extern IntPtr gst_color_balance_list_channels(IntPtr o);
		[DllImport(Application.VideoDll)]
		static extern int gst_color_balance_get_value(IntPtr o, IntPtr channel);
		[DllImport(Application.VideoDll)]
		static extern void gst_color_balance_set_value(IntPtr o, IntPtr channel, int val);

		public PlayBin (string name) : base(name)
		{
			IntPtr n = Marshal.StringToHGlobalAuto (name);
			IntPtr p = Marshal.StringToHGlobalAuto ("playbin");
			Raw = gst_element_factory_make (p,n);
		}

		public PlayBin() : this(null)
		{}

		public GLib.Object GetChildByName(string name){
			return GLib.Object.GetObject(gst_child_proxy_get_child_by_name (Handle,Marshal.StringToHGlobalAuto (name)));
		}
		public GLib.Object GetChildByIndex (uint index)
		{
			return GLib.Object.GetObject(gst_child_proxy_get_child_by_index(Handle,index));
		}
		public uint ChildrenCount {
			get{
				return gst_child_proxy_get_children_count (Handle);
			}
		}
		public void SetVolume(StreamVolumeFormat format, double val){
			gst_stream_volume_set_volume (Handle,(int)format,val);
		}
		public double GetVolume(StreamVolumeFormat format){
			return gst_stream_volume_get_volume (Handle,(int)format);
		}

		public void Expose(){
			gst_video_overlay_expose(Handle);
		}
		public void HandleEvents(bool handle){
			gst_video_overlay_handle_events (Handle,handle);
		}
		public void SetRenderRectangle(int x, int y, int width, int height){
			gst_video_overlay_set_render_rectangle (Handle,x,y,width,height);
		}

		public UIntPtr WindowHandle {
			set{
				gst_video_overlay_set_window_handle(Handle,value);
			}
		}

		public List<ColorBalanceChannel> Channels {
			get{
				GLib.List l = new GLib.List(gst_color_balance_list_channels (Handle));
				List<ColorBalanceChannel> c = new List<ColorBalanceChannel>();
				for(int i=0; i<l.Count; i++)
					c.Add ((ColorBalanceChannel)l[i]);
				return c;
			}
		}

		public int GetValue(ColorBalanceChannel channel){
			return gst_color_balance_get_value (Handle,channel.Handle);
		}
		public void SetValue(ColorBalanceChannel channel, int val){
			gst_color_balance_set_value (Handle,channel.Handle,val);
		}

		public double Volume {
			get{return (double)this["volume"];}
			set{this["volume"] = value;}
		}
		public bool Mute {
			get{return (bool)this["mute"];}
			set{this["mute"] = value;}
		}
		public Element AudioSink {
			get{return (Element)this["audio-sink"];}
			set{this["audio-sink"] = value;}
		}
		public Buffer Frame {
			get{return (Buffer)this["frame"];}
		}
		public string SubtitleFontDesc {
			set{
				this["subtitle-font-desc"] = value;
			}
		}
		public Element VideoSink {
			get{return (Element)this["video-sink"];}
			set{this["video-sink"] = value;}
		}
		public Element VisPlugin {
			get{return (Element)this["vis-plugin"];}
			set{this["vis-plugin"] = value;}
		}

		public string Uri {
			get{return (string)this["uri"];}
			set{this["uri"] = value;}
		}

		public Sample ConvertSample(Caps caps){
			return (Sample)GLib.Signal.Emit(this,"convert-sample",new object[]{caps});
		}

		[GLib.Signal("about-to-finish")]
		public event EventHandler AboutToFinish {
			add{
				base.AddSignalHandler("about-to-finish",value);
			}
			remove{
				base.RemoveSignalHandler ("about-to-finish", value);
			}
		}
	}
}

