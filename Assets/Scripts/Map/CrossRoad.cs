using UnityEngine;

namespace DeliveryRush
{
    public class CrossRoad : Building
    {
        [SerializeField] private TrafficLight trafficLight;

        private void Start()
        {
            trafficLight.SetRandomState();
        }
    }
}