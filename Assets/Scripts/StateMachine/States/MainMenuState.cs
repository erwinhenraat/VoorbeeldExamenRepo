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
            SetNextState<GameState>();

            MoveToNextState(); // Added so that the game will start, remove when added UI
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
        }
    }
}
