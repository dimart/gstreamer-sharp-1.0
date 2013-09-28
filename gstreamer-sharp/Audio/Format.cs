using System;

namespace Gst.Audio
{
	[Flags]
	public enum FormatFlags
	{
		Integer = 1 << 0,
		Float   = 1 << 1,
		Signed  = 1 << 2,
		Complex = 1 << 4,
		Unpack  = 1 << 5
	}

	[Flags]
	public enum PackFlags
	{
		None = 0
	}

	public enum Format
	{
	}
}

