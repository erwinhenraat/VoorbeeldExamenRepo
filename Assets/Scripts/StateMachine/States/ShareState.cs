using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class ShareState : State
    {
        private const string SCENE_NAME = "Share";

        public override void EnterState()
        {
            base.EnterState();
            LoadSceneAsync(SCENE_NAME, LoadSceneMode.Additive);
        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
        }
    }
}
