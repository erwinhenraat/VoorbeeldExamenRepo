using MarkUlrich.StateMachine;
using MarkUlrich.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace UntitledCube.UI.Buttons
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;

        private void Start()
        {
            _pauseButton.onClick.AddListener(TogglePauseState);
        }

        private void TogglePauseState()
        {
            if (GameStateMachine.Instance.CurrentState is PausedState)
                GameStateMachine.Instance.MoveToNextState();
            else
                GameStateMachine.Instance.SetState<PausedState>();
        }
    } 
}
