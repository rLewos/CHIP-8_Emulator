// See https://aka.ms/new-console-template for more information
using Chip8;

int tamanhoROM = 0;
byte[] romArray;

using (BinaryReader rom = new BinaryReader(File.Open("test_opcode.ch8", FileMode.Open)))
{
    tamanhoROM = Convert.ToInt32(rom.BaseStream.Length);
    
    romArray = new byte[tamanhoROM];
    romArray = rom.ReadBytes(tamanhoROM);
}

CHIP8 chip = new CHIP8();
chip.CarregarROM(romArray);
chip.Executar();