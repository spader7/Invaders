using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Invaders;

class Program
{
    static void Main(Scene scene)
    {
        using (var window = new RenderWindow(
        new VideoMode(800, 600), "Invaders"))
        {
            window.Closed += (o, e) => window.Close();
            //initialize
            Clock clock = new Clock();

            while (window.IsOpen) 
                {
                    //dispatch events
                    float deltaTime = clock.Restart().AsSeconds();
                    deltaTime = MathF.Min(deltaTime, 0.01f);

                    // Updates
                    scene.UpdateAll(deltaTime);
                    // Drawing
                    window.Clear(Color.Black);
                    scene.RenderAll(window);
                    // render
                    window.Display();
                }
        }
    }
}
