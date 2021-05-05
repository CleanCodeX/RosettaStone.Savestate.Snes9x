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

namespace WRAM.Snes9x.Helpers
{
	public static class SavestateReader
	{
		private const string CurrentVersion = "0011";

		public static SavestateSnex9x Load([NotNull] in string filePath)
		{
			filePath.ThrowIfNull(nameof(filePath));

			using FileStream file = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

			SavestateSnex9x result;

			if (file.CanRead)
				result = Load(file);
			else
				throw new ArgumentException("Savestate is empty.");

			return result;
		}

		public static SavestateSnex9x Load([NotNull] in Stream stream) => Load(stream, false);
		public static SavestateSnex9x Load([NotNull] in Stream stream, bool leaveOpen)
		{
			stream.ThrowIfNull(nameof(stream));
			stream.Position = 0;

			var uncompressed = GzipHelper.Decompress(stream, leaveOpen);

			using MemoryStream ms = new(uncompressed);

			var header = ms.Read<Header>();
			if (!header.IsValid(CurrentVersion))
				throw new ArgumentException($"Invalid header: [{header.GetString()}]. Supported version: {CurrentVersion}");

			SavestateSnex9x result = new();
			result.Header = header;

			// NAM
			if (!ReadFileBlock(ms, out result.NAM)) return result; 
			Debug.Print(ms.Position.ToString()); // 115

			// CPU
			if (!ReadFileBlock(ms, out result.CPU)) return result; 
			Debug.Print(ms.Position.ToString()); // 174

			// REG
			if (!ReadFileBlock(ms, out result.REG)) return result; 
			Debug.Print(ms.Position.ToString()); // 201

			// PPU
			if (!ReadFileBlock(ms, out result.PPU)) return result; 
			Debug.Print(ms.Position.ToString()); // 2864

			// DMA
			if (!ReadFileBlock(ms, out result.DMA)) return result; 
			Debug.Print(ms.Position.ToString()); // 3027

			// V-RAM
			if (!ReadFileBlock(ms, out result.VRA)) return result; 
			Debug.Print(ms.Position.ToString()); // 68574

			// W-RAM
			if (!ReadFileBlock(ms, out result.RAM)) return result; 
			Debug.Print(ms.Position.ToString()); // 199657

			// S-RAM
			if (!ReadFileBlock(ms, out result.SRA)) return result; 
			Debug.Print(ms.Position.ToString()); // 330740

			// FILL-RAM
			if (!ReadFileBlock(ms, out result.FIL)) return result; 
			Debug.Print(ms.Position.ToString()); // 363519

			// APU
			if (!ReadFileBlock(ms, out result.APU)) return result; 
			Debug.Print(ms.Position.ToString()); // 430090

			// ARE
			if (!ReadFileBlock(ms, out result.ARE)) return result; 
			Debug.Print(ms.Position.ToString()); // 430192

			// ARA
			if (!ReadFileBlock(ms, out result.ARA)) return result;  
			Debug.Print(ms.Position.ToString()); // 430273

			// SOU (SHO)
			if (!ReadFileBlock(ms, out result.SOU)) return result;  
			Debug.Print(ms.Position.ToString()); // 1164497

			// SA1
			if (!ReadFileBlock(ms, out result.SA1)) return result;  // 1164500
			Debug.Print(ms.Position.ToString());

			// SAR
			if (!ReadFileBlock(ms, out result.SAR)) return result;  // 1164500
			Debug.Print(ms.Position.ToString());

			// SP7
			if (!ReadFileBlock(ms, out result.SP7)) return result;  // 1164500
			Debug.Print(ms.Position.ToString());

			// RTC
			if (!ReadFileBlock(ms, out result.RTC)) return result;  // 1164500
			Debug.Print(ms.Position.ToString());

			return result;
		}

		public static bool ReadFileBlock(Stream source, out FileBlock block)
		{
			BinaryReader reader = new(source, Encoding.ASCII, true);
			var length = source.Length;

			block = new(true);

			if (source.Position == length) return InvalidateOutput(ref block);
			
			reader.Read(block.Name);
			if (source.Position == length) return false;;
			if (new string(block.Name) == "\0\0\0") return InvalidateOutput(ref block);

			block.Sepatator1 = reader.ReadChar();
			if (source.Position == length) return InvalidateOutput(ref block);
			if (block.Sepatator1 != ':') return InvalidateOutput(ref block);

			reader.Read(block.Size!, 0, 6);
			if (source.Position == length) return InvalidateOutput(ref block);

			var sizeString = new string(block.Size);
			var size = int.Parse(sizeString);
			if (size == 0) return InvalidateOutput(ref block);

			block.Sepatator2 = reader.ReadChar();
			if (source.Position == length) return InvalidateOutput(ref block);
			if (block.Sepatator2 != ':') return InvalidateOutput(ref block);

			block.Data = new byte[size];
			reader.Read(block.Data);

			return true;

			static bool InvalidateOutput(ref FileBlock block)
			{
				block = default;
				return false;
			}
		}
	}
}
