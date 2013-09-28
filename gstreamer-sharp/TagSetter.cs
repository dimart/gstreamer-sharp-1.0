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
		void AddTag(TagMergeMode mode, string name, GLib.Value val);
		TagMergeMode MergeMode { get; set; }
		TagList TagList { get; }
	}

	[GLib.GInterface(typeof(TagSetterAdapter))]
	public interface TagSetterImplementor : GLib.IWrapper {
		void AddTag(TagMergeMode mode, string name, GLib.Value val);
		TagMergeMode MergeMode { get; set; }
		TagList TagList { get; }
	}

	public class TagSetterAdapter : GLib.IWrapper, TagSetter
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_tag_setter_get_type();
		[DllImport(Application.Dll)]
		static extern void gst_tag_setter_set_tag_merge_mode(IntPtr setter, TagMergeMode mode);
		[DllImport(Application.Dll)]
		static extern TagMergeMode gst_tag_setter_get_tag_merge_mode(IntPtr setter);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_tag_setter_get_tag_list (IntPtr setter);
		[DllImport(Application.Dll)]
		static extern void gst_tag_setter_add_tag_value (IntPtr setter, TagMergeMode mode,
		                                                 IntPtr tag, ref GLib.Value val);

		public static GLib.GType GType {
			get{return new GLib.GType(gst_tag_setter_get_type ());}
		}

		IntPtr ptr;

		public IntPtr Handle
		{
			get
			{
				return ptr;
			}
		}

		static GstTagSetterInterface iface;

		static void Initialize (IntPtr ptr, IntPtr data)
		{
			var offset = 2*IntPtr.Size;
			IntPtr intPtr = new IntPtr (ptr.ToInt64 () + (long)offset);
			iface = (GstTagSetterInterface)Marshal.PtrToStructure (intPtr,typeof(GstTagSetterInterface));
			Marshal.StructureToPtr (iface,intPtr,false);
			GCHandle gc = (GCHandle)data;
			gc.Free ();
		}
		public TagSetterAdapter (TagSetterImplementor implementor)
		{
			if (implementor == null)
			{
				throw new ArgumentNullException ("implementor");
			}
			if (!(implementor is Object))
			{
				throw new ArgumentException ("implementor must be a subclass of GLib.Object");
			}
			ptr = (implementor as Object).Handle;
		}
		public TagSetterAdapter (IntPtr raw)
		{
			ptr = raw;
		}

		public void AddTag (TagMergeMode mode, string name, GLib.Value val){
			gst_tag_setter_add_tag_value (Handle, mode,
			                              Marshal.StringToHGlobalAuto (name),
			                              ref val);
		}

		public TagMergeMode MergeMode {
			get{return gst_tag_setter_get_tag_merge_mode (Handle);}
			set{gst_tag_setter_set_tag_merge_mode (Handle,value);}
		}
		public TagList TagList {
			get { return new Gst.TagList(gst_tag_setter_get_tag_list (Handle)); }
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GstTagSetterInterface {

		}
	}
}

