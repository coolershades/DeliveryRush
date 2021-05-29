using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var courier = other.GetComponent<Courier>();
        if (courier == null) return;
        
        courier.endMenuManager.Activate();
    }
}
