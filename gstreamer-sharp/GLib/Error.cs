using System;
using System.Runtime.InteropServices;

namespace GLib
{
	public struct Error
	{
		public uint domain;
		public int code;
		public IntPtr message;
	}
}

