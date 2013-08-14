using System;

namespace Gst.Audio
{
	public enum StreamVolumeFormat
	{
		Linear = 0,
		Cubic,
		Db
	}

	public interface StreamVolume
	{
		void SetVolume(StreamVolumeFormat format, double val);
		double GetVolume(StreamVolumeFormat format);
		double Volume{get;set;}
		bool Mute{get;set;}
	}
}

