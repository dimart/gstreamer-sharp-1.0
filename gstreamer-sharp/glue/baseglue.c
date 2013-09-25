#include <gst/gst.h>
#include <gst/base/gstadapter.h>
#include <gst/base/gstbaseparse.h>
#include <gst/base/gstbasesink.h>
#include <gst/base/gstbasesrc.h>
#include <gst/base/gstbasetransform.h>
#include <gst/base/gstbitreader.h>
#include <gst/base/gstbytereader.h>
#include <gst/base/gstbytewriter.h>
#include <gst/base/gstcollectpads.h>
#include <gst/base/gstpushsrc.h>
#include <gst/base/gsttypefindhelper.h>

GstCaps *
gstsharp_basesrc_get_caps(GstBaseSrc *src, GstCaps *filter){
	return GST_BASE_SRC_GET_CLASS (src)->get_caps (src, filter);
}

GstCaps *
gstsharp_basesrc_fixate(GstBaseSrc *src, GstCaps *caps){
	return GST_BASE_SRC_GET_CLASS (src)->get_caps (src, caps);
}
gboolean
gstsharp_basesrc_negotiate(GstBaseSrc *src){
	return GST_BASE_SRC_GET_CLASS (src)->negotiate(src);
}
gboolean
gstsharp_basesrc_decide_allocation(GstBaseSrc *src, GstQuery *query){
	return GST_BASE_SRC_GET_CLASS (src)->decide_allocation(src, query);
}
gboolean
gstsharp_basesrc_start(GstBaseSrc *src){
	return GST_BASE_SRC_GET_CLASS (src)->start(src);
}
gboolean
gstsharp_basesrc_stop(GstBaseSrc *src){
	return GST_BASE_SRC_GET_CLASS (src)->stop(src);
}
void gstsharp_basesrc_get_times(GstBaseSrc *src, GstBuffer *buffer,
                                 GstClockTime *start, GstClockTime *end){
	GST_BASE_SRC_GET_CLASS (src)->get_times(src, buffer, start, end);
}
gboolean
gstsharp_basesrc_get_size(GstBaseSrc *src, guint64 *size){
	return GST_BASE_SRC_GET_CLASS (src)->get_size(src, size);
}
gboolean
gstsharp_basesrc_is_seekable(GstBaseSrc *src){
	return GST_BASE_SRC_GET_CLASS(src)->is_seekable(src);
}
gboolean
gstsharp_basesrc_prepare_seek_segment(GstBaseSrc *src, GstEvent *seek,
                                         GstSegment *segment){
	return GST_BASE_SRC_GET_CLASS(src)->prepare_seek_segment(src, seek, segment);
}
gboolean
gstsharp_basesrc_unlock(GstBaseSrc *src){
	return GST_BASE_SRC_GET_CLASS(src)->unlock(src);
}
gboolean
gstsharp_basesrc_unlock_stop(GstBaseSrc *src){
	return GST_BASE_SRC_GET_CLASS(src)->unlock_stop(src);
}
gboolean
gstsharp_basesrc_query(GstBaseSrc *src, GstQuery *query){
	return GST_BASE_SRC_GET_CLASS(src)->query(src, query);
}
gboolean
gstsharp_basesrc_event(GstBaseSrc *src, GstEvent *event){
	return GST_BASE_SRC_GET_CLASS(src)->event(src, event);
}
GstFlowReturn
gstsharp_basesrc_create(GstBaseSrc *src, guint64 offset, guint size,
                                 GstBuffer **buf){
	return GST_BASE_SRC_GET_CLASS(src)->create(src, offset, size, buf);
}
GstFlowReturn
gstsharp_basesrc_alloc(GstBaseSrc *src, guint64 offset, guint size,
                                 GstBuffer **buf){
	return GST_BASE_SRC_GET_CLASS(src)->alloc(src, offset, size, buf);
}
GstFlowReturn
gstsharp_basesrc_fill(GstBaseSrc *src, guint64 offset, guint size,
                                 GstBuffer *buf){
	return GST_BASE_SRC_GET_CLASS(src)->fill(src, offset, size, buf);
}

GstFlowReturn
gstsharp_pushsrc_create(GstPushSrc *src, GstBuffer **buf){
	return GST_PUSH_SRC_GET_CLASS(src)->create(src, buf);
}
GstFlowReturn
gstsharp_pushsrc_alloc(GstPushSrc *src, GstBuffer **buf){
	return GST_PUSH_SRC_GET_CLASS(src)->alloc(src, buf);
}
GstFlowReturn
gstsharp_pushsrc_fill(GstPushSrc *src, GstBuffer *buf){
	return GST_PUSH_SRC_GET_CLASS(src)->fill(src, buf);
}
