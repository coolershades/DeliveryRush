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
    }
}
