
using SFML.Graphics;
using SFML.System;
namespace Invaders;

class Background : Entity
{
    public Background(): base("Backgrounds/darkPurple")
    {
        sprite.TextureRect = new IntRect(0, 0, 256, 256);
    }

    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(0, 0);
        target.Draw(sprite);
    }
}