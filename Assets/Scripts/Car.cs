using System.Collections;
using System.Collections.Generic;
using DeliveryRush;
using UnityEngine;

public class Car : Obstacle
{
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        var courier = other.gameObject.GetComponent<Courier>(); 
        if (courier == null) return; // courier is null => exit
        
        if (courier.transform.position.y < transform.position.y 
            + GameObjectExtensions.GetHeight(MapBuilder.GetGameObject(GameObjectType.Car)))
            courier.DamageParcel(0.2f);
    }
}
