using System;
using Gst;
using GLib;

namespace test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Gst.Application.Init ();
			TagList.Register ("Toto",TagFlag.Meta,GLib.GType.Double,"toto","toto",
			                  (i,o)=>{

			});
			var list = new TagList ();
			var val = new Value (3);
			list.AddValue (TagMergeMode.Append, "Toto", val);
			Console.WriteLine (list.ToString());
		}
	}
}
