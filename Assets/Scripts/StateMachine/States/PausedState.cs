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
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
        }
    }
}
