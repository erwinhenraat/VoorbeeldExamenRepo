using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Maze.Generation;

public class MazeGeneratorDebug : MonoBehaviour
{
    public bool activate;
    public Vector2 size;
    public string seed;

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
        MazeGenerator.Generate(size,seed);
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
