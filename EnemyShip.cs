
using SFML.Graphics;
using SFML.System;

namespace Invaders;

class EnemyShip: Ship
{
    private int yrow;
    private int texturechoice;
    private static string[] TEXTURES =
        {
            new ("Enemies/enemyBlack3"),
            new ("Enemies/enemyBlack1"),
            new ("Enemies/enemyBlack4"),
        };
    public EnemyShip(): base("Enemies/enemyBlack1")
    {
        sprite.TextureRect = new IntRect(0, 0, 93, 84);
        sprite.Scale = new Vector2f(0.4f, 0.4f);
    }
    public override void Create(Scene scene)
    {
        int yrow = Convert.ToInt32(Position.Y / 40);
        if (yrow == 3) texturechoice = 0;
        else if (yrow <= 5) texturechoice = 1;
        else if (yrow > 5) texturechoice = 2;
        sprite.Texture = scene.Assets.LoadTexture(TEXTURES[texturechoice]);
    }
}