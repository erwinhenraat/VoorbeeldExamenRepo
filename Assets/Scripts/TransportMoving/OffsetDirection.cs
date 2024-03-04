using UntitledCube.WorldRotation;
using UnityEngine;
using System;

namespace UntitledCube.Transport
{
    [Serializable]
    public struct OffsetDirection
    {
        [SerializeField] private WorldRotations _side;
        [SerializeField] private Vector3 _offset;

        public readonly WorldRotations Side => _side;
        public readonly Vector3 Offset => _offset;
    }
}