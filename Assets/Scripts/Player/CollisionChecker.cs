using UnityEngine;
using UntitledCube.AudioManagement;

namespace UntitledCube.Player
{
    [RequireComponent(typeof(AudioSource), typeof(DynamicGroundedCheck))]
    public class CollisionChecker : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _collisionSounds;
        private AudioSource _audioSource;
        private DynamicGroundedCheck _groundChecker;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _groundChecker = GetComponent<DynamicGroundedCheck>();
        }

        private void OnEnable() => _groundChecker.OnGrounded += Grounded;

        private void OnDisable() => _groundChecker.OnGrounded -= Grounded;

        private void Grounded() => AudioManager.Instance.Play(_collisionSounds, _audioSource, 0.5f);
    }
}