using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8
{
    public partial class CHIP8
    {
        private ushort PC;
        private ushort I;
        private byte DelayRegister;
        private byte TimerRegister;
        private byte[] Registers;
        private byte[] Memory;
        private ushort[] Stack;
        private byte SP;
        private byte[,] Screen;

        public CHIP8()
        {
            this.Screen = new byte[64,32];

            this.Registers = new byte[16];

            this.PC = 0x200;
            this.I = 0x0;
            this.Memory = new byte[4096];

            this.DelayRegister = 0x0;
            this.TimerRegister = 0x0;

            this.Stack = new ushort[16];
            this.SP = 0x0;

            this.LoadSprites();
        }

        public void Run()
        {
            byte registerNumber = 0x0;

            while (true)
            {
                byte opcodeByte = this.Memory.ElementAt(this.PC);
                byte opcodeByte_2 = this.Memory.ElementAt(this.PC + 1);

                ushort opcode = (ushort)(opcodeByte << 8 | opcodeByte_2);
                Console.WriteLine(string.Format("{0:X}", opcode));

                byte instructionNumber = (byte)(opcode >> 12);
                byte registerX = 0x0; // VX
                byte registerY = 0x0; // VY
                byte value = 0x0; // KK

                byte dataRegisterX = 0x0;
                byte dataRegisterY = 0x0;

                switch (instructionNumber)
                {
                    case 0x0:

                        switch (opcode & 0x00FF)
                        {
                            case 0xE0:
                                this.Screen = new byte[64, 32];
                                break;

                            case 0xEE:
                                Console.WriteLine("Return from a subroutine.");
                                break;
                        }

                        this.PC += 2;
                        break;

                    case 0x00E0:
                        this.PC += 2;
                        break;

                    case 0x00EE:
                        this.PC += 2;
                        break;

                    case 0x1:
                        this.PC = (ushort)(opcode & 0x0FFF);
                        break;

                    case 0x2: 
                        // There's still something odd.
                        this.Stack[this.SP] = (ushort) ((this.Memory[this.PC]) << 8 | this.Memory[this.PC + 1]);
                        this.SP += 1;

                        this.PC = (ushort) (opcode & 0x0FFF);

                        break;

                    case 0x3:
                        registerX = (byte)((opcode & 0x0F00) >> 8);
                        value = (byte)(opcode & 0x00FF);

                        dataRegisterX = this.Registers[registerX];

                        if (dataRegisterX == value)
                            this.PC += 4;
                        else
                            this.PC += 2;

                        break;

                    case 0x4:
                        registerX = (byte)((opcode & 0x0F00) >> 8);
                        value = (byte)(opcode & 0x00FF);

                        dataRegisterX = this.Registers[registerX];

                        if (dataRegisterX != value)
                            this.PC += 4;
                        else
                            this.PC += 2;

                        break;

                    case 0x5:
                        registerX = (byte)((opcode & 0x0F00) >> 8);
                        registerY = (byte)((opcode & 0x00F0) >> 4);

                        dataRegisterX = this.Registers[registerX];
                        dataRegisterY = this.Registers[registerY];

                        if (dataRegisterX == dataRegisterY)
                            this.PC += 4;
                        else
                            this.PC += 2;

                        break;

                    case 0x6:
                        registerNumber = (byte)((opcode & 0x0F00) >> 8);
                        this.Registers[registerNumber] = (byte)(opcode & 0x00FF);

                        this.PC += 2;
                        break;

                    case 0x7:
                        registerNumber = (byte)((opcode & 0x0F00) >> 8);
                        value = (byte)(opcode & 0x00FF);

                        this.Registers[registerNumber] += value;
                        



                        this.PC += 2;
                        break;

                    case 0x8:

                        byte mask = (byte)(opcode & 0x000F);
                        registerX = (byte)((opcode & 0x0F00) >> 8);
                        registerY = (byte)((opcode & 0x00F0) >> 4);

                        switch (mask)
                        {
                            case 0x0:
                                this.Registers[registerX] = this.Registers[registerY];
                                break;

                            case 0x1:
                                this.Registers[registerX] |= this.Registers[registerY];
                                break;

                            case 0x2:
                                this.Registers[registerX] &= this.Registers[registerY];
                                break;

                            case 0x3:
                                this.Registers[registerX] ^= this.Registers[registerY];
                                break;

                            case 0x4:

                                int x = this.Registers[registerX];
                                int y = this.Registers[registerY];

                                this.Registers[registerX] += this.Registers[registerY];

                                if (x + y > byte.MaxValue)
                                    this.Registers[(int)RegistersEnum.VF] = 0x01;
                                else
                                    this.Registers[(int)RegistersEnum.VF] = 0x00;

                                break;

                            case 0x5:

                                byte vx = this.Registers[registerX];
                                byte vy = this.Registers[registerY];

                                this.Registers[registerX] -= this.Registers[registerY];

                                if (vx - vy < byte.MinValue)
                                    this.Registers[(int)RegistersEnum.VF] = 0x00;
                                else
                                    this.Registers[(int)RegistersEnum.VF] = 0x01;

                                break;

                            case 0x6:
                                this.Registers[registerX] >>= this.Registers[registerY];
                                this.Registers[(int)RegistersEnum.VF] = 0x0;
                                break;

                            case 0x7:
                                //this.Registers[registerX] =- this.Registers[registerY];
                                break;

                            case 0xE:
                                this.Registers[registerX] <<= this.Registers[registerY];
                                this.Registers[(int)RegistersEnum.VF] = 0x0;
                                break;
                        }

                        this.PC += 2;
                        break;

                    case 0x9:
                        registerX = (byte)((opcode & 0x0F00) >> 8);
                        registerY = (byte)((opcode & 0x00F0) >> 4);

                        dataRegisterX = this.Registers[registerX];
                        dataRegisterY = this.Registers[registerY];

                        if (dataRegisterX != dataRegisterY)
                            this.PC += 4;
                        else
                            this.PC += 2;

                        break;

                    case 0xA:
                        this.I = (ushort)(opcode & 0x0FFF);

                        this.PC += 2;
                        break;

                    case 0xB:
                        this.PC = (ushort)((opcode ^ 0xB000) + this.Registers[(int)RegistersEnum.V0]);
                        break;

                    case 0xC:
                        this.PC += 2;
                        break;

                    case 0xD:

                        // Registers
                        registerX = (byte)((opcode & 0x0F00) >> 8);
                        registerY = (byte)((opcode & 0x00F0) >> 4);
                        dataRegisterX = this.Registers[registerX];
                        dataRegisterY = this.Registers[registerY];

                        // Bytes to read from memory.
                        byte bytesToRead = (byte)(opcode & 0x000F);

                        // I Address
                        ushort iAddress = this.I;
                        byte[] data = this.Memory.Skip(iAddress).Take(bytesToRead).ToArray();

                        this.DrawScreen(data, dataRegisterX, dataRegisterY);

                        this.PC += 2;
                        break;

                    case 0xE:
                        this.PC += 2;
                        break;

                    case 0xF:
                        this.PC += 2;
                        break;

                    default:
                        string error = String.Format("Instruction not implemented: {0}", opcode);

                        Debug.WriteLine(error);
                        throw new Exception(error);
                }
            }
        }

        public void LoadROM(byte[] rom)
        {
            if (rom == null)
            {
                Console.WriteLine("ROM was not loaded!");
                throw new ArgumentNullException("ROM was not loaded!");
            }

            rom.CopyTo(this.Memory, 0x200);
        }

        private void LoadSprites()
        {
            ushort memoryAddressAllocation = 0x050;

            Sprites.Zero.CopyTo(this.Memory, memoryAddressAllocation);
            Sprites.One.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.Two.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.Three.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.Four.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.Five.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.Six.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.Seven.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.Eight.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.Nine.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.A.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.B.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.C.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.D.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.E.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
            Sprites.F.CopyTo(this.Memory, memoryAddressAllocation += 0x005);
        }

        private void DrawScreen(byte[] data, byte dataRegisterX, byte dataRegisterY)
        {
            Console.WriteLine("--- DrawCall ---");

            for (int i = 0; i < data.Length; i++)
            {
                this.Screen[dataRegisterX, dataRegisterY + i] ^= data[i];

            }


        }
    }
}
