using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 _shift = Vector3.zero;

    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;

    // TODO получено эпирическим способом, должно вычисляться динамически
    // transform.position находится в центре экрана, 
    private const float CameraCenterToScreenBound = 9;

    public void SetBounds(float leftBound, float rightBound)
    {
        _leftBound = leftBound + CameraCenterToScreenBound;
        _rightBound = rightBound - CameraCenterToScreenBound;
    }

    private void Start()
    {
        _shift = new Vector3(5, 2.5f, -10);
    }

    private void Update()
    {
        transform.position = player.position + _shift;

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, _leftBound, _rightBound),
            transform.position.y,
            transform.position.z
        );
    }
}