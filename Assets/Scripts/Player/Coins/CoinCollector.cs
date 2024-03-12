using UnityEngine;
using UntitledCube.AudioManagement;

namespace UntitledCube.Player.Coins
{
    [RequireComponent(typeof(AudioSource))]
    public class CoinCollector : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Coin"))
                return;

            CoinPurse.Add();
            Destroy(other.gameObject);
            AudioManager.Instance.Play("CoinPickup", _audioSource);
        }
    }
}