using UnityEngine;

public class Courier : MonoBehaviour
{
    private CourierState _state;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private Animator _animator;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    [SerializeField] private LayerMask _groundLayer;
    
    void Start()
    {
        _speed = 7;
        _jumpForce = 10;
        
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        ManageMovement();
    }

    private void ManageMovement()
    {
        var hDirection = Input.GetAxis("Horizontal");
        var direction = hDirection > 0 ? 1 : -1;

        if (hDirection != 0)
        {
            _rigidBody.velocity = new Vector2(direction * _speed, _rigidBody.velocity.y);
            transform.localScale = new Vector2(direction, 1);
        }

        if (Input.GetButtonDown("Jump") && _collider.IsTouchingLayers(_groundLayer))
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        
        UpdateState();
    }

    private void UpdateState()
    {
        /*switch (_state)
        {
            case CourierState.Idle:
                break;
            case CourierState.Run:
                break;
        }*/

        if (Mathf.Abs(_rigidBody.velocity.x) > 0)
            _state = CourierState.Run;
        else _state = CourierState.Idle;
        
        _animator.SetInteger("State", (int) _state);
    }
}