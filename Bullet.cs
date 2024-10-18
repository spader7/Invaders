
using SFML.Graphics;

namespace Invaders;

class Bullet: Ship
{
    public Bullet(): base("Enemies/enemyBlack1")
    {
        sprite.TextureRect = new IntRect(0, 0, 93, 84);
    }
}