using System.IO;
using SavestateFormat.Snes9x.Helpers;

namespace SavestateFormat.Snes9x.Extensions
{
	public static partial class StreamExtensions
    {
	    public static Stream ConvertSnes9xSavestateToSram(this Stream source)
	    {
			using (source)
				return new SavestateManager().GetSramStream(source);
	    }
    }
}
