using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

namespace Gst
{
	public class Buffer : MiniObject
	{
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_new ();
		[DllImport (Application.Dll)]
		static extern IntPtr gst_buffer_new_wrapped(IntPtr data, uint size);
		[DllImport(Application.Dll)]
		static extern bool gst_buffer_find_memory(IntPtr buffer, uint offset, uint size,
                                            out uint idx, out uint length, out uint skip);
		[DllImport(Application.Dll)]
		static extern void gst_buffer_insert_memory(IntPtr buffer, int index, IntPtr memory);
		[DllImport(Application.Dll)]
		static extern void  gst_buffer_remove_memory(IntPtr buffer, int index);
		[DllImport(Application.Dll)]
		static extern IntPtr gst_buffer_get_memory(IntPtr buffer, int index);
		[DllImport(Application.Dll)]
		static extern void gst_buffer_replace_memory(IntPtr buffer, int index, IntPtr memory);
		[DllImport(Application.Dll)]
		static extern uint gst_buffer_n_memory(IntPtr buffer);

		~Buffer(){
			MiniObject.gst_mini_object_unref (Handle);
		}

		public Buffer (IntPtr raw) : base(raw)
		{}

		public Buffer ()
			: this(gst_buffer_new ())
		{
		}

		public Buffer (byte[] data) : base(IntPtr.Zero)
		{
			IntPtr ptr = IntPtr.Zero;
			Marshal.Copy(data,0,ptr,data.Length);
			Handle = MiniObject.gst_mini_object_ref (gst_buffer_new_wrapped (ptr,(uint)data.Length));
		}

		public int IndexOf(Memory m){
			uint i, j, k;
			gst_buffer_find_memory (Handle,0,m.Size,out i,out j, out k);
			return (int)i;
		}

		public void Insert(int index, Memory m){
			gst_buffer_insert_memory (Handle,index,m.Handle);
		}

		public void RemoveAt(int index){
			gst_buffer_remove_memory (Handle,index);
		}

		public Memory this [int index] {
			get{
				return new Memory(gst_buffer_get_memory (Handle,index));
			}
			set{gst_buffer_replace_memory (Handle,index,value.Handle);}
		}

		public int Count {
			get{
				return (int)gst_buffer_n_memory(Handle);
			}
		}
	}
}

