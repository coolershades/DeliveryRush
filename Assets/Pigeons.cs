using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeons : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _hasStarted;
    
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float flightSpeed;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        flightSpeed = 10;
    }

    
    void Update()
    {
        if (_spriteRenderer.isVisible)
        {
            _rigidbody2D.velocity = new Vector2(-flightSpeed, 0);
            _hasStarted = true;
        }
        else if (_hasStarted) Destroy(this);
    }
}
