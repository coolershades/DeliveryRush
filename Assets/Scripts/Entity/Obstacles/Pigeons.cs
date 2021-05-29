using DeliveryRush;
using UnityEngine;

public class Pigeons : Obstacle
{
    public static float FlightHeight = 1;
    
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
        else if (_hasStarted) Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var courier = other.gameObject.GetComponent<Courier>(); 
        if (courier == null) return; // courier is null => exit

        if (courier.State != CourierState.Sliding)
            courier.DamageParcel(0.25f);
    }
}
