using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    public class GameState : State
    {
        private const string SCENE_NAME = "Game";

        public override void EnterState()
        {
            base.EnterState();

            LoadSceneMode loadSceneMode = SceneManager.GetActiveScene() == null ? LoadSceneMode.Single : LoadSceneMode.Additive;
            LoadSceneAsync(SCENE_NAME, loadSceneMode);
            SetNextState<LevelEndState>();

        }

        public override void ExitState()
        {
            base.ExitState();
            UnloadScene(SCENE_NAME);
        }
    }
}
