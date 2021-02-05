using WRAM.Snes9x.Models.Structs;

namespace WRAM.Snes9x.Extensions
{
	public static class HeaderExtensions
	{
		private const string CurrentId = "#!s9xsnp";
		private const string CurrentVersion = "0011";
		
		public static bool IsValid(this Header source) => source.IsValid(CurrentVersion);
		public static bool IsValid(this Header source, string versionToCheck) =>
			new string(source.Signature) == CurrentId
			&& source.Colon == ':'
			&& new string(source.Version) == versionToCheck
			&& source.LineFeed == '\n';

		public static string GetString(this Header source) => $"{new string(source.Signature)}{source.Colon}{new string(source.Version)}{source.LineFeed}";
	}
}
