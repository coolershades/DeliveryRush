using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeons : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
