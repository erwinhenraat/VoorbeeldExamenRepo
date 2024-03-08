using MarkUlrich.StateMachine;
using MarkUlrich.StateMachine.States;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace UntitledCube.sceneChanging
{
    public class StateSwitcher : MonoBehaviour
    {
        //[SerializeField]
        //private List<State> states;

        [SerializeField]
        private MonoScript monoScript;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            //button.onClick.AddListener(GameStateMachine.Instance.SetState<monoScript>);
        }

        //private void Start() => SwitchToState<LevelEndState>();

        /*public void SwitchToState<TState>() where TState : State, new() =>
            GameStateMachine.Instance.SetState<TState>();*/
    }
}