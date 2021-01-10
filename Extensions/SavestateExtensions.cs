using System.IO;
using SavestateFormat.Snes9x.Models.Structs;

namespace SavestateFormat.Snes9x.Extensions
{
	public static class SavestateExtensions
	{
		public static Stream GetSramStream(this Savestate source) => new MemoryStream(source.SRA.Data);
		public static Stream GetWramStream(this Savestate source) => new MemoryStream(source.RAM.Data);
	}
}
