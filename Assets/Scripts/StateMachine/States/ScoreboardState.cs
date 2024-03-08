using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class ScoreboardState : State
    {
        private const string SCENE_NAME = "Scoreboard";

        public override void EnterState()
        {
            base.EnterState();
            LoadSceneAsync(SCENE_NAME, LoadSceneMode.Single);
            SetNextState<MainMenuState>();
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
        }
    }
}
