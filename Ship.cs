
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

    public enum Directions
    {
        direction1,
        direction2,
        direction3,
        direction4,
        direction5,
        direction6,
        direction7,
        direction8,
    }
public class Ship: Entity
{
    protected GUI _gui;
    protected int scoreCount;
    protected int _health;
    protected bool spawnedOutside;
    public float _speed;
    protected Vector2f velocity;
    protected float scoreTimer;
    protected bool _enemyShips;
    protected bool _bullets;
    
    protected Ship(string textureName): base(textureName) 
    {
        _speed = 100 + (_gui != null ? (float)_gui.currentScore / 100 : 0);
    }

    public override void Create(Scene scene)
    {
        scene.Events.gainSpeed += SpeedIncrease;
        base.Create(scene);
    }
    public override void Destroy(Scene scene)
    {
        scene.Events.gainSpeed -= SpeedIncrease;
        base.Destroy(scene);
    }
    public override void Update(Scene scene, float deltaTime)
    {
        if (spawnedOutside && !TryMove(new Vector2f(0,0), deltaTime)) spawnedOutside = false;
        Velocity();
        travel(scene, deltaTime);
        base.Update(scene, deltaTime);
    }
    protected virtual void travel(Scene scene, float deltaTime)
    {
        TryMove(new Vector2f(velocity.X, velocity.Y), deltaTime);
    }
    protected virtual bool TryMove(Vector2f movement,float deltaTime)
    {
        bool inside = true;
        Position += movement * deltaTime;
        if (_bullets == true);
        else
        {
            if (Position.X + Bounds.Width > GUI.DSCREENW2)
            {
                Position -= new Vector2f (Position.X + Bounds.Width - GUI.DSCREENW2, 0);
                if (_enemyShips)
                {
                    switch (_direction)
                    {
                        case 3:
                        _direction = 5;
                        break;
                        case 7:
                        _direction = 1;
                        break;
                    }
                }
            inside = false;
            }
            if (Position.X < GUI.DSCREENW1)
            {
                Position += new Vector2f (GUI.DSCREENW1 - Position.X, 0);
                if (_enemyShips)
                {
                    switch (_direction)
                    {
                        case 1:
                        _direction = 7;
                        break;
                        case 5:
                        _direction = 3;
                        break;
                    }
                }
            inside = false;
            }
            if (Position.Y + Bounds.Height> Program.SCREENH && !_enemyShips)
            {
                Position -= new Vector2f (0, Position.Y + Bounds.Height - Program.SCREENH);
                inside = false;
            }
            else if (Position.Y> Program.SCREENH && _enemyShips)
            {
                Position = new Vector2f (Position.X, -100);
                inside = false;
            }
            if (Position.Y < 0)
            {
                if (_enemyShips) inside = false;
                else 
                {
                    Position += new Vector2f (0, -Position.Y); 
                    inside = false;
                }
            }
        }
    return inside;
    }
    
    protected virtual void Velocity()
    {
        switch (_direction)
        {
            case 0:
            velocity = new Vector2f(0, -1) * _speed;
            break;
            case 1:
            velocity = new Vector2f(1, -1) * _speed;
            break;
            case 2:
            velocity = new Vector2f(1, 0) * _speed;
            break;
            case 3:
            velocity = new Vector2f(0.75f, 1) * _speed;
            break;
            case 4:
            velocity = new Vector2f(0, 1) * _speed;
            break;
            case 5:
            velocity = new Vector2f(-0.75f, 1) * _speed;
            break;
            case 6:
            velocity = new Vector2f(-1, 0) * _speed;
            break;
            case 7:
            velocity = new Vector2f(-1, -1) * _speed;
            break;
        }
    }
    protected virtual void SpeedIncrease(Scene scene, int amount)
    {
        _speed += amount;
        if (_speed >= 600) 
        {
            _speed = 600;
            scene.Events.gainSpeed -= SpeedIncrease;
        }
    }
}