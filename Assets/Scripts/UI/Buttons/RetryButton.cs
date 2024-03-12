using MarkUlrich.StateMachine.States;
using MarkUlrich.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Maze.Generation;

namespace UntitledCube.UI.Buttons
{
    public class RetryButton : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;

        private GenerationMediator _generationMediator;

        private void Start()
        {
            _generationMediator = FindAnyObjectByType<GenerationMediator>();

            _retryButton.onClick.AddListener(ResetLevel);
        }

        private void ResetLevel()
        {
            if (GameStateMachine.Instance.CurrentState is GameState)
                _generationMediator.GenerateMaze(MazeGenerator.Seed);
            
        }
    }
}
 
