using UnityEngine;
using UnityEngine.SceneManagement;
using UntitledCube.Input;

namespace MarkUlrich.StateMachine.States
{
    public class LevelEndState : State
    {
        private const string SCENE_NAME = "LevelEnd";

        public override void EnterState()
        {
            base.EnterState();
            LoadSceneAsync(SCENE_NAME, LoadSceneMode.Additive);
            SetNextState<GameState>();
            InputSystem.ToggleAllInputs(false);
            Time.timeScale = 0;
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
            InputSystem.ToggleAllInputs(true);
            Time.timeScale = 1;
        }
    }
}
