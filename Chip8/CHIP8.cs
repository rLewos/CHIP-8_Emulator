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
            this.PC = 0x200;
            this.Registers = new byte[16];
            this.Memory = new byte[4096];
            this.SP = 0x0;
            this.I = 0x0;
        }

        internal void Executar()
        {
            while (true)
            {
                byte dado = this.Memory.ElementAt(this.PC);
            
                this.PC += 1;
                byte dado2 = this.Memory.ElementAt(this.PC);

                ushort opcode = (ushort)(dado << 8 | dado2);

                if (opcode >> 12 == 0x1)
                {
                    this.PC = (ushort)(opcode ^ 0x1000);
                }
                else if (opcode >> 12 == 0x6)
                {
                    ushort registrador = ((ushort)((ushort)(opcode ^ 0x6000) >> 8));
                    this.Registers[registrador] = (byte) (registrador << 0x8 ^ opcode^0x6000);
                    this.PC += 1;
                }
                else if(opcode >> 12 == 0xa)
                {
                    ushort endereco = (ushort) (opcode ^ 0xa000);
                    this.I = endereco;
                    this.PC += 1;
                }
                else if (opcode >> 12 == 0xd)
                {
                    ushort op = ((ushort)(opcode ^ 0xd000));

                    ushort registrador_1 = (ushort) (op >> 8);
                    ushort registrador_2 = (ushort) (op >> 4 ^ registrador_1 << 4);

                    // Apenas retornando dados para ver.
                    ushort dado_1 = this.Memory[registrador_1];
                    ushort dado_2 = this.Memory[registrador_2];

                    this.PC += 1;
                }
                else
                {
                    throw new Exception(String.Format("Instrução não implementada: {0}",opcode));
                }
            }
        }

        public void CarregarROM(byte[] rom)
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
