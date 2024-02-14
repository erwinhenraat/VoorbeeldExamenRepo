using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UntitledCube.Maze.Generation;

public class MazeGeneratorDebug : MonoBehaviour
{
    public bool activate;
    public Vector2 size;
    private void Update()
    {
        if(activate)
        {
            activate = false;
            MazeGenerator.Generate(size);
        }
    }
    public void gen()
    {
        MazeGenerator.Generate(new(10,10));
    }
}
