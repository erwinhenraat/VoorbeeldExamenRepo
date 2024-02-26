using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Maze.Generation;

public class MazeGeneratorDebug : MonoBehaviour
{
    public bool activate;
    public Vector2 size;
    public string seed;
    public Vector3 position;
    public Vector3 rotation;

    public Text Text;
    private void Update()
    {
        if(activate)    
        {
            activate = false;
            gen();
        }
    }
    public void gen()
    {
        //Vector3 rot = new(UnityEngine.Random.RandomRange(0, 360), UnityEngine.Random.RandomRange(0, 360), UnityEngine.Random.RandomRange(0, 360));

        MazeGenerator.Generate(size, seed);
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
