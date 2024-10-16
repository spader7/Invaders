
using SFML.System;

namespace Invaders;

class Ship: Entity
{
    protected float _speed;
    protected int _health;
    protected int _direction;
    protected Vector2f _position;
    protected Vector2f _originalPosition;
    
    public Ship() : base(""){}
    public override void Create(Scene scene)
    {
        _originalPosition = _position;
        base.Create(scene);
    }
    protected virtual void Reset()
    {
        _position = _originalPosition;
    }
}