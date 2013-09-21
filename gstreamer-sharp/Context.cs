using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Context : MiniObject
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_context_new ();
		[DllImport(Application.Dll)]
		static extern IntPtr gst_context_get_structure (IntPtr context);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_context_writable_structure (IntPtr context);

		public Context (IntPtr raw) : base(raw)
		{
		}

		public Context()
			: this(gst_context_new ())
		{
		}

		public Gst.Structure Structure {
			get{
				return new Gst.Structure (gst_context_get_structure (Handle));
			}
		}
		public Gst.Structure WritableStructure {
			get {
				return new Gst.Structure (gst_context_writable_structure (Handle));
			}
		}
	}
}

