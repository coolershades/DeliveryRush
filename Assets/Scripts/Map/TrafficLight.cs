using DeliveryRush;
using UnityEngine;
using Random = System.Random;

public class TrafficLight : MonoBehaviour
{
    public TrafficLightState State { get; private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private CrossRoad crossRoad;

    private const float DefaultRedTime = 10f;
    private const float DefaultGreenTime = 10f;

    private float _redTimeLeft = DefaultRedTime;
    private float _greenTimeLeft = DefaultGreenTime;

    public bool hasStarted;

    public enum TrafficLightState
    {
        Red = 0,
        Green = 1,
        RedBlinking = 2,
        GreenBlinking = 3,
        Empty = 4
    }

    public void SetRandomState()
    {
        var rand = new Random();
        
        if (rand.Next(2) > 0)
        {
            _greenTimeLeft = (float) rand.NextDouble() * DefaultGreenTime;
            State = TrafficLightState.Green;
        }
        else
        {
            _redTimeLeft = (float) rand.NextDouble() * DefaultRedTime;
            State = TrafficLightState.Red;
        }
    }

    private void Update()
    {
        if (!hasStarted) return;
        
        switch (State)
        {
            case TrafficLightState.Red:
                if (_redTimeLeft <= 0)
                {
                    State = TrafficLightState.Green;
                    _redTimeLeft = DefaultRedTime;
                    break;
                }
                _redTimeLeft -= Time.deltaTime;
                break;
            case TrafficLightState.Green:
                if (_greenTimeLeft <= 0)
                {
                    State = TrafficLightState.Red;
                    _greenTimeLeft = DefaultGreenTime;
                }
                _greenTimeLeft -= Time.deltaTime;
                break;
        }
    }
}
