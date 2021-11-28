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
            byte dado = this.Memory.ElementAt(this.PC);
            
            this.PC += 1;
            byte dado2 = this.Memory.ElementAt(this.PC);

            ushort opcode = (ushort)(dado << 8 | dado2);

            ushort vv = (ushort)(0x1000 & opcode);
            Debug.WriteLine(Convert.ToString(opcode, 2));


            if (opcode >> 12 == 0x1)
            {
                this.PC = (ushort)(opcode ^ (ushort) 0x1000);
            }
            else if (opcode >> 12 == 0x6)
            {
                ushort registrador = ((ushort)((ushort)(opcode ^ 0x6000) >> 8));
                this.Registers[registrador] = ((byte)(registrador << 0x8 ^ opcode^0x6000));
                this.PC += 1;
            }
            else
            {
                throw new Exception(String.Format("Instrução não implementada: {0}",opcode));
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
