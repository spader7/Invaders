
using System.Dynamic;
using System.Runtime;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class GUI : Entity
{
    private Text playerScore = new Text();
    private Text scoreBoard = new Text();
    private Text reloadText = new Text();

    private const int MAXHEALTH = 3;
    public const int DSCREENW1 = 256; 
    private static string baseName = "Assets/PNG/UI/numeral";
    private string displayedScore;
    private int currentHealth;
    private float invincibleFrames;
    private float explotionABuffer;
    private float explotionTime;
    private int explotionASwitch;
    private bool reload = false;
    public int currentScore {get; private set; }
    private static Texture[] NUMTEXTURES = new Texture[10];
    private Sprite spriteScoreText;
    private Sprite spriteExplotion;
    public const int DSCREENW2 = 1024;

    public GUI() : base("PNG/UI/playerLife2_red")
    {
        spriteExplotion = new Sprite();
        ScoreText();
    }
    public override void Create(Scene scene)
    {
        spriteExplotion.Scale = new Vector2f(0.8f, 0.8f);
        spriteExplotion.Position = new Vector2f(-100, -100);
        explotionASwitch = 3;
        explotionTime = 0f;
        Font font = scene.Assets.LoadFont("Bonus/kenvector_future_thin");
        reloadText.Font = font;
        reloadText.CharacterSize = 50;
        reloadText.Scale = new Vector2f (0.75f, 0.75f);
        playerScore.Font = font;
        playerScore.DisplayedString = "Score";
        playerScore.CharacterSize = 40;
        playerScore.Scale = new Vector2f (0.75f, 0.75f);
        base.Create(scene);
        GenerateHighScore(scene);
        currentHealth = MAXHEALTH;
        currentScore = 0;
        scene.Events.loseHealth += OnLoseHealth;
        scene.Events.gainScore += OnGainScore;
        scene.Events.explotion += OnExplode;
    }
    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.Events.loseHealth -= OnLoseHealth;
        scene.Events.gainScore -= OnGainScore;
        scene.Events.explotion -= OnExplode;
    }
    public override void Update(Scene scene, float deltaTime)
    {
        if (invincibleFrames > 0) invincibleFrames -= deltaTime;
        if (explotionTime > 0) explotionABuffer += deltaTime;
        if (explotionABuffer > 0.2) 
        {
            explotionTime -= 0.2f;
            explotionASwitch -= 1;
            explotionABuffer = 0;
        }
        base.Update(scene, deltaTime);
    }
    private void OnLoseHealth(Scene scene, int amount)
    {
        if (invincibleFrames <= 0) 
        {
            currentHealth -= amount;
            invincibleFrames = 3;
        }
        if (currentHealth <= 0)
        {
            scene.gameOver = true;
            reload = true;
            GenerateHighScore(scene);
            currentScore = 0;
        }
    }
    private void OnGainScore(Scene scene, int amount)
    {
        currentScore += amount;
    }
    private void OnExplode(Vector2f position, float time)
    {
        spriteExplotion.Position = position;
        explotionASwitch = 3;
        explotionTime = time;
    }
    public override void Render(RenderTarget target)
    {
        if (spriteExplotion != null && explotionASwitch > 0)
        {
            spriteExplotion.Texture = new Texture($"Assets/PNG/Damage/playerShip1_damage{explotionASwitch}.png");
            target.Draw(spriteExplotion);
        }
        sprite.Position = new Vector2f(260, 765);
        for (int i = 0; i < currentHealth; i++)
        {
            base.Render(target);
            sprite.Position += new Vector2f(40, 0);
        }
        RenderScoreText(target);
        if (reload)
        {
            reloadText.DisplayedString = "Killed in Action.\n press Escape to play again";
            reloadText.Position = new Vector2f(Program.SCREENW/2 - reloadText.GetGlobalBounds().Width/2, Program.SCREENH/2);
            target.Draw(reloadText);
        } 
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
