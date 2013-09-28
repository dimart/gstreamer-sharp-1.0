using System;

namespace Gst.Audio
{
	public class RingBufferSpec : GLib.Opaque
	{
		public RingBufferSpec (IntPtr raw) : base(raw)
		{
		}

		struct GstAudioRingBufferSpec
		{
		  /*< public >*/
		  /* in */
		  IntPtr caps;               /* the caps of the buffer */

		  /* in/out */
		  int  type;
		  GstAudioInfo                  info;


		  ulong  latency_time;        /* the required/actual latency time, this is the
						 * actual the size of one segment and the
						 * minimum possible latency we can achieve. */
		  ulong  buffer_time;         /* the required/actual time of the buffer, this is
						 * the total size of the buffer and maximum
						 * latency we can compensate for. */
		  int     segsize;             /* size of one buffer segment in bytes, this value
						 * should be chosen to match latency_time as
						 * well as possible. */
		  int     segtotal;            /* total number of segments, this value is the
						 * number of segments of @segsize and should be
						 * chosen so that it matches buffer_time as
						 * close as possible. */
		  /* ABI added 0.10.20 */
		  int     seglatency;          /* number of segments queued in the lower
						 * level device, defaults to segtotal. */

		  /*< private >*/
		  IntPtr _gst_reserved;
		}
	}
}

