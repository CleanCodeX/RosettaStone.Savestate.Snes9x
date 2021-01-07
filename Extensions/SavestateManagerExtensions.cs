using System.IO;
using SavestateFormat.Snes9x.Helpers;

namespace SavestateFormat.Snes9x.Extensions
{
	public static class SavestateManagerExtensions
	{
		public static Stream GetSramStream(this SavestateManager source, Stream stream) => new MemoryStream(source.Load(stream).SRA.Data);
		public static Stream GetSramStream(this SavestateManager source, string filePath) => new MemoryStream(source.Load(filePath).SRA.Data);
	}
}
