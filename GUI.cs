
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

class GUI : Entity
{
    private Text score = new Text();
    private const int MAXHEALTH = 3;
    private int currentHealth;
    private int currentScore = 10000000;
    private string displayedScore;
    private Vector2f nextPosition;
    private static string baseName = "Assets/PNG/UI/numeral";
    private const int DSCREENW1 = 200;
    private const int DSCREENW2 = 800;
    Sprite spriteScoreText;
    

    //private static string[] NUMTEXTURES = new string[10];
    private static Texture[] NUMTEXTURES = new Texture[10];

    public GUI() : base("PNG/UI/playerLife1_red")
    {
        scoreText();
    }

    public override void Render(RenderTarget target)
    {
        displayedScore = $"{currentScore}";
        spriteScoreText.Position = new Vector2f((DSCREENW1/2 - (displayedScore.Length * 24)/2), 400);
        foreach (var n in displayedScore)
        {
            spriteScoreText.Texture = NUMTEXTURES[int.Parse(n.ToString())];
            target.Draw(spriteScoreText);
            spriteScoreText.Position += new Vector2f(24, 0);
            
        }
        base.Render(target);
    }
    private void scoreText()
    {
        for (int i = 0; i < 10; i++)
            NUMTEXTURES[i] = new Texture($"{baseName}{i}.png");
        {
        }
        spriteScoreText = new Sprite();
        spriteScoreText.Scale = new Vector2f(1.2f, 1.2f);
    }
}
