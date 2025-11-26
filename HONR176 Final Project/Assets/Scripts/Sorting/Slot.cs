using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public bool hasBox;

    [HideInInspector]
    public SortingManager sm;

    public int currentNumber;

    public int GetNumber()
    {
        return currentNumber;
    }

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
                sm.CallRefresh();
            }
        }
    }

    public void Refresh()
    {
        Box box = GetComponentInChildren<Box>();
        if (box != null)
        {
            hasBox = true;
            currentNumber = box.number;
        }
        else
        {
            hasBox = false;
            currentNumber = -1;
        }
    }
}
