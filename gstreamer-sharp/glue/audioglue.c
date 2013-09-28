#include <gst/gst.h>
#include <gst/audio/audio-channels.h>
#include <gst/audio/audio-enumtypes.h>
#include <gst/audio/audio-format.h>
#include <gst/audio/audio-info.h>
#include <gst/audio/audio.h>
#include <gst/audio/gstaudiobasesink.h>
#include <gst/audio/gstaudiobasesrc.h>
#include <gst/audio/gstaudiocdsrc.h>
#include <gst/audio/gstaudioclock.h>
#include <gst/audio/gstaudiodecoder.h>
#include <gst/audio/gstaudioencoder.h>
#include <gst/audio/gstaudiofilter.h>
#include <gst/audio/gstaudioiec61937.h>
#include <gst/audio/gstaudiometa.h>
#include <gst/audio/gstaudioringbuffer.h>
#include <gst/audio/gstaudiosink.h>
#include <gst/audio/gstaudiosrc.h>
#include <gst/audio/streamvolume.h>

GstAudioRingBuffer*
gstsharp_audio_basesrc_create_ringbuffer (GstAudioBaseSrc *src){
	return GST_AUDIO_BASE_SRC_GET_CLASS(src)->create_ringbuffer(src);
}

gboolean
gstsharp_audio_src_open (GstAudioSrc *src){
	return GST_AUDIO_SRC_GET_CLASS(src)->open(src);
}
gboolean
gstsharp_audio_src_prepare (GstAudioSrc *src, GstAudioRingBufferSpec *spec){
	return GST_AUDIO_SRC_GET_CLASS(src)->prepare(src, spec);
}
gboolean
gstsharp_audio_src_unprepare (GstAudioSrc *src){
	return GST_AUDIO_SRC_GET_CLASS(src)->unprepare(src);
}
gboolean
gstsharp_audio_src_close (GstAudioSrc *src){
	return GST_AUDIO_SRC_GET_CLASS(src)->close(src);
}
guint
gstsharp_audio_src_read (GstAudioSrc *src, guint8 *data, guint length, GstClockTime *timestamp){
	return GST_AUDIO_SRC_GET_CLASS(src)->read(src, data, length, timestamp);
}
guint
gstsharp_audio_src_delay (GstAudioSrc *src){
	return GST_AUDIO_SRC_GET_CLASS(src)->delay(src);
}
void
gstsharp_audio_src_reset (GstAudioSrc *src){
	GST_AUDIO_SRC_GET_CLASS(src)->reset(src);
}
