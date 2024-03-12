using System.Linq;
using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class GameState : State
    {
        private const string SCENE_NAME = "Game";

        public override void EnterState()
        {
            base.EnterState();
            LoadSceneMode loadSceneMode = LoadSceneMode.Single;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i) == SceneManager.GetSceneByName("InputSeed"))
                    loadSceneMode = LoadSceneMode.Additive;
            }
            
            LoadScene(SCENE_NAME, loadSceneMode);
            SetNextState<LevelEndState>();
        }

        public override void ExitState() => base.ExitState();
    }
}
