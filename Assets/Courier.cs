using System;
using DefaultNamespace;
using UnityEngine;

public class Courier : MonoBehaviour
{
    private CourierState _state;
    private float _parcelDamageStatus;
    public float ParcelDamageStatus
    {
        get => _parcelDamageStatus;
        set
        {
            if (value >= 0 && value <= 1)
                _parcelDamageStatus = value;
            else throw new Exception("Value must be >= 0 and <= 1.");
        }
    }
    
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
        ParcelDamageStatus = 0;
        
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
        switch (_state)
        {
            case CourierState.Running:
                break;
            case CourierState.Sliding:
                if (!Input.GetKey("left shift"))
                    _state = CourierState.Running;
                break;
        }

        if (Mathf.Abs(_rigidBody.velocity.x) > 0)
            _state = CourierState.Running;
        else _state = CourierState.Idle;
        
        if (Input.GetKey("left shift"))
            _state = CourierState.Sliding;
        
        print("State: " + _state);
        _animator.SetInteger("State", (int) _state);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case Tag.PigeonFlock:
                if (_state != CourierState.Sliding)
                    DamageParcel(0.25f);
                break;
        }
    }

    private void DamageParcel(float damage)
    {
        ParcelDamageStatus += damage;
        print("Parcel damaged! Damage: " + ParcelDamageStatus);
        if (ParcelDamageStatus >= 1)
            print("Parcel is at critical damage. You have to restart the game.");
    }
}