using System;

namespace Gst.Audio
{
	[Flags]
	public enum Flags
	{
		None         = 0,
		Unpositioned = 1 << 0
	}

	public enum Layout
	{
		Interleaved = 0,
		NotInterleaved
	}

	public struct GstAudioInfo
	{

	}

	public class Info : GLib.Opaque
	{
		public Info (IntPtr raw) : base(raw)
		{
		}
	}
}

