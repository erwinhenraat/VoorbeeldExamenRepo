using MarkUlrich.Utils;
using UnityEngine;

namespace UntitledCube.Maze.Generation
{
    public class MazeShapeInstance : SingletonInstance<MazeShapeInstance>
    {
        [SerializeField] private Transform[] _spawnpoints;

        public Transform[] Spawnpoints => _spawnpoints;
    }
}