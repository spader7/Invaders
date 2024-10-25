using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Net;

namespace Invaders;

class Program
{
    public const int SCREENW = 1280;
    public const int SCREENH =800;
    static void Main(string[] args)
    {
        using (var window = new RenderWindow(
        new VideoMode(1280, 800), "Invaders"))
        {
            window.Closed += (o, e) => window.Close();
            //initialize
            Clock clock = new Clock();
            Scene scene = new Scene();
            scene.Loader.LoadGame(scene);
            window.KeyPressed += (s, e) => 
            { 
                if (e.Code == Keyboard.Key.Escape && scene.gameOver)
                {
                    scene.Loader.LoadGame(scene);
                    scene.gameOver = false; 
                }
            };
            while (window.IsOpen) 
            {
                
                 //dispatch events
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();
                deltaTime = MathF.Min(deltaTime, 0.01f);
                 // Updates
                scene.UpdateAll(deltaTime);
                 // Drawing
                window.Clear(Color.Black);
                scene.RenderAll(window);
                window.Display();
            }
        }
    }
}
