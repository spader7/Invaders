using SFML.System;
using SFML.Graphics;

namespace Invaders;

class Block : Entity
{
    
    public Block() : base("PNG/Power-ups/PowerupGreen")
    {
        sprite.TextureRect = new IntRect(2, 2, 31, 31);
        sprite.Scale = new Vector2f(1.3f, 1f);
    } 
}