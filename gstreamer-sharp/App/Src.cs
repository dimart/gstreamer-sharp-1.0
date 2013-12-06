using System;
using System.Runtime.InteropServices;

namespace Gst.App
{
	public enum StreamType
	{
		Stream, Seekable, RandomAccess
	}

	public class Src : Gst.Base.Src
	{
		[DllImport(Application.AppDll)]
		static extern void gst_app_src_set_caps (IntPtr appsrc, IntPtr caps);
		[DllImport(Application.AppDll)]
		static extern IntPtr gst_app_src_get_caps (IntPtr appsrc);
		[DllImport(Application.AppDll)]
		static extern void gst_app_src_set_size (IntPtr appsrc, long size);
		[DllImport(Application.AppDll)]
		static extern long gst_app_src_get_size (IntPtr appsrc);
		[DllImport(Application.AppDll)]
		static extern void gst_app_src_set_stream_type (IntPtr appsrc, StreamType type);
		[DllImport(Application.AppDll)]
		static extern StreamType gst_app_src_get_stream_type (IntPtr appsrc);
		[DllImport(Application.AppDll)]
		static extern void gst_app_src_set_max_bytes (IntPtr appsrc, ulong max_bytes);
		[DllImport(Application.AppDll)]
		static extern ulong gst_app_src_get_max_bytes (IntPtr appsrc);
		[DllImport(Application.AppDll)]
		static extern ulong gst_app_src_get_current_level_bytes (IntPtr appsrc);
		[DllImport(Application.AppDll)]
		static extern void gst_app_src_set_latency (IntPtr appsrc, ulong min, ulong max);
		[DllImport(Application.AppDll)]
		static extern void gst_app_src_get_latency (IntPtr appsrc, out ulong min, out ulong max);
		[DllImport(Application.AppDll)]
		static extern void gst_app_src_set_emit_signals (IntPtr appsrc, bool emit_signals);
		[DllImport(Application.AppDll)]
		static extern bool gst_app_src_get_emit_signals (IntPtr appsrc);
		[DllImport(Application.AppDll)]
		static extern FlowReturn gst_app_src_push_buffer (IntPtr appsrc, IntPtr buffer);
		[DllImport(Application.AppDll)]
		static extern FlowReturn gst_app_src_end_of_stream (IntPtr appsrc);

		public Src (IntPtr raw) : base (raw)
		{
		}

		public FlowReturn EndOfStream ()
		{
			return gst_app_src_end_of_stream (Handle);
		}

		public void GetLatency (out ulong min, out ulong max)
		{
			gst_app_src_get_latency (Handle, out min, out max);
		}

		public FlowReturn PushBuffer (Buffer buffer)
		{
			return gst_app_src_push_buffer (Handle, buffer.Handle);
		}

		public void SetLatency (ulong min, ulong max)
		{
			gst_app_src_set_latency (Handle, min, max);
		}

		public Gst.Caps Caps {
			get { 
				return new Gst.Caps (gst_app_src_get_caps (Handle));
			}
			set { 
				gst_app_src_set_caps (Handle, value.Handle);
			}
		}
		public ulong CurrentLevelBytes {
			get { 
				return gst_app_src_get_current_level_bytes (Handle);
			}
		}
		public bool EmitSignals {
			get { 
				return gst_app_src_get_emit_signals (Handle);
			}
			set { 
				gst_app_src_set_emit_signals (Handle, value);
			}
		}
		public ulong MaxBytes {
			get { 
				return gst_app_src_get_max_bytes (Handle);
			}
			set { 
				gst_app_src_set_max_bytes (Handle, value);
			}
		}
		public long Size {
			get { 
				return gst_app_src_get_size (Handle);
			}
			set { 
				gst_app_src_set_size (Handle, value);
			}
		}
		public Gst.App.StreamType StreamType {
			get { 
				return gst_app_src_get_stream_type (Handle);
			}
			set { 
				gst_app_src_set_stream_type (Handle, value);
			}
		}

		[GLib.Signal("enough-data")]
		public event EventHandler EnoughData
		{
			add {
				base.AddSignalHandler("enough-data", value);
			}
			remove {
				base.RemoveSignalHandler ("enough-data", value);
			}
		}

		[GLib.Signal("need-data")]
		public event NeedDataHandler NeedData
		{
			add {
				base.AddSignalHandler("need-data", value, typeof (NeedDataArgs));
			}
			remove {
 				base.RemoveSignalHandler ("need-data", value);
			}
		}

		[GLib.Signal("seek-data")]
		public event SeekDataHandler SeekData
		{
			add {
				base.AddSignalHandler("seek-data", value, typeof(SeekDataArgs));
			}
			remove {
				base.RemoveSignalHandler ("seek-data", value);
			}
		}
	}

	public delegate bool SeekDataHandler (object o, SeekDataArgs args);
	public delegate void NeedDataHandler (object o, NeedDataArgs args);

	public class NeedDataArgs : GLib.SignalArgs
	{
		//
		// Properties
		//
		public uint Length
		{
			get
			{
				return (uint)base.Args [0];
			}
		}
	}

	public class SeekDataArgs : GLib.SignalArgs
	{
		//
		// Properties
		//
		public ulong Offset
		{
			get
			{
				return (ulong)base.Args [0];
			}
		}
	}
}

