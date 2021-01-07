using System.IO;
using System.IO.Compression;
using System.Text;

namespace SavestateFormat.Snes9x.Helpers
{
	public static class GzipHelper
	{
		public static byte[] Compress(string value, Encoding? encoding = null)
		{
			encoding ??= Encoding.ASCII;

			return Compress(encoding.GetBytes(value));
		}

		public static byte[] Compress(byte[] bytes)
		{
			using var memoryStream = new MemoryStream();
			using var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress);

			gZipStream.Write(bytes);

			return memoryStream.ToArray();
		}

		public static string Decompress(Stream stream, Encoding? encoding)
		{
			encoding ??= Encoding.ASCII;

			return encoding.GetString(Decompress(stream));
		}

		public static byte[] Decompress(Stream stream)
		{
			using var gZipStream = new GZipStream(stream, CompressionMode.Decompress);
			using var memoryStreamOutput = new MemoryStream();

			gZipStream.CopyTo(memoryStreamOutput);
			return memoryStreamOutput.ToArray();
		}
	}
}
