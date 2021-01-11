using System.IO;
using RosettaStone.Savestate.Snes9x.Extensions;

namespace RosettaStone.Savestate.Snes9x.Helpers
{
	public static class SavestateHelper
	{
		public static Stream? GetSramStream(Stream stream) => SavestateReader.Load(stream).GetSramStream();
		public static Stream? GetSramStream(string filePath) => SavestateReader.Load(filePath).GetSramStream();
		
		public static Stream? GetWramStream(Stream stream) => SavestateReader.Load(stream).GetWramStream();
		public static Stream? GetWramStream(string filePath) => SavestateReader.Load(filePath).GetWramStream();
		
		public static Stream? GetFillRamStream(Stream stream) => SavestateReader.Load(stream).GetFillRamStream();
		public static Stream? GetFillRamStream(string filePath) => SavestateReader.Load(filePath).GetFillRamStream();
	}
}
