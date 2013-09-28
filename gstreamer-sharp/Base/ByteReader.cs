using System;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class ByteReader : GLib.Opaque
	{
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_byte_reader_new (IntPtr data, uint size);
		[DllImport(Application.BaseDll)]
		static extern void gst_byte_reader_free (IntPtr reader);
		[DllImport(Application.BaseDll)]
		static extern uint gst_byte_reader_get_pos (IntPtr reader);
		[DllImport(Application.BaseDll)]
		static extern void gst_byte_reader_set_pos (IntPtr reader, uint pos);
		[DllImport(Application.BaseDll)]
		static extern uint gst_byte_reader_get_size (IntPtr reader);

		struct GstByteReader
		{
			IntPtr data;
			uint size;
			byte @byte;

			IntPtr _gst_reserved;
		}

		~ByteReader(){
			gst_byte_reader_free (Handle);
		}

		public ByteReader (IntPtr raw) : base(raw)
		{
		}

		public ByteReader (byte[] data)
		{
			IntPtr dest = IntPtr.Zero;
			Marshal.Copy (data,0,dest,data.Length);
			Raw = gst_byte_reader_new (dest, (uint)data.Length);
		}

		public uint Pos {
			get { return gst_byte_reader_get_pos (Handle); }
			set { gst_byte_reader_set_pos (Handle, value); }
		}
		public uint Size {
			get { return gst_byte_reader_get_size (Handle); }
		}
	}
}

