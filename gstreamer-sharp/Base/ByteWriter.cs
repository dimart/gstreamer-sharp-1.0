using System;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class ByteWriter : GLib.Opaque
	{
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_byte_writer_new ();
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_byte_writer_new_with_size (uint size, bool initialized);
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_byte_writer_new_with_data (IntPtr data, uint size, bool initialized);
		[DllImport(Application.BaseDll)]
		static extern void gst_byte_writer_free (IntPtr writer);
		[DllImport(Application.BaseDll)]
		static extern uint gst_byte_writer_get_size (IntPtr writer);

		~ByteWriter()
		{
			gst_byte_writer_free (Handle);
		}

		[StructLayout(LayoutKind.Sequential)]
		struct GstByteWriter
		{
			public ByteReader.GstByteReader parent;
			public uint alloc_size;
			public bool @fixed;
			public bool owned;

			IntPtr _gst_reserved;
		}

		public ByteWriter (IntPtr raw) : base(raw)
		{
		}

		public ByteWriter ()
		{
			Raw = gst_byte_writer_new ();
		}
		public ByteWriter (uint size, bool initialized)
		{
			Raw = gst_byte_writer_new_with_size (size, initialized);
		}
		public ByteWriter (byte[] data, bool initialized)
		{
			IntPtr _data = Marshal.AllocHGlobal (data.Length);
			Marshal.Copy (data, 0, _data, data.Length);
			Raw = gst_byte_writer_new_with_data (_data, data.Length, initialized);
		}
		public ByteWriter (uint size) : this(size, true)
		{
		}
		public ByteWriter (byte[] data) : this (data, true)
		{
		}

		public ByteReader Parent {
			get { 
				GstByteWriter bw = (GstByteWriter)Marshal.PtrToStructure (Handle, typeof(GstByteWriter));
				IntPtr br = Marshal.AllocHGlobal (Marshal.SizeOf(typeof (ByteReader.GstByteReader)));
				Marshal.StructureToPtr (bw.parent, br, false);
				return new ByteReader (br);
			}
		}
		public uint AllocSize {
			get { 
				GstByteWriter bw = (GstByteWriter)Marshal.PtrToStructure (Handle, typeof(GstByteWriter));
				return bw.alloc_size;
			}
		}
		public bool Fixed {
			get { 
				GstByteWriter bw = (GstByteWriter)Marshal.PtrToStructure (Handle, typeof(GstByteWriter));
				return bw.@fixed;
			}
		}
		public bool Owned {
			get { 
				GstByteWriter bw = (GstByteWriter)Marshal.PtrToStructure (Handle, typeof(GstByteWriter));
				return bw.owned;
			}
		}
		public uint Size {
			get { 
				return gst_byte_writer_get_size (Handle);
			}
		}
	}
}

