using System;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class PushSrc : Src
	{
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_pushsrc_create (IntPtr src, out IntPtr buffer);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_pushsrc_alloc (IntPtr src, out IntPtr buffer);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_pushsrc_fill (IntPtr src, IntPtr buffer);

		protected PushSrc (IntPtr raw) : base(raw)
		{
		}
		
		protected FlowReturn Create(out Buffer buffer){
			IntPtr b;
			FlowReturn fr = gstsharp_pushsrc_create (Handle, out b);
			buffer = new Buffer (b);
			return fr;
		}
		protected FlowReturn Alloc(out Buffer buffer){
			IntPtr b;
			FlowReturn fr = gstsharp_pushsrc_alloc (Handle, out b);
			buffer = new Buffer (b);
			return fr;
		}
		protected FlowReturn Fill(Buffer buffer){
			return gstsharp_pushsrc_fill (Handle, buffer.Handle);
		}
	}
}

