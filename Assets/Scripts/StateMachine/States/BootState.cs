using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class BootState : State
    {
        private const string SCENE_NAME = "Boot";

        public override void EnterState()
        {
            base.EnterState();
            LoadSceneAsync(SCENE_NAME, LoadSceneMode.Single);
            SetNextState<MainMenuState>();

            MoveToNextState();
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
        }
    }
}
