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
        private byte SP;
        private byte[] Registers;
        private byte[] Memory;

        public CHIP8()
        {
            this.Registers = new byte[16];
            
            this.PC = 0x200;
            this.I = 0x0;
            this.Memory = new byte[4096];
            this.SP = 0x0;
        }

        internal void Run()
        {
            byte registerNumber = 0x0;
            while (true)
            {
                byte memData = this.Memory.ElementAt(this.PC);
            
                this.PC += 1;
                byte memData2 = this.Memory.ElementAt(this.PC);

                ushort opcode = (ushort)(memData << 8 | memData2);
                Debug.WriteLine("Hex: 0x{0}", opcode);

                #region Switch
                byte instructionNumber = (byte)(opcode >> 12);                
                switch (instructionNumber)
                {
                    case 0x0:
                        this.PC += 1;
                        break;

                    case 0x1:
                        this.PC = (ushort)(opcode ^ 0x1000);
                        break;

                    case 0x2:
                        this.PC += 1;
                        break;

                    case 0x3:
                        this.PC += 1;
                        break;

                    case 0x4:
                        this.PC += 1;
                        break;

                    case 0x5:
                        this.PC += 1;
                        break;
                    
                    case 0x6:
                        registerNumber = (byte) ((opcode ^ 0x6000) >> 8);
                        this.Registers[registerNumber] = (byte) (registerNumber << 0x8 ^ opcode ^ 0x6000);
                        this.PC += 1;

                        break;
                    
                    case 0x7:
                        registerNumber = (byte) ((opcode ^ 0x7000) >> 8);
                        this.Registers[registerNumber] += (byte) (0x7000^(opcode ^ (registerNumber << 8)));
                        this.PC += 1;

                        break;
                    
                    case 0x8:

                        ushort opcode_8x = (ushort)(opcode ^ 0x8000);
                        byte mask = (byte) ((opcode_8x << 28) >> 28);

                        byte registerX = (byte) (opcode_8x >> 8);
                        byte registerY = (byte) ((registerX << 8 ^ opcode_8x) >> 4);

                        switch (mask)
                        {
                            case 0x0:
                                this.Registers[registerX] = this.Registers[registerY];
                                break;

                            case (0x1):
                                this.Registers[registerX] |= this.Registers[registerY];
                                break;

                            case (0x2):
                                this.Registers[registerX] &= this.Registers[registerY];
                                break;

                            case (0x3):
                                this.Registers[registerX] ^= this.Registers[registerY];
                                break;

                            case (0x4):

                                int x = this.Registers[registerX];
                                int y = this.Registers[registerY];

                                if (x + y > byte.MaxValue)
                                    this.Registers[0xF] = 0x01;
                                else
                                    this.Registers[0xF] = 0x00;

                                this.Registers[registerX] += this.Registers[registerY];
                                
                                break;

                            case (0x5):
                                
                                int vx = this.Registers[registerX];
                                int vy = this.Registers[registerY];

                                if (vx - vy < byte.MinValue)
                                    this.Registers[0xF] = 0x01;
                                else
                                    this.Registers[0xF] = 0x00;

                                this.Registers[registerX] -= this.Registers[registerY];
                                break;

                            case (0x6):
                                this.Registers[registerX] >>= this.Registers[registerY];
                                this.Registers[0xF] = 0x0; // Carry
                                break;

                            case (0x7):
                                //this.Registers[registerX] =- this.Registers[registerY];
                                break;

                            case (0xE):
                                this.Registers[registerX] <<= this.Registers[registerY];
                                this.Registers[0xF] = 0x0; // Carry
                                break;
                        }

                        this.PC += 1;
                        break;

                    case 0x9:
                        this.PC += 1;
                        break;

                    case 0xA:
                        ushort endereco = (ushort)(opcode ^ 0xa000);
                        this.I = endereco;
                        this.PC += 1;

                        break;

                    case 0xB:
                        this.PC += 1;
                        break;

                    case 0xC:
                        this.PC += 1;
                        break;

                    case 0xD:
                        ushort op = (ushort)(opcode ^ 0xd000);

                        ushort registrador_1 = (ushort)(op >> 8);
                        ushort registrador_2 = (ushort)(op >> 4 ^ registrador_1 << 4);

                        // Apenas retornando dados para ver.
                        ushort dado_1 = this.Memory[registrador_1];
                        ushort dado_2 = this.Memory[registrador_2];

                        this.PC += 1;
                        break;

                    case 0xE:
                        this.PC += 1;
                        break;

                    case 0xF:
                        this.PC += 1;
                        break;

                    default:
                        string error = String.Format("Instruction not implemented: {0}", opcode);
                        
                        Debug.WriteLine(error);
                        throw new Exception(error);
                }
                #endregion
            }
        }

        public void LoadROM(byte[] rom)
        {
            if (rom == null)
            {
                Console.WriteLine("A ROM não foi carregada!");
                throw new ArgumentNullException("A ROM não foi carregada!");
            }

            rom.CopyTo(this.Memory, 0x200);
        }
    }
}
