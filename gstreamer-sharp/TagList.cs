using System;
using System.Collections.Generic;

using System.Runtime.InteropServices;

namespace Gst
{
	public class TagList : MiniObject 
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_tag_list_nth_tag_name (IntPtr list, uint index);
		[DllImport(Application.Dll)]
		static extern int gst_tag_list_n_tags (IntPtr list);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_tag_list_new_empty ();

		public TagList (IntPtr raw) : base(raw)
		{
		}
		public TagList() : base(gst_tag_list_new_empty ())
		{}

		public int Size {
			get {
				return gst_tag_list_n_tags (Handle);
			}
		}
		public string[] TagsNames {
			get {
				var list = new List<string>();
				for(var i = 0; i < Size; i++)
					list.Add (Marshal.PtrToStringAuto (gst_tag_list_nth_tag_name (Handle,(uint)i)));
				return list.ToArray ();
			}
		}
	}
}

