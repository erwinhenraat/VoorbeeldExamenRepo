using UnityEngine;

public class RuntimeHandler : MonoBehaviour // Todo: make this a singleton when implemented
{
    [SerializeField] private int _targetFrameRate = 120;

    private void OnEnable() => Application.targetFrameRate = _targetFrameRate;
}
