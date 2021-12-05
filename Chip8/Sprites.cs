using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8
{
    public static class Sprites
    {
        public static readonly byte[] Zero = new byte[5] { 0xF0, 0x90, 0x90, 0x90, 0xF0 };
        public static readonly byte[] One = new byte[5] { 0x20, 0x60, 0x20, 0x20, 0x70 };
        public static readonly byte[] Two = new byte[5] { 0xF0, 0x10, 0xF0, 0x80, 0xF0 };
        public static readonly byte[] Three = new byte[5] { 0xF0, 0x10, 0xF0, 0x10, 0xF0 };
        public static readonly byte[] Four = new byte[5] { 0x90, 0x90, 0xF0, 0x10, 0x10 };
        public static readonly byte[] Five = new byte[5] { 0xF0, 0x80, 0xF0, 0x10, 0xF0 };
        public static readonly byte[] Six = new byte[5] { 0xF0, 0x80, 0xF0, 0x90, 0xF0 };
        public static readonly byte[] Seven = new byte[5] { 0xF0, 0x10, 0x20, 0x40, 0x40 };
        public static readonly byte[] Eight = new byte[5] { 0xF0, 0x90, 0xF0, 0x90, 0xF0 };
        public static readonly byte[] Nine = new byte[5] { 0xF0, 0x90, 0xF0, 0x10, 0xF0 };
        public static readonly byte[] A = new byte[5] { 0xF0, 0x90, 0xF0, 0x90, 0x90 };
        public static readonly byte[] B = new byte[5] { 0xE0, 0x90, 0xE0, 0x90, 0xE0 };
        public static readonly byte[] C = new byte[5] { 0xF0, 0x80, 0x80, 0x80, 0xF0 };
        public static readonly byte[] D = new byte[5] { 0xE0, 0x90, 0x90, 0x90, 0xE0 };
        public static readonly byte[] E = new byte[5] { 0xF0, 0x80, 0xF0, 0x80, 0xF0 };
        public static readonly byte[] F = new byte[5] { 0xF0, 0x80, 0xF0, 0x80, 0x80 };
    }
}
