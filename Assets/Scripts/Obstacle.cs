using UnityEngine;

namespace DeliveryRush
{
    public enum ObstacleType
    {
        Puddle,
        Pigeon,
        TrashCan,
        Fence,
        Animal,
        Car
    }
    
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private ObstacleType _type = ObstacleType.TrashCan;
        
        public Vector3 spawnPoint = Vector3.zero;
        // public BuildingType buildingType = BuildingType.None;
        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            var courier = other.gameObject.GetComponent<Courier>(); 
            if (courier == null) return; // courier is null => exit
            
            courier.DamageParcel(0.2f);
        }
    }
}
