using System.IO;
using SavestateFormat.Snes9x.Models.Structs;

namespace SavestateFormat.Snes9x.Extensions
{
	public static class SavestateExtensions
	{
		public static Stream? GetSramStream(this Savestate source) => CreateStreamIfNotNull(source.SRA.Data);
		public static Stream? GetWramStream(this Savestate source) => CreateStreamIfNotNull(source.RAM.Data);
		public static Stream? GetFillRamStream(this Savestate source) => CreateStreamIfNotNull(source.FIL.Data);

		private static MemoryStream? CreateStreamIfNotNull(byte[]? data) => data is null ? null : new MemoryStream(data);
	}
}
