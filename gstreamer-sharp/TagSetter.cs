using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public enum TagMergeMode
	{
		Undefined,
		ReplaceAll,
		Replace,
		Append,
		Prepend,
		Keep,
		KeepAll,
		Count
	}

	public interface TagSetter : GLib.IWrapper
	{
		TagMergeMode MergeMode{get;set;}
	}

	public class TagSetterAdapter : GLib.IWrapper, TagSetter
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_tag_setter_get_type();
		[DllImport(Application.Dll)]
		static extern void gst_tag_setter_set_tag_merge_mode(IntPtr setter, TagMergeMode mode);
		[DllImport(Application.Dll)]
		static extern TagMergeMode gst_tag_setter_get_tag_merge_mode(IntPtr setter);

		public static GLib.GType GType {
			get{return new GLib.GType(gst_tag_setter_get_type ());}
		}

		IntPtr ptr;

		public IntPtr Handle {
			get{return ptr;}
		}

		public TagSetterAdapter (IntPtr raw)
		{
			ptr = raw;
		}

		public TagMergeMode MergeMode {
			get{return gst_tag_setter_get_tag_merge_mode (ptr);}
			set{gst_tag_setter_set_tag_merge_mode (ptr,value);}
		}
	}
}

