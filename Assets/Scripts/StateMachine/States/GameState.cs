namespace MarkUlrich.StateMachine.States
{
    public class GameState : State
    {
        private const string LOAD_SCENE_NAME = "StatemachineTest";

        /// <summary>
        /// Executes code related to entering the Game state.
        /// </summary>
        public override void EnterState()
        {
            base.EnterState();
            SetNextState<BootState>();

            LoadScene(LOAD_SCENE_NAME);
        }

        /// <summary>
        /// Executes code related to leaving the Game state.
        /// </summary>
        public override void ExitState() => base.ExitState();
    }
}
