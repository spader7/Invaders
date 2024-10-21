
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
        
    }
    public void HandleSceneLoad(Scene scene)
    {
        if (waitScene == "") return;
        scene.Clear();
        scene.StartGrace();
        scene.Spawn(new Background());
        scene.Spawn(new GUI());
        scene.Spawn(new PlayerShip());

        
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