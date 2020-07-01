using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Item item;
    public bool dropCorrectly = false;
    private Vector2 backPosition;
    public ItemSlot slot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }
    private void Start()
    {
        ItemAspect();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        backPosition = transform.parent.transform.position;
        dropCorrectly = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        if (slot != null)
        {
            slot.item = null;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (dropCorrectly == false)
        {
            transform.position=backPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta /canvas.scaleFactor;
    }

    public void ItemAspect()
    {
        gameObject.GetComponent<Image>().sprite = item.aspect;
        gameObject.GetComponentInChildren<Text>().text = item.name;
    }
}
