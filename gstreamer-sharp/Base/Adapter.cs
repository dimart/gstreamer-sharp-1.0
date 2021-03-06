using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class Adapter : GLib.Opaque
	{
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_adapter_new ();
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_adapter_map(IntPtr adapter, uint size);
		[DllImport(Application.BaseDll)]
		static extern void gst_adapter_unmap (IntPtr adapter);
		[DllImport(Application.BaseDll)]
		static extern void gst_adapter_copy (IntPtr adapter, IntPtr dest, uint offset, uint size);
		[DllImport(Application.BaseDll)]
		static extern void gst_adapter_clear (IntPtr adapter);
		[DllImport(Application.BaseDll)]
		static extern void gst_adapter_push (IntPtr adapter, IntPtr buffer);
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_adapter_take (IntPtr adapter, UInt32 size);
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_adapter_take_buffer (IntPtr adapter, UInt32 size);
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_adapter_take_list (IntPtr adapter, UInt32 size);

		public Adapter (IntPtr raw) : base(raw)
		{
		}
		public Adapter()
			: this(gst_adapter_new())
		{}

		public byte[] Map(uint size){
			byte[] data = new byte[size];
			IntPtr src = gst_adapter_map (Handle, size);
			Marshal.Copy (src, data, 0, (int)size);
			return data;
		}
		public void Unmap(){ gst_adapter_unmap (Handle); }
		public void Copy(out byte[] data, uint size, uint offset){
			data = new byte[size];
			IntPtr dest = Marshal.AllocHGlobal (data.Length);
			gst_adapter_copy (Handle, dest, offset, (uint)data.Length);
			Marshal.Copy (dest, data, 0, data.Length);
		}
		public void Clear(){
			gst_adapter_clear (Handle);
		}
		public void Push(Buffer buffer){
			gst_adapter_push (Handle, buffer.Handle);
		}
		public byte[] Take(UInt32 size){
			IntPtr dest = gst_adapter_take (Handle, size);
			byte[] res = new byte[size];
			Marshal.Copy (dest, res, 0, (int)size);
			return res;
		}
		public Buffer TakeBuffer(UInt32 size){
			return new Buffer (gst_adapter_take_buffer(Handle,size));
		}
		/*
		public GLib.List TakeList(UInt32 size){
			return new GLib.List (gst_adapter_take_list (Handle, size));
		}
		*/
		public List<Buffer> TakeList(uint size) {
			GLib.List l = new GLib.List (gst_adapter_take_list (Handle, size));
			List<Buffer> list = new List<Buffer> ();
			for (var i = 0; i < l.Count; i++)
				list.Add ((Buffer)l[i]);
			return list;
		}
	}
}

