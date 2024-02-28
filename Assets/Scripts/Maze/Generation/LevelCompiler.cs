using System.Collections.Generic;
using UntitledCube.Maze.Cell;
using UnityEngine;

namespace UntitledCube.Maze.Generation
{
    public class LevelCompiler : MonoBehaviour
    {
        private void OnEnable() => MazeGenerator.OnGenerated += Compile;

        private void OnDisable() => MazeGenerator.OnGenerated -= Compile;

        private void Compile(string seed)
        {
            Dictionary<GameObject, MazeCell[,]> mazes = GridGenerator.Grids;
            foreach (GameObject maze in mazes.Keys)
                maze.transform.parent = transform;
        }
    }
}