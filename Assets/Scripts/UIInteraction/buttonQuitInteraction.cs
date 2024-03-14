using UnityEngine;
using UnityEngine.UI;

namespace UntitledCube.UIInteraction
{
    public class buttonQuitInteraction : MonoBehaviour
    {
        private void Awake() => GetComponent<Button>().onClick.AddListener(() => Application.Quit());
    }
}
