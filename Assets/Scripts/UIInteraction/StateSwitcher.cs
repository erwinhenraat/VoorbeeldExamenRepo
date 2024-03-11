using MarkUlrich.StateMachine;
using MarkUlrich.StateMachine.States;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace UntitledCube.UIInteraction
{
    public class StateSwitcher : MonoBehaviour
    {
        [SerializeField]
        private string monoScript;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => GameStateMachine.Instance.SetState(monoScript));
        }
    }
}