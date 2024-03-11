using UnityEngine;
using UntitledCube.AudioManagement;

namespace UntitledCube.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class CollisionChecker : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        private void OnCollisionEnter(Collision collision) => AudioManager.Instance.Play("PlayerHit", _audioSource);
    }
}