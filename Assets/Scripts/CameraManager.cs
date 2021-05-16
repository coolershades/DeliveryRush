using UnityEngine;

public class CameraManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Transform player;
#pragma warning restore 0649

    private Vector3 _shift = Vector3.zero;

    private void Start()
    {
        _shift = transform.position - player.position;
    }

    private void Update()
    {
        transform.position = player.position + _shift;
    }
}
