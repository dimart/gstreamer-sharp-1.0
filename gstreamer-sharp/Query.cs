using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public enum QueryTypeFlags
	{
		Upstream   = 1 << 0,
		Downstream = 1 << 1,
		Serialized = 1 << 2,
		Both       = QueryTypeFlags.Upstream | QueryTypeFlags.Downstream
	}

	public enum QueryType
	{
		Unknown    = (0 << 8)   | 0,
		Position   = (10 << 8)  | QueryTypeFlags.Both,
		Duration   = (20 << 8)  | QueryTypeFlags.Both,
		Latency    = (30 << 8)  | QueryTypeFlags.Both,
		Jitter     = (40 << 8)  | QueryTypeFlags.Both,
		Rate       = (50 << 8)  | QueryTypeFlags.Both,
		Seeking    = (60 << 8)  | QueryTypeFlags.Both,
		Segment    = (70 << 8)  | QueryTypeFlags.Both,
		Convert    = (80 << 8)  | QueryTypeFlags.Both,
		Format     = (90 << 8)  | QueryTypeFlags.Both,
		Buffering  = (110 << 8) | QueryTypeFlags.Both,
		Custom     = (120 << 8) | QueryTypeFlags.Both,
		Uri        = (130 << 8) | QueryTypeFlags.Both,
		Allocation = (140 << 8) | (QueryTypeFlags.Downstream | QueryTypeFlags.Serialized),
		Scheduling = (150 << 8) | QueryTypeFlags.Upstream,
		AcceptCaps = (160 << 8) | QueryTypeFlags.Both,
		Caps       = (170 << 8) | QueryTypeFlags.Both,
		Drain      = (180 << 8) | (QueryTypeFlags.Downstream | QueryTypeFlags.Serialized)
	}

	public class Query : MiniObject
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_query_new_custom(QueryType type, IntPtr structure);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_query_get_structure(IntPtr query);
 
		public Query (IntPtr raw) : base(raw)
		{
		}

		public Query(QueryType type, Structure structure) : base(IntPtr.Zero)
		{
			Handle = gst_query_new_custom (type,structure.Handle);
		}

		public Gst.Structure Structure {
			get{return new Gst.Structure(gst_query_get_structure (Handle));}
		}
	}
}

