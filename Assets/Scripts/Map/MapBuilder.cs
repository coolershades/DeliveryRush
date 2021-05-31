using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace DeliveryRush
{
    public class MapBuilder : MonoBehaviour
    {
        private Vector3 _nextSpawn = Vector3.zero;
        
        [SerializeField] private float _spacing = 0.35f;
        
        private int _buildingsCount;
        public float currentMapLength;
        
        [SerializeField] private CountdownManager countdownManager;
        [SerializeField] private TilemapGenerator tilemapGenerator;
        [SerializeField] private CameraManager cameraManager;

        public void Reset()
        {
            Clear();
            var map = MapGenerator.GenerateTypeMap(MapGenerator.GenerateAreaTypeMap());
            BuildMap(map);
        }

        // возможно, стоит заменить на словарь, чтобы не прогружать всё по-новому каждый раз
        public static GameObject GetGameObject(GameObjectType type) => Resources.Load<GameObject>("Prefabs/" + type);

        public void BuildMap(List<Tuple<AreaType, List<GameObjectType>>> map)
        {
            currentMapLength = 0;

            // строим район
            foreach (var areaContent in map)
            {
                var areaType = areaContent.Item1;
                var nextBackgroundSpawn = _nextSpawn;

                // строим здания
                foreach (var gameObject in areaContent.Item2)
                {
                    var g = GetGameObject(gameObject);
                    Spawn(g);

                    var building = g.GetComponent<Building>();
                    if (building != null)
                    {
                        building.GenerateObstacles();

                        foreach (var positionInfo in Building.Positions[building.buildingType])
                        {
                            if (building.freePositions[positionInfo.PositionNumber]) continue;

                            var obstacle = GetGameObject(positionInfo.Info.ObType);
                            Spawn(obstacle, _nextSpawn + positionInfo.RelativePosition);
                        }
                    }

                    currentMapLength += g.GetWidth() + _spacing;
                    _nextSpawn += Vector3.right * (g.GetWidth() + _spacing);
                }

                _buildingsCount += areaContent.Item2.Count;

                // ставим фон для района
                // TODO: change the logic behind calculation of iterations 
                var backIterations = areaContent.Item2.Count;
                var backgroundObjects = MapGenerator.AreaBackground[areaType];

                // TODO это ЧТО??
                if (backgroundObjects.Length == 0) continue;

                var rand = new Random();

                for (var i = 0; i < backIterations; i++)
                {
                    var index = rand.Next(backgroundObjects.Length);
                    var backgroundItem = GetGameObject(backgroundObjects[index]);
                    Spawn(backgroundItem, nextBackgroundSpawn);
                    nextBackgroundSpawn += Vector3.right * backgroundItem.GetWidth();
                }
            }
            
            // время на прохождение карты
            countdownManager.SetTime(_buildingsCount);
            tilemapGenerator.Generate((int) _nextSpawn.x + 1);
            cameraManager.SetBounds(0, _nextSpawn.x);
        }

        public void Clear()
        {
            _nextSpawn = Vector3.zero;
            _buildingsCount = 0;
            
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
            
            tilemapGenerator.Clear();
        }

        private Transform Spawn(GameObject gameObject)
            => Spawn(gameObject, _nextSpawn);

        private Transform Spawn(GameObject gameObject, Vector3 spawnPoint) 
            => Instantiate(gameObject, spawnPoint, Quaternion.identity, transform).transform;
    }
}