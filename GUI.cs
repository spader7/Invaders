
using System.Dynamic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

class GUI : Entity
{
    private Text playerScore = new Text();
    private Text scoreBoard = new Text();

    private const int MAXHEALTH = 3;
    private int currentHealth = 3;
    private int currentScore;
    private string displayedScore;
    private Vector2f nextPosition;
    private static string baseName = "Assets/PNG/UI/numeral";
    private static Texture[] NUMTEXTURES = new Texture[10];
    private Sprite spriteScoreText;
    public const int DSCREENW1 = 256; 
    public const int DSCREENW2 = 1024;

    public GUI() : base("PNG/UI/playerLife1_red")
    {
        sprite.Scale = new Vector2f(1.2f, 1.2f);
        ScoreText();
    }
    public override void Create(Scene scene)
    {
        Font font = scene.Assets.LoadFont("Bonus/kenvector_future_thin");
        playerScore.Font = font;
        playerScore.DisplayedString = "Score";
        playerScore.CharacterSize = 40;
        playerScore.Scale = new Vector2f (0.75f, 0.75f);
        base.Create(scene);
        GenerateHighScore(scene);
    }
    //public override void Update(Scene scene, float deltaTime)
    //{
    //    GenerateHighScore(scene);
    //    //TEMPORARY
    //    //TEMPORARY
    //    //TEMPORARY
    //    //TEMPORARY
    //    //TEMPORARY
    //    //TEMPORARY
    //}
    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(260, 765);
        for (int i = 0; i < currentHealth; i++)
        {
            base.Render(target);
            sprite.Position += new Vector2f(40, 0);
        }

        RenderScoreText(target);
        playerScore.Position = new Vector2f(DSCREENW1/2 - playerScore.GetGlobalBounds().Width/2, 50);
        target.Draw(playerScore);
    }
    private void ScoreText()
    {
        for (int i = 0; i < 10; i++)
            NUMTEXTURES[i] = new Texture($"{baseName}{i}.png");
        {
        }
        spriteScoreText = new Sprite();
        spriteScoreText.Scale = new Vector2f(1.2f, 1.2f);
    }
    private void RenderScoreText(RenderTarget target)
    {
        displayedScore = $"{currentScore}";
        spriteScoreText.Position = new Vector2f((DSCREENW1/2 - (displayedScore.Length * 24)/2), 100);
        foreach (var n in displayedScore)
        {
            spriteScoreText.Texture = NUMTEXTURES[int.Parse(n.ToString())];
            target.Draw(spriteScoreText);
            spriteScoreText.Position += new Vector2f(26, 0);
            target.Draw(scoreBoard);
        }
    }
    private void GenerateHighScore(Scene scene)
    {
        Font font = scene.Assets.LoadFont("Bonus/kenvector_future_thin");
        scoreBoard.Font = font;
        scoreBoard.DisplayedString = "Score";
        scoreBoard.CharacterSize = 40;
        scoreBoard.Scale = new Vector2f(0.75f, 0.75f);
        
        string file = "Assets/ScoreBoard.txt";
        List<int> scores = new List<int>();
        if (File.Exists(file))
        {
            File.ReadLines(file, Encoding.UTF8).ToList().ForEach(score =>
            {
                int output;
                if (int.TryParse(score, out output)) scores.Add(output);
                else Console.WriteLine($"Unable to read score from {file}");
            });
        }
        scores.Add(currentScore);
        scores.Sort((a, b) => a > b? -1: 1);
        if (scores.Count > 20) scores.RemoveRange(20, scores.Count -20);
        string scoresString = ""; 
        int n = 1;
        scores.ForEach(s =>
        {
            scoresString += $"{s}\n";
            n++;
        });
        File.WriteAllText(file, scoresString);
        scoreBoard.DisplayedString = $"HighScores:\n{scoresString}";
        scoreBoard.Position = new Vector2f(DSCREENW1/2 - scoreBoard.GetGlobalBounds().Width/2, 455);
    }
}
