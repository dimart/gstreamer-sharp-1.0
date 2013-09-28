using System;

namespace Gst.Audio
{
	public enum ChannelPosition
	{
		None = -3,
		Mono = -2,
		Invalid = -1,

		/* Normal cases */
		FrontLeft = 0,
		FrontRight,
		FrontCenter,
		Lfe1,
		RearLeft,
		RearRight,
		FrontLeftOfCenter,
		FrontRightOfCenter,
		RearCenter,
		Lfe2,
		SideLeft,
		SideRight,
		TopFrontLeft,
		TopFrontRight,
		TopFrontCenter,
		TopCenter,
		TopRearLeft,
		TopRearRight,
		TopSideLeft,
		TopSideRight,
		TopRearCenter,
		BottomFrontCenter,
		BottomFrontLeft,
		BottomFrontRight,
		WideLeft,
		WideRight,
		SurroundLeft,
		SurroundRight
	}
}

