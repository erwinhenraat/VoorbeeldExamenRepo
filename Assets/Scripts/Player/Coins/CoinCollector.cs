using UnityEngine;

namespace UntitledCube.Player.Coins
{
    public class CoinCollector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Coin"))
                return;

            CoinPurse.Add();
            Destroy(other.gameObject);
        }
    }
}