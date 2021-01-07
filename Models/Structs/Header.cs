using System.Runtime.InteropServices;

namespace SavestateFormat.Snes9x.Models.Structs
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 14)]
	public struct Header
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public char[] Signature;
		public char Colon;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public char[] Version;
		public char LineFeed;
	}
}
