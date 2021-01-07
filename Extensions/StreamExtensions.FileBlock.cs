using System.IO;
using System.Text;
using SavestateFormat.Snes9x.Models.Structs;

namespace SavestateFormat.Snes9x.Extensions
{
	public static partial class StreamExtensions
    {
	    public static FileBlock ReadFileBlock(this Stream source)
        {
	        var block = new FileBlock(true);
	        var reader = new BinaryReader(source, Encoding.ASCII);
			var length = source.Length;

	        if (source.Position == length) return default;
			
	        reader.Read(block.Name);
			if (source.Position == length) return default;
			if (new string(block.Name) == "000") return default;
			
			block.Sepatator1 = reader.ReadChar();
			if (source.Position == length) return default;
			if (block.Sepatator1 != ':') return default;
			
			reader.Read(block.Size, 0, 6);
			if (source.Position == length) return default;

			var sizeString = new string(block.Size);
			var size = int.Parse(sizeString);
			if (size == 0) return default;
			
			block.Sepatator2 = reader.ReadChar();
			if (source.Position == length) return default;
			if (block.Sepatator2 != ':') return default;
			
			block.Data = new byte[size];
			reader.Read(block.Data);

			return block;
        }
    }
}
