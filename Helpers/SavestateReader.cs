﻿using System;
using System.IO;
using System.Text;
using RosettaStone.Savestate.Snes9x.Extensions;
using RosettaStone.Savestate.Snes9x.Models.Structs;

namespace RosettaStone.Savestate.Snes9x.Helpers
{
	internal static class SavestateReader
	{
		private const string CurrentVersion = "0011";

		public static Models.Structs.Savestate Load(in string filepath) => Load(filepath, LoadIncludeOffset.SRA);
		public static Models.Structs.Savestate Load(in string filepath, in LoadIncludeOffset includeOffset)
		{
			using var file = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);

			Models.Structs.Savestate result = default;

			if (file.CanRead)
				result = Load(file, includeOffset);
			else
				Console.WriteLine("SavestateEmpty");

			return result;
		}

		public static Models.Structs.Savestate Load(in Stream stream) => Load(stream, LoadIncludeOffset.SRA);
		public static Models.Structs.Savestate Load(in Stream stream, in LoadIncludeOffset includeOffset)
		{
			if (stream.Position != 0)
				stream.Position = 0;

			var uncompressed = GzipHelper.Decompress(stream);
			var ms = new MemoryStream(uncompressed);

			var header = ms.Read<Header>();
			if (!header.IsValid(CurrentVersion))
				throw new ArgumentException($"Invalid header: [{header.GetString()}]. Supported version: {CurrentVersion}");

			Models.Structs.Savestate result = new()
			{
				Header = header,
				NAM = ReadFileBlock(ms),
				CPU = ReadFileBlock(ms),
				REG = ReadFileBlock(ms),
				PPU = ReadFileBlock(ms),
				DMA = ReadFileBlock(ms),
				VRA = ReadFileBlock(ms),
				RAM = ReadFileBlock(ms),
				SRA = ReadFileBlock(ms) // SRAM
			};

			if(includeOffset == LoadIncludeOffset.SRA)
				return result;

			result.FIL = ReadFileBlock(ms);

			if (includeOffset == LoadIncludeOffset.FIL)
				return result;

			result.APU = ReadFileBlock(ms);
			result.ARE = ReadFileBlock(ms); // (if emulating the APU)
			result.ARA = ReadFileBlock(ms); // (if emulating the APU)
			result.SOU = ReadFileBlock(ms); // (if emulating the APU)
			result.SA1 = ReadFileBlock(ms); // (if emulating the SA-1)
			result.SAR = ReadFileBlock(ms); // (if emulating the SA-1)
			result.SP7 = ReadFileBlock(ms); // (if emulating the SPC7110)
			result.RTC = ReadFileBlock(ms); // (if emulating the SPC7110 RTC)

			return result;
		}

		private static FileBlock ReadFileBlock(Stream source)
		{
			var block = new FileBlock(true);
			var reader = new BinaryReader(source, Encoding.ASCII, true);
			var length = source.Length;

			if (source.Position == length) return default;

			reader.Read(block.Name);
			if (source.Position == length) return default;
			if (new string(block.Name) == "000") return default;

			block.Sepatator1 = reader.ReadChar();
			if (source.Position == length) return default;
			if (block.Sepatator1 != ':') return default;

			reader.Read(block.Size, 0, 6);
			if (source.Position == length) return default;

			var sizeString = new string(block.Size);
			var size = int.Parse(sizeString);
			if (size == 0) return default;

			block.Sepatator2 = reader.ReadChar();
			if (source.Position == length) return default;
			if (block.Sepatator2 != ':') return default;

			block.Data = new byte[size];
			reader.Read(block.Data);

			return block;
		}
	}
}