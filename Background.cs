
using System.Data;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Background : Entity
{
    private Sprite sideSprite1;
    private Sprite sideSprite2;
    private Vector2f screenRows;
    private Vector2f sideScreenRows;
    public Background() : base("Backgrounds/darkPurple") 
    {
    sideSprite1 = new Sprite();
    sideSprite2 = new Sprite();    
    }

    public override void Create(Scene scene)
    {
        sideSprite1.Position = new Vector2f (0, 0);
        sideSprite2.Position = new Vector2f (GUI.DSCREENW2, 0);
        sideScreenRows = new Vector2f ((int)MathF.Ceiling((GUI.DSCREENW1/256)), (int)MathF.Ceiling(Program.SCREENH/256));
        
        sprite.Position = new Vector2f(GUI.DSCREENW1, 0);
        screenRows = new Vector2f ((int)MathF.Ceiling((GUI.DSCREENW1/256*2) + (Program.SCREENW/256)), (int)MathF.Ceiling(Program.SCREENH/256));
        sideSprite1.Texture = scene.Assets.LoadTexture("Backgrounds/black");
        sideSprite2.Texture = scene.Assets.LoadTexture("Backgrounds/black");
        base.Create(scene);
    }
    public override void Render(RenderTarget target)
    {
        for (int col = 0; col < screenRows.X; col++)
        {
            for (int row = 0; row <= screenRows.Y; row++)
            {
                sprite.Position = new Vector2f(GUI.DSCREENW1,0) + (256 * new Vector2f(col, row));
                target.Draw(sprite);
            };
        }
        for (int col = 0; col < sideScreenRows.X; col++)
        {
            for (int row = 0; row <= sideScreenRows.Y; row++)
            {
                sideSprite1.Position = new Vector2f(0,0) + (256 * new Vector2f(col, row));
                sideSprite2.Position = new Vector2f(1024,0) + (256 * new Vector2f(col, row));
                target.Draw(sideSprite1);
                target.Draw(sideSprite2);
            }
        }
    }
}