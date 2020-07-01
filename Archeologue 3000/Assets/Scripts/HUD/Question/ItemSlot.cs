using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Item item;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            item = eventData.pointerDrag.GetComponent<DragableItem>().item;
            eventData.pointerDrag.GetComponent<DragableItem>().dropCorrectly = true;
            eventData.pointerDrag.GetComponent<DragableItem>().slot = this;
        }
    }
}
