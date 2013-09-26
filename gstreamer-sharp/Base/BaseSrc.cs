using System;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class BaseSrc : Element
	{
		[DllImport(Application.GlueDll)]
		static extern IntPtr gstsharp_basesrc_get_caps (IntPtr src, IntPtr filter);
		[DllImport(Application.GlueDll)]
		static extern IntPtr gstsharp_basesrc_fixate (IntPtr src, IntPtr caps);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_get_size (IntPtr src, out ulong size);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_start (IntPtr src);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_stop (IntPtr src);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_decide_allocation (IntPtr src, IntPtr query);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_negotiate (IntPtr src);
		[DllImport(Application.GlueDll)]
		static extern void gstsharp_basesrc_get_times (IntPtr src, IntPtr buffer, out ulong start, out ulong end);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_is_seekable (IntPtr src);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_prepare_seek_segment (IntPtr src, IntPtr evt, IntPtr segment);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_unlock (IntPtr src);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_unlock_stop (IntPtr src);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_query (IntPtr src, IntPtr query);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesrc_event (IntPtr src, IntPtr evt);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesrc_create (IntPtr src, ulong offset, uint size, out IntPtr buffer);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesrc_alloc (IntPtr src, ulong offset, uint size, out IntPtr buffer);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesrc_fill (IntPtr src, ulong offset, uint size, IntPtr buffer);

		[DllImport(Application.BaseDll)]
		static extern void gst_base_src_set_live (IntPtr src, bool live);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_src_is_live (IntPtr src);

		protected BaseSrc (IntPtr raw) : base(raw)
		{
		}

		protected Caps GetCaps(Caps filter) {
			return new Caps (gstsharp_basesrc_get_caps(Handle,filter.Handle));
		}
		protected Caps Fixate (Caps caps){
			return new Caps (gstsharp_basesrc_fixate(Handle,caps.Handle));
		}

		protected bool Negotiate(){
			return gstsharp_basesrc_negotiate (Handle);
		}
		protected bool Start (){
			return gstsharp_basesrc_start (Handle);
		}
		protected bool Stop (){
			return gstsharp_basesrc_stop (Handle);
		}
		protected bool DecideAllocation(Query query){
			return gstsharp_basesrc_decide_allocation (Handle, query.Handle);
		}
		protected void GetTimes(Buffer buffer, out ulong start, out ulong end){
			gstsharp_basesrc_get_times (Handle, buffer.Handle, out start, out end);
		}
		protected bool PrepareSegment(Event e, Segment segment){
			return gstsharp_basesrc_prepare_seek_segment (Handle, e.Handle, segment.Handle);
		}
		protected bool Unlock(){
			return gstsharp_basesrc_unlock (Handle);
		}
		protected bool UnlockStop(){
			return gstsharp_basesrc_unlock_stop (Handle);
		}
		protected new bool Query (Gst.Query query){
			return gstsharp_basesrc_query (Handle, query.Handle);
		}
		protected bool Event (Gst.Event evt){
			return gstsharp_basesrc_event (Handle, evt.Handle);
		}
		protected FlowReturn Create(ulong offset, uint size, out Buffer buffer){
			IntPtr b;
			var fr = gstsharp_basesrc_create (Handle, offset, size, out b);
			buffer = new Buffer (b);
			return fr;
		}
		protected FlowReturn Alloc(ulong offset, uint size, out Buffer buffer){
			IntPtr b;
			var fr = gstsharp_basesrc_alloc (Handle, offset, size, out b);
			buffer = new Buffer (b);
			return fr;
		}
		protected FlowReturn Fill(ulong offset, uint size, Buffer buffer){
			return gstsharp_basesrc_fill (Handle, offset, size, buffer.Handle);
		}

		protected bool Live {
			get{ return gst_base_src_is_live (Handle); }
			set{ gst_base_src_set_live (Handle, value); }
		}

		protected ulong Size {
			get {
				ulong size = 0;
				gstsharp_basesrc_get_size (Handle, out size);
				return size;
			}
		}

		protected bool Seekable {
			get {
				return gstsharp_basesrc_is_seekable (Handle);
			}
		}

		struct GstBaseSrc {
			IntPtr    element;

			/*< protected >*/
			public IntPtr srcpad;

			/* available to subclass implementations */
			/* MT-protected (with LIVE_LOCK) */
			IntPtr          live_lock;
			IntPtr         live_cond;
			public bool       is_live;
			public bool      live_running;

			/* MT-protected (with LOCK) */
			public uint        blocksize;     /* size of buffers when operating push based */
			public bool        can_activate_push;     /* some scheduling properties */
			public bool       random_access;

			public IntPtr     clock_id;      /* for syncing */

			/* MT-protected (with STREAM_LOCK *and* OBJECT_LOCK) */
			public IntPtr     segment;
			/* MT-protected (with STREAM_LOCK) */
			public bool       need_newsegment;

			public int           num_buffers;
			public int           num_buffers_left;

			public bool       typefind;
			public bool       running;
			public IntPtr pending_seek;

			IntPtr priv;

			/*< private >*/
			IntPtr       _gst_reserved;
		}

		public Pad SrcPad {
			get {
				GstBaseSrc src = (GstBaseSrc)Marshal.PtrToStructure (Handle, typeof(GstBaseSrc));
				return new Pad (src.srcpad);
			}
		}
	}
}

