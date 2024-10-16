
using SFML.Graphics;

namespace Invaders;

class GUI : Entity
{
    private Text score = new Text();
    private const int MAXHEALTH = 3;
    private int currentHealth;
    private int currentScore;
    public GUI() : base("")
    {
        
    }
}