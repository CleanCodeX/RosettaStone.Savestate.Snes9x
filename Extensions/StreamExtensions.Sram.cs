using System.IO;
using RosettaStone.Savestate.Snes9x.Helpers;

namespace RosettaStone.Savestate.Snes9x.Extensions
{
	public static partial class StreamExtensions
    {
	    public static Stream? GetSramFromSavestate(this Stream source) => SavestateHelper.GetSramStream(source);
    }
}
