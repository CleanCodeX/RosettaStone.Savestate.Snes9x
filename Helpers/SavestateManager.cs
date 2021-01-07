using System;
using System.IO;
using SavestateFormat.Snes9x.Extensions;
using SavestateFormat.Snes9x.Models.Structs;

namespace SavestateFormat.Snes9x.Helpers
{
	public class SavestateManager
	{
		public Savestate Load(string filepath)
		{
			using var file = new FileStream(filepath, FileMode.Open, FileAccess.Read);
			Savestate result = default;

			if (file.CanRead)
			{
				result = Load(file);

				file.Close();
			}
			else
				Console.WriteLine("SavestateEmpty");

			return result;
		}

		public Savestate Load(Stream stream)
		{
			var uncompressed = GzipHelper.Decompress(stream);
			var ms = new MemoryStream(uncompressed);

			var header = ms.Read<Header>();
			if (!header.IsValidSnes9xHeader())
			{
				Console.WriteLine("SavestateInvalidFile");
				return default;
			}

			Savestate result = new()
			{
				Header = header,
				NAM = ms.ReadFileBlock(),
				CPU = ms.ReadFileBlock(),
				REG = ms.ReadFileBlock(),
				PPU = ms.ReadFileBlock(),
				DMA = ms.ReadFileBlock(),
				VRA = ms.ReadFileBlock(),
				RAM = ms.ReadFileBlock(),
				SRA = ms.ReadFileBlock(), // SRAM
				FIL = ms.ReadFileBlock(), 
				APU = ms.ReadFileBlock(), 
				ARE = ms.ReadFileBlock(), // (if emulating the APU)
				ARA = ms.ReadFileBlock(), // (if emulating the APU)
				SOU = ms.ReadFileBlock(), // (if emulating the APU)
				SA1 = ms.ReadFileBlock(), // (if emulating the SA-1)
				SAR = ms.ReadFileBlock(), // (if emulating the SA-1)
				SP7 = ms.ReadFileBlock(), // (if emulating the SPC7110)
				RTC = ms.ReadFileBlock() // (if emulating the SPC7110 RTC)
			};

			return result;
		}
	}
}
