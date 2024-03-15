using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class SeedState : State
    {
        private const string SCENE_NAME = "Seed";

        public override void EnterState()
        {
            base.EnterState();
            LoadSceneAsync(SCENE_NAME, LoadSceneMode.Single);
            SetNextState<GameState>();
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
        }
    }
}
