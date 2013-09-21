using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class BufferList : MiniObject
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_list_new ();
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_list_new_sized (uint size);
		[DllImport(Application.Dll)]
		static extern uint gst_buffer_list_length (IntPtr list);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_list_get (IntPtr list, uint index);

		public BufferList (IntPtr raw) : base(raw)
		{

		}
		public BufferList()
			: this(gst_buffer_list_new ())
		{

		}
		public BufferList(uint size)
			: this(gst_buffer_list_new_sized (size))
		{

		}

		public uint Length {
			get {
				return gst_buffer_list_length (Handle);
			}
		}
	}
}

