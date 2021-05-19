using System.Collections.Generic;
using UnityEngine;

namespace DeliveryRush
{
    public enum AreaType
    {
        Restaurant,
        Downtown,
        Residential,
        Poor,
        Yard, 
        CrossRoad
    }
    
    public class Area : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> obstacles;
        [SerializeField] private int obstaclesCount = 3;
        [SerializeField] private float obstaclesShift = 5f;
        // [SerializeField] private AreaType areaType;

        public void GenerateObstacles()
        {
            if (obstacles.Count == 0)
            {
                Debug.Log("Obstacles count is zero. Generating stopped");
                return;
            }

            var spawn = Vector3.zero;
            for (var i = 0; i < obstaclesCount; i++)
            {
                var obstacle = obstacles[Random.Range(0, obstacles.Count)];
                Instantiate(obstacle, spawn + obstacle.transform.position, 
                    Quaternion.identity, transform.Find("Obstacles"));
                spawn.x += obstaclesShift;
            }
        }

        public void Clear()
        {
            var obstaclesWrap = transform.Find("Obstacles");
            while (obstaclesWrap.childCount > 0)
            {
                DestroyImmediate(obstaclesWrap.GetChild(0).gameObject);
            }
        }
    }
}
