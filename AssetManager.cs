
using SFML.Graphics;

namespace Invaders;

class AssetManager
{
    public readonly string Assetpath = "Assets/";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;

    public AssetManager()
    {
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
    } 
    public Texture LoadTexture (string name)
    {
        if (textures.TryGetValue(name, out Texture found)) return found;
        string fileName = $"Assets/{name}.png";
        Texture texture = new Texture(fileName);
        textures.Add(name, texture);
        return texture;
    }
    public Font LoadFont (string name)
    {
        if (fonts.TryGetValue(name, out Font found)) return found;
        string fileName = $"Assets/{name}.ttf";
        Font font = new Font(fileName);
        fonts.Add(name, font);
        return font;
    }
}