using System.IO;

namespace RosettaStone.Savestate.Snes9x.Extensions
{
	public static class ArrayExtensions
	{
		public static MemoryStream? ToStreamIfNotNull(this byte[]? source)
		{
			if (source is null) return null;

			return new(source);
		}

		public static MemoryStream ToStream(this byte[] source) => new(source);
	}
}
