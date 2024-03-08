using UnityEngine;
using MarkUlrich.StateMachine.States;
using MarkUlrich.Utils;
using UnityEditor;

namespace MarkUlrich.StateMachine
{
    public class GameStateMachine : SingletonInstance<GameStateMachine>
    {
        [SerializeField] private bool _enableDebugging = true;

        private StateMachine StateMachine { get; set; } = new();
        
        public State CurrentState => StateMachine.CurrentState;
        public int StateMachineHashCode => StateMachine.GetHashCode();

        public bool IsDebugging => _enableDebugging;

        private void Awake()
        {
            if (_enableDebugging)
                Debug.Log($"Initialising StateMachine ({StateMachineHashCode})...");

            // Example of how to set the initial state.
            SetState<BootState>();
        }

        /// <summary>
        /// Subscribes a state to the game state machine.
        /// </summary>
        /// <param name="state">The state to subscribe.</param>
        public void Subscribe(State state) => StateMachine.Subscribe(state);

        /// <summary>
        /// Sets the current state to the state parsed in the param.
        /// </summary>
        /// <param name="newState">The object reference of the state to change to.</param>
        public void SetState(State newState) => StateMachine.SetState(newState);

        /// <summary>
        /// Sets the current state to the state parsed in the Type param.
        /// </summary>
        /// <typeparam name="TState">The type reference of the state to change to.</typeparam>
        public void SetState<TState>() where TState : State, new()
            => StateMachine.SetState<TState>();

        /// <summary>
        /// Sets the current state to the state parsed in the monoscript param.
        /// </summary>
        public void SetState(MonoScript monoScript)
            => StateMachine.SetState(monoScript);

        /// <summary>
        /// Retrieves the state of type <typeparamref name="TState"/> from the owning state machine.
        /// </summary>
        /// <typeparam name="TState">The type of the state to retrieve.</typeparam>
        /// <returns>The state of type <typeparamref name="TState"/>.</returns>
        public State GetState<TState>() where TState : State, new() => StateMachine.GetState<TState>();

        /// <summary>
        /// Moves the state machine to the next state in the static flow.
        /// </summary>
        public void MoveToNextState() => StateMachine.MoveToNextState();
    }
}
