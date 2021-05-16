using DeliveryRush;
using UnityEngine;

public class Pigeons : Obstacle
{
    // Start is called before the first frame update
    public static float FlightHeight = 48;
    
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
