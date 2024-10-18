
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using SFML.System;

namespace Invaders;

class SceneLoader
{
    private Dictionary<char, Func<Entity>> loaders;
    private string loadScene ="";
    private string waitScene ="";
    private const string WORLD = "world.txt";
    private const string MENU = "Menu.txt";
    
    public SceneLoader()
    {
        loaders = new Dictionary<char, Func<Entity>>
        {
            {'B', () => new Block() },
            {'E', () => new EnemyShip() },
            {'P', () => new PlayerShip() },
        };
    }
    public void HandleSceneLoad(Scene scene)
    {
        if (waitScene == "") return;
        scene.Spawn(new Background());
        scene.Clear();
        scene.StartGrace();

        string file = $"assets/maps/{waitScene}.txt";
        List<string> lines = File.ReadLines(file, Encoding.UTF8).ToList();

        for (int y = 0; y < lines.Count() ; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                Entity entity;
                if (Create(line[x], out entity))
                {
                    Console.WriteLine(line[x]);
                    entity.Position = new Vector2f(
                        (Program.SCREENW * 0.5f) + ((x - line.Length/2) * 40),
                        (Program.SCREENH * 0.5f) + ((y - lines.Count()/2) * 40));
                    scene.Spawn(entity);
                }
            }
        }
        //scene.Spawn(new GUI());
        loadScene = waitScene;
        waitScene = "";
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
    public void Load(string scene) => waitScene = scene;

    public void ReLoad() => Load(loadScene);
}