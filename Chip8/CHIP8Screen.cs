using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8
{
    public class CHIP8Screen
    {
        public IntPtr Window { get; set; }
        public IntPtr Renderer { get; set; }
        public IntPtr Texture { get; set; }

        public CHIP8Screen()
        {
            this.Window = IntPtr.Zero;
        }

        public void InitializeWindow()
        {
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

            Window = SDL.SDL_CreateWindow("CHIP8 Emulator",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                800,
                600,
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
        }

        public bool InitializeRenderer()
        {
            Renderer = SDL.SDL_CreateRenderer(this.Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            
            if (Renderer == IntPtr.Zero)
            {
                Console.WriteLine("Renderer could not be initialize: {0}", SDL.SDL_GetError());
                return false;
            }

            return true;
        }

        public void CreateTexture(string fileName)
        {
            Texture = SDL_image.IMG_LoadTexture(this.Window, fileName);
            
            if (Texture == IntPtr.Zero)
            {
                Console.WriteLine("Could not initialize player texture: {0}", SDL.SDL_GetError());
            }
        }
        
        public void Close()
        {
            SDL.SDL_DestroyTexture(Texture);
            SDL.SDL_DestroyRenderer(Renderer);
            SDL.SDL_DestroyWindow(Window);
            SDL.SDL_Quit();
        }

    }
}
