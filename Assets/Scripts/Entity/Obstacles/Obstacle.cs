using System;
using UnityEngine;

namespace DeliveryRush
{
    public class Obstacle : MonoBehaviour
    {
        private Collider2D _collider;
        public static bool ObstaclesAreCollidable = true;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            _collider.isTrigger = !ObstaclesAreCollidable;
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            var courier = other.gameObject.GetComponent<Courier>();
            if (courier == null || courier.IsOnTransport) return;
            
            courier.DamageParcel(0.2f);
        }
    }
}
