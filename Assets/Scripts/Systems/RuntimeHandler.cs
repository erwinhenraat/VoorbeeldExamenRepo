using UnityEngine;

public class RuntimeHandler : MonoBehaviour // Todo: make this a singleton when implemented
{
    private void OnEnable() => Application.targetFrameRate = 120;
}
