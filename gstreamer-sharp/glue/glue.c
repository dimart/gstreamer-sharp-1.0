#include <gst/gst.h>

GstMessageType gst_message_get_message_type (GstMessage *message){
	return message->type;
}
GstObject *gst_message_get_src(GstMessage *message){
	return message->src;
}

guint
gstsharp_gst_message_get_src_offset (void)
{
  return (guint) G_STRUCT_OFFSET (GstMessage, src);
}

GType
gstsharp_g_type_from_instance (GTypeInstance * instance)
{
  return G_TYPE_FROM_INSTANCE (instance);
}

GstMapInfo *
gstsharp_memory_get_info(GstMemory *memory, GstMapFlags flags){
	GstMapInfo info = {0};
	gst_memory_map(memory,&info,flags);
	return &info;
}

GstSample *
gstsharp_element_convert_sample(GstElement *element, GstCaps *caps){
	GstSample* sample = NULL;
	g_return_val_if_fail (element != NULL, NULL);
	g_return_val_if_fail (caps != NULL, NULL);
	g_signal_emit_by_name (element, "convert-sample", caps, &sample, NULL);
	g_return_val_if_fail (sample != NULL, NULL);
	return sample;
}

GstSample *
gstsharp_element_convert_sample2(GstElement *element, gchar *caps_str){
	GstSample* sample = NULL;
	g_return_val_if_fail (element != NULL, NULL);
	GstCaps *caps = gst_caps_from_string (caps_str);
	g_signal_emit_by_name (element, "convert-sample", caps, &sample, NULL);
	return sample;
}

gchar *
gstsharp_value_get_type_name(GValue *val){
	return G_VALUE_TYPE_NAME(val);
}
