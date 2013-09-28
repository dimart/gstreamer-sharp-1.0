using System;
using System.Runtime.InteropServices;

namespace Gst.Audio
{
		public enum SlaveMethod
		{
			Resample,
			Retimestamp,
			Skew,
			None
		}

	public class BaseSrc : Gst.Base.PushSrc
	{

		[DllImport(Application.AudioDll)]
		static extern IntPtr gst_audio_base_src_create_ringbuffer (IntPtr src);
		[DllImport(Application.AudioDll)]
		static extern bool gst_audio_base_src_get_provide_clock (IntPtr src);
		[DllImport(Application.AudioDll)]
		static extern void gst_audio_base_src_set_provide_clock (IntPtr src, bool provide);
		[DllImport(Application.AudioDll)]
		static extern SlaveMethod gst_audio_base_src_get_slave_method (IntPtr src);
		[DllImport(Application.AudioDll)]
		static extern void gst_audio_base_src_set_slave_method (IntPtr src, SlaveMethod method);

		protected BaseSrc (IntPtr raw) : base(raw)
		{
		}

		public RingBuffer CreateRingBuffer(){
			return new RingBuffer(gst_audio_base_src_create_ringbuffer(Handle));
		}

		public bool ProvideClock {
			get { return gst_audio_base_src_get_provide_clock (Handle); }
			set { gst_audio_base_src_set_provide_clock (Handle, value); }
		}

		public SlaveMethod SlaveMethod {
			get { return gst_audio_base_src_get_slave_method (Handle); }
			set { gst_audio_base_src_set_slave_method (Handle, value); }
		}
	}
}

