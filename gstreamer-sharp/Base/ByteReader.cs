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
		[DllImport(Application.BaseDll)]
		static extern uint gst_byte_reader_get_remaining (IntPtr reader);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_skip (IntPtr reader, uint bytes);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint8 (IntPtr reader, out byte val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int8 (IntPtr reader, out sbyte val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint16_le (IntPtr reader, out ushort val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int16_le (IntPtr reader, out short val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint16_be (IntPtr reader, out ushort val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int16_be (IntPtr reader, out short val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint24_le (IntPtr reader, out uint val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int24_le (IntPtr reader, out int val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint24_be (IntPtr reader, out uint val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int24_be (IntPtr reader, out int val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint32_le (IntPtr reader, out uint val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int32_le (IntPtr reader, out int val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint32_be (IntPtr reader, out uint val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int32_be (IntPtr reader, out int val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint64_le (IntPtr reader, out ulong val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int64_le (IntPtr reader, out long val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_uint64_be (IntPtr reader, out ulong val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_int64_be (IntPtr reader, out long val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_float32_le (IntPtr reader, out float val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_float32_be (IntPtr reader, out float val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_float64_le (IntPtr reader, out double val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_get_float64_be (IntPtr reader, out double val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_float32_le (IntPtr reader, out float val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_float32_be (IntPtr reader, out float val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_float64_le (IntPtr reader, out double val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_float64_be (IntPtr reader, out double val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint8 (IntPtr reader, out byte val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int8 (IntPtr reader, out sbyte val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint16_le (IntPtr reader, out ushort val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int16_le (IntPtr reader, out short val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint16_be (IntPtr reader, out ushort val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int16_be (IntPtr reader, out short val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint24_le (IntPtr reader, out uint val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int24_le (IntPtr reader, out int val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint24_be (IntPtr reader, out uint val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int24_be (IntPtr reader, out int val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint32_le (IntPtr reader, out uint val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int32_le (IntPtr reader, out int val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint32_be (IntPtr reader, out uint val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int32_be (IntPtr reader, out int val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint64_le (IntPtr reader, out ulong val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int64_le (IntPtr reader, out long val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_uint64_be (IntPtr reader, out ulong val);
		[DllImport(Application.BaseDll)]
		static extern bool gst_byte_reader_peek_int64_be (IntPtr reader, out long val);

		[StructLayout(LayoutKind.Sequential)]
		internal struct GstByteReader
		{
			public IntPtr data;
			public uint size;
			public byte @byte;

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

		public bool GetUInt8 (out byte val)
		{
			return gst_byte_reader_get_uint8 (Handle, out val);
		}

		public bool GetInt8 (out sbyte val)
		{
			return gst_byte_reader_get_int8 (Handle, out val);
		}

		public bool GetUInt16BE (out ushort val)
		{
			return gst_byte_reader_get_uint16_be (Handle, out val);
		}

		public bool GetInt16BE (out short val)
		{
			return gst_byte_reader_get_int16_be (Handle, out val);
		}

		public bool GetUInt16LE (out ushort val)
		{
			return gst_byte_reader_get_uint16_le (Handle, out val);
		}

		public bool GetInt16LE (out short val)
		{
			return gst_byte_reader_get_int16_le (Handle, out val);
		}

		public bool GetUInt24BE (out uint val)
		{
			return gst_byte_reader_get_uint24_be (Handle, out val);
		}

		public bool GetInt24BE (out int val)
		{
			return gst_byte_reader_get_int24_be (Handle, out val);
		}

		public bool GetUInt24LE (out uint val)
		{
			return gst_byte_reader_get_uint24_le (Handle, out val);
		}

		public bool GetInt24LE (out int val)
		{
			return gst_byte_reader_get_int24_le (Handle, out val);
		}

		public bool GetUInt32BE (out uint val)
		{
			return gst_byte_reader_get_uint32_be (Handle, out val);
		}

		public bool GetInt32BE (out int val)
		{
			return gst_byte_reader_get_int32_be (Handle, out val);
		}

		public bool GetUInt32LE (out uint val)
		{
			return gst_byte_reader_get_uint32_le (Handle, out val);
		}

		public bool GetInt32LE (out int val)
		{
			return gst_byte_reader_get_int32_le (Handle, out val);
		}

		public bool GetUInt64BE (out ulong val)
		{
			return gst_byte_reader_get_uint64_be (Handle, out val);
		}

		public bool GetInt64BE (out long val)
		{
			return gst_byte_reader_get_int64_be (Handle, out val);
		}

		public bool GetUInt64LE (out ulong val)
		{
			return gst_byte_reader_get_uint64_le (Handle, out val);
		}

		public bool GetInt64LE (out long val)
		{
			return gst_byte_reader_get_int64_le (Handle, out val);
		}

		public bool PeekUInt8 (out byte val)
		{
			return gst_byte_reader_peek_uint8 (Handle, out val);
		}

		public bool PeekInt8 (out sbyte val)
		{
			return gst_byte_reader_peek_int8 (Handle, out val);
		}

		public bool PeekUInt16BE (out ushort val)
		{
			return gst_byte_reader_peek_uint16_be (Handle, out val);
		}

		public bool PeekInt16BE (out short val)
		{
			return gst_byte_reader_peek_int16_be (Handle, out val);
		}

		public bool PeekUInt16LE (out ushort val)
		{
			return gst_byte_reader_peek_uint16_le (Handle, out val);
		}

		public bool PeekInt16LE (out short val)
		{
			return gst_byte_reader_peek_int16_le (Handle, out val);
		}

		public bool PeekUInt24BE (out uint val)
		{
			return gst_byte_reader_peek_uint24_be (Handle, out val);
		}

		public bool PeekInt24BE (out int val)
		{
			return gst_byte_reader_peek_int24_be (Handle, out val);
		}

		public bool PeekUInt24LE (out uint val)
		{
			return gst_byte_reader_peek_uint24_le (Handle, out val);
		}

		public bool PeekInt24LE (out int val)
		{
			return gst_byte_reader_peek_int24_le (Handle, out val);
		}

		public bool PeekUInt32BE (out uint val)
		{
			return gst_byte_reader_peek_uint32_be (Handle, out val);
		}

		public bool PeekInt32BE (out int val)
		{
			return gst_byte_reader_peek_int32_be (Handle, out val);
		}

		public bool PeekUInt32LE (out uint val)
		{
			return gst_byte_reader_peek_uint32_le (Handle, out val);
		}

		public bool PeekInt32LE (out int val)
		{
			return gst_byte_reader_peek_int32_le (Handle, out val);
		}

		public bool PeekUInt64BE (out ulong val)
		{
			return gst_byte_reader_peek_uint64_be (Handle, out val);
		}

		public bool PeekInt64BE (out long val)
		{
			return gst_byte_reader_peek_int64_be (Handle, out val);
		}

		public bool PeekUInt64LE (out ulong val)
		{
			return gst_byte_reader_peek_uint64_le (Handle, out val);
		}

		public bool PeekInt64LE (out long val)
		{
			return gst_byte_reader_peek_int64_le (Handle, out val);
		}

		public bool GetFloat32BE (out float val)
		{
			return gst_byte_reader_get_float32_be (Handle, out val);
		}

		public bool GetFloat32LE (out float val)
		{
			return gst_byte_reader_get_float32_le (Handle, out val);
		}

		public bool GetFloat64BE (out double val)
		{
			return gst_byte_reader_get_float64_be (Handle, out val);
		}

		public bool GetFloat64LE (out double val)
		{
			return gst_byte_reader_get_float64_le (Handle, out val);
		}

		public bool PeekFloat32BE (out float val)
		{
			return gst_byte_reader_peek_float32_be (Handle, out val);
		}

		public bool PeekFloat32LE (out float val)
		{
			return gst_byte_reader_peek_float32_le (Handle, out val);
		}

		public bool PeekFloat64BE (out double val)
		{
			return gst_byte_reader_peek_float64_be (Handle, out val);
		}

		public bool PeekFloat64LE (out double val)
		{
			return gst_byte_reader_peek_float64_le (Handle, out val);
		}

		public bool Skip (uint bytes)
		{
			return gst_byte_reader_skip (Handle, bytes);
		}

		public uint Pos {
			get { return gst_byte_reader_get_pos (Handle); }
			set { gst_byte_reader_set_pos (Handle, value); }
		}
		public uint Remaining {
			get { 
				return gst_byte_reader_get_remaining (Handle);
			}
		}
		public uint Size {
			get { return gst_byte_reader_get_size (Handle); }
		}

		public byte[] Data {
			get { 
				GstByteReader br = (GstByteReader)Marshal.PtrToStructure (Handle, typeof(GstByteReader));
				var bytes = new byte[br.size];
				Marshal.Copy (br.data, bytes, 0, br.size);
				return bytes;
			}
		}

		public static explicit operator byte[] (ByteReader reader)
		{
			return reader.Data;
		}
		public static explicit operator Buffer (ByteReader reader)
		{
			return new Buffer (reader.Data);
		}
	}
}

