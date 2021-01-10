using System;

namespace SavestateFormat.Snes9x.Helpers
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