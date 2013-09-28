using System;
using System.Runtime.InteropServices;

namespace Gst.Audio
{
	public class Src : BaseSrc
	{
		[DllImport(Application.AudioDll)]
		static extern bool gstsharp_audio_src_open (IntPtr src);
		[DllImport(Application.AudioDll)]
		static extern bool gstsharp_audio_src_prepare (IntPtr src, IntPtr spec);

		protected Src (IntPtr raw) : base(raw)
		{
		}

		public bool Open(){
			return gstsharp_audio_src_open (Handle);
		}
	}
}

