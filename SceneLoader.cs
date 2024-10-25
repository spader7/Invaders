
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using SFML.System;

namespace Invaders;

public class SceneLoader
{
    private bool game;
    
    public SceneLoader() { }
    public void HandleSceneLoad(Scene scene)
    {
        if (!game) return;
        scene.Clear();
        if (game)
        {
            Game(scene);
            return;
        }
        else Console.WriteLine("Error with sceneloader");
    }
    
    public void LoadGame(Scene scene)
    {
        game = true;
    }
    public void Game(Scene scene)
    {
        scene.Spawn(new Background());
        scene.Spawn(new PlayerShip());
        scene.Spawn(new GUI());
        game = false;
    }
}