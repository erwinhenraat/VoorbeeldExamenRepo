using UnityEngine;
using UnityEngine.UI;

namespace UntitledCube.UIInteraction
{
    public class buttonQuitInteraction : MonoBehaviour
    {
        public class ButtonQuitInteraction : MonoBehaviour
        {
            private void Awake() => GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        }
    }
}
