using System.Diagnostics.CodeAnalysis;
using System.IO;
using SavestateFormat.Snes9x.Helpers;
using SramCommons.Models;

namespace SavestateFormat.Snes9x.Extensions
{
	public static class SramFileExtensions
	{
		public static void Load([NotNull] this ISramFile source, string filePath) => source.Load(GetSramStream(filePath));

		public static void Load<TSaveSlot>([NotNull] this ISramFile<TSaveSlot> source, string filePath)
			where TSaveSlot : struct =>
			source.Load(GetSramStream(filePath));

		private static Stream GetSramStream(string filePath) => new SavestateManager().GetSramStream(filePath);
	}
}
