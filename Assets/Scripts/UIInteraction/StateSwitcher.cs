using MarkUlrich.StateMachine;
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