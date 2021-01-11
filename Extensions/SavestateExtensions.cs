using System.IO;

namespace RosettaStone.Savestate.Snes9x.Extensions
{
	public static class SavestateExtensions
	{
		public static Stream? GetSramStream(this Models.Structs.Savestate source) => CreateStreamIfNotNull(source.SRA.Data);
		public static Stream? GetWramStream(this Models.Structs.Savestate source) => CreateStreamIfNotNull(source.RAM.Data);
		public static Stream? GetFillRamStream(this Models.Structs.Savestate source) => CreateStreamIfNotNull(source.FIL.Data);

		private static MemoryStream? CreateStreamIfNotNull(byte[]? data) => data is null ? null : new MemoryStream(data);
	}
}
