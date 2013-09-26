using System;
using System.Runtime.InteropServices;

namespace Gst.CorePlugins
{
	public enum BufferMode
	{
		Default = -1, 
		Full = 0, 
		Line = 1, 
		Unbuffered = 2
	}

	public class FileSink : Gst.Base.BaseSink
	{

		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make (IntPtr element, IntPtr name);

		public static GLib.GType BufferModeType
		{
			get {
				return GLib.GType.FromName ("GstFileSinkBufferMode");
			}
		}

		public FileSink (IntPtr raw) : base(raw)
		{
		}
		public FileSink (string name)
			: base(gst_element_factory_make (
				Marshal.StringToHGlobalAuto ("filesink"),
				Marshal.StringToHGlobalAuto (name)
				))
		{

		}
		public FileSink() : this(null)
		{}

		public bool Append {
			get { return (bool)this ["append"]; }
			set { this ["append"] = value; }
		}
		public Gst.CorePlugins.BufferMode BufferMode {
			get { return (Gst.CorePlugins.BufferMode)this ["buffer-mode"]; }
			set { this ["buffer-mode"] = value; }
		}
		public uint BufferSize {
			get { return (uint)this ["buffer-size"]; }
			set { this ["buffer-size"] = value; }
		}
		public string Location {
			get { return (string)this ["location"]; }
			set { this ["location"] = value; }
		}
	}
}

