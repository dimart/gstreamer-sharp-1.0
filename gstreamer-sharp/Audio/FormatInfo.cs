using System;
using System.Runtime.InteropServices;
using GstSharp;

namespace Gst.Audio
{
	public delegate void PackFunc (FormatInfo info, PackFlags flags, byte[] data1, byte[] data2, int length);

	public class FormatInfo : GLib.Opaque
	{
		public FormatInfo (IntPtr raw) : base(raw)
		{
		}

		struct GstAudioFormatInfo{
			public Format format;
			public IntPtr name;
			public IntPtr description;
			public FormatFlags flags;
			public int endianness;
			public int width;
			public int depth;
			public IntPtr silence;
			public Format unpack_format;
			public PackFuncNative unpack_func;
			public PackFuncNative pack_func;

			IntPtr _gst_reserved;
		}

		public Gst.Audio.Format Format {
			get {
				GstAudioFormatInfo info = (GstAudioFormatInfo)Marshal.PtrToStructure (Handle,typeof(GstAudioFormatInfo));
				return info.format;
			}
		}

		public Gst.Audio.PackFunc PackFunc {
			set {
				PackFuncWrapper wrapper = new PackFuncWrapper(value);
				GstAudioFormatInfo info = (GstAudioFormatInfo)Marshal.PtrToStructure (Handle,typeof(GstAudioFormatInfo));
				info.pack_func = wrapper.native;
				Marshal.StructureToPtr (info,Raw,false);
			}
			get {
				GstAudioFormatInfo info = (GstAudioFormatInfo)Marshal.PtrToStructure (Handle, typeof(GstAudioFormatInfo));
				PackFuncUnwrapper unwrapper = new PackFuncUnwrapper(info.pack_func);
				return unwrapper.managed;
			}
		}
		public Gst.Audio.PackFunc UnpackFunc {
			set {
				PackFuncWrapper wrapper = new PackFuncWrapper (value);
				GstAudioFormatInfo info = (GstAudioFormatInfo)Marshal.PtrToStructure (Handle, typeof(GstAudioFormatInfo));
				info.unpack_func = wrapper.native;
				Marshal.StructureToPtr (info, Raw, false);
			}
			get {
				GstAudioFormatInfo info = (GstAudioFormatInfo)Marshal.PtrToStructure (Handle, typeof(GstAudioFormatInfo));
				PackFuncUnwrapper unwrapper = new PackFuncUnwrapper(info.unpack_func);
				return unwrapper.managed;
			}
		}

		[DllImport(Application.AudioDll)]
		static extern void gst_audio_format_fill_silence(IntPtr info, IntPtr data, uint length);

		public byte[] Silence {
			get {
				byte[] silence = new byte[8];
				GstAudioFormatInfo info = (GstAudioFormatInfo)Marshal.PtrToStructure (Handle, typeof(GstAudioFormatInfo));
				Marshal.Copy (info.silence, silence, 0, silence.Length);
				return silence;
			}
			set {
				IntPtr data = IntPtr.Zero;
				Marshal.Copy (value,0,data,value.Length);
				gst_audio_format_fill_silence (Handle, data,(uint)value.Length);
			}
		}
	}
}

