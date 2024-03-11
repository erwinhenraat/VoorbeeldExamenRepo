using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class GameState : State
    {
        private bool _isSceneLoaded = false;
        private const string SCENE_NAME = "Game";

        public override void EnterState()
        {
            base.EnterState();
            LoadScene(SCENE_NAME, LoadSceneMode.Single);
            SetNextState<LevelEndState>();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
