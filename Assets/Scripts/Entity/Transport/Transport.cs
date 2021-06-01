using DeliveryRush;
using UnityEngine;

public class Transport : MonoBehaviour
{
    public static readonly float DefaultRideTime = 5;

    private const int RideCost = 150;

    private void OnTriggerStay2D(Collider2D other)
    {

        var courier = other.gameObject.GetComponent<Courier>();
        if (courier == null) return;

        if (Input.GetKey(KeyCode.E) && courier.State == CourierState.Idle && courier.Money >= RideCost)
        {
            courier.Money -= RideCost;
            courier.SwitchToTransport(true, GameObjectType.Scooter);
            Destroy(this.gameObject);
        }
    }
}