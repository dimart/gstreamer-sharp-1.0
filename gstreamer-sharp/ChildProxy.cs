using System;

namespace Gst
{
	public interface ChildProxy
	{
		GLib.Object GetChildByName(string name);
		GLib.Object GetChildByIndex(uint index);
		uint ChildrenCount{get;}
	}
}

