using System;
using System.Runtime.InteropServices;

namespace Gst.Base
{
	public class Sink : Element
	{
		[DllImport(Application.GlueDll)]
		static extern IntPtr gstsharp_basesink_get_caps (IntPtr sink, IntPtr filter);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_set_caps (IntPtr sink, IntPtr caps);
		[DllImport(Application.GlueDll)]
		static extern IntPtr gstsharp_basesink_fixate (IntPtr sink, IntPtr caps);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_activate_pull (IntPtr sink, bool active);
		[DllImport(Application.GlueDll)]
		static extern void gstsharp_basesink_get_times (IntPtr sink, IntPtr buffer, out ulong start,
		                                              out ulong end);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_propose_allocation (IntPtr sink, IntPtr query);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_start (IntPtr sink);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_stop (IntPtr sink);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_unlock (IntPtr sink);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_unlock_stop (IntPtr sink);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_query (IntPtr sink, IntPtr query);
		[DllImport(Application.GlueDll)]
		static extern bool gstsharp_basesink_event (IntPtr sink, IntPtr evt);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesink_wait_event (IntPtr sink, IntPtr evt);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesink_prepare (IntPtr sink, IntPtr buffer);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesink_prepare_list (IntPtr sink, IntPtr list);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesink_preroll (IntPtr sink, IntPtr buffer);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesink_render (IntPtr sink, IntPtr buffer);
		[DllImport(Application.GlueDll)]
		static extern FlowReturn gstsharp_basesink_render_list (IntPtr sink, IntPtr list);
		
