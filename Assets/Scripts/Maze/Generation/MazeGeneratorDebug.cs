using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Maze.Generation;

public class MazeGeneratorDebug : MonoBehaviour
{
    public string seed;

    public Text Text;

    public void gen()
    {
        MazeGenerator.Generate(new(6,6), seed);
    }

    private void Awake()
    {
        MazeGenerator.OnGenerated += updateText;
    }

    private void updateText(string seed)
    {
        Text.text = seed;
    }
}
