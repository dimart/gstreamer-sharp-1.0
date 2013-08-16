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
gstsharp_basesrc_get_caps(GstBaseSrc *src, GstCaps *filter);
GstCaps *
gstsharp_basesrc_fixate(GstBaseSrc *src, GstCaps *caps);

GstCaps *
gstsharp_basesrc_get_caps(GstBaseSrc *src, GstCaps *filter){
	return GST_BASE_SRC_GET_CLASS (src)->get_caps (src, filter);
}

GstCaps *
gstsharp_basesrc_fixate(GstBaseSrc *src, GstCaps *caps){
	return GST_BASE_SRC_GET_CLASS (src)->get_caps (src, caps);
}
