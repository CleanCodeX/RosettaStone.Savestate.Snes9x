namespace SavestateFormat.Snes9x.Models.Structs
{
	public struct Savestate
	{
		public Header Header;
		public FileBlock NAM;
		public FileBlock CPU;
		public FileBlock REG;
		public FileBlock PPU;
		public FileBlock DMA;
		public FileBlock VRA;
		public FileBlock RAM;
		public FileBlock SRA; // SRAM
		public FileBlock FIL;
		public FileBlock APU; // (if emulating the APU)
		public FileBlock ARE; // (if emulating the APU)
		public FileBlock ARA; // (if emulating the APU)
		public FileBlock SOU; // (if emulating the APU)
		public FileBlock SA1; // (if emulating the SA-1)
		public FileBlock SAR; // (if emulating the SA-1)
		public FileBlock SP7; // (if emulating the SPC7110)
		public FileBlock RTC; // (if emulating the SPC7110 RTC)
	}
}