#include <gst/gst.h>

GType
gstsharp_g_type_from_instance (GTypeInstance * instance);
GstObject *gst_message_get_src(GstMessage *message);
guint
gstsharp_gst_message_get_src_offset (void);


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
