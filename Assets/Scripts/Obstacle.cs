using UnityEngine;

namespace DeliveryRush
{
    public class Obstacle : MonoBehaviour
    {
        // [SerializeField] private ObstacleType _type = ObstacleType.TrashCan;
        // [SerializeField] private GameObjectType _type = GameObjectType.None;
        
        // public Vector3 spawnPoint = Vector3.zero;
        // public BuildingType buildingType = BuildingType.None;
        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            var courier = other.gameObject.GetComponent<Courier>(); 
            if (courier == null) return; // courier is null => exit
            
            courier.DamageParcel(0.2f);
        }
    }
}
