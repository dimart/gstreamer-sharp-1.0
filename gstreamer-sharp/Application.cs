using System;
using System.Runtime.InteropServices;
using GstSharp;

namespace Gst
{
	public class Application
	{
		public const string Dll = "gstreamer-1.0";
		public const string VideoDll = "gstvideo-1.0";
		public const string GlueDll = "gstglue-1.0";
		public const string AudioDll = "gstaudio-1.0";

		[DllImport("gstreamer-1.0")]
		static extern void gst_init(ref int argc,[MarshalAs(UnmanagedType.LPArray)]ref string[] argv);

		[DllImport(Dll)]
		static extern IntPtr gst_version_string();

		public static void Init(ref int argc, ref string[] argv){
			gst_init (ref argc, ref argv);
			ObjectManager.Register ();
		}

		public static string Version {
			get{
				return Marshal.PtrToStringAuto (gst_version_string ());
			}
		}

		public static void Init(){
			int i = 0;
			string[] t = new string[0];
			Init (ref i, ref t);
		}
	}
}

