using System;
using DeliveryRush;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Camera _camera;
    private Vector3 _shift = Vector3.zero;

    public static float LeftBound;
    public static float RightBound;

    private static float CenterToScreenBound;

    private void Start()
    {
        _shift = new Vector3(5, 1, -10);
        
        /*var tmpPos = _camera.ScreenToWorldPoint(transform.position); // левый нижный угол камеры
        CenterToScreenBound = Math.Abs(tmpPos.x - transform.position.x);
        print(CenterToScreenBound);*/
    }

    private void Update()
    {
        transform.position = player.position + _shift;

        /*var tmpPos = _camera.ScreenToWorldPoint(transform.position); // левый нижный угол камеры
        CenterToScreenBound = Math.Abs(tmpPos.x - transform.position.x);
        print("CenterToScreenBound " + CenterToScreenBound);
        print("tmppos " + tmpPos);
        // if ()*/
    }
}
