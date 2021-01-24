using System.IO;
using Savestate.Snes9x.Models.Structs;
using IO.Extensions;

namespace Savestate.Snes9x.Extensions
{
	public static class SavestateExtensions
	{
		public static Stream? GetSramStream(this SavestateSnex9x source) => source.SRA.Data.ToStreamIfNotNull();
		public static Stream? GetWramStream(this SavestateSnex9x source) => source.RAM.Data.ToStreamIfNotNull();
		public static Stream? GetFillRamStream(this SavestateSnex9x source) => source.FIL.Data.ToStreamIfNotNull();
	}
}
