using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8
{
    public static class Sprites
    {
        public static readonly byte[] Zero =    new byte[5] { 0xF0, 0x90, 0x90, 0x90, 0xF0 };
        public static readonly byte[] One =     new byte[5] { 0x20, 0x60, 0x20, 0x20, 0x70 };
        public static readonly byte[] Two =     new byte[5] { 0xF0, 0x10, 0xF0, 0x80, 0xF0 };
        public static readonly byte[] Three =   new byte[5] { 0xF0, 0x10, 0xF0, 0x10, 0xF0 };
    }
}
