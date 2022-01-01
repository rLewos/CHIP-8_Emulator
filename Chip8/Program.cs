using Chip8;
using SDL2;

int romLength = 0;
byte[] romArray;

using (BinaryReader rom = new BinaryReader(File.Open("test_opcode.ch8", FileMode.Open)))
{
    romLength = Convert.ToInt32(rom.BaseStream.Length);
    
    romArray = new byte[romLength];
    romArray = rom.ReadBytes(romLength);
}

CHIP8Screen screen = new CHIP8Screen();
screen.InitializeWindow();

if (screen.InitializeRenderer())
{
    screen.CreateTexture("Tifa_Portrait.png");

    while (true)
    {
        SDL.SDL_RenderClear(screen.Window);

        SDL.SDL_Rect sourceRect;
        sourceRect.x = 0;
        sourceRect.y = 0;
        sourceRect.w = 632;
        sourceRect.h = 768;

        SDL.SDL_Rect targetRect;
        targetRect.x = 180;
        targetRect.y = 200;
        targetRect.w = 100;
        targetRect.h = 100;

        SDL.SDL_RenderCopy(screen.Window, screen.Texture, ref sourceRect, ref targetRect);
        SDL.SDL_RenderPresent(screen.Window);
        
        SDL.SDL_Delay(5000);
    }

    SDL.SDL_Event e = new();
    bool quit = false;

    while (!quit)
    {
        while(SDL.SDL_PollEvent(out e) != 0)
        {
            switch (e.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    quit = true;
                    break;
            }
        }
    }

    screen.Close();
}

CHIP8 chip = new CHIP8();
chip.LoadROM(romArray);
chip.Run();

