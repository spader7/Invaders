
using SFML.Graphics;
using SFML.System;

namespace Invaders;

class EnemyShip : Ship
{
    public EnemyShip() : base("Enemies/enemyBlack3")
    {

        sprite.Scale = new Vector2f(0.4f, 0.4f);
    }
}