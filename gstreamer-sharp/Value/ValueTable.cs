using System;
using System.Runtime.InteropServices;

namespace Gst.Value
{
	public delegate int ValueCompareFunc(ref GLib.Value v1, ref GLib.Value v2);
	public delegate IntPtr ValueSerializeFunc(ref GLib.Value val);
	public delegate bool ValueDeserializeFunc(out GLib.Value val, IntPtr src);

	public class ValueTable
	{
		[DllImport(Application.Dll)]
		static extern void gst_value_register(ref GstValueTable t);

		[StructLayout(LayoutKind.Sequential)]
		struct GstValueTable {
			IntPtr type;
			public ValueCompareFunc compare;
			public ValueSerializeFunc serialize;
			public ValueDeserializeFunc deserialize;
			/*< private >*/
			IntPtr _gst_reserved;
		}

		GstValueTable table;

		public ValueTable (ValueCompareFunc compare, ValueSerializeFunc serialize, 
		                   ValueDeserializeFunc deserialize)
		{
			table = new GstValueTable();
			table.compare = compare;
			table.deserialize = deserialize;
			table.serialize = serialize;
		}

		public void Register()
		{
			gst_value_register (ref table);
		}
	}
}

