using System.Diagnostics.CodeAnalysis;
using System.IO;
using Savestate.Snes9x.Extensions;

namespace Savestate.Snes9x.Helpers
{
	public static class SavestateHelper
	{
		public static Stream? GetSramStream([NotNull] Stream stream) => SavestateReader.Load(stream).GetSramStream();
		public static Stream? GetSramStream([NotNull] string filePath) => SavestateReader.Load(filePath).GetSramStream();
		
		public static Stream? GetWramStream([NotNull] Stream stream) => SavestateReader.Load(stream).GetWramStream();
		public static Stream? GetWramStream([NotNull] string filePath) => SavestateReader.Load(filePath).GetWramStream();
		
		public static Stream? GetFillRamStream([NotNull] Stream stream) => SavestateReader.Load(stream).GetFillRamStream();
		public static Stream? GetFillRamStream([NotNull] string filePath) => SavestateReader.Load(filePath).GetFillRamStream();
	}
}
