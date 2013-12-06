using System;
using System.Runtime.InteropServices;

namespace Gst.App
{
	public delegate Gst.FlowReturn NewHandler (object sender, NewHandlerArgs args);

	public class NewHandlerArgs : GLib.SignalArgs
	{}

	public class Sink : Gst.Base.Sink
	{
		[DllImport(Application.AppDll)]
		static extern IntPtr gst_app_sink_pull_preroll (IntPtr appsink);
		[DllImport(Application.AppDll)]
		static extern IntPtr gst_app_sink_pull_sample (IntPtr appsink);
		[DllImport(Application.AppDll)]
		static extern void gst_app_sink_set_caps (IntPtr appsink, IntPtr caps);
		[DllImport(Application.AppDll)]
		static extern IntPtr gst_app_sink_get_caps (IntPtr appsink);
		[DllImport(Application.AppDll)]
		static extern bool gst_app_sink_is_eos (IntPtr appsink);
		[DllImport(Application.AppDll)]
		static extern void gst_app_sink_set_emit_signals (IntPtr appsink, bool emit_signals);
		[DllImport(Application.AppDll)]
		static extern bool gst_app_sink_get_emit_signals (IntPtr appsink);
		[DllImport(Application.AppDll)]
		static extern void gst_app_sink_set_max_buffers (IntPtr appsink, uint max_buffers);
		[DllImport(Application.AppDll)]
		static extern uint gst_app_sink_get_max_buffers (IntPtr appsink);
		[DllImport(Application.AppDll)]
		static extern void gst_app_sink_set_drop (IntPtr appsink, bool drop);
		[DllImport(Application.AppDll)]
		static extern bool gst_app_sink_get_drop (IntPtr appsink);

		[DllImport(Application.Dll)]
		static extern IntPtr gst_element_factory_make(IntPtr factory_name, IntPtr name);

		public Sink (string name)
		{
			Raw = gst_element_factory_make ("appsink", Marshal.StringToHGlobalAuto (name));
		}

		public Sink () : this(null)
		{}

		public Sink (IntPtr raw) : base(raw)
		{
		}

		public Sample PullPreroll ()
		{
			return new Sample (gst_app_sink_pull_preroll (Handle));
		}

		public Sample PullSample ()
		{
			return new Sample (gst_app_sink_pull_sample (Handle));
		}

		public Gst.Caps Caps
		{
			get {
				return new Gst.Caps (gst_app_sink_get_caps (Handle));
			}
			set { 
				gst_app_sink_set_caps (Handle, value.Handle);
			}
		}

		public bool Drop {
			get { 
				return gst_app_sink_get_drop (Handle);
			}
			set {
				gst_app_sink_set_drop (Handle, value);
			}
		}

		public bool EmitSignals {
			get { 
				return gst_app_sink_get_emit_signals (Handle);
			}
			set { 
				gst_app_sink_set_emit_signals (Handle, value);
			}
		}

		public uint MaxBuffers {
			get {
				return gst_app_sink_get_max_buffers (Handle);
			}
			set { 
				gst_app_sink_set_max_buffers (Handle, value);
			}
		}

		[GLib.Signal("eos")]
		public event EventHandler Eos 
		{
			add {
				base.AddSignalHandler ("eos", value);
			}
			remove{
				base.RemoveSignalHandler ("eos", value);
			}
		}
		[GLib.Signal("new-preroll")]
		public event NewHandler NewPreroll
		{
			add {
				base.AddSignalHandler("new-preroll", value, typeof (NewHandlerArgs));
			}
			remove {
				base.RemoveSignalHandler ("new-preroll", value);
			}
		}
		[GLib.Signal("new-sample")]
		public event NewHandler NewSample
		{
			add {
				base.AddSignalHandler("new-sample", value, typeof (NewHandlerArgs));
			}
			remove {
				base.RemoveSignalHandler ("new-sample", value);
			}
		}
	}
}

