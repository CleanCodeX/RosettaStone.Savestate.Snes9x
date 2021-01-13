using System.IO;

namespace RosettaStone.Savestate.Snes9x.Extensions
{
	public static class SavestateExtensions
	{
		public static Stream? GetSramStream(this Models.Structs.Savestate source) => source.SRA.Data.ToStreamIfNotNull();
		public static Stream? GetWramStream(this Models.Structs.Savestate source) => source.RAM.Data.ToStreamIfNotNull();
		public static Stream? GetFillRamStream(this Models.Structs.Savestate source) => source.FIL.Data.ToStreamIfNotNull();
	}
}
