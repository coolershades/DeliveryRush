using System;
using DeliveryRush;
using UnityEngine;

public class Courier : MonoBehaviour
{
    public CourierState State { get; private set; }
    public bool IsOnTransport { get; private set; }
    public GameObjectType CurrentTransportType { get; private set; }

    private float _parcelPreservationStatus;
    public float ParcelPreservationStatus
    {
        get => _parcelPreservationStatus;
        private set
        {
            if (value > 0 && value <= 1)
                _parcelPreservationStatus = value;
            else
            {
                _parcelPreservationStatus = 0;
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
    
    [SerializeField] private UIManager uiManager;
    [SerializeField] public DeathMenuManager deathMenuManager;
    [SerializeField] public EndMenuManager endMenuManager;

    private void Start()
    {
        State = CourierState.Idle;
        CurrentTransportType = GameObjectType.Courier;
        
        speed = 10;
        jumpForce = 10;
        ParcelPreservationStatus = 1;
        slidingMultiplier = 1.6f;
        
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }
    
    // TODO https://www.youtube.com/watch?v=jfFOD9TRKeQ&list=PLpj8TZGNIBNy51EtRuyix-NYGmcfkNAuH&index=25&t=1s

    private void Update()
    {
        var hDirection = Input.GetAxis("Horizontal");
        var directionDx = hDirection.CompareTo(0);
        
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

        UpdateTransport();
        UpdateState();
    }


    private float _timeToDriveLeft = Transport.DefaultRideTime;
    
    private void UpdateTransport()
    {
        if (!IsOnTransport) return;

        if (_timeToDriveLeft <= 0)
        {
            print("end of ride.");
            Instantiate(MapBuilder.GetGameObject(CurrentTransportType), transform.position, Quaternion.identity, null);
            SwitchToTransport(false);
            _timeToDriveLeft = Transport.DefaultRideTime;
        }
        
        print("time left: " + (int)_timeToDriveLeft);
        _timeToDriveLeft -= Time.deltaTime;
    }

    private static readonly int EditorStateHash = Animator.StringToHash("State");
    private static readonly int OnTransport = Animator.StringToHash("IsOnTransport");

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
                State = Mathf.Abs(_rigidBody.velocity.x) > 0.05 && State != CourierState.Sliding
                    ? CourierState.Running : CourierState.Idle;
                
                if (Input.GetKey("left shift"))
                    State = CourierState.Sliding;
                break;
        }
        
        // print("State: " + State);
        _animator.SetInteger(EditorStateHash, (int) State);
    }

    public void DamageParcel(float damage)
    {
        ParcelPreservationStatus -= damage;
        uiManager.UpdateStatusBar();
    }

    public void SwitchToTransport(bool isOnTransport, GameObjectType transportType = GameObjectType.Courier)
    {
        IsOnTransport = isOnTransport;
        _animator.SetBool(OnTransport, isOnTransport);
        _animator.SetInteger("CurrentTransportType", (int) transportType);
        CurrentTransportType = transportType;
        
        Obstacle.ObstaclesAreCollidable = !isOnTransport;
    }
}

public enum CourierState
{
    Idle = 0,
    Running = 1,
    Jumping = 2,
    Falling = 3,
    Sliding = 4,
    // Damaged = 6
}