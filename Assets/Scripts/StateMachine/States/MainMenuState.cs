using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class MainMenuState : State
    {
        private const string SCENE_NAME = "MainMenu";

        public override void EnterState()
        {
            base.EnterState();
            LoadSceneAsync(SCENE_NAME, LoadSceneMode.Single);
            MoveToNextState();
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
        }
    }
}
