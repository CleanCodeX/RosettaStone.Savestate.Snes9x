using System;
using System.IO;
using System.Runtime.InteropServices;
using SramCommons.Extensions;

namespace RosettaStone.Savestate.Snes9x.Extensions
{
	public static partial class StreamExtensions
	{
		internal static T Read<T>(this Stream source) where T: struct
		{
			T result = default;
			var size = Marshal.SizeOf(result);
			var data = new byte[size];

			source.Read(data.AsSpan());

			result = data.ByteArrayToStructure<T>();

			return result;
		}

		internal static T Read<T>(this Stream source, int offset) where T : struct
		{
			T result = default;
			var size = Marshal.SizeOf(result);
			var data = new byte[size];

			source.Read(data, offset, size);

			result = data.ByteArrayToStructure<T>();

			return result;
		}
	}
}
