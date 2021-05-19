using UnityEngine;

namespace DeliveryRush
{
    public class MyArea : MonoBehaviour
    {
        public float Width { get; private set; }

        private void Start()
        {
            Width = ((RectTransform) transform).rect.width;
        }
    }
}