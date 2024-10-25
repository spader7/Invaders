
using System.Collections;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class EnemyShip : Ship
{
    private static Random rng = new Random();
    private float shoots;
    private float shootingBuffer;
    private Vector2f pShipDirectionNormalized;
    public EnemyShip() : base("Enemies/ufoGreen")
    {
        sprite.Scale = new Vector2f(1, 1) * ((float)rng.Next(4, 8)/10);
    }
    private void RngDirection()
    {
        bool validPosition = false;

        while (!validPosition)
        {
            switch (rng.Next(2))
            {
                case 0:
                _direction = 3;
                break;
                case 1:
                _direction = 5;
                break;
            }
            switch (_direction)
            {
                case 3:
                Position = new Vector2f(
                    rng.Next(-200, 0), 
                    rng.Next(-200, 0));
                    if (!TryMove(new Vector2f(0,0), 1)) validPosition = true;
                break;
                case 5:
                Position = new Vector2f(
                    rng.Next(Program.SCREENW, Program.SCREENW + 200), 
                    rng.Next(-200, 0));
                    if (!TryMove(new Vector2f(0,0), 1)) validPosition = true;
                break;
            }
        }
    }
    public override void Create(Scene scene)
    {
        _enemyShips = true;
        spawnedOutside = true;
        base.Create(scene);
        RngDirection();
        scene.Events.enemyShootingRate += OnEnemyShoots;
    }
    public override void Destroy(Scene scene)
    {
        scene.Events.enemyShootingRate -= OnEnemyShoots;
        scene.Events.publishExplode(Position, 0.6f);
        base.Destroy(scene);
    }
    public override void Update(Scene scene, float deltaTime)
    {
        Shooting(scene, deltaTime);
        if (shootingBuffer <= -4)
        {
            shootingBuffer = -4;
            scene.Events.enemyShootingRate -= OnEnemyShoots;
        }
        base.Update(scene, deltaTime);
    }
    protected override void collideWith(Scene scene, Entity other)
    {
        if (other is PlayerShip)
        {
            scene.Events.publishLoseHealth(1);
            Destroy(scene);
        }  
    }
    private void Shooting(Scene scene ,float deltaTime)
    {
        shoots += deltaTime;
        if (rng.Next(5,6) + shootingBuffer < shoots)
        {
            FindPlayerShip(scene);
            scene.Spawn(new Bullet(pShipDirectionNormalized * _speed, false, new Vector2f(Position.X + Bounds.Width/(3 * 0.5f), Position.Y)));
            shoots = 0;
        }
    }
    private void OnEnemyShoots(float time)
    {
        shootingBuffer -= time;
    }
    private void FindPlayerShip(Scene scene)
    {
        scene.FindByType<PlayerShip>(out PlayerShip playerShip);
        Vector2f pShipDirection =  playerShip.Position - Position;
        pShipDirectionNormalized = 
            pShipDirection / (float)Math.Sqrt
            (pShipDirection.X * pShipDirection.X + 
            pShipDirection.Y * pShipDirection.Y);
    }
    
}
