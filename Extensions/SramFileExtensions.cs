using System.Diagnostics.CodeAnalysis;
using System.IO;
using Common.Shared.Min.Extensions;
using RosettaStone.Savestate.Snes9x.Helpers;
using SramCommons.Models;

namespace RosettaStone.Savestate.Snes9x.Extensions
{
	public static class SramFileExtensions
	{
		public static void LoadSavestateSram([NotNull] this ISramFile source, string filePath) => source.Load(GetSramStreamOrThrow(filePath));

		public static void LoadSavestateSram<TSaveSlot>([NotNull] this ISramFile<TSaveSlot> source, string filePath)
			where TSaveSlot : struct =>
			source.Load(GetSramStreamOrThrow(filePath));

		private static Stream GetSramStreamOrThrow(string filePath) => SavestateHelper.GetSramStream(filePath).GetOrThrowIfNull("Stream");
	}
}
