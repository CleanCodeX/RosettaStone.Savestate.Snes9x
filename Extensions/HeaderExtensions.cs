using SavestateFormat.Snes9x.Models.Structs;

namespace SavestateFormat.Snes9x.Extensions
{
	public static class HeaderExtensions
	{
		private const string CurrentVersion = "0011";

		public static bool IsValid(this Header source) => source.IsValid(CurrentVersion);
		public static bool IsValid(this Header source, string versionToCheck) =>
			new string(source.Signature) == "#!s9xsnp"
			&& source.Colon == ':'
			&& new string(source.Version) == versionToCheck
			&& source.LineFeed == '\n';
	}
}
