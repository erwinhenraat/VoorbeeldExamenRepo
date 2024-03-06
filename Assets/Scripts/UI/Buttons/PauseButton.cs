using MarkUlrich.StateMachine;
using MarkUlrich.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;

    private void Start()
    {
        _pauseButton.onClick.AddListener(GoToPauseState);
    }

    private void GoToPauseState() => GameStateMachine.Instance.SetState<PausedState>();

}
