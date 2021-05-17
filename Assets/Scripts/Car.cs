using System.Collections;
using System.Collections.Generic;
using DeliveryRush;
using UnityEngine;

public class Car : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        var courier = other.gameObject.GetComponent<Courier>(); 
        if (courier == null) return; // courier is null => exit
        
        // if (courier.transform.position.y > transform.position.y + )
            
        courier.DamageParcel(0.2f);
    }
}
