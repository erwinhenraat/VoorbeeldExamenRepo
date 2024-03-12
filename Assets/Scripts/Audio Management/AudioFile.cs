using System;
using UnityEngine;

namespace UntitledCube.AudioManagement
{
    [Serializable] public struct AudioFile
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _clip;

        public readonly string Name => _name;
        public readonly AudioClip Clip => _clip;
    }
}