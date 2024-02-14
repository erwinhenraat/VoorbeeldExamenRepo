using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UntitledCube.Maze.Generation;

public class MazeGeneratorDebug : MonoBehaviour
{
    public bool activate;
    public Vector2 size;
    public string seed;
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
}
