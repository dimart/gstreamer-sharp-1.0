#include <gst/gst.h>

GType
gstsharp_g_type_from_instance (GTypeInstance * instance);
GstObject *gst_message_get_src(GstMessage *message);
guint
gstsharp_gst_message_get_src_offset (void);
void
gstsharp_memory_get_info_data(GstMemory *memory, GstMapFlags flags, guint8 **data, long *length);
void
gstsharp_memory_unmap(GstMemory *memory, GstMapInfo info);

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

void
gstsharp_memory_get_info_data(GstMemory *memory, GstMapFlags flags, guint8 **data, long *length){
	GstMapInfo info = {0};
	gst_memory_map(memory,&info,flags);
	*data = info.data;
	*length = info.size;
}

void
gstsharp_memory_unmap(GstMemory *memory, GstMapInfo info){
	gst_memory_unmap(memory,&info);	
}