		[DllImport(Application.BaseDll)]
		static extern FlowReturn gst_base_sink_wait_preroll (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern FlowReturn gst_base_sink_do_preroll (IntPtr sink, IntPtr mini);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_sync (IntPtr sink, bool sync);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_sink_get_sync (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_max_lateness (IntPtr sink, long max_lateness);
		[DllImport(Application.BaseDll)]
		static extern long gst_base_sink_get_max_lateness (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_qos_enabled (IntPtr sink, bool enabled);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_sink_get_qos_enabled (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_async_enabled (IntPtr sink, bool enabled);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_sink_get_async_enabled (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_ts_offset (IntPtr sink, long offset);
		[DllImport(Application.BaseDll)]
		static extern long gst_base_sink_get_ts_offset (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern IntPtr gst_base_sink_get_last_sample (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_last_sample_enabled (IntPtr sink, bool enabled);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_sink_get_last_sample_enabled (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern bool gst_base_sink_query_latency (IntPtr sink, out bool live, out bool upstream_live,
		                                                out ulong min_latency, out ulong max_latency);
		[DllImport(Application.BaseDll)]
		static extern ulong gst_base_sink_get_latency (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_render_delay (IntPtr sink, ulong delay);
		[DllImport(Application.BaseDll)]
		static extern ulong gst_base_sink_get_render_delay (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_blocksize (IntPtr sink, uint size);
		[DllImport(Application.BaseDll)]
		static extern uint gst_base_sink_get_blocksize (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_throttle_time (IntPtr sink, ulong time);
		[DllImport(Application.BaseDll)]
		static extern ulong gst_base_sink_get_throttle_time (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern void gst_base_sink_set_max_bitrate (IntPtr sink, ulong bitrate);
		[DllImport(Application.BaseDll)]
		static extern ulong gst_base_sink_get_max_bitrate (IntPtr sink);
		[DllImport(Application.BaseDll)]
		static extern ClockReturn gst_base_sink_wait_clock (IntPtr sink, ulong time, out long jitter);
		[DllImport(Application.BaseDll)]
		static extern FlowReturn gst_base_sink_wait (IntPtr sink, ulong time, out long jitter);

		protected Sink (IntPtr raw) : base(raw)
		{
		}

		struct GstBaseSink {
			IntPtr    element;

			/*< protected >*/
			public IntPtr  sinkpad;
			public PadMode     pad_mode;

			/*< protected >*/ /* with LOCK */
			public ulong        offset;
			public bool      can_activate_pull;
			public bool       can_activate_push;

			/*< protected >*/ /* with PREROLL_LOCK */
			IntPtr        preroll_lock;
			IntPtr        preroll_cond;
			public bool      eos;
			public bool      need_preroll;
			public bool       have_preroll;
			public bool      playing_async;

			/*< protected >*/ /* with STREAM_LOCK */
			public bool       have_newsegment;
			IntPtr     segment;

			/*< private >*/ /* with LOCK */
			public IntPtr     clock_id;
			public bool       sync;
			public bool       flushing;
			public bool      running;

			public long         max_lateness;

			/*< private >*/
			IntPtr priv;

			IntPtr _gst_reserved;
		}

		protected Caps GetCaps (Caps filter){
			return new Caps (gstsharp_basesink_get_caps (Handle,filter.Handle));
		}
		protected bool SetCaps (Caps caps){
			return gstsharp_basesink_set_caps (Handle, caps.Handle);
		}
		protected Caps Fixate (Caps caps){
			return new Caps (gstsharp_basesink_fixate (Handle,caps.Handle));
		}
		protected bool ActivatePull (bool active){
			return gstsharp_basesink_activate_pull (Handle, active);
		}
		protected void GetTimes (Buffer buffer, out ulong start, out ulong end){
			gstsharp_basesink_get_times (Handle, buffer.Handle, out start, out end);
		}
		protected bool ProposeAllocation(Query query){
			return gstsharp_basesink_propose_allocation (Handle, query.Handle);
		}
		protected bool Start(){
			return gstsharp_basesink_start (Handle);
		}
		protected bool Stop(){
			return gstsharp_basesink_stop (Handle);
		}
		protected bool Unlock(){
			return gstsharp_basesink_unlock (Handle);
		}
		protected bool UnlockStop(){
			return gstsharp_basesink_unlock_stop (Handle);
		}
		protected bool Query (Gst.Query query){
			return gstsharp_basesink_query (Handle, query.Handle);
		}
		protected bool Event (Gst.Event evt){
			return gstsharp_basesink_event (Handle, evt.Handle);
		}
		protected FlowReturn WaitEvent (Gst.Event evt){
			return gstsharp_basesink_wait_event (Handle, evt.Handle);
		}
		protected FlowReturn Prepare (Buffer buffer){
			return gstsharp_basesink_prepare (Handle, buffer.Handle);
		}
		protected FlowReturn PrepareList (BufferList list){
			return gstsharp_basesink_prepare_list (Handle, list.Handle);
		}
		protected FlowReturn Preroll (Buffer buffer){
			return gstsharp_basesink_preroll (Handle, buffer.Handle);
		}
		protected FlowReturn Render (Buffer buffer){
			return gstsharp_basesink_render (Handle, buffer.Handle);
		}
		protected FlowReturn RenderList (BufferList list){
			return gstsharp_basesink_render_list (Handle, list.Handle);
		}

		public FlowReturn WaitPreroll (){
			return gst_base_sink_wait_preroll (Handle);
		}
		public FlowReturn DoPreroll(MiniObject mini){
			return gst_base_sink_do_preroll (Handle, mini.Handle);
		}
		public bool QueryLatency(out bool live, out bool upstream_live, out ulong min_latency, 
		                         out ulong max_latency){
			return gst_base_sink_query_latency (Handle, out live, out upstream_live,
			                                    out min_latency, out max_latency);
		}
		public ClockReturn WaitClock(ulong time, out long jitter){
			return gst_base_sink_wait_clock (Handle, time, out jitter);
		}
		public FlowReturn Wait (ulong time, out long jitter){
			return gst_base_sink_wait (Handle, time, out jitter);
		}

		public ulong Latency {
			get { return gst_base_sink_get_latency (Handle); }
		}
		public ulong RenderDelay {
			get { return gst_base_sink_get_render_delay (Handle); }
			set { gst_base_sink_set_render_delay (Handle, value); }
		}
		public uint BlockSize {
			get { return gst_base_sink_get_blocksize (Handle); }
			set { gst_base_sink_set_blocksize (Handle, value); }
		}
		public ulong ThrottleTime {
			get { return gst_base_sink_get_throttle_time (Handle); }
			set { gst_base_sink_set_throttle_time (Handle, value); }
		}
		public ulong MaxBitrate {
			get { return gst_base_sink_get_max_bitrate (Handle); }
			set { gst_base_sink_set_max_bitrate (Handle, value); }
		}
		public bool Sync {
			get { return gst_base_sink_get_sync (Handle); }
			set { gst_base_sink_set_sync (Handle, value); }
		}
		public long MaxLateness {
			get { return gst_base_sink_get_max_lateness (Handle); }
			set { gst_base_sink_set_max_lateness (Handle, value); }
		}
		public bool QosEnabled {
			get { return gst_base_sink_get_qos_enabled (Handle); }
			set { gst_base_sink_set_qos_enabled (Handle, value); }
		}
		public bool AsyncEnabled {
			get { return gst_base_sink_get_async_enabled (Handle); }
			set { gst_base_sink_set_async_enabled (Handle, value); }
		}
		public long TsOffset {
			get { return gst_base_sink_get_ts_offset (Handle); }
			set { gst_base_sink_set_ts_offset (Handle, value); }
		}
		public Sample LastSample {
			get { return new Sample (gst_base_sink_get_last_sample (Handle)); }
		}
		public bool LastSampleEnabled {
			get { return gst_base_sink_get_last_sample_enabled (Handle); }
			set { gst_base_sink_set_last_sample_enabled (Handle, value); }
		}
		public Pad SinkPad {
			get {
				GstBaseSink sink = (GstBaseSink)Marshal.PtrToStructure (Handle,typeof(GstBaseSink));
				return new Pad(sink.sinkpad);
			}
		}
		public Gst.PadMode PadMode {
			get {
				GstBaseSink sink = (GstBaseSink)Marshal.PtrToStructure (Handle,typeof(GstBaseSink));
			    	return sink.pad_mode;
			}
		}
	}
}

