
using System.Text;
using SFML.System;

namespace Invaders;

class SceneLoader
{
    private Dictionary<char, Func<Entity>> loaders;
    private string world ="";
    public SceneLoader()
    {
        // Load the scene from a vector2 list
    }
    public void HandleSceneLoad(Scene scene)
    {
        scene.Clear();
        scene.StartGrace();

        string file = $"assets/maps/{world}.txt";
        // make a new file for world. Like platformer
    }
    private bool Create(char symbol, out Entity created)
    {
        if (loaders.TryGetValue(symbol, out Func<Entity> loader))
        {
            created = loader();
            return true;
        }
        created = null;
        return false;
    }
    public void Load(string scene) => world = scene;

    public void ReLoad() => Load(world);
}