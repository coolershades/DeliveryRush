using UnityEngine;

namespace DeliveryRush
{
    // TODO: добавить спавн земли и неба
    public class MapBuilder : MonoBehaviour
    {
        private Vector3 _nextSpawn = Vector3.zero;
        
        // возможно, стоит заменить на словарь, чтобы не прогружать всё по-новому каждый раз
        private GameObject GetGameObject(GameObjectType type) => Resources.Load<GameObject>("Prefabs/" + type);

        public Vector3 BuildMap(GameObjectType[] map)
        {
            // TODO: заменить на foreach и map == List<GameObjectType>
            for (var i = 0; i < map.Length; i++)
            {
                var g = GetGameObject(map[i]);
                Spawn(g);

                var building = g.GetComponent<Building>();
                if (building != null)
                {
                    building.GenerateObstacles();
                    
                    foreach (var positionInfo in Building.Positions[building.BuildingType])
                    {
                        if (building.FreePositions[positionInfo.PositionNumber]) continue;
                        
                        var obstacle = GetGameObject(positionInfo.Info.ObType); // не ищет Obstacle prefab
                        Spawn(obstacle, _nextSpawn + positionInfo.RelativePosition);
                    }
                }
                
                AddSpace(g.GetWidth());
            }
            
            // воздращаем позицию для следующего спавна
            return _nextSpawn;
        }

        public void Clear()
        {
            _nextSpawn = Vector3.zero;
            // DestroyImmediate(transform.Find("Ground(Clone)").gameObject);
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
        private Transform Spawn(GameObject gameObject)
            => Spawn(gameObject, _nextSpawn);

        private Transform Spawn(GameObject gameObject, Vector3 spawnPoint) 
            => Instantiate(gameObject, spawnPoint, Quaternion.identity, transform).transform;

        private void AddSpace(float length) => _nextSpawn += Vector3.right * length;
    }
}