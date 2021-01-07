using SavestateFormat.Snes9x.Models.Structs;

namespace SavestateFormat.Snes9x.Extensions
{
	public static class HeaderExtensions
	{
		public static bool IsValidSnes9xHeader(this Header source) => new string(source.Signature) == "#!s9xsnp"
														  && source.Colon == ':'
		                                                  && new string(source.Version) == "0011"
		                                                  && source.LineFeed == '\n';
	}
}
