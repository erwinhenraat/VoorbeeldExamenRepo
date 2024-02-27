using UnityEngine;

public class RuntimeHandler : MonoBehaviour // Todo: make this a singleton when implemented
{
    [SerializeField] private int targetFrameRate = 120;

    private void OnEnable() => Application.targetFrameRate = targetFrameRate;
}
