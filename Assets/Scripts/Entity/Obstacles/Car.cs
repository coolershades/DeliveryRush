using DeliveryRush;
using UnityEngine;

public class Car : Obstacle
{
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        var courier = other.gameObject.GetComponent<Courier>();
        if (courier == null || courier.IsOnTransport) return;

        if (courier.transform.position.y < transform.position.y
            + MapBuilder.GetGameObject(GameObjectType.Car).GetHeight())
            courier.DamageParcel(0.2f);
    }
}