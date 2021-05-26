using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace DeliveryRush
{
    public class MapBuilder : MonoBehaviour
    {
        private Vector3 _nextSpawn = Vector3.zero;
        private Vector3 _groundNextSpawn = Vector3.zero;
        
        [SerializeField] private float _spacing = 0.35f;
        
        private int BuildingsCount;
        [SerializeField] private CountdownManager _countdownManager;
        
        [SerializeField] private TilemapGenerator _tilemapGenerator;

        // возможно, стоит заменить на словарь, чтобы не прогружать всё по-новому каждый раз
        public static GameObject GetGameObject(GameObjectType type) => Resources.Load<GameObject>("Prefabs/" + type);

        public void BuildMap(List<Tuple<AreaType, List<GameObjectType>>> map)
        {
            CameraManager.LeftBound = _nextSpawn.x;

            foreach (var areaContent in map)
            {
                var areaType = areaContent.Item1;
                var nextBackgroundSpawn = _nextSpawn;

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

                    AddSpace(g.GetWidth() + _spacing);
                }

                BuildingsCount += areaContent.Item2.Count;

                // Area background spawn
                // TODO: change the logic behind calculation of iterations 
                var backIterations = areaContent.Item2.Count;
                var backgroundObjects = MapGenerator.AreaBackground[areaType];

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

            CameraManager.RightBound = _nextSpawn.x;

            // время на прохождение карты
            _countdownManager.SetTime(BuildingsCount);
            _tilemapGenerator.Generate((int) _nextSpawn.x + 1);
        }

        public void Clear()
        {
            _nextSpawn = Vector3.zero;
            _groundNextSpawn = Vector3.zero;
            
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
            
            _tilemapGenerator.Clear();
        }
        
        private Transform Spawn(GameObject gameObject)
            => Spawn(gameObject, _nextSpawn);

        private Transform Spawn(GameObject gameObject, Vector3 spawnPoint) 
            => Instantiate(gameObject, spawnPoint, Quaternion.identity, transform).transform;

        private void AddSpace(float length) => _nextSpawn += Vector3.right * length;
    }
}