using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text theText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = Color.white;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = Color.black;
    }
}
