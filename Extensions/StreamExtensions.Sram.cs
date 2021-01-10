using System.IO;
using SavestateFormat.Snes9x.Helpers;

namespace SavestateFormat.Snes9x.Extensions
{
	public static partial class StreamExtensions
    {
	    public static Stream GetSramFromSavestateStream(this Stream source) => new SavestateManager().GetSramStream(source);
    }
}
