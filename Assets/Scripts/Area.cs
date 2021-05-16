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
        Yard
    }
    
    public class Area : MonoBehaviour
    {
        [SerializeField] List<Obstacle> obstacles;
        [SerializeField] int obstaclesCount = 3;
        [SerializeField] float obstaclesShift = 5f;
        [SerializeField] AreaType areaType;

        public void GenerateObstacles()
        {
            if (obstacles.Count == 0)
            {
                Debug.Log("Obstacles count is zero. Generating stopped");
                return;
            }

            Vector3 spawn = Vector3.zero;
            for (int i = 0; i < obstaclesCount; i++)
            {
                Obstacle obstacle = obstacles[Random.Range(0, obstacles.Count)];
                Instantiate(obstacle, spawn + obstacle.transform.position, 
                    Quaternion.identity, transform.Find("Obstacles"));
                spawn.x += obstaclesShift;
            }
        }

        public void Clear()
        {
            Transform obstaclesWrap = transform.Find("Obstacles");
            while (obstaclesWrap.childCount > 0)
            {
                DestroyImmediate(obstaclesWrap.GetChild(0).gameObject);
            }
        }
    }
}
