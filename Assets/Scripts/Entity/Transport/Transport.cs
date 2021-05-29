using System;
using DeliveryRush;
using UnityEngine;

public class Transport : MonoBehaviour
{
    public static readonly float DefaultRideTime = 5;
    
    private void OnTriggerStay2D(Collider2D other)
    {

        var courier = other.gameObject.GetComponent<Courier>();
        if (courier == null) return;

        if (Input.GetKey(KeyCode.E))
        {
            courier.SwitchToTransport(true, GameObjectType.Scooter);
            print("ride started!");
            Destroy(this.gameObject);
        }
    }
}