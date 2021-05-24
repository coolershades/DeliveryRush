using System.Collections.Generic;
using UnityEngine;

namespace DeliveryRush
{
    // TODO: добавить спавн земли и неба
    public class MapBuilder : MonoBehaviour
    {
        private Vector3 _nextSpawn = Vector3.zero;
        private Vector3 _groundNextSpawn = Vector3.zero;
        
        [SerializeField] private float _spacing = 0.35f;
        private float _mapWidth;

        // возможно, стоит заменить на словарь, чтобы не прогружать всё по-новому каждый раз
        public static GameObject GetGameObject(GameObjectType type) => Resources.Load<GameObject>("Prefabs/" + type);

        public void BuildMap(List<GameObjectType> map)
        {
            
            foreach (var gameObject in map)
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
                _mapWidth += g.GetWidth() + _spacing;
            }
            
            var groundWidth = Ground.GetWidth();
            var iterations = (int) (_mapWidth / groundWidth) + 1;
            print(_mapWidth + " " + groundWidth);
            
            for (var i = 0; i < iterations; i++)
            {
                Spawn(Ground, _groundNextSpawn + new Vector3(2.5f, -0.5f, 0));
                _groundNextSpawn += Vector3.right * groundWidth;
            }
        }

        public void Clear()
        {
            _nextSpawn = Vector3.zero;
            _groundNextSpawn = Vector3.zero;
            // DestroyImmediate(transform.Find("Ground(Clone)").gameObject);
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
        private Transform Spawn(GameObject gameObject)
            => Spawn(gameObject, _nextSpawn);

        private Transform Spawn(GameObject gameObject, Vector3 spawnPoint) 
            => Instantiate(gameObject, spawnPoint, Quaternion.identity, transform).transform;

        private void AddSpace(float length) => _nextSpawn += Vector3.right * length;

        #region Ground stuff
        private static GameObject _ground;
        private static GameObject Ground
        {
            get
            {
                if (_ground == null)
                    _ground = Resources.Load<GameObject>("Prefabs/Ground");
                return _ground;
            }
        }
        private void SpawnGround()
        {
            var ground = Instantiate(Ground, new Vector3(2.5f, -0.5f, 0), Quaternion.identity,transform);
            // ground.transform.localScale = new Vector3(1f, 1f, 0f);
        }
        #endregion
        
    }
}