using System.Collections.Generic;
using UnityEngine;

namespace DeliveryRush
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] List<Area> areasList;
        [SerializeField] float areasShift = 5f;

        private Vector3 _nextSpawn = Vector3.zero;
        private static GameObject _ground;
        private static GameObject _crossRoad;

        private static GameObject Ground
        {
            get
            {
                if (_ground == null)
                    _ground = Resources.Load<GameObject>("Prefabs/Ground");
                return _ground;
            }
        }

        public static GameObject CrossRoad
        {
            get
            {
                if (_crossRoad == null)
                    _crossRoad = Resources.Load<GameObject>("Prefabs/CrossRoad");
                return _crossRoad;
            }
        }

        public void Generate()
        {
            var children = new List<Transform>();

            foreach (var area in areasList)
            {
                children.Add(Spawn(area));
                AddSpace(areasShift);
            }

            SpawnGround();

            for (var i = 0; i < children.Count - 1; i++)
            {
                var a1 = children[i];
                var a2 = children[i + 1];

                var middlePoint = a1.localPosition + (a2.localPosition - a1.localPosition) * .5f;
                middlePoint.x += 17f;

                var road = Instantiate(CrossRoad, transform).transform;
                road.localPosition = middlePoint;
            }
        }

        public void Clear()
        {
            _nextSpawn = Vector3.zero;
            DestroyImmediate(transform.Find("Ground(Clone)").gameObject);
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
        }

        private Transform Spawn(Area area)
        {
            return Instantiate(area, _nextSpawn, Quaternion.identity, transform).transform;
        }

        private void AddSpace(float length)
        {
            _nextSpawn += Vector3.right * length;
        }

        private void SpawnGround()
        {
            var ground = Instantiate(Ground, transform);
            ground.transform.localScale = new Vector3(1000f, 1f, 0f);
        }
    }
}
