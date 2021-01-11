using System;

namespace RosettaStone.Savestate.Snes9x.Helpers
{
	[Flags]
	public enum LoadIncludeOffset
	{
		All,
		RAM,
		SRA,
		FIL
	}
}