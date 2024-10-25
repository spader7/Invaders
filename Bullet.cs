
using System.Data.Common;
using System.Net.Http.Headers;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Bullet: Ship
{
    private bool IsFriendly;
    private EnemyShip mother;
    public Bullet(Vector2f _velocity, bool isFriendly, Vector2f spawnposition): base(isFriendly ?"PNG/Lasers/laserBlue07":"PNG/Lasers/laserGreen14") 
    {
        if (isFriendly) sprite.Scale = new Vector2f(0.75f, 0.75f);
        else if (!isFriendly) sprite.Scale = new Vector2f(0.5f, 0.5f);
        Position = spawnposition;
        IsFriendly = isFriendly;
        velocity = _velocity;
    }
    public override void Create(Scene scene)
    {
        _bullets = true;
        velocity = velocity * 1.2f;
        base.Create(scene);
        foreach (Entity found in scene.FindIntersects
            (new FloatRect(Bounds.Left,Bounds.Top,Bounds.Width,-Bounds.Height)))
        {
            if (found is EnemyShip enemyShip)
            {
                mother = enemyShip;
                break;
            }    
        }
    }
    public override void Update(Scene scene, float deltaTime)
    {
        travel(scene, deltaTime);
        foreach (Entity found in scene.FindIntersects
            (new FloatRect(Bounds.Left,Bounds.Top,Bounds.Width,-Bounds.Height))) 
            collideWith(scene, found);
    }
    protected override void travel(Scene scene, float deltaTime)
    {
        if (Position.Y > Program.SCREENH + 100 || Position.Y < -200 ||
            Position.X > Program.SCREENW + 200 || Position.X < -200) Destroy(scene);
        base.travel(scene, deltaTime);
    }
    protected override void collideWith(Scene scene, Entity other)
    {
        if (other is PlayerShip && !IsFriendly)
        {
            scene.Events.PublishLoseHealth(1);
            Destroy(scene);
        }
        if (other is EnemyShip && IsFriendly)
            {
                scene.Events.PublishGainScore(200);
                Destroy(scene);
                other.Destroy(scene);
            }
        if (other is EnemyShip && !IsFriendly && other != mother)
        {
            Destroy(scene);
        }
    }
}