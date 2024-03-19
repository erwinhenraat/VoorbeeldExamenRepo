using UnityEngine;

namespace Pickup
{
    public class Coinpickup : MonoBehaviour
    {
        public int coinWorth = 2;
        
        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Car")) 
            {
                Destroy(gameObject);
                ScoreCounter.instance.IncreaseScore(coinWorth);
            }
        }
    }
}
