using System;
using System.Runtime.InteropServices;

namespace Gst.CorePlugins
{
	public enum QueueLeaky
	{
		NoLeak,
		LeakUpstream,
		LeakDownstream
	}

	public class Queue : Element
	{
		public Queue (IntPtr raw) : base(raw)
		{
		}
		
		public uint CurrentLevelBuffers {
			get { return (uint)this ["current-level-buffers"]; }
		}
		public uint CurrentLevelBytes {
			get { return (uint)this ["current-level-bytes"]; }
		}
		public ulong CurrentLevelTime {
			get { return (ulong)this ["current-level-time"]; }
		}
		public bool FlushOnEos {
			get { return (bool)this ["flush-on-eos"]; }
			set { this ["flush-on-eos"] = value; }
		}
		public QueueLeaky Leaky {
			get { return (QueueLeaky)this ["leaky"]; }
			set { this ["leaky"] = value; }
		}
		public uint MaxSizeBuffers {
			get { return (uint)this ["max-size-buffers"]; }
			set { this ["max-size-buffers"] = value; }
		}
		public uint MaxSizeBytes {
			get { return (uint)this ["max-size-bytes"]; }
			set { this ["max-size-bytes"] = value; }
		}
		public ulong MaxSizeTime {
			get { return (ulong)this ["max-size-time"]; }
			set { this ["max-size-time"] = value; }
		}
		public uint MinThresoldBuffers {
			get { return (uint)this ["min-threshold-buffers"]; }
			set { this ["min-threshold-buffers"] = value; }
		}
		public uint MinThresoldBytes {
			get { return (uint)this ["min-threshold-bytes"]; }
			set { this ["min-threshold-bytes"] = value; }
		}
		public ulong MinThresoldTime {
			get { return (ulong)this ["min-thresold-time"]; }
			set { this ["min-thresold-time"] = value; }
		}
		public bool Silent {
			get { return (bool)this ["silent"]; }
			set { this ["silent"] = value; }
		}
		
		[GLib.Signal("overrun")]
		public event EventHandler Overrun {
			add{
				base.AddSignalHandler ("overrun", value);
			}
			remove{
				base.RemoveSignalHandler ("overrun", value);
			}
		}
		[GLib.Signal("pushing")]
		public event EventHandler Pushing {
			add{
				base.AddSignalHandler ("pushing", value);
			}
			remove{
				base.RemoveSignalHandler ("pushing", value);
			}
		}
		[GLib.Signal("running")]
		public event EventHandler Running {
			add{
				base.AddSignalHandler ("running", value);
			}
			remove{
				base.RemoveSignalHandler ("running", value);
			}
		}
		[GLib.Signal("underrun")]
		public event EventHandler Underrun {
			add{
				base.AddSignalHandler ("underrun", value);
			}
			remove{
				base.RemoveSignalHandler ("underrun", value);
			}
		}
	}
}

