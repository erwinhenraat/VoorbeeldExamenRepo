using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class PausedState : State
    {
        private const string SCENE_NAME = "Pause";

        public override void EnterState()
        {
            base.EnterState();
            LoadSceneAsync(SCENE_NAME, LoadSceneMode.Additive);
            SetNextState<GameState>();
            Time.timeScale = 0;
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
            Time.timeScale = 1;
        }
    }
}
