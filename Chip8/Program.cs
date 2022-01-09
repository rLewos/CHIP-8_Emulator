using Chip8;
using SDL2;


CHIP8Screen screen = new CHIP8Screen();
if (screen.InitializeWindow() && screen.InitializeRenderer())
{
    SDL.SDL_Event e;
    bool quit = false;

    CHIP8 chip = new CHIP8();
    if (chip.LoadROM("test_opcode.ch8"))
    {
        Random random = new Random();
        while (!quit)
        {
            chip.RunCicle();

            SDL.SDL_SetRenderDrawColor(screen.Renderer, 0, 0, 0, 0);
            SDL.SDL_RenderClear(screen.Renderer);

            SDL.SDL_SetRenderDrawColor(screen.Renderer, 255, 255, 255, 255);
            SDL.SDL_RenderDrawPoint(screen.Renderer, random.Next(64), random.Next(32));
            SDL.SDL_RenderPresent(screen.Renderer);
            

            while (SDL.SDL_PollEvent(out e) != 0)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        quit = true;
                        break;

                    case SDL.SDL_EventType.SDL_KEYUP:
                        quit = true;
                        break;
                }
            }
        }
    }    

    screen.Close();
}