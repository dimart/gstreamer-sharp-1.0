using System;
using Gst;
using System.Runtime.InteropServices;

namespace Test
{
	class MainClass
	{
		[DllImport (Application.GlueDll)]
		private static extern IntPtr gstsharp_g_type_from_instance (IntPtr raw);
		[DllImport("gstreamer-1.0")]
		static extern TagMergeMode gst_tag_setter_get_tag_merge_mode(IntPtr setter);

		public static void Main (string[] args)
		{
			Gst.Application.Init ();
			var playbin = ElementFactory.Make ("playbin");
			playbin["uri"] = "file:///home/yannick/Documents/test.mp3";
			playbin.Bus.AddWatch ((bus,message) => {
				var type = TagSetterAdapter.GType;
				Console.WriteLine (type.IsInstance (message.Src.Handle));
				switch(message.Type){
				case MessageType.Error:
					string debug;
					message.ParseError(out debug);
					Console.WriteLine (message.Structure.Name+" :: "+debug);
					break;
				}

				return true;
			});
			playbin.SetState (State.Playing);
			new GLib.MainLoop().Run ();
		}
	}

	public class Toto
	{
		public Toto(){}

		public string ToString(){
			return "Toto";
		}
	}
}
