using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Common.Shared.Min.Extensions;
using IO.Extensions;
using IO.Helpers;
using WRAM.Snes9x.Extensions;
using WRAM.Snes9x.Models.Structs;
using static WRAM.Snes9x.Helpers.SavestateReader;

namespace WRAM.Snes9x.Helpers
{
	public static class SavestateWriter
	{
		private const string CurrentVersion = "0011";

		public static void Save([NotNull] in string filePath, SavestateSnex9x savestate)
		{
			filePath.ThrowIfNull(nameof(filePath));

			using FileStream file = new(filePath, FileMode.Open, FileAccess.Write, FileShare.Read);

			if (file.CanRead)
				Save(file, savestate);
			else
				throw new ArgumentException("Savestate is empty.");
		}

		public static byte[] Save([NotNull] in Stream stream, SavestateSnex9x savestate)
		{
			stream.ThrowIfNull(nameof(stream));
			stream.Position = 0;

			var uncompressed = GzipHelper.Decompress(stream);

			using MemoryStream ms = new(uncompressed);

			var header = ms.Read<Header>();
			if (!header.IsValid(CurrentVersion))
				throw new ArgumentException($"Invalid header: [{header.GetString()}]. Supported version: {CurrentVersion}");

			// NAM
			ReadFileBlock(ms, out _);
			Debug.Print($"{nameof(savestate.NAM)}: {ms.Position}"); // 115

			// CPU
			ReadFileBlock(ms, out _);
			Debug.Print($"{nameof(savestate.CPU)}: {ms.Position}"); // 174

			// REG
			ReadFileBlock(ms, out _);
			Debug.Print($"{nameof(savestate.REG)}: {ms.Position}"); // 201

			// PPU
			ReadFileBlock(ms, out _);
			Debug.Print($"{nameof(savestate.PPU)}: {ms.Position}"); // 2864

			// DMA
			ReadFileBlock(ms, out _); 
			Debug.Print($"{nameof(savestate.DMA)}: {ms.Position}"); // 3027

			// V-RAM
			ReadFileBlock(ms, out _);
			Debug.Print($"{nameof(savestate.VRA)}: {ms.Position}"); // 68574

			// W-RAM
			WriteFileBlock(ms, savestate.RAM); 
			Debug.Print($"{nameof(savestate.RAM)}: {ms.Position}"); // 199657

			// S-RAM
			WriteFileBlock(ms, savestate.SRA);
			Debug.Print($"{nameof(savestate.SRA)}: {ms.Position}"); // 330740

			// FILL-RAM
			WriteFileBlock(ms, savestate.FIL);
			Debug.Print($"{nameof(savestate.FIL)}: {ms.Position}"); // 363519

			// APU
			WriteFileBlock(ms, savestate.APU);
			Debug.Print($"{nameof(savestate.APU)}: {ms.Position}"); // 430090

			// ARE
			WriteFileBlock(ms, savestate.ARE);
			Debug.Print($"{nameof(savestate.ARE)}: {ms.Position}"); // 430192 (if emulating the APU)

			// ARA
			WriteFileBlock(ms, savestate.ARA);
			Debug.Print($"{nameof(savestate.ARA)}: {ms.Position}"); // 430273 (if emulating the APU)
			
			// SOU (SHO)
			WriteFileBlock(ms, savestate.SOU);
			Debug.Print($"{nameof(savestate.SOU)}: {ms.Position}"); // 1164497 (if emulating the APU)

			// SA1
			WriteFileBlock(ms, savestate.SA1);
			Debug.Print($"{nameof(savestate.SA1)}: {ms.Position}"); // 1164497 (if emulating the SA-1)

			// SAR
			WriteFileBlock(ms, savestate.SAR);
			Debug.Print($"{nameof(savestate.SAR)}: {ms.Position}"); // 1164497 (if emulating the SA-1)

			// SP7
			WriteFileBlock(ms, savestate.SP7);
			Debug.Print($"{nameof(savestate.SP7)}: {ms.Position}"); // 1164497 (if emulating the SPC7110)

			// RTC
			WriteFileBlock(ms, savestate.RTC);
			Debug.Print($"{nameof(savestate.RTC)}: {ms.Position}"); // 1164497 (if emulating the SPC7110 RTC)

			return GzipHelper.Compress(uncompressed);
		}

		private static void WriteFileBlock(Stream source, FileBlock block)
		{
			BinaryWriter writer = new(source, Encoding.ASCII, true);

			if (block.Name is null || block.Name.Length == 0) return;

			writer.Write(block.Name);
			writer.Write(block.Sepatator1);

			if (block.Size is null || block.Size.Length == 0) return;

			writer.Write(block.Size, 0, 6);
			writer.Write(block.Sepatator2);

			if (block.Data is null || block.Data.Length == 0) return;

			writer.Write(block.Data);
		}
	}
}