using System;
using System.Runtime.InteropServices;
using GstSharp;

namespace Gst
{
	public class Application
	{
		public const string Dll = "libgstreamer-1.0-0.dll";
		public const string VideoDll = "libgstvideo-1.0-0.dll";
		public const string GlueDll = "libgstglue-1.0.so";
		public const string AudioDll = "libgstaudio-1.0-0.dll";
		public const string BaseDll = "libgstbase-1.0-0.dll";

		[DllImport(Dll)]
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

