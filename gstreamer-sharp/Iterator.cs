using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Gst
{
	public class Iterator : GLib.Opaque, IEnumerable
	{
		[DllImport(Application.Dll)]
		static extern void gst_iterator_free(IntPtr i);
		[DllImport(Application.Dll)]
		static extern void gst_iterator_resync(IntPtr i);
		[DllImport(Application.Dll)]
		static extern int gst_iterator_next(IntPtr i, IntPtr e);
 
		~Iterator(){gst_iterator_free (Handle);}

		public Iterator (IntPtr raw) : base(raw)
		{
		}

		public IEnumerator GetEnumerator ()
		{
			return new Enumerator(this);
		}

		class Enumerator : IEnumerator
		{
			Iterator i;
			IntPtr val;

			public Enumerator(Iterator iterator){
				i = iterator;
				val = IntPtr.Zero;
			}

			public bool MoveNext(){
				if(val == IntPtr.Zero)return false;
				gst_iterator_next (i.Handle,val);
				return true;
			}

			public object Current {
				get{
					return GLib.Object.GetObject (val,true);
				}
			}

			public void Reset(){
				gst_iterator_resync (i.Handle);
			}
		}
	}
}

