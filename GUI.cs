
using System.Dynamic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

class GUI : Entity
{
    private Text playerScore = new Text();
    private const int MAXHEALTH = 3;
    private int currentHealth;
    private int currentScore;
    private string displayedScore;
    private Vector2f nextPosition;
    private static string baseName = "Assets/PNG/UI/numeral";
    private static Texture[] NUMTEXTURES = new Texture[10];
    public const int DSCREENW1 = 256; 
    public const int DSCREENW2 = 1024;
    private Sprite spriteScoreText;

    public GUI() : base("PNG/UI/playerLife1_red")
    {
        scoreText();
    }
    public override void Create(Scene scene)
    {
        Font font = scene.Assets.LoadFont("Bonus/kenvector_future_thin");
        playerScore.Font = font;
        playerScore.DisplayedString = "Score";
        playerScore.CharacterSize = 40;
        playerScore.Scale = new Vector2f (0.75f, 0.75f);
        base.Create(scene);
    }
    public override void Render(RenderTarget target)
    {
        displayedScore = $"{currentScore}";
        spriteScoreText.Position = new Vector2f((DSCREENW1/2 - (displayedScore.Length * 24)/2), 200);
        foreach (var n in displayedScore)
        {
            spriteScoreText.Texture = NUMTEXTURES[int.Parse(n.ToString())];
            target.Draw(spriteScoreText);
            spriteScoreText.Position += new Vector2f(26, 0);
            
        }
        base.Render(target);

        playerScore.Position = new Vector2f(DSCREENW1/2 - playerScore.GetGlobalBounds().Width/2, 150);
        target.Draw(playerScore);
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
