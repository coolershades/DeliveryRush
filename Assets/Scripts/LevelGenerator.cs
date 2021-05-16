using System.Collections.Generic;
using UnityEngine;

namespace DeliveryRush
{
    public class LevelGenerator : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] List<Area> areasList;
        [SerializeField] float areasShift = 5f;
#pragma warning restore 0649

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
                    _crossRoad = Resources.Load<GameObject>("Prefabs/RoadCross");
                return _crossRoad;
            }
        }

        public void Generate()
        {
            List<Transform> childs = new List<Transform>();

            foreach (Area area in areasList)
            {
                childs.Add(Spawn(area));
                AddSpace(areasShift);
            }

            SpawnGround();

            for (int i = 0; i < childs.Count - 1; i++)
            {
                Transform a1 = childs[i];
                Transform a2 = childs[i + 1];

                Vector3 middlePoint = a1.localPosition + (a2.localPosition - a1.localPosition) * .5f;
                middlePoint.x += 17f;

                Transform road = Instantiate(CrossRoad, transform).transform;
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
            GameObject ground = Instantiate(Ground, transform);
            ground.transform.localScale = new Vector3(1000f, 1f, 0f);
        }
    }
}
