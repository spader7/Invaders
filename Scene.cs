
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Scene
{
    private List<Entity> entities;
    private float scoreTimer;
    public float spawnTimer;
    private float spawnBuffer;
    public readonly SceneLoader Loader = new SceneLoader();
    public readonly AssetManager Assets = new AssetManager();
    public readonly Event Events = new Event();
    public bool outOfBounds;
    public bool gameOver;

    public Scene()
    {
        entities = new List<Entity>();
        Events.enemySpawnRate += OnSpawnEnemy;
    }
    private void GetScore(float deltaTime)
    {
        scoreTimer += deltaTime;
        if (scoreTimer >= 1) 
        {
            Events.publishGainScore(100);
            scoreTimer = 0;
        }
    }
    
    private void OnSpawnEnemy(float time)
    {
        spawnBuffer -= time;
        if (spawnBuffer <= -8) 
        {
            Events.enemySpawnRate -= SpawnEnemy;
            spawnBuffer = -8;
        }
    }
    private void SpawnEnemy(float deltaTime)
    {
        spawnTimer += deltaTime;
        if (!FindByType<EnemyShip>(out _))
        {
            Spawn(new EnemyShip());
            Events.publishEnemySpawnRate(0.2f);
            Events.publishEnemyShootingRate(0.2f);
            Events.publishGainSpeed(1);
        }
        else 
        {
            if (10 + spawnBuffer <  spawnTimer) 
            {
                Spawn(new EnemyShip());
                spawnTimer = 0;
                Events.publishEnemySpawnRate(0.2f);
                Events.publishEnemyShootingRate(0.2f);
                Events.publishGainSpeed(1);
            }
        }
    }
    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }
    public void Clear()
    {
        for (int e = entities.Count - 1; e >= 0; e--)
        {
            Entity entity = entities[e];
            entities.RemoveAt(e);
            entity.Destroy(this);
        }
        Events.enemySpawnRate -= SpawnEnemy;
        gameOver = false;
    }
    public void UpdateAll(float deltaTime)
    {
        if (gameOver == true) return;
        else
        {
            Loader.HandleSceneLoad(this);
            for (int i = entities.Count -1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);
            }
            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];
                if (entity.dead) entities.RemoveAt(i);
                else i++;
            }
            SpawnEnemy(deltaTime);
            GetScore(deltaTime);
            Events.UpdateEvents(this, deltaTime);
        }

    }
    public void RenderAll(RenderTarget target)
    {
        foreach (var entity in entities)
        {
            entity.Render(target);
        }
    }
    
    public bool FindByType<T>(out T found) where T : Entity
    {
        foreach (var entity in entities)
        {
            if (!entity.dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }

        found = default;
        return false;
    }
    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        FloatRect otherentity;
        int lastEntity = entities.Count - 1;
        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.dead) continue;
            otherentity = new FloatRect(entity.Bounds.Left,entity.Bounds.Top,entity.Bounds.Width,-entity.Bounds.Height);
            if (bounds.Intersects(otherentity)) yield return entity;
        }
    }
}