namespace RosettaStone.Savestate.Snes9x.Models.Structs
{
	/// <summary>
	/// Format according to specs at https://www.romhacking.net/documents/383/
	/// </summary>
	public struct SavestateSnex9x
	{
		public Header Header;

		// A null-terminated string storing the filename of the cartridge ROM
		public FileBlock NAM; 

		// The status of the main processor "Ricoh 5A22", which is based on WDC/GTE's "65c816" core.
		public FileBlock CPU; 

		// The registers of the 65c816.
		public FileBlock REG; 

		// The status of the two PPU (picture-processing unit) chips, and some joypad and mouse data.
		public FileBlock PPU; 

		// The "DMA" file block consists of 8 sub-structures, one for each DMA/HDMA channel. 
		public FileBlock DMA; 

		// V-RAM
		// The "video RAM" (VRAM) data, which the SNES stores in two RAM chips.
		public FileBlock VRA; 

		// W-RAM
		// The "work RAM" (WRAM) data, usually accessed via banks $7E and $7F.
		public FileBlock RAM; 

		// S-RAM
		// The "Save RAM" (SRAM) data, used by cartridges.
		public FileBlock SRA; 

		/* Fill-RAM In the SNES9x code, "FillRAM" is a pointer to 32 KB of scratch memory. Whenever the 65c816 does a write access to addresses $xx:0000..$xx:7FFF, the value is also written into this array.With "FillRAM" the developers can access registers by address rather than by name. */
		public FileBlock FIL;

		// (if emulating the APU)
		// The status of the audio processing unit.
		public FileBlock APU;

		// (if emulating the APU)
		// The "APU registers", i.e. the registers of the Sony "SPC700" audio processor.
		public FileBlock ARE;

		// (if emulating the APU)
		// The "audio processing unit" (APU) RAM, which stores BRR-encoded samples and the program to play them.
		public FileBlock ARA;

		// (if emulating the APU)
		// The status of the APU's DSP. On the SNES, these settings are accessed by the SPC700 by writing to address $00F2 (selects a DSP address) and reading/writing $00F3 (reads/writes a DSP register value).
		public FileBlock SOU;

		// (if emulating the SA-1)
		// The status of the SA-1, which is another "65c816" clocked at 10 MHz.
		public FileBlock SA1;

		// (if emulating the SA-1)
		// The SA-1 processor registers.
		public FileBlock SAR;

		// (if emulating the SPC7110)
		// The status of the "SPC7110", which is a data decompression chip.
		public FileBlock SP7;

		// (if emulating the SPC7110 RTC)
		// The status of the SPC7110's real-time clock.
		public FileBlock RTC; 
	}
}