using System;
using System.Runtime.InteropServices;
using Gst;
using Gst.BasePlugins;
using Gst.Video;
using GLib;

namespace GstSharp
{
	public class ObjectManager
	{
		public static void Register(){
			GType.Register (Gst.Bus.GType,typeof(Gst.Bus));
			//GType.Register (Clock.GType,typeof(Clock));
			GType.Register (Element.GType,typeof(Element));
			GType.Register (ElementFactory.GType,typeof(ElementFactory));
			GType.Register (Message.GType,typeof(Message));
			GType.Register (Gst.Object.GType,typeof(Gst.Object));
			GType.Register (Structure.GType,typeof(Structure));
			GType.Register (PlaySink.GType,typeof(PlaySink));
			GType.Register (OverlayAdapter.GType,typeof(Overlay));
			GType.Register (TagSetterAdapter.GType,typeof(TagSetter));
			GType.Register (Gst.CorePlugins.FileSink.BufferModeType,typeof(Gst.CorePlugins.BufferMode));
		}
	}
}

