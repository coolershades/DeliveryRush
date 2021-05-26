using UnityEngine;

namespace DeliveryRush
{
    public class Obstacle : MonoBehaviour
    {
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            var courier = other.gameObject.GetComponent<Courier>();
            if (courier == null) return; // courier is null => exit
            
            courier.DamageParcel(0.2f);
        }
    }
}
