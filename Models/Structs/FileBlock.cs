using System.Runtime.InteropServices;

namespace RosettaStone.Savestate.Snes9x.Models.Structs
{
	public struct FileBlock
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public char[] Name;
		public char Sepatator1;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public char[] Size;
		public char Sepatator2;
		public byte[] Data;

		public FileBlock(bool init)
		{
			Name = new char[3];
			Size = new char[6];
			Sepatator1 = (char)0;
			Sepatator2 = (char)0;
			Data = new byte[0];
		}
	}
}