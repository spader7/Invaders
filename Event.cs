
using SFML.System;

namespace Invaders;

public delegate void valueChangedEvent (Scene scene, int value);
public delegate void timeChangedEvent (float time);
public delegate void timePlaceChangedEvent (Vector2f position, float time);
public class Event
{
    public event valueChangedEvent gainScore;
    public event valueChangedEvent loseHealth;
    public event valueChangedEvent gainSpeed;
    public event timePlaceChangedEvent explotion;
    public event timeChangedEvent enemySpawnRate;
    public event timeChangedEvent enemyShootingRate;
    private int scoreGained;
    private int healthLost;
    private int speedGained;
    private float explode;
    private float spawnEnemy;
    private float  enemyShoots;
    private Vector2f explodePos;

    public void UpdateEvents(Scene scene, float deltaTime)
    {
        if (scoreGained != 0)
        {
            gainScore?.Invoke(scene, scoreGained);
            scoreGained = 0;
        }
        if (healthLost != 0)
        {
            loseHealth?.Invoke(scene, healthLost);
            healthLost = 0;
        }
        if (speedGained != 0)
        {
            gainSpeed?.Invoke(scene, speedGained);
            speedGained = 0;
        }
        if (explode != 0)
        {
            explotion?.Invoke(explodePos, explode);
            explode = 0;
        }
        if (spawnEnemy != 0)
        {
            enemySpawnRate?.Invoke(spawnEnemy);
            spawnEnemy = 0;
        }
        if (enemyShoots != 0)
        {
            enemyShootingRate?.Invoke(enemyShoots);
            enemyShoots = 0;
        }
    }
    public void PublishGainScore(int amount)
        => scoreGained += amount;
    public void PublishLoseHealth(int amount)
        => healthLost += amount;
    public void PublishGainSpeed(int amount)
        => speedGained += amount;
    public void PublishExplode(Vector2f position, float time)
    {
        explode += time;
        explodePos = position;
    }
    public void PublishEnemySpawnRate(float time)
        => spawnEnemy += time;
    public void PublishEnemyShootingRate(float time)
        => enemyShoots += time;
}