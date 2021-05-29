using System;
using DeliveryRush;
using UnityEngine;

public class Courier : MonoBehaviour
{
    public CourierState State { get; private set; }
    
    private float _parcelDamageStatus;
    public float ParcelDamageStatus
    {
        get => _parcelDamageStatus;
        private set
        {
            if (value >= 0 && value < 1)
                _parcelDamageStatus = value;
            else
            {
                _parcelDamageStatus = 1;
                deathMenuManager.TriggerDeath();
            }
        }
    }
    
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private Animator _animator;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float slidingMultiplier; // ужасное название, надо как-то переименовать
    
    [SerializeField] private LayerMask groundLayer;
    // [SerializeField] private LayerMask groundObjectsLayer;
    
    [SerializeField] private UIManager uiManager;
    [SerializeField] public DeathMenuManager deathMenuManager;
    [SerializeField] public EndMenuManager endMenuManager;

    private void Start()
    {
        speed = 10;
        jumpForce = 10;
        ParcelDamageStatus = 0;
        slidingMultiplier = 1.6f;
        
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ManageMovement();
    }

    private void ManageMovement()
    {
        var hDirection = Input.GetAxis("Horizontal");
        var directionDx = hDirection.CompareTo(0);

        /*if (hDirection != 0)
        {
            _rigidBody.velocity = State != CourierState.Sliding 
                ? new Vector2(directionDx * speed, _rigidBody.velocity.y) 
                : new Vector2(directionDx * speed * slidingMultiplier, _rigidBody.velocity.y);

            transform.localScale = new Vector2(directionDx, 1);
        }*/
        
        if (Math.Abs(hDirection) > 0.05)
        {
            _rigidBody.velocity = State != CourierState.Sliding 
                ? new Vector2(directionDx * speed, _rigidBody.velocity.y) 
                : new Vector2(directionDx * speed * slidingMultiplier, _rigidBody.velocity.y);
            transform.localScale = new Vector2(directionDx, 1);
        }
        else _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y); // для того, чтобы не было скольжения

        if (Input.GetButtonDown("Jump") && _collider.IsTouchingLayers(groundLayer))
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            State = CourierState.Jumping;
        }

        /*// скольжение по лужам
        // TODO: сделать что-то с этим!! заменить 20f и выделение целого слоя под лужи 
        // ОЧЕНЬ сомнительное решение, но пока так
        if (_collider.IsTouchingLayers(groundObjectsLayer))
            _rigidBody.velocity = new Vector2(10f * direction + _rigidBody.velocity.x, _rigidBody.velocity.y);*/

        UpdateState();
    }
    
    private static readonly int EditorStateHash = Animator.StringToHash("State");

    private void UpdateState()
    {
        switch (State)
        {
            case CourierState.Sliding:
                if (!Input.GetKey("left shift"))
                    State = CourierState.Running;
                break;
            
            case CourierState.Jumping:
                if (_rigidBody.velocity.y < 0)
                    State = CourierState.Falling;
                break;
            
            case CourierState.Falling:
                if (_collider.IsTouchingLayers(groundLayer))
                {
                    State = CourierState.Idle;
                    if (Input.GetKey("left shift") && Mathf.Abs(_rigidBody.velocity.x) > 0)
                        State = CourierState.Sliding;
                }
                break;
            
            default:
                State = Mathf.Abs(_rigidBody.velocity.x) > 0 && State != CourierState.Sliding
                    ? CourierState.Running : CourierState.Idle;
                
                if (Input.GetKey("left shift"))
                    State = CourierState.Sliding;
                break;
        }
        
        // print("State: " + _state);
        _animator.SetInteger(EditorStateHash, (int) State);
    }

    public void DamageParcel(float damage)
    {
        ParcelDamageStatus += damage;
        uiManager.UpdateStatusBar();
    }
}