using System.IO;
using Common.Shared.Min.Extensions;
using WRAM.Snes9x.Models.Structs;

namespace WRAM.Snes9x.Extensions
{
	public static class SavestateExtensions
	{
		public static Stream? GetSramStream(this SavestateSnex9x source) => source.SRA.Data.ToStreamIfNotNull();
		public static Stream? GetWramStream(this SavestateSnex9x source) => source.RAM.Data.ToStreamIfNotNull();
		public static Stream? GetFillRamStream(this SavestateSnex9x source) => source.FIL.Data.ToStreamIfNotNull();
	}
}
