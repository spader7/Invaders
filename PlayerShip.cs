
using SFML.Graphics;
using SFML.System;
namespace Invaders;

class PlayerShip: Ship
{
    public PlayerShip(): base("PNG/playerShip2_red") //wrong base
    {
        sprite.TextureRect = new IntRect(0, 0, 112, 75);
        sprite.Scale = new Vector2f(0.5f, 0.5f);
    }
}