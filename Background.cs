
using SFML.Graphics;
using SFML.System;
namespace Invaders;

class Background : Entity
{
    public Background() : base("Backgrounds/darkPurple") { }

        public override void Create(Scene scene)
        {
            sprite.Position = new Vector2f(0, 0);
            base.Create(scene);
        }
}