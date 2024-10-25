
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Invaders;

public sealed class PlayerShip: Ship
{
    private bool isSpacePressed;
    private float shootingRate = 2f;
    private float lastShot;
    private float animationBuffer = 0.1f;
    private float invincibleFrames;
    private float animationSwitch;
    private bool blinking;
    public PlayerShip(): base("PNG/playerShip2_red")
    {
        sprite.Scale = new Vector2f(0.5f, 0.5f);
    }
    public override void Create(Scene scene)
    {
        base.Create(scene);
        _direction = (int)Directions.direction1;
        Position =  new Vector2f(Program.SCREENW/2 - Bounds.Width/2, 0.8f*Program.SCREENH - Bounds.Height/2);
        scene.Events.loseHealth += OnLoseHealth;
    }
    public override void Update(Scene scene, float deltaTime)
    {
        if (invincibleFrames > 0) 
        {
            invincibleFrames -= deltaTime;
            animationSwitch += deltaTime;
            if (animationSwitch >= animationBuffer) 
            {
                blinking = !blinking;
                animationSwitch = 0;
            }
        }
        else blinking = false;
        if (lastShot > 0) lastShot -= deltaTime;
        base.Update(scene, deltaTime);
        Shooting(scene);
        
    }
    private void OnLoseHealth(Scene scene, int amount)
    {
        invincibleFrames = 3;
        float blinking = 3;
    }
    public override void Render(RenderTarget target)
    {
        if (blinking == false) base.Render(target);
    }
    protected override void travel(Scene scene, float deltaTime)
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
        {
            TryMove(new Vector2f(-_speed, 0), deltaTime);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
        {
            TryMove(new Vector2f(_speed, 0), deltaTime);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.W))
        {
            TryMove(new Vector2f(0, -_speed), deltaTime);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.S))
        {
            TryMove(new Vector2f(0, _speed), deltaTime);
        }    
    }
    private void Shooting(Scene scene)
    {
        shootingRate = 2/(_speed/80);
        if (lastShot <= 0) isSpacePressed = false;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !isSpacePressed)
        {
            isSpacePressed = true;
            scene.Spawn(new Bullet(velocity, true, new Vector2f(Position.X + Bounds.Width/(3 * 0.5f), Position.Y)));
            scene.Spawn(new Bullet(velocity, true, new Vector2f(Position.X + Bounds.Width/(3 * 1.5f), Position.Y)));
            lastShot = shootingRate;
        }    
    }
}