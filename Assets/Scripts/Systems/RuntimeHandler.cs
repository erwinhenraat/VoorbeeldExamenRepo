using MarkUlrich.Utils;
using UnityEngine;

public class RuntimeHandler : SingletonInstance<RuntimeHandler>
{
    [SerializeField] private int _targetFrameRate = 120;

    private void Awake() => Application.targetFrameRate = _targetFrameRate;
}
