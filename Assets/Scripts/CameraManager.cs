using DeliveryRush;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 _shift = Vector3.zero;

    public static float LeftBound;
    public static float RightBound;

    private void Start()
    {
        _shift = transform.position - player.position;
    }

    private void Update()
    {
        transform.position = player.position + _shift;

        // if (player.position.x)
    }
}
