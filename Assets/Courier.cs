using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Courier : MonoBehaviour
{
    private CourierState _state;
    private float _parcelDamageStatus;

    private float ParcelDamageStatus
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

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float slidingMultiplier; // ужасное название, надо как-то переименовать
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask groundObjectsLayer;
    [SerializeField] private Text damageTextField;  
    
    private static readonly int State = Animator.StringToHash("State");

    void Start()
    {
        speed = 10;
        jumpForce = 10;
        ParcelDamageStatus = 0;
        slidingMultiplier = 1.6f;
        
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
        var direction = hDirection.CompareTo(0);

        if (hDirection != 0)
        {
            _rigidBody.velocity = _state != CourierState.Sliding 
                ? new Vector2(direction * speed, _rigidBody.velocity.y) 
                : new Vector2(direction * speed * slidingMultiplier, _rigidBody.velocity.y);

            transform.localScale = new Vector2(direction, 1);
        }

        // print("Jump: " + Input.GetButtonDown("Jump") + " " + _collider.IsTouchingLayers(groundLayer));
        if (Input.GetButtonDown("Jump") && _collider.IsTouchingLayers(groundLayer))
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            _state = CourierState.Jumping;
        }

        // скольжение по лужам
        // TODO: сделать что-то с этим!! заменить 20f и выделение целого слоя под лужи 
        // ОЧЕНЬ сомнительное решение, но пока так
        if (_collider.IsTouchingLayers(groundObjectsLayer))
            _rigidBody.velocity = new Vector2(10f * direction + _rigidBody.velocity.x, _rigidBody.velocity.y);

        UpdateState();
    }

    private void UpdateState()
    {
        switch (_state)
        {
            case CourierState.Sliding:
                if (!Input.GetKey("left shift"))
                    _state = CourierState.Running;
                break;
            case CourierState.Jumping:
                if (_rigidBody.velocity.y < 0)
                    _state = CourierState.Falling;
                break;
            case CourierState.Falling:
                if (_collider.IsTouchingLayers(groundLayer))
                {
                    _state = CourierState.Idle;
                    if (Input.GetKey("left shift") && Mathf.Abs(_rigidBody.velocity.x) > 0)
                        _state = CourierState.Sliding;
                }
                break;
            default:
                _state = Mathf.Abs(_rigidBody.velocity.x) > 0 && _state != CourierState.Sliding
                    ? CourierState.Running : CourierState.Idle;
                
                if (Input.GetKey("left shift"))
                    _state = CourierState.Sliding;
                break;
        }
        
        print("State: " + _state);
        _animator.SetInteger(State, (int) _state);
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
        damageTextField.text = ParcelDamageStatus * 100 + "%";
        
        print("Parcel damaged! Damage: " + ParcelDamageStatus);
        if (ParcelDamageStatus >= 1)
            print("Parcel is at critical damage. You have to restart the game.");
    }
}