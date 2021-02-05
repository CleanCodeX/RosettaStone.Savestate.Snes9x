using System;

namespace WRAM.Snes9x.Helpers
{
	[Flags]
	public enum LoadIncludeOffset
	{
		All = 0,
		RAM = 0x1,
		SRA = 0x2,
		FIL = 0x4
	}
}