using System;

namespace Gst
{
	public enum StateChangeReturn
	{
		Failure,
		Sucess,
		Async,
		NoPreroll
	}

	public enum State
	{
		VoidPending,
		Null,
		Ready,
		Paused,
		Playing
	}

	public enum StateChange
	{
		NullToReady = (State.Null << 3) | State.Ready,
		ReadyToPaused = (State.Ready << 3) | State.Paused,
		PausedToPlaying = (State.Paused << 3) | State.Playing,
		PlayingToPaused = (State.Playing << 3) | State.Paused,
		PausedToReady = (State.Paused << 3) | State.Ready,
		ReadyToNull = (State.Ready << 3) | State.Null
	}

	public enum ObjectFlags
	{
		Last = 1 << 4 
	}

	public enum ElementFlags
	{
		LockedState = ObjectFlags.Last << 0,
		Sink = ObjectFlags.Last << 1,
		Source = ObjectFlags.Last << 2,
		ProvideClock = ObjectFlags.Last << 3,
		RequireClock = ObjectFlags.Last << 4,
		Indexable = ObjectFlags.Last << 5,
		Last = ObjectFlags.Last << 10,
	}
}

