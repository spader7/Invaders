
using SFML.Graphics;
using SFML.System;

namespace Invaders;

class Entity
{
    private string textureName;
    protected Sprite sprite = new Sprite();
    public bool dead = false;

    protected Entity(string textureName)
    {
        this.textureName = textureName;
        sprite = new Sprite();
    }
    public Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }
    public virtual FloatRect Bounds => sprite.GetGlobalBounds();

    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.Assets.LoadTexture($"PNG/{textureName}");
    }
    public virtual void Destroy(Scene scene)
    {
        dead = true;
    }
    public virtual void Update(Scene scene, float deltaTime)
    {
        foreach (Entity found in scene.FindIntersects(Bounds)) collideWith(scene, found);
    }
    protected virtual void collideWith(Scene s, Entity other)
    {
        //overrides
    }
    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
}