using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarkUlrich.StateMachine.States
{
    /// <summary>
    /// Represents a base class for game states in the game state machine.
    /// </summary>
    public abstract class State
    {
        private string Name => GetType().Name;

        private State _nextState;

        protected GameStateMachine OwningStateMachine { get; private set; } = GameStateMachine.Instance;

        public Action OnStateEnter;
        public Action OnStateExit;
        public Action OnSceneLoaded;

        protected State() => InitState();

        protected void InitState() => OwningStateMachine.Subscribe(this);
        
        protected void SetNextState<TState>() where TState: State, new() 
            => _nextState = OwningStateMachine.GetState<TState>();

        protected void LoadScene(string sceneName, LoadSceneMode loadSceneMode, bool forceReload = false)
        {
            if (forceReload)
            {
                SceneManager.LoadScene(sceneName, loadSceneMode);

                if (OwningStateMachine.IsDebugging)
                    Debug.Log($"Loaded Scene ({sceneName})");
                return;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneName))
                return;
            
            SceneManager.LoadScene(sceneName, loadSceneMode);

            if (OwningStateMachine.IsDebugging)
                Debug.Log($"Loaded Scene ({sceneName})");

            OwningStateMachine.StartCoroutine(WaitForSceneLoaded(sceneName));
        }

        protected void LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode, bool forceReload = false)
        {
            if (forceReload)
            {
                SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

                if (OwningStateMachine.IsDebugging)
                    Debug.Log($"Loaded Scene ({sceneName}) Async");
                return;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneName))
                return;
            
            SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            if (OwningStateMachine.IsDebugging)
                Debug.Log($"Loaded Scene ({sceneName}) Async");

            OwningStateMachine.StartCoroutine(WaitForSceneLoaded(sceneName));
        }

        protected void UnloadScene(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneName);

                if (OwningStateMachine.IsDebugging)
                    Debug.Log($"Unloaded Scene ({sceneName})");
            }
        }

        private IEnumerator WaitForSceneLoaded(string sceneName)
        {
            yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);

            OnSceneLoaded?.Invoke();
        }


        /// <summary>
        /// Executes code related to entering the state.
        /// </summary>
        public virtual void EnterState()
        {
            OnStateEnter?.Invoke();

            DebugStateEntry("Entering");
        }

        /// <summary>
        /// Executes code related to leaving the state.
        /// </summary>
        public virtual void ExitState()
        {
            DebugStateEntry("Leaving");

            OnStateExit?.Invoke();
        }

        /// <summary>
        /// Triggers the Owning StateMachine to move to the next state.
        /// </summary>
        public void MoveToNextState()
        {
            if (_nextState == null)
            {
                Debug.LogError("Next State variable was not set! Cancelling...");
                return;
            }
        
            OwningStateMachine.SetState(_nextState);
        }

        private void DebugStateEntry(string stateEntry)
        {
            if(!OwningStateMachine.IsDebugging)
                return;

            Debug.Log
            (
                $"{stateEntry} - {Name} ({GetHashCode()}) in StateMachine ({OwningStateMachine.StateMachineHashCode})"
            );
        }
    }
}
