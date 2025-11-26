using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            RectTransform boxRT = eventData.pointerDrag.GetComponent<RectTransform>();
            eventData.pointerDrag.transform.SetParent(transform);
            boxRT.anchoredPosition = Vector2.zero;

            Box box = eventData.pointerDrag.GetComponent<Box>();
            if (box != null)
            {
                box.boxImage.color = Color.white;
            }
        }
    }
}
