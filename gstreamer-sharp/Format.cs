using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public enum Format
	{
		Undefined = 0,
		Default   = 1,
		Bytes     = 2,
		Time      = 3,
		Buffer    = 4,
		Percents  = 5
	}

	public class FormatDefinition
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct GstFormatDefinition
		{
			public Format value;
			public IntPtr nick;
			public IntPtr definition;
			IntPtr quark;
		}

		GstFormatDefinition def;

		public FormatDefinition(IntPtr raw){
			def = (GstFormatDefinition)Marshal.PtrToStructure (raw,typeof(GstFormatDefinition));
		}

		public Format Value {
			get{return def.value;}
		}
		public string Nick {
			get{return Marshal.PtrToStringAuto (def.nick);}
		}
		public string Definition {
			get{return Marshal.PtrToStringAuto (def.definition);}
		}
	}

	public static class FormatUtility
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_format_get_name(Format f);
		[DllImport(Application.Dll)]
		static extern Format gst_format_register(IntPtr name, IntPtr desc);
		[DllImport(Application.Dll)]
		static extern Format gst_format_get_by_nick(IntPtr nick);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_format_get_details(Format f);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_format_iterate_definitions();

		public static string GetName(this Format format){
			return Marshal.PtrToStringAuto (gst_format_get_name (format));
		}
		public static Format Register(string name, string description){
			return gst_format_register (
				Marshal.StringToHGlobalAuto (name),
				Marshal.StringToHGlobalAuto (description)
			);
		}
		public static Format GetByNick(string nick){
			return gst_format_get_by_nick (Marshal.StringToHGlobalAuto (nick));
		}
		public static FormatDefinition GetDetails(this Format format){
			return new FormatDefinition(gst_format_get_details (format));
		}
		public static Iterator Definitions {
			get{
				return new Iterator(gst_format_iterate_definitions ());
			}
		}
	}
}

