// See https://aka.ms/new-console-template for more information
using Chip8;

int romLength = 0;
byte[] romArray;

using (BinaryReader rom = new BinaryReader(File.Open("test_opcode.ch8", FileMode.Open)))
{
    romLength = Convert.ToInt32(rom.BaseStream.Length);
    
    romArray = new byte[romLength];
    romArray = rom.ReadBytes(romLength);
}

CHIP8 chip = new CHIP8();
chip.LoadROM(romArray);
chip.Run();