
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using SFML.Graphics;

namespace Invaders;

class Scene
{
    private List<Entity> entities;
    public readonly SceneLoader Loader = new SceneLoader();
    public readonly AssetManager Assets = new AssetManager();
    private const int GRACELENGLTH = 5;
    private float grace;

    public Scene()
    {
        entities = new List<Entity>();
    }
    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this); //create function in Entity.cs
    }
    public void Clear()
    {
        for (int e = entities.Count - 1; e >= 0; e--)
        {
            Entity entity = entities[e];
            entities.RemoveAt(e);
            entity.Destroy(this);
        }
    }
    public void UpdateAll(float deltaTime)
    {

    }
    public void RenderAll(RenderTarget target)
    {
        foreach (var entity in entities)
        {
            entity.Render(target);
        }
    }
    public void StartGrace() 
    {
        grace = GRACELENGLTH;
        //if (grace > 0)
        //{
        //    grace -= deltaTime;
        //    return false;
        //}
        //else return true;
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
        int lastEntity = entities.Count - 1;
        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.dead) continue;
            if (bounds.Contains(entity.Position.X, entity.Position.Y)) yield return entity;
        }
    }
}