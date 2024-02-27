using UnityEngine;

namespace UntitledCube.Gravity
{
    public class GravityManager : MonoBehaviour
    {
        [SerializeField]
        private float _gravityMultiplier = 1f;
        
        [SerializeField]
        private float _gravityAmount = 9.81f;

        public void SetGravityMultiplier(float multiplier) 
            => _gravityMultiplier = multiplier;

        public void SetGravityAmount(float amount) 
            => _gravityAmount = amount;

        public void SetGravityDirection(Vector3 direction) 
            => Physics.gravity = direction * _gravityAmount * _gravityMultiplier;
    }
}
