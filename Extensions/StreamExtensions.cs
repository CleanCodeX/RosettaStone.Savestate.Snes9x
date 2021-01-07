using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using SramCommons.Extensions;

namespace SavestateFormat.Snes9x.Extensions
{
	public enum Endianness
	{
		Little,
		Big
	}

	public static partial class StreamExtensions
	{
		public static T Read<T>(this Stream source, Endianness endianness = default) where T: struct
		{
			T result = default;
			var size = Marshal.SizeOf(result);
			var data = new byte[size];

			source.Read(data.AsSpan());

			if(endianness == Endianness.Big)
				data = data.Reverse().ToArray();

			result = data.ByteArrayToStructure<T>();

			return result;
		}

		public static T Read<T>(this Stream source, int offset, Endianness encoding = default) where T : struct
		{
			T result = default;
			var size = Marshal.SizeOf(result);
			var data = new byte[size];

			source.Read(data, offset, size);

			if (encoding == Endianness.Big)
				data = data.Reverse().ToArray();

			result = data.ByteArrayToStructure<T>();

			return result;
		}
	}
}
